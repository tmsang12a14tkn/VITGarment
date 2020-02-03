using Data.DataAccessLayer;
using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Garment.Web.Controllers.api
{
    public class PointData
    {
        public int TimeValue { get; set; }
        public DateTime Date {get;set;}
        public int Value {get;set;}
        public IEnumerable<ProductData> Products { get; set; }
    }
    public class Factory
    {
        public int id { get; set; }
        public string name { get; set; }

        public override int GetHashCode()
        {
            var obj = new { id = this.id, name = this.name};
            return obj.GetHashCode();
        }

        // IEquatable uses overriden Equals implementation
        public override bool Equals(object obj)
        {
            var objFactory = (Factory)obj;
            return this.id == objFactory.id && this.name == objFactory.name;
        }
    }
    public class ProductData
    {
        public string ProductId { get; set; }
        public int ProduceQuantity { get; set; }
    }
    public class ChartPoint
    {
        public double x { get; set; }
        public int? y { get; set; }
        public IEnumerable<ProductData> products { get; set; }
    }
    public class HourChartPoint
    {
        public string x { get; set; }
        public int y { get; set; }
    }
    public class TeamChartView
    {
        public string id { get; set; }
        public Factory factory { get; set; }
        public string name { get; set; }
        [JsonIgnore]
        public IEnumerable<PointData> PointsData { get; set; }
        public IEnumerable<ChartPoint> data { get; set; }
    }
    public class HourChartData
    {
        public string[] Categories { get; set; }
        public HourChartView[] Series { get; set; }
    }
    public class HourChartView
    {
        public string name { get; set; }
        public int[] data { get; set; }
    }
    public class ReasonChartView
    {
        public string id { get; set; }
        public string name { get; set; }
        public string color { get; set; }
        [JsonIgnore]
        public IEnumerable<PointData> PointsData { get; set; }
        public int[] cells { get; set; }
        public IEnumerable<ChartPoint> data { get; set; }
    }
    public class EmployeeChartView
    {
        public string id { get; set; }
        public string name { get; set; }
        public string color { get; set; }
        public bool showInLegend { get; set; }
        public bool visible { get; set; }
        public int legendIndex { get; set; }
        [JsonIgnore]
        public IEnumerable<PointData> PointsData { get; set; }
        public int[] cells { get; set; }
        public List<ChartPoint> data { get; set; }
    }

    public class FactoryRow
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int ProduceQuantity { get; set; }
        public int Quantity { get; set; }
        //public int NoProduct { get; set; }
        public int NoDay { get; set; }
        public IEnumerable<ProductData> Products { get; set; }

        public IEnumerable<TeamRow> Teams { get; set; }
    } 
    public class TeamRow {
        public int Id {get;set;}
        public string Name {get;set;}
        public int FactoryId { get; set; }
        public int ProduceQuantity {get;set;}
        public int Quantity { get; set; }
        //public int NoProduct { get; set; }
        public int NoDay { get; set; }
        public IEnumerable<ProductData> Products { get; set; }
    }
    public class TableView
    {
        public List<FactoryRow> Factories { get; set; }
    }
    public class AnalyticsController : ApiController
    {
        private GarmentContext db = new GarmentContext();
        [HttpGet]
        public IEnumerable<TeamChartView> TeamsChart(DateTime? from=null, DateTime? to=null, string groupBy="day")
        {
            if(to.HasValue == false)
                to = DateTime.Now.Date.AddDays(1);
            if (!from.HasValue)
                from = to.Value.AddDays(-30).Date;
            IEnumerable<TeamChartView> result;
           
            if (groupBy == "day")
            {
                result =  TeamsChartByDate(from.Value, to.Value);
            }
            else if(groupBy == "week")
            {
                result = TeamsChartByWeek(from.Value, to.Value);
            }
            else if (groupBy == "month")
            {
                result = TeamsChartByMonth(from.Value, to.Value);
            }
            else if (groupBy == "week")
            {
                result = TeamsChartByQuater(from.Value, to.Value);
            }
            else
            {
                return TeamsChartByYear(from.Value, to.Value);
            }
            result = result.Concat(result.GroupBy(t => t.factory).Select(factory =>
                new TeamChartView
                {
                    id = "factory_" + factory.Key.id,
                    name = factory.Key.name,
                    data = factory.SelectMany(t => t.data).GroupBy(d => d.x).Select(
                    g => new ChartPoint
                    {
                        x = g.Key,
                        y = g.Sum(d => d.y),
                        products = g.Where(point=> point.products!=null).SelectMany(point => point.products).GroupBy(p => p.ProductId).Select(gp => new ProductData
                        {
                            ProductId = gp.Key,
                            ProduceQuantity = gp.Sum(p => p.ProduceQuantity)
                        }).ToList()
                    })
                }));
            return result;
        }
        [HttpGet]
        public TableView TeamsTable(DateTime from, DateTime to)
        {
            var result = new TableView();
            var factories = db.Goals.Where(g => g.GoalDate >= from && g.GoalDate <= to).GroupBy(g => g.Team.Factory)
                .Select(factoryGoals => new FactoryRow
                {
                    Id = factoryGoals.Key.Id,
                    Name = factoryGoals.Key.Name,
                    Quantity = factoryGoals.Sum(date => date.GoalDetails.Sum(hour => hour.ProductDetails.Sum(product => product.Quantity))),
                    ProduceQuantity = factoryGoals.Sum(date => date.GoalDetails.Sum(hour => hour.ProductDetails.Sum(product => product.ProduceQuantity.HasValue ? product.ProduceQuantity.Value:0))),
                    Products = factoryGoals.SelectMany(g => g.GoalDetails.SelectMany(d => d.ProductDetails)).GroupBy(p => p.ProductId).Select(pg => new ProductData
                    {
                        ProductId = pg.Key,
                        ProduceQuantity = pg.Sum(p => p.ProduceQuantity.HasValue ? p.ProduceQuantity.Value : 0)
                    }).OrderByDescending(p => p.ProduceQuantity),
                    NoDay = factoryGoals.Where(g=>g.GoalDetails.All(d=>d.ProductDetails.All(p=>p.ProductId!=null))).Select(g=>g.GoalDate).Distinct().Count(),
                    Teams = factoryGoals.GroupBy(g => g.Team).Select(teamGoals => new TeamRow
                    {
                        Id = teamGoals.Key.Id,
                        Name = teamGoals.Key.Name,
                        FactoryId = teamGoals.Key.FactoryId,
                        Quantity = teamGoals.Sum(date => date.GoalDetails.Sum(hour => hour.ProductDetails.Sum(product => product.Quantity))),
                        ProduceQuantity = teamGoals.Sum(date => date.GoalDetails.Sum(hour => hour.ProductDetails.Sum(product => product.ProduceQuantity.HasValue ? product.ProduceQuantity.Value : 0))),
                        Products = teamGoals.SelectMany(g => g.GoalDetails.SelectMany(d => d.ProductDetails)).GroupBy(p=>p.ProductId).Select(pg=>new ProductData
                            {
                                ProductId = pg.Key,
                                ProduceQuantity = pg.Sum(p=>p.ProduceQuantity.HasValue? p.ProduceQuantity.Value: 0)
                            }).OrderByDescending(p=>p.ProduceQuantity),
                        NoDay = teamGoals.Where(g => g.GoalDetails.All(d => d.ProductDetails.All(p => p.ProductId != null))).Count()
                    })
                }).ToList();
            //teams
            result.Factories = factories;
            return result;
        }
        [HttpGet]
        public IEnumerable<TeamChartView> SingleTeamChart(int id, DateTime? from = null, DateTime? to = null)
        {
            if (to.HasValue == false)
                to = DateTime.Now.Date.AddDays(1);
            if (!from.HasValue)
                from = to.Value.AddDays(-30).Date;
            //var hours = Enumerable.Range(0, (int)to.Value.Subtract(from.Value).TotalHours)
            //                        .Select(offset => new ChartPoint { x = 
            //                            ToUnixTimestamp(from.Value.AddHours(offset)), y = 0
            //                        });
            var result = new List<TeamChartView>();
            var data = db.Goals.Where(g => g.TeamId == id && g.GoalDate >= from && g.GoalDate <= to).OrderBy(g=>g.GoalDate).ToList();
            var quantityData = new TeamChartView
            {
                id = id.ToString(),
                name = "Mục tiêu",
                data = data.Select(g => new ChartPoint { x = ToUnixTimestamp(g.GoalDate), y = g.GoalDetails.Sum(gd=>gd.ProductDetails.Sum(pd=>pd.Quantity)) }).ToList()
            };
            var produceQuantityData = new TeamChartView
            {
                id = id.ToString(),
                name = "Đạt được",
                data = data.Select(g => new ChartPoint { x = ToUnixTimestamp(g.GoalDate), y = g.GoalDetails.Sum(gd => gd.ProductDetails.Where(pd=>pd.ProduceQuantity.HasValue).Sum(pd => pd.ProduceQuantity.Value)) }).ToList()
            };
            result.Add(quantityData);
            result.Add(produceQuantityData);

            //foreach (var team in result)
            //{
            //    var existHours = team.data.Select(d => d.x);
            //    var emptyHours = hours.Where(h => !existHours.Contains(h.x));
            //    team.data = team.data.Union(emptyHours).OrderBy(h => h.x);
            //}

            return result;
        }


        [HttpGet]
        public IEnumerable<ReasonChartView> ReasonChart(int id, DateTime? from = null, DateTime? to = null)
        {
            if (to.HasValue == false)
                to = DateTime.Now.Date.AddDays(1);
            if (!from.HasValue)
                from = to.Value.AddDays(-30).Date;

            var dates = Enumerable.Range(0, (int)to.Value.Subtract(from.Value).TotalDays + 1)
                        .Select(offset => from.Value.AddDays(offset));
            var data = db.Reasons.Select(r => new ReasonChartView
                {
                    name = r.Name,
                    color = r.ColorCode,
                    PointsData = r.ProductDetails.Where(pd => pd.GoalDetail.Goal.TeamId == id && pd.GoalDetail.Goal.GoalDate >= from && pd.GoalDetail.Goal.GoalDate <= to)
                    .GroupBy(pd=>pd.GoalDetail.Goal)
                    .Select(gg => new PointData {
                        Date = gg.Key.GoalDate,
                        Value = gg.Count()
                    })
                }).ToList();
            data.ForEach(reason => reason.data = reason.PointsData.Select(point => new ChartPoint { x = ToUnixTimestamp(point.Date), y = point.Value}));
            foreach (var reason in data)
            {
                var existHours = reason.PointsData.Select(d => d.Date);
                var emptyHours = dates.Where(d => !existHours.Contains(d)).Select(d => new ChartPoint { x = ToUnixTimestamp(d), y = null});
                reason.data = reason.data.Union(emptyHours).OrderBy(d => d.x);
            }

            var dateLength = dates.Count();
            foreach (var error in data)
            {
                error.cells = new int[dateLength];
                for (var i = 0; i < dateLength; i++)
                {
                    var point = error.PointsData.FirstOrDefault(p => p.Date == dates.ElementAt(i));
                    if (point != null)
                        error.cells[i] = point.Value;
                }
            }
            return data;
        }

        [HttpGet]
        public IEnumerable<EmployeeChartView> EmployeeChart(int id, DateTime? from = null, DateTime? to = null)
        {
            if (to.HasValue == false)
                to = DateTime.Now.Date.AddDays(1);
            if (!from.HasValue)
                from = to.Value.AddDays(-30).Date;

            var dates = Enumerable.Range(0, (int)to.Value.Subtract(from.Value).TotalDays + 1)
                        .Select(offset => ToUnixTimestamp(from.Value.AddDays(offset)));
            var employeedata = new List<EmployeeChartView>()
            {
                 new EmployeeChartView
                {
                    name = "Không lý do",
                    color = "red",
                    showInLegend = true,
                    visible = true,
                    legendIndex = 0,
                    data = new List<ChartPoint>() 
                },
                new EmployeeChartView
                {
                    name = "Có lý do",
                    color = "orange",
                    showInLegend = true,
                    visible = true,
                    legendIndex = 2,
                    data = new List<ChartPoint>() 
                },
                new EmployeeChartView
                {
                    name = "Có mặt",
                    color = "green",
                    showInLegend = true,
                    visible = true,
                    legendIndex = 1,
                    data = new List<ChartPoint>() 
                },
                
                new EmployeeChartView
                {
                    name = "Tổng số",
                    color = "black",
                    showInLegend = false,
                    visible = false,
                    data = new List<ChartPoint>() 
                },
                
            };

            db.TeamSessions.Where(g => g.TeamId == id && g.Date >= from && g.Date <= to).ToList().ForEach(
                tsession =>
                    {
                        employeedata[3].data.Add(new ChartPoint
                        {
                            x = ToUnixTimestamp(tsession.Date),
                            y = tsession.TotalEmployee
                        });
                        employeedata[2].data.Add(new ChartPoint
                        {
                            x = ToUnixTimestamp(tsession.Date),
                            y = tsession.NoEmployee
                        });
                        employeedata[1].data.Add(new ChartPoint
                        {
                            x = ToUnixTimestamp(tsession.Date),
                            y = tsession.TotalEmployee - tsession.NoEmployee - tsession.AbsentWithoutReason
                        });
                        employeedata[0].data.Add(new ChartPoint
                        {
                            x = ToUnixTimestamp(tsession.Date),
                            y = tsession.AbsentWithoutReason
                        });
                    }
            );
                
           foreach (var status in employeedata)
            {
                var existHours = status.data.Select(d => d.x);
                var emptyHours = dates.Where(d => !existHours.Contains(d)).Select(d => new ChartPoint { x = d, y = null });
                status.data = status.data.Union(emptyHours).OrderBy(d => d.x).ToList();
            }

           //var dateLength = dates.Count();
           //foreach (var status in employeedata)
           //{
           //    status.cells = new int[dateLength];
           //    for (var i = 0; i < dateLength; i++)
           //    {
           //        var point = status.data[i].y;
           //        if (point != null)
           //            status.cells[i] = point.Value;
           //    }
           //}
           return employeedata;
        }

        [HttpGet]
        public HourChartData HourDetailsChart(int id, DateTime date)
        {
            var goaldetails = db.GoalDetails.Where(gd => gd.Goal.TeamId == id && gd.Goal.GoalDate == date).ToList();
            var categories = goaldetails.Select(gd => gd.EndCountTime.ToString("h\\h")).ToArray();
            var series = new HourChartView[]{ 
                new HourChartView { 
                    name = "Mục tiêu",
                    data = goaldetails.Select(gd=> gd.ProductDetails.Sum(pd=>pd.Quantity)).ToArray()//, x = gd.EndCountTime.ToString("h\\h")
                },
                new HourChartView { 
                    name = "Thực tế",
                    data = goaldetails.Select(gd=> gd.ProductDetails.Sum(pd=>pd.ProduceQuantity.HasValue?pd.ProduceQuantity.Value:0)).ToArray()
                },
            };

            return new HourChartData { Series = series, Categories = categories};
        }

        private double ToUnixTimestamp(DateTime target)
        {
            var date = new DateTime(1970, 1, 1, 0, 0, 0, target.Kind);
            var unixTimestamp = (target - date).TotalMilliseconds;
            return unixTimestamp;
        }
        private DateTime GetFirstMonday(int year)
        {
            DateTime jan1 = new DateTime(year, 1, 1);
            var mondayOffset = 7 - (jan1.DayOfWeek - DayOfWeek.Monday);
            mondayOffset = mondayOffset == 7 ? 0 : mondayOffset;
            return jan1.AddDays(mondayOffset);
        }
        private IEnumerable<TeamChartView> TeamsChartByDate(DateTime from, DateTime to)
        {
            //var dates = Enumerable.Range(0, (int)to.Subtract(from).TotalDays)
            //                                   .Select(offset => new ChartPoint { 
            //                            x = ToUnixTimestamp(from.AddDays(offset)), y = 0
            //                        });
            var query = db.GoalDetails.Where(gd => gd.Goal.GoalDate >= from && gd.Goal.GoalDate <= to).GroupBy(gd => gd.Goal.Team);
            var result = query.Select(g => new TeamChartView
            {
                id = g.Key.Id.ToString(),
                factory = new Factory { id = g.Key.FactoryId, name = g.Key.Factory.Name },
                name = g.Key.Name,
                PointsData = g.OrderBy(gd => gd.Goal.GoalDate).GroupBy(gd => gd.Goal.GoalDate).Select(gday => new PointData
                {
                    Date = gday.Key,
                    Value = gday.Sum(hour => hour.ProductDetails.Count != 0 ? hour.ProductDetails.Sum(product => product.ProduceQuantity.HasValue ? product.ProduceQuantity.Value : 0) : 0),
                    Products = gday.SelectMany(d => d.ProductDetails).Where(pd => pd.ProduceQuantity.HasValue).GroupBy(p => p.ProductId).Select(gp => new ProductData { 
                        ProductId = gp.Key,
                        ProduceQuantity = gp.Sum(p => p.ProduceQuantity.Value)
                    })
                })
            }).ToList();
            result.ForEach(team => team.data = team.PointsData.Select(point => new ChartPoint { x = ToUnixTimestamp(point.Date), y = point.Value, products = point.Products }));

            //foreach (var team in result)
            //{
            //    var existHours = team.data.Select(d => d.x);
            //    var emptyHours = dates.Where(h => !existHours.Contains(h.x));
            //    team.data = team.data.Union(emptyHours).OrderBy(h => h.x);
            //}

            return result;
        }
        private IEnumerable<TeamChartView> TeamsChartByWeek(DateTime from, DateTime to)
        {
            var firstMonday = GetFirstMonday(from.Year);
            //var months = Enumerable.Range(0, (int)to.Subtract(from).TotalDays)
            //                                   .Select(offset => new ArrayList { 
            //                            ToUnixTimestamp(from.AddDays(offset)), 0
            //                        });
            var query = db.GoalDetails.Where(gd => gd.Goal.GoalDate >= from && gd.Goal.GoalDate <= to).GroupBy(gd => gd.Goal.Team);
            var result = query.Select(g => new TeamChartView
            {
                id = g.Key.Id.ToString(),
                factory = new Factory { id = g.Key.FactoryId, name = g.Key.Factory.Name },
                name = g.Key.Name,
                PointsData = g.OrderBy(gd => gd.Goal.GoalDate).GroupBy(gd => DbFunctions.DiffDays(firstMonday, gd.Goal.GoalDate) / 7).Select(gweek => new PointData
                {
                    TimeValue = 7 * gweek.Key.Value,
                    //Date = firstMonday.AddDays(7 * gday.Key.Value),
                    Value = gweek.Sum(hour => hour.ProductDetails.Count != 0 ? hour.ProductDetails.Sum(product => product.ProduceQuantity.HasValue ? product.ProduceQuantity.Value : 0) : 0),
                    Products = gweek.SelectMany(d => d.ProductDetails).Where(pd => pd.ProduceQuantity.HasValue).GroupBy(p => p.ProductId).Select(gp => new ProductData
                    {
                        ProductId = gp.Key,
                        ProduceQuantity = gp.Sum(p => p.ProduceQuantity.Value)
                    })
                })
            }).ToList();
            result.ForEach(team => team.data = team.PointsData.Select(point => new ChartPoint { x = ToUnixTimestamp(firstMonday.AddDays(point.TimeValue)), y = point.Value, products = point.Products}));

            //foreach (var team in result)
            //{
            //    var existHours = team.data.Select(d => d[0]);
            //    var emptyHours = months.Where(h => !existHours.Contains(h[0]));
            //    team.data = team.data.Union(emptyHours).OrderBy(h => h[0]);
            //}

            return result;
        }
        private IEnumerable<TeamChartView> TeamsChartByMonth(DateTime from, DateTime to)
        {

            //var dates = Enumerable.Range(0, (int)to.Subtract(from).TotalDays)
            //                                   .Select(offset => new ArrayList { 
            //                            ToUnixTimestamp(from.AddDays(offset)), 0
            //                        });
            var query = db.GoalDetails.Where(gd => gd.Goal.GoalDate >= from && gd.Goal.GoalDate <= to).GroupBy(gd => gd.Goal.Team);
            var result = query.Select(g => new TeamChartView
            {
                id = g.Key.Id.ToString(),
                factory = new Factory { id = g.Key.FactoryId, name = g.Key.Factory.Name },
                name = g.Key.Name,
                PointsData = g.OrderBy(gd => gd.Goal.GoalDate).GroupBy(gd => gd.Goal.GoalDate.Year*12 + gd.Goal.GoalDate.Month - 1).Select(gmonth => new PointData
                {
                    TimeValue = gmonth.Key,
                    Value = gmonth.Sum(hour => hour.ProductDetails.Count != 0 ? hour.ProductDetails.Sum(product => product.ProduceQuantity.HasValue ? product.ProduceQuantity.Value : 0) : 0),
                    Products = gmonth.SelectMany(d => d.ProductDetails).Where(pd => pd.ProduceQuantity.HasValue).GroupBy(p => p.ProductId).Select(gp => new ProductData
                    {
                        ProductId = gp.Key,
                        ProduceQuantity = gp.Sum(p => p.ProduceQuantity.Value)
                    })
                })
            }).ToList();
            result.ForEach(team => team.data = team.PointsData.Select(point => new ChartPoint { x = ToUnixTimestamp(new DateTime(point.TimeValue / 12, point.TimeValue % 12 + 1, 1)), y = point.Value, products = point.Products }));

            //foreach (var team in result)
            //{
            //    var existHours = team.data.Select(d => d[0]);
            //    var emptyHours = dates.Where(h => !existHours.Contains(h[0]));
            //    team.data = team.data.Union(emptyHours).OrderBy(h => h[0]);
            //}

            return result;
        }
        private IEnumerable<TeamChartView> TeamsChartByQuater(DateTime from, DateTime to)
        {

            //var dates = Enumerable.Range(0, (int)to.Subtract(from).TotalDays)
            //                                   .Select(offset => new ArrayList { 
            //                            ToUnixTimestamp(from.AddDays(offset)), 0
            //                        });
            var query = db.GoalDetails.Where(gd => gd.Goal.GoalDate >= from && gd.Goal.GoalDate <= to).GroupBy(gd => gd.Goal.Team);
            var result = query.Select(g => new TeamChartView
            {
                id = g.Key.Id.ToString(),
                factory = new Factory { id = g.Key.FactoryId, name = g.Key.Factory.Name },
                name = g.Key.Name,
                PointsData = g.OrderBy(gd => gd.Goal.GoalDate).GroupBy(gd => gd.Goal.GoalDate.Year * 12 + ((gd.Goal.GoalDate.Month - 1) / 3) * 3 + 1).Select(gquater => new PointData
                {
                    TimeValue = gquater.Key,
                    Value = gquater.Sum(hour => hour.ProductDetails.Count != 0 ? hour.ProductDetails.Sum(product => product.ProduceQuantity.HasValue ? product.ProduceQuantity.Value : 0) : 0),
                    Products = gquater.SelectMany(d => d.ProductDetails).Where(pd => pd.ProduceQuantity.HasValue).GroupBy(p => p.ProductId).Select(gp => new ProductData
                    {
                        ProductId = gp.Key,
                        ProduceQuantity = gp.Sum(p => p.ProduceQuantity.Value)
                    })
                })
            }).ToList();
            result.ForEach(team => team.data = team.PointsData.Select(point => new ChartPoint { x = ToUnixTimestamp(new DateTime(point.TimeValue / 12, point.TimeValue % 12, 1)), y = point.Value, products = point.Products }));

            //foreach (var team in result)
            //{
            //    var existHours = team.data.Select(d => d[0]);
            //    var emptyHours = dates.Where(h => !existHours.Contains(h[0]));
            //    team.data = team.data.Union(emptyHours).OrderBy(h => h[0]);
            //}

            return result;
        }
        private IEnumerable<TeamChartView> TeamsChartByYear(DateTime from, DateTime to)
        {

            //var dates = Enumerable.Range(0, (int)to.Subtract(from).TotalDays)
            //                                   .Select(offset => new ArrayList { 
            //                            ToUnixTimestamp(from.AddDays(offset)), 0
            //                        });
            var query = db.GoalDetails.Where(gd => gd.Goal.GoalDate >= from && gd.Goal.GoalDate <= to).GroupBy(gd => gd.Goal.Team);
            var result = query.Select(g => new TeamChartView
            {
                id = g.Key.Id.ToString(),
                factory = new Factory { id = g.Key.FactoryId, name = g.Key.Factory.Name },
                name = g.Key.Name,
                PointsData = g.OrderBy(gd => gd.Goal.GoalDate).GroupBy(gd => gd.Goal.GoalDate.Year).Select(gyear => new PointData
                {
                    TimeValue = gyear.Key,
                    Value = gyear.Sum(hour => hour.ProductDetails.Count != 0 ? hour.ProductDetails.Sum(product => product.ProduceQuantity.HasValue ? product.ProduceQuantity.Value : 0) : 0),
                    Products = gyear.SelectMany(d => d.ProductDetails).Where(pd => pd.ProduceQuantity.HasValue).GroupBy(p => p.ProductId).Select(gp => new ProductData
                    {
                        ProductId = gp.Key,
                        ProduceQuantity = gp.Sum(p => p.ProduceQuantity.Value)
                    })
                })
            }).ToList();
            result.ForEach(team => team.data = team.PointsData.Select(point => new ChartPoint { x = ToUnixTimestamp(new DateTime(point.TimeValue, 1, 1)), y = point.Value, products = point.Products }));

            //foreach (var team in result)
            //{
            //    var existHours = team.data.Select(d => d[0]);
            //    var emptyHours = dates.Where(h => !existHours.Contains(h[0]));
            //    team.data = team.data.Union(emptyHours).OrderBy(h => h[0]);
            //}

            return result;
        }
    }
}
