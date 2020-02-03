using AutoMapper;
using Data.DataAccessLayer;
using Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using Microsoft.AspNet.Identity;
using Garment.Web.Helpers;
using Newtonsoft.Json;
using System.Data.Entity.Core.Objects;
using System.Data.Entity;

namespace Garment.Web.Controllers.api
{
    public class ChoseTeamModel
    {
        public List<TeamModel> teams { get; set; }
        public DateTime date { get; set; }
    }

    public class ProductReasonModel
    {
        public string ProductId { get; set; }
        public ReasonModel[] Reasons { get; set; }
    }
    public class ProductCommentModel
    {
        public string ProductId { get; set; }
        public string Comment { get; set; }
    }
    public class HomeTeamSession
    {
        public int TeamId { get; set; }
        public string TeamName { get; set; }
      
        public int SessionOrder { get; set; }
        public string TimeLeft { get; set; }
        public GoalDetailModel LastHour { get; set; }
        //public string LastUpdatedHour { get; set; } //deprecated
        public int LastUpdatedQuantity { get; set; } //so muc tieu tong cong gio gan nhat
        //public ProductReasonModel[] LastReasons { get; set; } //deprecated
        //public ProductCommentModel[] LastComments { get; set; } //deprecated
        public int LastUpdatedProduceQuantity { get; set; } //so san pham tong cong gio gan nhat
        //public bool UpdatedData { get; set; } //nhap day du hay chua
        //public List<string> CurrentProductIds { get; set; }
        public int CurrentNoEmployee { get; set; }
        public int TotalEmployee { get; set; }
        public int NoEmployee { get; set; }
        public int AbsentWithoutReason { get; set; }

        public double TotalHour
        {
            get
            {
                return GoalDetails.Sum(dt => dt.TotalMinutes) / 60f;
            }
        }

        public int TotalQuantity
        {
            get
            {
                return GoalDetails.Sum(dt => dt.Quantity);
            }
        }

        public int TotalProduceQuantity
        {
            get
            {
                return GoalDetails.Sum(dt => dt.ProduceQuantity);
            }
        }

        public List<GoalDetailModel> GoalDetails { get; set; }
        
    }
    public class GoalSessionModel
    {
        public int Id { get; set; }
        //public DateTime GoalDate { get; set; }
        public string Date { get; set; }
        public string DayOfWeek { get; set; }
        public int TeamId { get; set; }
        public string TeamName { get; set; }
        public int TeamOrder { get; set; }
        public bool IsCurrentSession { get; set; }
        public int SessionOrder { get; set; }
        public string TimeLeft { get; set; }
        public GoalDetailModel LastHour { get; set; }
        public string LastUpdatedHour { get; set; } //deprecated
        public int LastUpdatedQuantity { get; set; } //so muc tieu tong cong gio gan nhat
        public ProductReasonModel[] LastReasons { get; set; } //deprecated
        public ProductCommentModel[] LastComments { get; set; } //deprecated
        public int LastUpdatedProduceQuantity { get; set; } //so san pham tong cong gio gan nhat
        public bool UpdatedData { get; set; } //nhap day du hay chua
        public List<string> CurrentProductIds { get; set; }
        public int CurrentNoEmployee { get; set; }
        public int TotalEmployee { get; set; }
        public int NoEmployee { get; set; }
        public int AbsentWithoutReason { get; set; }

        public double TotalHour
        {
            get
            {
                return GoalDetails.Sum(dt => dt.TotalMinutes) / 60f;
            }
        }

        public int TotalQuantity
        {
            get
            {
                return GoalDetails.Sum(dt => dt.Quantity);
            }
        }

        public int TotalProduceQuantity
        {
            get
            {
                return GoalDetails.Sum(dt => dt.ProduceQuantity);
            }
        }
        
        public List<GoalDetailModel> GoalDetails { get; set; }
       
        public ChartModel ChartModel { get; set; }
    }

    public class ChartModel
    {
        public int TotalMinutes { get; set; }
        public int WorkingMinutes { get; set; }
        public string CurrentTime { get; set; } //thoi gian hien tai
        public float PCurrentTime { get; set; } //% thoi gian hien tai
        public int PredictProduceQuantity { get; set; } //so san pham du doan
        public float PPredictProduceQuantity { get; set; } // % san pham du doan
        public float CurrentProduceQuantity { get; set; }
        // public int LastProduceQuantity { get; set; }
        public List<HourPiece> HourPieces { get; set; }

    }
    public class HourPiece
    {
        public string StartTime { get; set; }
        public string EndTime { get; set; }
        public float PStartTime { get; set; }
        public float PStartQuantity { get; set; }
        //public float PProduceQuantity { get; set; }
        public bool IsPass { get; set; } //sau gio hien tai
        public bool HasData { get; set; } //da nhap du lieu
        public int ProduceQuantity { get; set; }
        public int RealProduceQuantity { get; set; } //so san pham cong don tu cac gio truoc
        public float PRealProduceQuantity { get; set; } //vi tri cua so sp tung gio trên bieu do
        public float PProduceQuantity { get; set; } //phan tram cua san pham gio do (ca uoc luong thoi gian nghi) so voi toan bo
    }
    //public class ProductDetailModel
    //{
    //    public int Id { get; set; }
    //    public int Quantity { get; set; }
    //    //public int NoEmployee { get; set; }
    //    public string ProductId { get; set; }
    //}

    public class FactoryDetailModel
    {
        public string Date { get; set; }
        public string DayOfWeek { get; set; }
        public double PercentProcess { get; set; }
        public int TotalQuantityProduct { get; set; } //so SP da lam - tat ca cac ca
        public int TotalQuantityTarget { get; set; } //tong so SP phai lam  den thoi diem hien tai - tat ca cac ca
        public int TotalQuantity { get; set; } // tong so SP phai lam trong ngay - tat ca cac ca

        public int CurrentSessionQuantityProduct { get; set; } //so SP da lam - tat ca cac ca
        public int CurrentSessionQuantityTarget { get; set; } //tong so SP phai lam  den thoi diem hien tai - tat ca cac ca
        public int CurrentSessionQuantity { get; set; } // tong so SP phai lam trong ngay - tat ca cac ca

        public int TotalNoEmployeeDefault { get; set; }
        public int TotalNoEmployeeAvailable { get; set; }
        public int TeamCount { get; set; }
        public int GoodTeamCount { get; set; } //count teams that good
        public string FactoryName { get; set; }
        public string FactoryPicture { get; set; }
        public int FactoryId { get; set; }
        public List<TeamDetailModel> TeamDetails { get; set; }

        public string TimeLeft { get; set; }
        public string TimePass { get; set; }
        public string TimeTotal { get; set; }
        public string LastHourEnd { get; set; }
    }

    public class HomeFactoryDetailModel
    {
        public string Date { get; set; }
        public string DayOfWeek { get; set; }
        public double PercentProcess { get; set; }

        public int CurrentSessionQuantityProduct { get; set; } //so SP da lam - ca hien tai
        public int CurrentSessionQuantityTarget { get; set; } //tong so SP phai lam  den thoi diem hien tai - ca hien tai
        public int CurrentSessionQuantity { get; set; } // tong so SP phai lam trong ngay - ca hien tai

        public int TotalNoEmployeeDefault { get; set; }
        public int TotalNoEmployeeAvailable { get; set; }
        public int TeamCount { get; set; }
        public int GoodTeamCount { get; set; } //count teams that good
        public string FactoryName { get; set; }
        public int FactoryId { get; set; }
        public List<HomeTeamDetailModel> TeamDetails { get; set; }

        public string TimeLeft { get; set; }
        public string TimePass { get; set; }
        public string TimeTotal { get; set; }
        public string LastHourEnd { get; set; }
    }

    public class HomeTeamDetailModel
    {
        public int TeamId { get; set; }
        public string TeamName { get; set; }
        //public double PercentProcess { get; set; }
        //public int TotalQuantityProduct { get; set; } //so SP da lam - tat ca cac ca
        //public int TotalQuantityTarget { get; set; } //tong so SP phai lam  den thoi diem hien tai - tat ca cac ca
        //public int TotalQuantity { get; set; } // tong so SP phai lam trong ngay - tat ca cac ca
        public int TotalEmployee { get; set; }
        public int CurrentNoEmployee { get; set; }
        public string TimeLeft { get; set; }
        //public List<string> CurrentProductIds { get; set; }
        public List<HomeTeamSession> HomeTeamSessions { get; set; }
        //[JsonIgnore]
        //public GoalSessionModel CurrentGoalSession { get; set; }
        public int CurrentGoalSessionId { get; set; }

    }

    public class TeamDetailModel
    {
        public int TeamId { get; set; }
        public string TeamName { get; set; }
        public double PercentProcess { get; set; }
        public int TotalQuantityProduct { get; set; } //so SP da lam - tat ca cac ca
        public int TotalQuantityTarget { get; set; } //tong so SP phai lam  den thoi diem hien tai - tat ca cac ca
        public int TotalQuantity { get; set; } // tong so SP phai lam trong ngay - tat ca cac ca
        public int TotalEmployee { get; set; }
        public int CurrentNoEmployee { get; set; }
        public string TimeLeft { get; set; }
        public List<string> CurrentProductIds { get; set; }
        public List<GoalSessionModel> GoalSessions { get; set; }
        //[JsonIgnore]
        //public GoalSessionModel CurrentGoalSession { get; set; }
        public int CurrentGoalSessionId { get; set; }

    }
    public class QuantityGoalDetailModel
    {
        public int Id { get; set; }
        public int Quantity { get; set; }
        public int NoEmployee { get; set; }
        public string ProductId { get; set; }
    }
    public class QuantityGoalModel
    {
        public int OrderTeam { get; set; }
        public string TeamName { get; set; }
        public List<ProductDetailModel[]> GoalProductDetailList { get; set; }
    }
    public class QuantityHour
    {
        public TimeModel Start { get; set; }
        public TimeModel End { get; set; }
    }
    public class TimeModel
    {
        public int Days { get; set; }
        public int Hours { get; set; }
        public int Minutes { get; set; }

    }
    public class FactoryQuantity
    {
        public List<QuantityGoalModel> QuantitiesTeams { get; set; }
        public List<QuantityHour> QuantityHours { get; set; }

    }
    //public class ProduceHistoryModel
    //{
    //    public int Id { get; set; }
    //    public int EmployeeId { get; set; }
    //    public string EmployeeName { get; set; }
    //    public int Quantity { get; set; }
    //    public int OldQuantity { get; set; }
    //}

    public class ProductDetailModel
    {
        public int Id { get; set; }
        public int GoalDetailId { get; set; }
        public string ProductId { get; set; }
        public int Quantity { get; set; }
        public int? ProduceQuantity { get; set; }
        public ReasonModel[] Reasons { get; set; }
        public string Comment { get; set; }
        public bool IsDelete { get; set; }
        public string Start { get; set; }
        public string End { get; set; }
    }
    public class GoalDetailModel
    {
        public int Id { get; set; }
        public int GoalId { get; set; }
        public DateTime Date { get; set; }
        public TimeSpan StartCountTime { get; set; }
        public int StartCountDay { get; set; }
        public TimeSpan EndCountTime { get; set; }
        public int EndCountDay { get; set; }
        public int TotalMinutes { get; set; }
        //public string ProductId { get; set; }
        public int TotalEmployee { get; set; }
        public int NoEmployee { get; set; }
        //public int? ReasonId { get; set; }
        //public string Comment { get; set; }
        //public int SessionId { get; set; }
        public int TeamId { get; set; }
        public string TeamName { get; set; }
        //public int NoError { get; set; }
        public bool IsPass { get; set; } //qua thoi gian hien tai hay chua

        private DateTime fullStartTime;
        private DateTime fullEndTime;
        public DateTime FullStartTime
        {
            get {
                if (fullStartTime == default(DateTime))
                    fullStartTime = Date.AddDays(StartCountDay).Add(StartCountTime);
                return fullStartTime;
            }
        }
        public DateTime FullEndTime
        {
            get {
                if (fullEndTime == default(DateTime))
                    fullEndTime = Date.AddDays(EndCountDay).Add(EndCountTime);
                return fullEndTime; 
                }
        }

        public string StartCountTimeString
        {
            get
            {
                if (StartCountTime.Minutes == 0)
                    return StartCountTime.ToString("h\\h");
                else
                    return StartCountTime.ToString("h\\hmm");
            }
        }
        public string EndCountTimeString
        {
            get
            {
                if (EndCountTime.Minutes == 0)
                    return EndCountTime.ToString("h\\h");
                else
                    return EndCountTime.ToString("h\\hmm");
            }
        }
        public int Quantity { get; set; }
        public int ProduceQuantity { get; set; }
        public bool IsNoData { get; set; }
        public int IsChangeProQuantity { get; set; }
        public List<ProductDetailModel> ProductDetails { get; set; }

        public List<GoalDetailRevisionModel> Revisions { get; set; }
        public void UpdateChangeProQuantity()
        {
            IsChangeProQuantity = ProductDetails.All(gd => gd.ProduceQuantity.HasValue) ? 1 : ProductDetails.All(gd => !gd.ProduceQuantity.HasValue) ? 0 : 2;
        }
    }
    public class GoalDetailRevisionModel
    {
        public int Quantity { get; set; }
        public int TotalMinutes { get; set; }
        public int ProduceQuantity { get; set; }
        public int NoEmployee { get; set; }
        public string Reason { get; set; }
        public string Comment { get; set; }
        public string ProductId { get; set; }
        public string UserName { get; set; }
        public string DateTime { get; set; }
    }
    public class HourGoalModel
    {
        public DateTime Date { get; set; }
        public int FactoryId { get; set; }
        public string StartTime { get; set; }
        public string EndTime { get; set; }
        public TimeSpan Start { get; set; }
        //public int StartHours { get { return Start.Hours; } }
        public int StartDay { get; set; }
        public TimeSpan End { get; set; }
        //public int EndHours { get { return End.Hours; } }
        public int EndDay { get; set; }
        public List<GoalDetailModel> GoalDetails { get; set; }
        public bool IsNoData { get; set; }
        public int IsChangeProQuantity { get; set; }
    }
    public class GoalNoEmployee
    {
        public int GoalId { get; set; }
        public string TeamName { get; set; }
        public int TotalEmployee { get; set; }
        public int NoEmployee { get; set; }
        public int AbsentWithoutReason { get; set; }
        public string AbsentComment { get; set; }
    }
    public class FactoryGoal
    {
        public List<HourGoalModel> SessionGoals { get; set; }
        public int[] GoalIds { get; set; }
        public int MaxHour { get; set; }
        public int MinHour { get; set; }
        public HourGoalModel NextSessionGoal { get; set; }
        public HourGoalModel PreSessionGoal { get; set; }
    }

    public class NewSessionModel
    {
        public HourGoalModel Session { get; set; }
        public HourGoalModel NewSession { get; set; }
    }

    public class TeamSessionModel
    {
        public int TeamId { get; set; }
        public string TeamName { get; set; }
        public DateTime Date { get; set; }
        public int Order { get; set; }
        public string StartStr { get; set; }
        public string EndStr { get; set; }
        public TimeModel Start { get; set; }
        public TimeModel End { get; set; }
        public bool NotWork { get; set; }
        public int NoEmployee { get; set; } //so cong nhan di lam
        public int TotalEmployee { get; set; } //tong so cong nhan
        public int AbsentWithoutReason { get; set; } //so cong nhan vang ko co ly do
        public string AbsentComment { get; set; }
    }

    public class FactorySessionModel
    {
        public int FactoryId { get; set; }
        public DateTime Date { get; set; } 
        public int Order { get; set; }
        public string StartStr { get; set; }
        public TimeModel Start { get; set; }
        public string EndStr { get; set; }
        public TimeModel End { get; set; }
        public int Type { get; set; }
        public TeamSessionModel[] TeamSessions { get; set; } 
    }

    public class GoalsController : ApiController
    {
        private GarmentContext db = new GarmentContext();
        private MapperConfiguration config = new MapperConfiguration(cfg => {
            cfg.CreateMap<Goal, GoalSessionModel>();
            cfg.CreateMap<GoalDetail, GoalDetailModel>()
                .ForMember(x=>x.TeamName, opt=>opt.MapFrom(src=>src.Goal.Team.Name))
                .ForMember(x=>x.IsNoData, opt=>opt.MapFrom(src=>src.IsNoData()));
            cfg.CreateMap<ProductDetail, ProductDetailModel>();
            cfg.CreateMap<Reason, ReasonModel>();
            cfg.CreateMap<GoalDetailModel, GoalDetail>();
            //cfg.CreateMap<ProduceHistory, ProduceHistoryModel>();
            cfg.CreateMap<GoalDetailRevision, GoalDetailRevisionModel>()
                .ForMember(x=>x.Reason, opt=>opt.MapFrom(src=>src.Reason!=null?src.Reason.Name:""))
                .ForMember(x=>x.UserName, opt=>opt.MapFrom(src=>src.User!=null?src.User.UserName:""))
                .ForMember(x=>x.DateTime, opt=>opt.MapFrom(src=>src.DateTime.ToString("dd/MM/yyyy hh:mm")));
        });
       
        [HttpGet]
        public List<GoalSessionModel> GetAllGoals()
        {
            var goal = db.Goals.ToList();
            IMapper mapper = config.CreateMapper();
            var goalModel = mapper.Map<List<Goal>, List<GoalSessionModel>>(goal);
            return goalModel;
        }

        //dung trong view Goals\Create
        [HttpGet]
        public GoalSessionModel Find(DateTime? date, int teamId)
        {
            if (date.HasValue == false) date = DateTime.Now.Date;
            var goal = db.Goals.Include("GoalDetails").FirstOrDefault(g => g.GoalDate == date && g.TeamId == teamId);
            if (goal != null)
            {
                IMapper mapper = config.CreateMapper();
                var goalModel = mapper.Map<Goal, GoalSessionModel>(goal);
                return goalModel;
            }
            else
            {
                var team = db.Teams.Find(teamId);
                return new GoalSessionModel
                {
                    //GoalDate = date.Value,
                    TeamId = teamId,
                    TeamName = team.Name,
                    GoalDetails = new List<GoalDetailModel>()
                };
            }
        }

        [HttpGet]
        public bool IsChoseTeams(int factoryId, DateTime date)
        {
            var teams = db.Teams.Where(t => t.FactoryId == factoryId).ToList();
            foreach (var team in teams)
            {
                if (db.Goals.Any(g => g.TeamId == team.Id && g.GoalDate == date) == true)
                {
                    return true;
                };
            }
            return false;
        }

        [HttpPost]
        public bool IsChoseTeams1(string fName, string lName)
        {
            
            return false;
        }
        
        [HttpPost]
        public bool CreateTeamByDate(ChoseTeamModel obj)
        {
            try
            {
                var templates = db.GoalDetailTemplates.Where(t => t.DayOfWeek == obj.date.DayOfWeek).ToList();
                foreach (var team in obj.teams)
                {
                    //if (db.Goals.Any(g => g.TeamId == team.Id && g.GoalDate == date) == false)
                    if(team.Selected)
                    {
                        var goal = new Goal
                        {
                            CreateUserId = User.Identity.GetUserId(),
                            GoalDate = obj.date,
                            CreateTime = DateTime.Now,
                            TeamId = team.Id,
                            GoalDetails = new List<GoalDetail>()
                        };
                        //tạo danh sách chi tiết theo giờ
                        foreach (var template in templates)
                        {
                            var goalDetail = new GoalDetail
                            {
                                StartCountTime = template.StartCountTime,
                                EndCountTime = template.EndCountTime,
                                SessionOrder = 1, //need check again
                                TotalMinutes = template.Minute,
                                ProductDetails = new List<ProductDetail>
                            {
                                new ProductDetail()
                            }
                            };
                            goal.GoalDetails.Add(goalDetail);
                        }
                        db.Goals.Add(goal);
                    }
                }
                db.SaveChanges();
            }
            catch
            {
                return false;
            }
            return true;
        }

        //dung trong Goals\FactoryCreate
        [Authorize]
        [HttpGet]
        public FactoryGoal FactoryFind(int factoryId, DateTime date)
        {
            
            IMapper mapper = config.CreateMapper();
            var teams = db.Teams.Where(t => t.FactoryId == factoryId).ToList();
            var templates = db.GoalDetailTemplates.Where(t => t.DayOfWeek == date.DayOfWeek).OrderBy(t=>t.StartCountTime).ToList();
           
            foreach (var team in teams)
            {
                if (db.Goals.Any(g => g.TeamId == team.Id && g.GoalDate == date) == false)
                {
                    //tạo 1 goal mới
                    var goal = new Goal
                    {
                        CreateUserId = User.Identity.GetUserId(),
                        GoalDate = date,
                        CreateTime = DateTime.Now,
                        TeamId = team.Id,
                        NoEmployee = team.NoEmployee,
                        GoalDetails = new List<GoalDetail>()
                    };
                    //tạo danh sách chi tiết theo giờ
                    foreach (var template in templates)
                    {
                        var goalDetail = new GoalDetail
                        {
                            StartCountTime = template.StartCountTime,
                            EndCountTime = template.EndCountTime,
                            SessionOrder = 1,
                            TotalMinutes = template.Minute,
                            ProductDetails = new List<ProductDetail>
                            {
                                new ProductDetail()
                            }
                        };
                        goal.GoalDetails.Add(goalDetail);
                    }
                    db.Goals.Add(goal);
                };
            }
            db.SaveChanges();
            //orderteam
            var goaldetails = db.GoalDetails.Where(gd => gd.Goal.GoalDate == date && !gd.Hide && gd.Goal.Team.FactoryId == factoryId).ToList();
            var goals = goaldetails.GroupBy(gd => gd.Goal).Select(g => g.Key);
            var hourGoals = goaldetails.OrderBy(gd=>gd.StartCountDay).ThenBy(gd=> gd.StartCountTime).GroupBy(gd => new {StartDay = gd.StartCountDay, EndDay = gd.EndCountDay, Start = gd.StartCountTime, End = gd.EndCountTime }).Select(
                g => new HourGoalModel
                {
                    Date = date,
                    FactoryId = factoryId,
                    Start = g.Key.Start,
                    StartDay = g.Key.StartDay,
                    StartTime = g.Key.Start.Minutes == 0? g.Key.Start.ToString("hh\\h") : g.Key.Start.ToString("hh\\hmm"),
                    End = g.Key.End,
                    EndDay = g.Key.EndDay,
                    EndTime = g.Key.End.Hours == 0 ? "24h" : g.Key.End.Minutes == 0 ? g.Key.End.ToString("hh\\h") : g.Key.End.ToString("hh\\hmm"),
                    GoalDetails = mapper.Map<List<GoalDetailModel>>(g.OrderBy(t => t.Goal.Team.Order).ToList()),
                }).ToList();
            hourGoals.ForEach(sg => sg.IsChangeProQuantity = (sg.GoalDetails.All(gd => gd.IsChangeProQuantity == 1) ? 1 : (sg.GoalDetails.All(gd => gd.IsChangeProQuantity == 0) ? 0 : 2)));
            hourGoals.ForEach(sg => sg.IsNoData = sg.GoalDetails.All(gd => gd.IsNoData));
        
            HourGoalModel nextSessionGoal = null;
            var nextStartHour = hourGoals.Max(sg => sg.Start.Add(TimeSpan.FromDays(sg.StartDay))).Add(new TimeSpan(1, 0, 0));
            var nextStartDay = nextStartHour.Days;
            //if (nextStartHour.TotalHours <= 23)
            {
                var nextEndHour = nextStartHour.Add(new TimeSpan(1, 0, 0));
                var nextEndDay = nextEndHour.Days;

                nextStartHour = nextStartHour.Subtract(TimeSpan.FromDays(nextStartDay));
                nextEndHour =  nextEndHour.Subtract(TimeSpan.FromDays(nextEndDay));
                var nextGoalDetails = db.GoalDetails.Where(gd => gd.Goal.GoalDate == date && gd.StartCountTime == nextStartHour && gd.EndCountTime == nextEndHour && gd.Goal.Team.FactoryId == factoryId);
                var nextGoalDetailModels = mapper.Map<List<GoalDetailModel>>(nextGoalDetails);
                nextSessionGoal = new HourGoalModel
                {
                    Date = date,
                    FactoryId = factoryId,
                    Start = nextStartHour,
                    StartDay = nextStartDay,
                    End = nextEndHour,
                    EndDay = nextEndDay,
                    StartTime = nextStartHour.Minutes == 0 ? nextStartHour.ToString("hh\\h") : nextStartHour.ToString("hh\\hmm"),
                    EndTime = nextEndHour.Hours == 0?"24h": nextEndHour.Minutes == 0 ? nextEndHour.ToString("hh\\h") : nextEndHour.ToString("hh\\hmm"),
                    GoalDetails = nextGoalDetailModels.Count != 0 ? nextGoalDetailModels : goals.Select(g => new GoalDetailModel
                    {
                        StartCountTime = nextStartHour,
                        StartCountDay = nextStartDay,
                        EndCountTime = nextEndHour,
                        EndCountDay = nextEndDay,
                        GoalId = g.Id,
                        TeamId = g.Team.Id,
                        TeamName = g.Team.Name,
                        TotalMinutes = 60,
                        ProductDetails = new List<ProductDetailModel> { 
                            new ProductDetailModel()
                        }
                        
                    }).ToList()
                };
            }

            HourGoalModel preSessionGoal = null;

            var preEndHour = hourGoals.Where(sg=>sg.StartDay == 0).Min(sg => sg.Start);
            if (preEndHour.Hours > 0)
            {
                var preStartHour = preEndHour.Add(new TimeSpan(-1, 0, 0));
                var preGoalDetails = mapper.Map<List<GoalDetailModel>>(db.GoalDetails.Where(gd => gd.Goal.GoalDate == date && gd.StartCountTime == preStartHour && gd.EndCountTime == preEndHour && gd.Goal.Team.FactoryId == factoryId));

                preSessionGoal = new HourGoalModel
                {
                    Date = date,
                    FactoryId = factoryId,
                    Start = preStartHour,
                    End = preEndHour,
                    StartTime = preStartHour.Minutes == 0 ? preStartHour.ToString("hh\\h") : preStartHour.ToString("hh\\hmm"),
                    EndTime = preEndHour.Minutes == 0 ? preEndHour.ToString("hh\\h") : preEndHour.ToString("hh\\hmm"),
                    GoalDetails = preGoalDetails.Count != 0 ? preGoalDetails : goals.Select(g => new GoalDetailModel
                    {
                        StartCountTime = preStartHour,
                        EndCountTime = preEndHour,
                        GoalId = g.Id,
                        TeamId = g.Team.Id,
                        TeamName = g.Team.Name,
                        TotalMinutes = 60,
                        ProductDetails = new List<ProductDetailModel> { 
                            new ProductDetailModel()
                        }
                    }).ToList()
                };
            }
            var result = new FactoryGoal
            { 
                SessionGoals = hourGoals,
                GoalIds = goals.Select(g=>g.Id).ToArray(),
                NextSessionGoal = nextSessionGoal,
                PreSessionGoal = preSessionGoal
            };
            return result;
        }
        
        [HttpGet]
        public FactoryQuantity FactoryQuantity(int factoryId, DateTime date)
        {
            var goaldetails = db.GoalDetails.Where(gd => gd.Goal.GoalDate == date && !gd.Hide && gd.Goal.Team.FactoryId == factoryId).ToList();
            var sessions = goaldetails.GroupBy(gd => new { Start = gd.StartCountTime, End = gd.EndCountTime , StartDay = gd.StartCountDay, EndDay = gd.EndCountDay}).Select(g=>g.Key).OrderBy(s=>s.StartDay).ThenBy(s=>s.Start);
            
            var result = new List<QuantityGoalModel>();
            foreach(var gdgroup in goaldetails.GroupBy(gd => gd.Goal))
            {
                var quantityGoalModel = new QuantityGoalModel();
                var team = db.Teams.Find(gdgroup.Key.TeamId);
                quantityGoalModel.TeamName = team.Name;
                quantityGoalModel.OrderTeam = team.Order;
                quantityGoalModel.GoalProductDetailList = (from session in sessions
                                                           join goaldetail in gdgroup.ToList()
                                                           on session equals new { Start = goaldetail.StartCountTime, End = goaldetail.EndCountTime, StartDay = goaldetail.StartCountDay, EndDay = goaldetail.EndCountDay }
                                                           into temp
                                                           from goaldetail in temp.DefaultIfEmpty(new GoalDetail { 
                                                            ProductDetails = new List<ProductDetail>()
                                                           })
                                                           select (goaldetail.ProductDetails.Select(pd => new ProductDetailModel
                                                           {
                                                               Id = pd.Id,
                                                               Quantity = pd.Quantity,
                                                               //NoEmployee = pd.NoEmployee != 0 && goaldetail.NoEmployee != goaldetail.Goal.Team.NoEmployee ? goaldetail.NoEmployee : goaldetail.Goal.Team.NoEmployee,
                                                               ProductId = pd.ProductId,
                                                               GoalDetailId = pd.GoalDetailId,
                                                               //Start = pd.GoalDetail.StartCountTime.Minutes == 0 ? pd.GoalDetail.StartCountTime.ToString("hh\\h") : pd.GoalDetail.StartCountTime.ToString("hh\\hmm"),
                                                               //End = pd.GoalDetail.EndCountTime.Minutes == 0 ? pd.GoalDetail.EndCountTime.ToString("hh\\h") : pd.GoalDetail.EndCountTime.ToString("hh\\hmm")
                                                           }).ToArray())).ToList();
                //quantityGoalModel.NoRow = quantityGoalModel.GoalProductDetailList.Max(g => g.Count());
                result.Add(quantityGoalModel);

            };
            //orderteam
            return new FactoryQuantity
            {
                QuantitiesTeams = result.OrderBy(t => t.OrderTeam).ToList(),
                QuantityHours = sessions.Select(s => new QuantityHour {
                    Start = new TimeModel { Days = s.StartDay, Hours = s.Start.Hours, Minutes = s.Start.Minutes},
                    End = new TimeModel { Days = s.EndDay, Hours = s.End.Hours, Minutes = s.End.Minutes }
                }).ToList()
            };
        }

        [HttpGet]
        public FactorySessionModel[] GetSessions(int factoryId, DateTime date)
        {
            var sessions = db.FactorySessions.Where(fs => fs.FactoryId == factoryId && fs.Date == date).OrderBy(fs => fs.Order).ToList();
            if (sessions.Count == 0)
            {
                var templates = db.GoalDetailTemplates.Where(t => t.DayOfWeek == date.DayOfWeek).OrderBy(t => t.StartCountTime).ToList();
                var teams = db.Teams.Where(t => t.FactoryId == factoryId).ToList();

                var startSession = templates.First().StartCountTime;
                var endSession = templates.Last().EndCountTime;
                var factorySession = new FactorySession
                {
                    Date = date,
                    FactoryId = factoryId,
                    Type = 0,
                    Order = 1,
                    Start = startSession,
                    StartDay = 0, //need check again
                    End = endSession,
                    EndDay = 0, //need check again
                };
                db.FactorySessions.Add(factorySession);
                foreach (var team in teams)
                {
                    var teamSession = new TeamSession
                    {
                        Date = date,
                        TeamId = team.Id,
                        Order = 1,
                        Start = startSession,
                        StartDay = 0, //need check again
                        End = endSession,
                        EndDay = 0, //need check again
                        NoEmployee = team.NoEmployee,
                        TotalEmployee = team.NoEmployee,                        
                    };
                    db.TeamSessions.Add(teamSession);
                }
                db.SaveChanges();
                sessions.Add(factorySession);
            }
            return sessions.Select(fs =>
            new FactorySessionModel
            {
                FactoryId =fs.FactoryId,
                Date = fs.Date,
                Type = fs.Type,
                EndStr = fs.End.ToString(@"hh\:mm"),
                StartStr = fs.Start.ToString(@"hh\:mm"),
                Start = new TimeModel { Days = fs.StartDay, Hours = fs.Start.Hours, Minutes = fs.Start.Minutes },
                End = new TimeModel { Days = fs.EndDay, Hours = fs.End.Hours, Minutes = fs.End.Minutes },
                Order = fs.Order,
                TeamSessions = db.TeamSessions.Where(ts => ts.Team.FactoryId == factoryId && ts.Date == date && ts.Order == fs.Order).ToList().Select(ts =>
                      new TeamSessionModel
                      {
                          TeamId = ts.TeamId,
                          Date = ts.Date,
                          TeamName = ts.Team.Name,
                          Order = ts.Order,
                          Start = new TimeModel { Days = ts.StartDay, Hours = ts.Start.Hours, Minutes = ts.Start.Minutes },
                          End = new TimeModel { Days = ts.EndDay, Hours = ts.End.Hours, Minutes = ts.End.Minutes },
                          StartStr = ts.Start.ToString(@"hh\:mm"),
                          EndStr = ts.End.ToString(@"hh\:mm"),
                          NotWork = ts.NotWork,
                          NoEmployee = ts.NoEmployee,
                          AbsentComment = ts.AbsentComment,
                          TotalEmployee = ts.TotalEmployee,
                          AbsentWithoutReason = ts.AbsentWithoutReason
                          
                      }).ToArray()
            }).ToArray();
        }

        [HttpPost]
        public bool UpdateFactoryQuantity(List<QuantityGoalModel> QuantitiesTeams)
        {
            foreach(var quantityTeam in QuantitiesTeams)
            {
                foreach(var gpd in quantityTeam.GoalProductDetailList)
                {

                    foreach (var pd in gpd)
                    {
                        //Delete
                        if(pd.IsDelete)
                        {
                            var productdetail = db.ProductDetails.Find(pd.Id);
                            if(productdetail!=null)
                            {
                                db.ProductDetails.Remove(productdetail);
                            }
                        }
                        //Edit
                        else if (pd.Id != 0)
                        {
                            var productdetail = db.ProductDetails.Find(pd.Id);
                            productdetail.Quantity = pd.Quantity;
                            productdetail.ProductId = pd.ProductId;
                        }
                        //Add New
                        else if(!string.IsNullOrEmpty(pd.ProductId))
                        {
                            var productdetail = new ProductDetail
                            {
                                ProductId = pd.ProductId,
                                Quantity = pd.Quantity,
                                GoalDetailId = pd.GoalDetailId
                            };
                            db.ProductDetails.Add(productdetail);
                        }
                    }
                }
                
            }
            db.SaveChanges();
            return true;
        }

        //[HttpPost]
        //public bool UpdateFactoryEmployee(List<GoalNoEmployee> factoryNoEmployee)
        //{
        //    foreach (var goalNE in factoryNoEmployee)
        //    {
        //        var goal = db.Goals.Find(goalNE.GoalId);
        //        goal.NoEmployee = goalNE.NoEmployee;
        //        goal.TotalEmployee = goalNE.TotalEmployee;
        //        goal.AbsentComment = goalNE.AbsentComment;
        //        goal.AbsentWithoutReason = goalNE.AbsentWithoutReason;

        //        foreach(var gd in goal.GoalDetails)
        //        {
        //            gd.NoEmployee = goalNE.NoEmployee;
        //        }
        //    }
        //    db.SaveChanges();
        //    return true;
        //}
        
        [HttpPost]
        public bool UpdateFactoryEmployee(FactorySessionModel[] factorySessions)
        {
            foreach (var fSession in factorySessions)
            {
                foreach (var tSession in fSession.TeamSessions)
                {
                    var teamSession = db.TeamSessions.Find(tSession.TeamId, tSession.Date, tSession.Order);
                    teamSession.NoEmployee = tSession.NoEmployee;
                    teamSession.TotalEmployee = tSession.TotalEmployee;
                    teamSession.AbsentComment = tSession.AbsentComment;
                    teamSession.AbsentWithoutReason = tSession.AbsentWithoutReason;
                    var endHours = tSession.End.Days * 24 + tSession.End.Hours;
                    var startHours = tSession.Start.Days * 24 + tSession.Start.Hours;
                    var goalDetails = db.GoalDetails.Where(gd => gd.Goal.TeamId == tSession.TeamId && gd.Goal.GoalDate == tSession.Date && gd.EndCountDay * 24 + gd.EndCountTime.Hours <= endHours && gd.StartCountDay * 24 + gd.StartCountTime.Hours >= startHours).ToList();
                    foreach (var gd in goalDetails)
                    {
                        gd.NoEmployee = tSession.NoEmployee;
                        gd.TotalEmployee = tSession.TotalEmployee;
                    }
                }
            }
            db.SaveChanges();
            return true;
        }

        [HttpPost]
        public FactorySessionModel[] UpdateFactorySessions(FactorySessionModel[] factorySessions)
        {
            foreach(var fSessionModel in factorySessions)
            {
                var fSession = db.FactorySessions.Find(fSessionModel.FactoryId, fSessionModel.Date, fSessionModel.Order);
                if (fSession == null) //create new
                {
                    var startTime = new TimeSpan(fSessionModel.Start.Hours, fSessionModel.Start.Minutes, 0);
                    var endTime = new TimeSpan(fSessionModel.End.Hours, fSessionModel.End.Minutes, 0);
                    fSession = new FactorySession
                    {
                        FactoryId = fSessionModel.FactoryId,
                        Date = fSessionModel.Date,
                        Order = fSessionModel.Order,
                        End = endTime,
                        EndDay = fSessionModel.End.Days,
                        Start = startTime,
                        StartDay = fSessionModel.Start.Days,
                        Type = fSessionModel.Type
                    };
                    foreach (var tSessionModel in fSessionModel.TeamSessions)
                    {
                        if (fSession.Type == 0)
                        {
                            var teamSession = new TeamSession
                            {
                                Date = fSessionModel.Date,
                                TeamId = tSessionModel.TeamId,
                                Order = fSessionModel.Order,
                                Start = startTime,
                                StartDay = fSessionModel.Start.Days,
                                End = endTime,
                                EndDay = fSessionModel.End.Days,
                                //NoEmployee = team.NoEmployee,
                                //TotalEmployee = team.NoEmployee,
                            };
                            db.TeamSessions.Add(teamSession);
                        }
                        else
                        {
                            var teamSession = new TeamSession
                            {
                                Date = fSessionModel.Date,
                                TeamId = tSessionModel.TeamId,
                                Order = fSessionModel.Order,
                                Start = new TimeSpan(tSessionModel.Start.Hours, tSessionModel.Start.Minutes,0),
                                StartDay = tSessionModel.Start.Days,
                                End = new TimeSpan(tSessionModel.End.Hours, tSessionModel.End.Minutes, 0),
                                EndDay = tSessionModel.End.Days,
                                NotWork = tSessionModel.NotWork,
                                //NoEmployee = team.NoEmployee,
                                //TotalEmployee = team.NoEmployee,
                            };
                            db.TeamSessions.Add(teamSession);

                            //cap nhat goaldetails
                            //var goalDetails = db.GoalDetails.Where(gd => gd.Goal.TeamId == teamSession.TeamId && gd.Goal.GoalDate == teamSession.Date && gd.EndCountDay * 24 + gd.EndCountTime.Hours <= teamSession.EndDay*24 + teamSession.End.Hours && gd.StartCountDay * 24 + gd.StartCountTime.Hours >= teamSession.StartDay * 24 + teamSession.Start.Hours).ToList();
                            //foreach (var gd in goalDetails)
                            //{
                            //    gd.Hide = teamSession.NotWork;
                            //}
                        }
                    }
                    //var teams = db.Teams.Where(t => t.FactoryId == fSessionModel.FactoryId).ToList();
                    //foreach (var team in teams)
                    //{
                    //    var teamSession = new TeamSession
                    //    {
                    //        Date = fSessionModel.Date,
                    //        TeamId = team.Id,
                    //        Order = fSessionModel.Order,
                    //        Start = startTime,
                    //        StartDay = fSessionModel.Start.Days,
                    //        End = endTime,
                    //        EndDay = fSessionModel.End.Days,
                    //        NoEmployee = team.NoEmployee,
                    //        TotalEmployee = team.NoEmployee,                        
                    //    };
                    //    db.TeamSessions.Add(teamSession);
                    //}
                    db.FactorySessions.Add(fSession);
                }
                else //update
                {
                    fSession.End = new TimeSpan(fSessionModel.End.Hours, fSessionModel.End.Minutes, 0);
                    fSession.EndDay = fSessionModel.End.Days;
                    fSession.Start = new TimeSpan(fSessionModel.Start.Hours, fSessionModel.Start.Minutes, 0);
                    fSession.StartDay = fSessionModel.Start.Days;
                    fSession.Type = fSessionModel.Type;

                    foreach(var tSessionModel in fSessionModel.TeamSessions)
                    {
                        var tSession = db.TeamSessions.Find(tSessionModel.TeamId, tSessionModel.Date, tSessionModel.Order);
                        if (fSession.Type == 0)
                        {
                            tSession.End = new TimeSpan(fSessionModel.End.Hours, fSessionModel.End.Minutes, 0);
                            tSession.EndDay = fSessionModel.End.Days;
                            tSession.Start = new TimeSpan(fSessionModel.Start.Hours, fSessionModel.Start.Minutes, 0);
                            tSession.StartDay = fSessionModel.Start.Days;
                            tSession.NotWork = false;
                        }
                        else
                        {
                            tSession.End = new TimeSpan(tSessionModel.End.Hours, tSessionModel.End.Minutes, 0);
                            tSession.EndDay = tSessionModel.End.Days;
                            tSession.Start = new TimeSpan(tSessionModel.Start.Hours, tSessionModel.Start.Minutes, 0);
                            tSession.StartDay = tSessionModel.Start.Days;
                            tSession.NotWork = tSessionModel.NotWork;
                        }
                    }
                }
                
                var goals = db.Goals.Where(g => g.Team.FactoryId == fSession.FactoryId && g.GoalDate == fSession.Date).ToList();
                
                    
                foreach(var goal in goals)
                {
                    var teamSession = db.TeamSessions.Find(goal.TeamId, goal.GoalDate, fSession.Order);
                    //automatically create hours
                    for (var start = fSession.StartDay * 24 + fSession.Start.Hours; start < fSession.EndDay * 24 + fSession.End.Hours; start++)
                    {
                        var startHours = start % 24;
                        var startDays = start / 24;
                        var endHours = (start + 1) % 24;
                        var endDays = (start + 1) / 24;
                        var goaldetail = goal.GoalDetails.FirstOrDefault(gd => gd.StartCountTime.Hours == startHours && gd.StartCountDay == startDays && gd.EndCountTime.Hours == endHours && gd.EndCountDay == endDays);
                        if (goaldetail != null)
                            goaldetail.Hide = teamSession.NotWork;
                        else
                        {
                            goaldetail = new GoalDetail
                            {
                                StartCountTime = TimeSpan.FromHours(startHours),
                                StartCountDay = startDays,
                                EndCountTime = TimeSpan.FromHours(endHours),
                                EndCountDay = endDays,
                                TotalMinutes = 60,
                                Hide = teamSession.NotWork,
                                SessionOrder = teamSession.Order,
                                //NoEmployee = goal.Team.NoEmployee,
                                Goal = goal,
                                Revisions = new List<GoalDetailRevision>(),
                                ProductDetails = new List<ProductDetail>
                                {
                                    new ProductDetail()
                                }
                            };
                            db.GoalDetails.Add(goaldetail);
                        }
                    }
                }
                db.SaveChanges();
            }
            return GetSessions(factorySessions[0].FactoryId, factorySessions[0].Date);
        }
        [HttpGet]
        public List<GoalNoEmployee> FactoryNoEmployee(int factoryId, DateTime date)
        {
            var goals = db.Goals.Where(g => g.Team.FactoryId == factoryId && g.GoalDate == date).Select(
                    g=> new GoalNoEmployee
                    {
                        GoalId = g.Id,
                        TeamName = g.Team.Name,
                        TotalEmployee = g.TotalEmployee,
                        NoEmployee = g.NoEmployee,
                        AbsentComment = g.AbsentComment,
                        AbsentWithoutReason = g.AbsentWithoutReason
                    }
                ).ToList();
            return goals;
        }

        //[HttpPost]
        //public bool CreateFactorySession(List<GoalDetailModel> goalDetailModels)
        //{
        //    IMapper mapper = config.CreateMapper();
        //    foreach (var goalDetailModel in goalDetailModels)
        //    {
        //        //var gd = new GoalDetail
        //        //{
        //        //    Comment = goalDetail.Comment,
        //        //    EndCountTime = goalDetail.EndCountTime,
        //        //    GoalId = goalDetail.GoalId,
        //        //    ManualProduceQuantity = goalDetail.ManualProduceQuantity,
        //        //    NoEmployee = goalDetail.NoEmployee,
        //        //    ProductId = goalDetail.ProductId,
        //        //    ProduceQuantity = goalDetail.ProduceQuantity,

        //        //};
        //        if (goalDetailModel.Id != 0)
        //        {
        //            var goalDetail = db.GoalDetails.Find(goalDetailModel.Id);

        //        }
        //        else
        //        {
        //            var goalDetail = mapper.Map<GoalDetail>(goalDetailModel);
        //            db.GoalDetails.Add(goalDetail);
        //        }
        //    }
        //    db.SaveChanges();
        //    return true;
        //}

        [HttpGet]
        public HomeTeamSession HomeTeamSession(DateTime? date, int teamId, int? order = null)
        {
            IMapper mapper = config.CreateMapper();
            var now = DateTime.Now;
            if (date.HasValue == false) date = now.Date;
            var sessions = db.TeamSessions.Where(ts => ts.TeamId == teamId && ts.Date == date.Value).OrderBy(ts => ts.Order).ToList();
            if (!order.HasValue)
            {
                //tim ca hien tai

                order = 1;

            }
            var session = sessions.FirstOrDefault(s => s.Order == order.Value);
            var goal = db.Goals.FirstOrDefault(g => g.GoalDate == date && g.TeamId == teamId);
            if (goal != null)
            {
                var goalSession = new HomeTeamSession();
                goalSession.TeamId = goal.TeamId;
                goalSession.TeamName = goal.Team.Name;
                //goalSession.TeamOrder = goal.Team.Order;
                goalSession.TotalEmployee = session.TotalEmployee;
                goalSession.NoEmployee = session.NoEmployee;
                goalSession.AbsentWithoutReason = session.AbsentWithoutReason;
                goalSession.SessionOrder = order.Value;
                var goalDetails = goal.GoalDetails.Where(gd => gd.Hide == false && gd.StartCountDay * 24 + gd.StartCountTime.Hours >= session.StartDay * 24 + session.Start.Hours && gd.EndCountDay * 24 + gd.EndCountTime.Hours <= session.EndDay * 24 + session.End.Hours).OrderBy(gd => gd.EndCountDay).ThenBy(gd => gd.EndCountTime).ToList();
                goalSession.GoalDetails = goalDetails.Select(
                    d => new GoalDetailModel
                    {
                        Date = date.Value,
                        StartCountTime = d.StartCountTime,
                        EndCountTime = d.EndCountTime,
                        ProduceQuantity = d.ProductDetails.Sum(pd => pd.ProduceQuantity.HasValue ? pd.ProduceQuantity.Value : 0),
                        Quantity = d.ProductDetails.Sum(pd => pd.Quantity),
                        IsChangeProQuantity = d.IsChangeProQuantity(),
                        TotalMinutes = d.TotalMinutes,
                        NoEmployee = d.NoEmployee,
                        TotalEmployee = d.TotalEmployee,
                        IsNoData = d.IsNoData(),
                        ProductDetails = mapper.Map<List<ProductDetailModel>>(d.ProductDetails)
                    }).ToList();

                var timeNow = DateTime.Now;

                goalSession.TimeLeft = new TimeSpan(goalSession.GoalDetails.Where(gd => gd.FullEndTime > now)
                               .Sum(gd => gd.FullEndTime.Ticks - Math.Max(now.Ticks, gd.FullStartTime.Ticks))).ToString("h\\hmm");

                var passHours = goalSession.GoalDetails.Where(gd => gd.FullEndTime < timeNow); //nhung gio da qua
                var updatedHours = passHours.Where(gd => gd.IsChangeProQuantity > 0); //nhung gio da nhap du lieu
                var lastHour = passHours.OrderByDescending(gd => gd.EndCountTime).FirstOrDefault();
                if (lastHour != null)
                {
                    //goalSession.UpdatedData = lastHour.IsChangeProQuantity > 0;
                    //goalSession.LastUpdatedHour = lastHour.EndCountTime.Minutes == 0 ? lastHour.EndCountTime.ToString("hh\\h") : lastHour.EndCountTime.ToString("hh\\hmm");
                    //goalSession.LastComments = lastHour.ProductDetails.Select(p => new ProductCommentModel
                    //{
                    //    ProductId = p.ProductId,
                    //    Comment = p.Comment
                    //}).ToArray();
                    //goalSession.LastReasons = lastHour.ProductDetails.Select(p => new ProductReasonModel
                    //{
                    //    ProductId = p.ProductId,
                    //    Reasons = p.Reasons.Select(r => new ReasonModel { Id = r.Id, Name = r.Name }).ToArray()
                    //}).ToArray();
                }
                goalSession.LastHour = lastHour;
                goalSession.LastUpdatedQuantity = passHours.Sum(gd => gd.Quantity);
                goalSession.LastUpdatedProduceQuantity = passHours.Sum(gd => gd.ProduceQuantity);
                goalSession.CurrentNoEmployee = goal.NoEmployee;
                goalSession.AbsentWithoutReason = goal.AbsentWithoutReason;
                var currentHour = goalSession.GoalDetails.Where(gd => gd.FullStartTime < timeNow).LastOrDefault();
                if (currentHour != null)
                {
                    //goalSession.CurrentProductIds = currentHour.ProductDetails.Select(p => p.ProductId).ToList();
                }

                passHours.ToList().ForEach(gd => gd.IsPass = true);
                
                return goalSession;
            }
            else
            {
                var team = db.Teams.Find(teamId);
                return new HomeTeamSession
                {
                    TeamId = teamId,
                    TeamName = team.Name,
                    GoalDetails = new List<GoalDetailModel>()
                };
            }
        }

        //dung trong Home/DailyGoal

        [HttpGet]
        public GoalSessionModel GoalFind(DateTime? date, int teamId, int? order=null, bool includeChart = false)
        {
           IMapper mapper = config.CreateMapper();
            var now = DateTime.Now;
            if (date.HasValue == false) date = now.Date;
            var sessions = db.TeamSessions.Where(ts => ts.TeamId == teamId && ts.Date == date.Value).OrderBy(ts => ts.Order).ToList();
            if (!order.HasValue)
            {
                //tim ca hien tai
                
                order = 1;

            }
            var session = sessions.FirstOrDefault(s => s.Order == order.Value);
            var goal = db.Goals.FirstOrDefault(g => g.GoalDate == date && g.TeamId == teamId);
            if (goal != null)
            {   
                var goalSession = new GoalSessionModel();
                goalSession.TeamId = goal.TeamId;
                //goalSession.GoalDate = goal.GoalDate;
                goalSession.TeamName = goal.Team.Name;
                goalSession.TeamOrder = goal.Team.Order;
                goalSession.TotalEmployee = session.TotalEmployee;
                goalSession.NoEmployee = session.NoEmployee;
                goalSession.AbsentWithoutReason= session.AbsentWithoutReason;
                goalSession.Date = date.Value.ToString("dd/MM/yyyy");
                goalSession.DayOfWeek = date.Value.DayOfWeekVN();
                goalSession.SessionOrder = order.Value;
                var goalDetails = goal.GoalDetails.Where(gd => gd.Hide == false && gd.StartCountDay*24 + gd.StartCountTime.Hours >= session.StartDay*24 + session.Start.Hours && gd.EndCountDay * 24 + gd.EndCountTime.Hours <= session.EndDay * 24 + session.End.Hours).OrderBy(gd=>gd.EndCountDay).ThenBy(gd=>gd.EndCountTime).ToList();
                goalSession.GoalDetails = goalDetails.Select(
                    d => new GoalDetailModel
                    {
                        Date = date.Value,
                        StartCountTime = d.StartCountTime,
                        EndCountTime = d.EndCountTime,
                        ProduceQuantity = d.ProductDetails.Sum(pd => pd.ProduceQuantity.HasValue ? pd.ProduceQuantity.Value : 0),
                        Quantity = d.ProductDetails.Sum(pd => pd.Quantity),
                        IsChangeProQuantity = d.IsChangeProQuantity(),
                        TotalMinutes = d.TotalMinutes,
                        NoEmployee = d.NoEmployee,
                        TotalEmployee = d.TotalEmployee,
                        IsNoData = d.IsNoData(),
                        ProductDetails = mapper.Map<List<ProductDetailModel>>(d.ProductDetails)
                    }).ToList();

                var timeNow = DateTime.Now;// != date ? new TimeSpan(23,59,59) : DateTime.Now.TimeOfDay;

                goalSession.TimeLeft = new TimeSpan(goalSession.GoalDetails.Where(gd => gd.FullEndTime > now)
                               .Sum(gd => gd.FullEndTime.Ticks - Math.Max(now.Ticks, gd.FullStartTime.Ticks))).ToString("h\\hmm");

                var passHours = goalSession.GoalDetails.Where(gd => gd.FullEndTime < timeNow); //nhung gio da qua
                var updatedHours = passHours.Where(gd => gd.IsChangeProQuantity > 0); //nhung gio da nhap du lieu
                var lastHour = passHours.OrderByDescending(gd => gd.EndCountTime).FirstOrDefault();
                if (lastHour != null)
                {
                    goalSession.UpdatedData = lastHour.IsChangeProQuantity > 0;
                    goalSession.LastUpdatedHour = lastHour.EndCountTime.Minutes == 0 ? lastHour.EndCountTime.ToString("hh\\h"): lastHour.EndCountTime.ToString("hh\\hmm");
                    goalSession.LastComments = lastHour.ProductDetails.Select(p => new ProductCommentModel
                        {
                            ProductId = p.ProductId,
                            Comment = p.Comment
                        }).ToArray();
                    goalSession.LastReasons = lastHour.ProductDetails.Select(p => new ProductReasonModel
                    {
                        ProductId = p.ProductId,
                        Reasons = p.Reasons.Select(r=> new ReasonModel {Id = r.Id, Name = r.Name}).ToArray()
                    }).ToArray();
                }
                goalSession.LastHour = lastHour;
                goalSession.LastUpdatedQuantity = passHours.Sum(gd => gd.Quantity);
                goalSession.LastUpdatedProduceQuantity = passHours.Sum(gd => gd.ProduceQuantity);
                goalSession.CurrentNoEmployee = goal.NoEmployee;
                goalSession.AbsentWithoutReason = goal.AbsentWithoutReason;
                var currentHour = goalSession.GoalDetails.Where(gd => gd.FullStartTime < timeNow).LastOrDefault();
                if (currentHour != null)
                {
                    goalSession.CurrentProductIds = currentHour.ProductDetails.Select(p => p.ProductId).ToList();
                    //goalModel.CurrentNoEmployee = currentHour.NoEmployee;
                }

                passHours.ToList().ForEach(gd => gd.IsPass = true);

                if (includeChart)
                {
                    if (order.Value == 1 && goalSession.GoalDetails.All(gd => gd.StartCountTime.Hours != 12))
                    {
                        //them gio nghi trua
                        var rest = new GoalDetailModel
                        {
                            StartCountTime = new TimeSpan(12, 0, 0),
                            EndCountTime = new TimeSpan(13, 0, 0),
                            ProductDetails = new List<ProductDetailModel>()
                        };
                        goalSession.GoalDetails.Add(rest);
                    }
                    goalSession.GoalDetails = goalSession.GoalDetails.OrderBy(gd=>gd.EndCountDay).ThenBy(gd => gd.EndCountTime).ToList();

                    var allWorkingMinutes = goalSession.GoalDetails.Sum(d => d.TotalMinutes); //thoi gian lam viec
                    var totalMinutes = goalSession.GoalDetails.Count * 60; //tong thoi gian 
                    //var currentProduceQuantity = goalModel.LastUpdatedProduceQuantity; //so san pham da lam viec (cap nhat)
                    var currentWorkingMinutes = updatedHours.Sum(d => d.TotalMinutes); //tong so phut (da cap nhat)
                    var totalQuantity = goalDetails.Sum(d=>d.ProductDetails.Sum(pd=>pd.Quantity)); //tong so san pham muc tieu
                    //float pProduceQuantity = currentProduceQuantity*100f / totalQuantity; //phan tram san pham da lam
                    float speed = totalQuantity*1f / allWorkingMinutes; //toc do san xuat trung binh sp/phut
                    float includeRestTotalQuantity = speed * goalSession.GoalDetails.Count * 60; // tong so san pham muc tieu, trong truong hop cong ca gio nghi
                    float currentSpeed = currentWorkingMinutes != 0 ? goalSession.LastUpdatedProduceQuantity * 1f / currentWorkingMinutes : 0; // toc do san xuat hien tai
                    var currentMinutes = goalSession.GoalDetails.Count == 0 ? 0 : (int)(timeNow - date.Value.Add(goalSession.GoalDetails.Min(gd=>gd.StartCountTime))).TotalMinutes;//so phut chenh lech hien tai va bat dau
                    var pCurrentMinutes = currentMinutes*100f / totalMinutes; //% thoi gian hien tai
                    var realWorkingMinutes = (int)goalSession.GoalDetails.Sum(gd=> gd.FullEndTime < timeNow ? gd.TotalMinutes: gd.FullStartTime < timeNow? Math.Max((timeNow - gd.FullStartTime).TotalMinutes - (60-gd.TotalMinutes),0):0); //so phut lam viec hien tai
                    var predictProduceQuantity = currentSpeed * realWorkingMinutes;
                    //var pPredictProduceQuantity = predictProduceQuantity * 100 / totalQuantity;// % san pham du doan


                    var chartModel = new ChartModel
                    {
                        WorkingMinutes = allWorkingMinutes,
                        TotalMinutes = totalMinutes,
                        //LastProduceQuantity = currentProduceQuantity,
                        PCurrentTime = pCurrentMinutes,
                        CurrentTime = timeNow.ToString("hh\\hmm"),
                        HourPieces = new List<HourPiece>()
                    };

                    //tinh so lieu tung  gio
                    //float countProduceQuantity = 0;
                    //float countPredictProduceQuantity = 0;
                    //float restPredictPQ = 0;
                    int countRealProduceQuantity = 0;
                    float pRealProduceQuantity = 0;
                    float pLastProduceQuantity = 0;
                    foreach (var gd in goalSession.GoalDetails)
                    {
                        float pStartTime = 100*(60 - gd.TotalMinutes) / 60f;
                        
                        //so san pham trong mui gio do
                        var rHourProduceQuantity = gd.ProductDetails.Sum(pd => pd.ProduceQuantity.HasValue ? pd.ProduceQuantity.Value : 0);
                        //so sp tuong duong voi thoi gian nghi
                        var restHourProduceQuantity = (60 - gd.TotalMinutes) * speed;
                        var totalHourProduceQuantity = rHourProduceQuantity + restHourProduceQuantity;
                        //tong so sp toi thoi diem nay
                        countRealProduceQuantity += rHourProduceQuantity;
                        //phan tram so sp cua gio nay so voi tong cong (ca thoi gian nghi)
                        var pProduceQuantity = (totalHourProduceQuantity) * 100f / includeRestTotalQuantity;
                        if(pRealProduceQuantity + pProduceQuantity > 100)
                        {
                            pProduceQuantity = 100 - pRealProduceQuantity;
                        }
                        pRealProduceQuantity += pProduceQuantity;
                        var isPass = gd.FullEndTime > timeNow;
                        if (isPass == false)
                            pLastProduceQuantity = pRealProduceQuantity;
                        var hourPiece = new HourPiece
                        {
                            StartTime = gd.StartCountTime.ToString("hh\\h"),
                            EndTime  = gd.EndCountTime.ToString("hh\\h"),
                            PStartTime = pStartTime,
                            PProduceQuantity = pProduceQuantity,
                            PRealProduceQuantity = pRealProduceQuantity,
                            PStartQuantity = restHourProduceQuantity*100f/totalHourProduceQuantity,
                            IsPass = isPass,
                            ProduceQuantity = rHourProduceQuantity,
                            RealProduceQuantity = countRealProduceQuantity,
                            HasData = gd.ProductDetails.Any(pd=>pd.ProduceQuantity.HasValue) || gd.ProductDetails.Count == 0
                        };
                        chartModel.HourPieces.Add(hourPiece);
                    }
                    
                    //vi tri cua so san pham du doan hien tai
                    var pPredictProduceQuantity = pLastProduceQuantity + (predictProduceQuantity - goalSession.LastUpdatedProduceQuantity) * 100 / (totalMinutes * speed);
                    chartModel.PredictProduceQuantity = Convert.ToInt32(predictProduceQuantity);
                    chartModel.PPredictProduceQuantity = pPredictProduceQuantity;
                    goalSession.ChartModel = chartModel;
                }
                return goalSession;
            }
            else
            {
                var team = db.Teams.Find(teamId);
                return new GoalSessionModel
                {
                    //GoalDate = date.Value,
                    TeamId = teamId,
                    TeamName = team.Name,
                    //TotalEmployee = team.NoEmployee,
                    GoalDetails = new List<GoalDetailModel>()
                };
            }
        }

        //dung trong View Factories\Details
        [HttpGet]
        public FactoryDetailModel FindInFactory(DateTime? date, int factoryId, bool includeChart = true)
        {
            return GetInfoByFactory(date, factoryId, includeChart);
        }

        [HttpGet]
        public List<HomeFactoryDetailModel> FindAllFactory(DateTime? date)
        {
            var factories = db.Factories.ToList();
            List<HomeFactoryDetailModel> factoryInfos = new List<HomeFactoryDetailModel>();
            foreach(var f in factories)
            {
                var fi = HomeGetFactory(date, f.Id);
                factoryInfos.Add(fi);
            }
            return factoryInfos;
        }

        public HomeFactoryDetailModel HomeGetFactory(DateTime? date, int factoryId)
        {
            var factory = db.Factories.Find(factoryId);
            var teamDetails = new List<HomeTeamDetailModel>();
            var now = DateTime.Now;
            if (date.HasValue == false) date = now.Date;

            HomeFactoryDetailModel fd = new HomeFactoryDetailModel
            {
                Date = date.Value.ToString("dd/MM/yyyy"),
                DayOfWeek = date.Value.DayOfWeekVN(),
                FactoryName = factory.Name,
                FactoryId = factory.Id,
                TeamDetails = teamDetails
            };
            var fsession = db.FactorySessions.Where(fs => fs.FactoryId == factoryId && fs.Date == date.Value).ToList().Where(fs => fs.Date.AddDays(fs.StartDay).AddSeconds(fs.Start.TotalSeconds) <= now).OrderByDescending(fs => fs.Start).FirstOrDefault();
            if (fsession == null) return fd;

            foreach (var team in factory.Teams)
            {
                if (team.VisibleStatus != 2) //not hide
                {
                    var sessions = db.TeamSessions.Where(ts => ts.TeamId == team.Id && ts.Date == date.Value).OrderBy(ts => ts.Order).ToList();
                    var teamDetail = new HomeTeamDetailModel()
                    {
                        TeamId = team.Id,
                        TeamName = team.Name,
                        HomeTeamSessions = new List<HomeTeamSession>(),
                    };
                    var currentGoalSessionId = -1;

                    for (var i = 0; i < sessions.Count; i++)
                    {
                        var session = sessions[i];
                        var homeTeamSession = HomeTeamSession(date, team.Id, session.Order);

                        if (team.VisibleStatus == 1 || (team.VisibleStatus == 0 && homeTeamSession.GoalDetails.Count > 0 && homeTeamSession.GoalDetails.Any(gd => gd.IsNoData == false)))
                        {
                            teamDetail.HomeTeamSessions.Add(homeTeamSession);
                            //check if it is current session
                            if (session.Date.AddDays(session.StartDay).Add(session.Start) <= now && session.Date.AddDays(session.EndDay).Add(session.End) >= now)
                            {
                                currentGoalSessionId = teamDetail.HomeTeamSessions.Count - 1;
                            }
                        }
                    }
                    if (currentGoalSessionId == -1) currentGoalSessionId = teamDetail.HomeTeamSessions.Count - 1; //last session

                    teamDetail.CurrentGoalSessionId = currentGoalSessionId;

                    if (teamDetail.HomeTeamSessions.Count > 0) //check team contain data
                    {
                        //teamDetail.TotalQuantity = teamDetail.GoalSessions.Sum(q => q.TotalQuantity);
                        //teamDetail.TotalQuantityProduct = teamDetail.GoalSessions.Sum(q => q.LastUpdatedProduceQuantity);
                        //teamDetail.TotalQuantityTarget = teamDetail.GoalSessions.Sum(q => q.LastUpdatedQuantity);

                        if (currentGoalSessionId >= 0)
                        {
                            var currentGoalSession = teamDetail.HomeTeamSessions[currentGoalSessionId];
                            teamDetail.TimeLeft = new TimeSpan(currentGoalSession.GoalDetails.Where(gd => gd.FullEndTime > now)
                            .Sum(gd => gd.FullEndTime.Ticks - Math.Max(now.Ticks, gd.FullStartTime.Ticks))).ToString("h\\hmm");
                            //teamDetail.CurrentProductIds = currentGoalSession.CurrentProductIds;
                            if (currentGoalSession.LastHour != null)
                            {
                                teamDetail.TotalEmployee = currentGoalSession.LastHour.TotalEmployee;
                                teamDetail.CurrentNoEmployee = currentGoalSession.LastHour.NoEmployee;
                            }
                        }
                        teamDetails.Add(teamDetail);
                    }
                }
            }

            var currentSessionTeams = teamDetails.Where(t => t.CurrentGoalSessionId >= 0 && t.HomeTeamSessions[t.CurrentGoalSessionId].SessionOrder == fsession.Order).ToList();
            fd.CurrentSessionQuantity += currentSessionTeams.Sum(t => t.HomeTeamSessions[t.CurrentGoalSessionId].TotalQuantity);
            fd.CurrentSessionQuantityProduct += currentSessionTeams.Sum(t => t.HomeTeamSessions[t.CurrentGoalSessionId].LastUpdatedProduceQuantity);
            fd.CurrentSessionQuantityTarget += currentSessionTeams.Sum(t => t.HomeTeamSessions[t.CurrentGoalSessionId].LastUpdatedQuantity);
            
            fd.TotalNoEmployeeDefault = teamDetails.Sum(t => t.TotalEmployee);
            fd.TotalNoEmployeeAvailable = teamDetails.Sum(t => t.CurrentNoEmployee);
            fd.TeamCount = teamDetails.Count();
            fd.GoodTeamCount = teamDetails.Count(t => t.HomeTeamSessions[t.CurrentGoalSessionId] != null && t.HomeTeamSessions[t.CurrentGoalSessionId].LastUpdatedProduceQuantity >= t.HomeTeamSessions[t.CurrentGoalSessionId].LastUpdatedQuantity && (t.HomeTeamSessions[t.CurrentGoalSessionId].LastHour == null || t.HomeTeamSessions[t.CurrentGoalSessionId].LastHour.ProductDetails.All(pd => pd.Reasons.Length == 0)));
            fd.PercentProcess = fd.CurrentSessionQuantity == 0 ? 0 : fd.CurrentSessionQuantityProduct * 100 / fd.CurrentSessionQuantity;

            var endTime = date.Value.AddDays(fsession.EndDay).Add(fsession.End);
            var timeTotal = TimeSpan.FromDays(fsession.EndDay).Add(fsession.End) - TimeSpan.FromDays(fsession.StartDay).Add(fsession.Start);
            var timeLeft = now > endTime ? new TimeSpan(0) : endTime - now;
            fd.TimeTotal = timeTotal.ToString("h\\hmm");
            fd.TimeLeft = timeLeft.ToString("h\\hmm");
            fd.TimePass = (timeTotal - timeLeft).ToString("h\\hmm");

            if (currentSessionTeams.Count > 0)
            {
                fd.LastHourEnd = currentSessionTeams[0].HomeTeamSessions[currentSessionTeams[0].CurrentGoalSessionId].LastHour.EndCountTime.ToString("h\\hmm");
            }

            return fd;
        }

        public FactoryDetailModel GetInfoByFactory(DateTime? date, int factoryId, bool includeChart = false)
        {
            var factory = db.Factories.Find(factoryId);
            var teams = factory.Teams;
            var teamDetails = new List<TeamDetailModel>();
            var now = DateTime.Now;
            if (date.HasValue == false) date = now.Date;

            FactoryDetailModel fd = new FactoryDetailModel
            {
                Date = date.Value.ToString("dd/MM/yyyy"),
                DayOfWeek = date.Value.DayOfWeekVN(),
                FactoryName = factory.Name,
                FactoryPicture = factory.Picture,
                FactoryId = factory.Id,
                TeamDetails = teamDetails
            };
            var fsession = db.FactorySessions.Where(fs => fs.FactoryId == factoryId && fs.Date == date.Value).ToList().Where(fs=>fs.Date.AddDays(fs.StartDay).AddSeconds(fs.Start.TotalSeconds) <= now).OrderByDescending(fs => fs.Start).FirstOrDefault();
            if (fsession == null) return fd;

            foreach (var team in teams)
            {
                if (team.VisibleStatus != 2) //not hide
                {
                    var sessions = db.TeamSessions.Where(ts => ts.TeamId == team.Id && ts.Date == date.Value).OrderBy(ts => ts.Order).ToList();
                    var teamDetail = new TeamDetailModel()
                    {
                        TeamId = team.Id,
                        TeamName = team.Name,
                        GoalSessions = new List<GoalSessionModel>(),
                    };
                    var currentGoalSessionId = -1;
                    
                    for (var i = 0;i < sessions.Count;i++)
                    {
                        var session = sessions[i];
                        var goalSession = GoalFind(date, team.Id, session.Order, includeChart);

                        if (team.VisibleStatus == 1 || (team.VisibleStatus == 0 && goalSession.GoalDetails.Count > 0 && goalSession.GoalDetails.Any(gd => gd.IsNoData == false)))
                        {
                            teamDetail.GoalSessions.Add(goalSession);
                            //check if it is current session
                            if (session.Date.AddDays(session.StartDay).Add(session.Start) <= now && session.Date.AddDays(session.EndDay).Add(session.End) >= now)
                            {
                                currentGoalSessionId = teamDetail.GoalSessions.Count - 1;
                            }
                        }
                    }
                    if (currentGoalSessionId == -1) currentGoalSessionId = teamDetail.GoalSessions.Count - 1; //last session
                     
                    teamDetail.CurrentGoalSessionId = currentGoalSessionId;
                   
                    if (teamDetail.GoalSessions.Count > 0) //check team contain data
                    {
                        teamDetail.TotalQuantity = teamDetail.GoalSessions.Sum(q => q.TotalQuantity);
                        teamDetail.TotalQuantityProduct = teamDetail.GoalSessions.Sum(q => q.LastUpdatedProduceQuantity);
                        teamDetail.TotalQuantityTarget = teamDetail.GoalSessions.Sum(q => q.LastUpdatedQuantity);
                        
                        if (currentGoalSessionId>=0)
                        {
                            var currentGoalSession = teamDetail.GoalSessions[currentGoalSessionId];
                            teamDetail.TimeLeft = new TimeSpan(currentGoalSession.GoalDetails.Where(gd => gd.FullEndTime > now)
                            .Sum(gd => gd.FullEndTime.Ticks - Math.Max(now.Ticks, gd.FullStartTime.Ticks))).ToString("h\\hmm");
                            teamDetail.CurrentProductIds = currentGoalSession.CurrentProductIds;
                            if (currentGoalSession.LastHour != null)
                            {
                                teamDetail.TotalEmployee = currentGoalSession.LastHour.TotalEmployee;
                                teamDetail.CurrentNoEmployee = currentGoalSession.LastHour.NoEmployee;
                            }
                        }
                        teamDetails.Add(teamDetail);
                    }
                }
            }

            var currentSessionTeams = teamDetails.Where(t => t.CurrentGoalSessionId >= 0 && t.GoalSessions[t.CurrentGoalSessionId].SessionOrder == fsession.Order).ToList();
            fd.CurrentSessionQuantity += currentSessionTeams.Sum(t=> t.GoalSessions[t.CurrentGoalSessionId].TotalQuantity);
            fd.CurrentSessionQuantityProduct += currentSessionTeams.Sum(t => t.GoalSessions[t.CurrentGoalSessionId].LastUpdatedProduceQuantity);
            fd.CurrentSessionQuantityTarget += currentSessionTeams.Sum(t => t.GoalSessions[t.CurrentGoalSessionId].LastUpdatedQuantity);

            fd.TotalQuantity = teamDetails.Sum(q => q.TotalQuantity);
            fd.TotalQuantityProduct = teamDetails.Sum(q => q.TotalQuantityProduct);
            fd.TotalQuantityTarget = teamDetails.Sum(q => q.TotalQuantityTarget);
            fd.TotalNoEmployeeDefault = teamDetails.Sum(t => t.TotalEmployee);
            fd.TotalNoEmployeeAvailable = teamDetails.Sum(t => t.CurrentNoEmployee);
            fd.TeamCount = teamDetails.Count();
            fd.GoodTeamCount = teamDetails.Count(t => t.GoalSessions[t.CurrentGoalSessionId] != null && t.GoalSessions[t.CurrentGoalSessionId].LastUpdatedProduceQuantity >= t.GoalSessions[t.CurrentGoalSessionId].LastUpdatedQuantity && (t.GoalSessions[t.CurrentGoalSessionId].LastHour == null || t.GoalSessions[t.CurrentGoalSessionId].LastHour.ProductDetails.All(pd => pd.Reasons.Length == 0)));
            //fd.TeamDetails = teamDetails;
            fd.PercentProcess = fd.TotalQuantity == 0 ? 0 : fd.TotalQuantityProduct * 100 / fd.TotalQuantity;

            var endTime = date.Value.AddDays(fsession.EndDay).Add(fsession.End);
            var timeTotal = TimeSpan.FromDays(fsession.EndDay).Add(fsession.End) - TimeSpan.FromDays(fsession.StartDay).Add(fsession.Start);
            var timeLeft = now > endTime ? new TimeSpan(0) : endTime - now;
            fd.TimeTotal = timeTotal.ToString("h\\hmm");
            fd.TimeLeft = timeLeft.ToString("h\\hmm");
            fd.TimePass = (timeTotal - timeLeft).ToString("h\\hmm");

            if (currentSessionTeams.Count > 0)
            {
                fd.LastHourEnd = currentSessionTeams[0].GoalSessions[currentSessionTeams[0].CurrentGoalSessionId].LastHour.EndCountTime.ToString("h\\hmm");
            }

            return fd;
        }

        //[Authorize]
        //[HttpPost]
        //public GoalSessionModel CreateOrUpdate([FromBody]GoalSessionModel goalModel)
        //{
        //    if (goalModel.Id == 0)
        //    {
        //        var goal = new Goal
        //        {
        //            Id = goalModel.Id,
        //            GoalDate = goalModel.GoalDate,
        //            TeamId = goalModel.TeamId,
        //            CreateTime = DateTime.Now,
        //            CreateUserId = User.Identity.GetUserId(),
        //            GoalDetails = new List<GoalDetail>()
        //        };
        //        foreach (var detailModel in goalModel.GoalDetails)
        //        {
        //            var detail = new GoalDetail
        //            {
        //                StartCountTime = detailModel.StartCountTime,
        //                EndCountTime = detailModel.EndCountTime,
        //                TotalMinutes = detailModel.TotalMinutes,
        //                NoEmployee = detailModel.NoEmployee,
        //                TotalEmployee = detailModel.TotalEmployee,
        //                Goal = goal,
        //                Revisions = new List<GoalDetailRevision>()
        //            };
        //            if(!detail.IsNoData())
        //            {
        //                SaveDetailRevision(detail);
        //            }
        //            //foreach (var phModel in detailModel.ProduceHistories)
        //            //{
        //            //    var ph = new ProduceHistory
        //            //    {
        //            //        EmployeeId = phModel.EmployeeId,
        //            //        Quantity = phModel.Quantity
        //            //    };
        //            //    detail.ProduceHistories.Add(ph);
        //            //}
        //            goal.GoalDetails.Add(detail);
        //        }
        //        db.Goals.Add(goal);
        //        db.SaveChanges();
        //        IMapper mapper = config.CreateMapper();
        //        return mapper.Map<GoalSessionModel>(goal);
        //    }
        //    else
        //    {
        //        var goal = db.Goals.Find(goalModel.Id);
        //        //goal.GoalDate = goalModel.GoalDate;
        //        //goal.TeamId = goalModel.TeamId;
        //        goal.LastestUpdateTime = DateTime.Now;

        //        foreach (var detailModel in goalModel.GoalDetails)
        //        {
        //            var detail = db.GoalDetails.Find(detailModel.Id);
        //            //Kiểm tra để lưu phiên bản
        //            var isDataChanged = detail.TotalMinutes != detailModel.TotalMinutes || detail.NoEmployee != detailModel.NoEmployee;
                  
        //            detail.TotalMinutes = detailModel.TotalMinutes;
        //            //detail.Quantity = detailModel.Quantity;
        //            //detail.ProduceQuantity = detailModel.ProduceQuantity;
        //            detail.NoEmployee = detailModel.NoEmployee;
        //            //detail.ProductId = detailModel.ProductId;
        //            //detail.ReasonId = detailModel.ReasonId;
        //            //detail.Comment = detailModel.Comment;
        //            //detail.NoError = detail.NoError;
        //            if(isDataChanged)
        //                SaveDetailRevision(detail);

        //            //foreach (var phModel in detailModel.ProduceHistories)
        //            //{
        //            //    if (phModel.Id == 0)
        //            //    {
        //            //        var ph = new ProduceHistory
        //            //        {
        //            //            EmployeeId = phModel.EmployeeId,
        //            //            Quantity = phModel.Quantity
        //            //        };
        //            //        detail.ProduceHistories.Add(ph);
        //            //    }
        //            //    else
        //            //    {
        //            //        var ph = db.ProduceHistories.Find(phModel.Id);
        //            //        ph.Quantity = phModel.Quantity;
        //            //    }
        //            //}
        //        }
        //        db.SaveChanges();
        //        IMapper mapper = config.CreateMapper();
        //        return mapper.Map<GoalSessionModel>(goal);
        //    }

        //}

        [Authorize]
        [HttpPost]
        //, string starttime [FromBody]
        public List<HourGoalModel> UpdateFactory([FromBody]List<HourGoalModel> groups)
        {

            foreach (var group in groups)
            {
                    foreach (var detailModel in group.GoalDetails)
                    {
                        var detail = db.GoalDetails.Find(detailModel.Id);
                        //Kiểm tra để lưu phiên bản
                        var isDataChanged = detail.TotalMinutes != detailModel.TotalMinutes || detail.NoEmployee != detailModel.NoEmployee;


                        detail.TotalMinutes = detailModel.TotalMinutes;
                        //detail.Quantity = detailModel.Quantity;
                        //detail.ProduceQuantity = detailModel.ProduceQuantity;
                        detail.NoEmployee = detailModel.NoEmployee;
                        //detail.ProductId = detailModel.ProductId;
                        //detail.ReasonId = detailModel.ReasonId;
                        //detail.Comment = detailModel.Comment;
                        //detail.NoError = detailModel.NoError;
                        if (isDataChanged)
                            SaveDetailRevision(detail);

                    //foreach (var phModel in detailModel.ProduceHistories)
                    //{
                    //    if (phModel.Id == 0)
                    //    {
                    //        var ph = new ProduceHistory
                    //        {
                    //            EmployeeId = phModel.EmployeeId,
                    //            Quantity = phModel.Quantity
                    //        };
                    //        detail.ProduceHistories.Add(ph);
                    //    }
                    //    else
                    //    {
                    //        var ph = db.ProduceHistories.Find(phModel.Id);
                    //        ph.Quantity = phModel.Quantity;
                    //    }
                    //}
                    }
                }
                
            db.SaveChanges();
            return groups;
            }

        [Authorize]
        [HttpPost]
        public HourGoalModel UpdateFactoryHour(HourGoalModel group)
        {

            foreach (var detailModel in group.GoalDetails)
            {
                var detail = db.GoalDetails.Find(detailModel.Id);
                //Kiểm tra để lưu phiên bản
                var isDataChanged = detail.TotalMinutes != detailModel.TotalMinutes || detail.NoEmployee != detailModel.NoEmployee;


                detail.TotalMinutes = detailModel.TotalMinutes;
                detail.NoEmployee = detailModel.NoEmployee;
                //detail.Quantity = detailModel.Quantity;
                /* Khong dung nua
                detail.ProduceQuantity = detailModel.ProduceQuantity;
                detail.ManualProduceQuantity = detailModel.ManualProduceQuantity;
                detail.ProductId = detailModel.ProductId;
                detail.ReasonId = detailModel.ReasonId;
                detail.Comment = detailModel.Comment;
                detail.NoError = detailModel.NoError;
                 * */
                foreach(var pd in detailModel.ProductDetails)
                {
                    var productDetail = db.ProductDetails.Include("Reasons").Single(obj=>obj.Id == pd.Id);
                    productDetail.ProductId = pd.ProductId;
                    productDetail.Reasons = pd.Reasons == null? new List<Reason>() : pd.Reasons.Select(r => db.Reasons.Find(r.Id)).ToList();
                    productDetail.Quantity = pd.Quantity;
                    productDetail.ProduceQuantity = pd.ProduceQuantity;
                    productDetail.Comment = pd.Comment;
                }

                detailModel.UpdateChangeProQuantity();

                if (isDataChanged)
                    SaveDetailRevision(detail);
            }
            db.SaveChanges();

            group.IsChangeProQuantity = group.GoalDetails.All(gd => gd.IsChangeProQuantity == 1) ? 1 : (group.GoalDetails.All(gd => gd.IsChangeProQuantity == 0) ? 0 : 2);

            return group;
        }
        [HttpPost]
        public NewSessionModel CreateNextSession([FromBody]HourGoalModel group)
        {
            var mapper = config.CreateMapper();
            var session = CreateOrUpdateNewSession(group);
            HourGoalModel newSession = null;
            //if (group.Start.Hours < 23)
            //{
            var nextStartHour = group.End;
            var nextStartDay = group.EndDay;

            var nextEndHour = nextStartHour.Add(new TimeSpan(nextStartDay, 1, 0, 0));
            var nextEndDay = nextEndHour.Days;
                
            nextEndHour = nextEndHour.Subtract(TimeSpan.FromDays(nextEndDay));

            var nextGoalDetails = mapper.Map<List<GoalDetailModel>>(db.GoalDetails.Where(gd => gd.Goal.GoalDate == group.Date && gd.StartCountTime == nextStartHour && gd.EndCountTime == nextEndHour && gd.Goal.Team.FactoryId == group.FactoryId));

            newSession = new HourGoalModel
            {
                Date = group.Date,
                FactoryId = group.FactoryId,
                Start = nextStartHour,
                StartDay = nextStartDay,
                End = nextEndHour,
                EndDay = nextEndDay,
                StartTime = nextStartHour.Minutes == 0 ? nextStartHour.ToString("hh\\h") : nextStartHour.ToString("hh\\hmm"),
                EndTime = nextEndHour.Hours == 0 ? "24h" : nextEndHour.Minutes == 0 ? nextEndHour.ToString("hh\\h") : nextEndHour.ToString("hh\\hmm"),
                GoalDetails = nextGoalDetails.Count != 0 ? nextGoalDetails : group.GoalDetails.Select(gd => new GoalDetailModel
                {
                    GoalId = gd.GoalId,
                    StartCountTime = nextStartHour,
                    StartCountDay = nextStartDay,
                    //StartCountDay = next
                    EndCountTime = nextEndHour,
                    EndCountDay = nextEndDay,
                    TeamId = gd.TeamId,
                    TeamName = gd.TeamName,
                    TotalMinutes = 60,
                    ProductDetails = new List<ProductDetailModel> {
                            new ProductDetailModel()
                        }
                }).ToList()
            };
            //}
            return new NewSessionModel
            {
                Session = session,
                NewSession = newSession
            };
        }

        [HttpPost]
        public NewSessionModel CreatePreSession([FromBody]HourGoalModel group)
        {
            var mapper = config.CreateMapper();
            var session = CreateOrUpdateNewSession(group);
            HourGoalModel newSession = null;
            if (group.Start.Hours > 0)
            {
                var preStartHour = group.Start.Add(new TimeSpan(-1, 0, 0));
                var preEndHour = group.Start;

                var preGoalDetails = mapper.Map<List<GoalDetailModel>>(db.GoalDetails.Where(gd => gd.Goal.GoalDate == group.Date && gd.StartCountTime == preStartHour && gd.EndCountTime == preEndHour && gd.Goal.Team.FactoryId == group.FactoryId));

                newSession = new HourGoalModel
                {
                    Date = group.Date,
                    FactoryId = group.FactoryId,
                    Start = preStartHour,
                    StartDay = group.StartDay,
                    End = preEndHour,
                    EndDay = group.EndDay,
                    StartTime = preStartHour.Minutes == 0 ? preStartHour.ToString("hh\\h") : preStartHour.ToString("hh\\hmm"),
                    EndTime = preEndHour.Minutes == 0 ? preEndHour.ToString("hh\\h") : preEndHour.ToString("hh\\hmm"),
                    GoalDetails = preGoalDetails.Count >0 ? preGoalDetails : group.GoalDetails.Select(gd => new GoalDetailModel
                    {
                        GoalId = gd.GoalId,
                        StartCountTime = preStartHour,
                        StartCountDay = group.StartDay,
                        EndCountTime = preEndHour,
                        EndCountDay = group.EndDay,
                        TeamId = gd.TeamId,
                        TeamName = gd.TeamName,
                        TotalMinutes = 60,
                        ProductDetails = new List<ProductDetailModel> { 
                            new ProductDetailModel()
                        }
                    }).ToList()
                };
            }
            return new NewSessionModel
            {
                Session = session,
                NewSession = newSession
            };
        }

        
        public HourGoalModel CreateOrUpdateNewSession([FromBody]HourGoalModel group)
        {
            bool IsNoData = true;
            foreach (var detailModel in group.GoalDetails)
            {
                var goal = db.Goals.Find(detailModel.GoalId);
                if (detailModel.Id == 0) //Create new
                {
                    var detail = new GoalDetail
                    {
                        StartCountTime = detailModel.StartCountTime,
                        StartCountDay = detailModel.StartCountDay,
                        EndCountTime = detailModel.EndCountTime,
                        EndCountDay = detailModel.EndCountDay,
                        TotalMinutes = detailModel.TotalMinutes,
                        NoEmployee = detailModel.NoEmployee,
                        //ProductId = detailModel.ProductId,
                        //ReasonId = detailModel.ReasonId,
                        //Comment = detailModel.Comment,
                        //Quantity = detailModel.Quantity,
                        //ProduceQuantity = detailModel.ProduceQuantity,
                        //ManualProduceQuantity = detailModel.ManualProduceQuantity,
                        //NoError = detailModel.NoError,
                        //SessionId = detailModel.SessionId,
                        Goal = goal,
                        Revisions = new List<GoalDetailRevision>(),
                        ProductDetails = new List<ProductDetail>
                        {
                            new ProductDetail()
                        }
                    };
                    if (!detail.IsNoData())
                    {
                        IsNoData = false;
                        SaveDetailRevision(detail);
                    }

                    goal.GoalDetails.Add(detail);
                    db.SaveChanges();
                    detailModel.Id = detail.Id;
                }
                else //Edit exist
                {
                    var detail = db.GoalDetails.Find(detailModel.Id);
                    var isDataChanged = detail.TotalMinutes != detailModel.TotalMinutes || detail.NoEmployee != detailModel.NoEmployee;


                    detail.TotalMinutes = detailModel.TotalMinutes;
                    detail.NoEmployee = detailModel.NoEmployee;
                    //detail.Quantity = detailModel.Quantity;
                    //detail.ProduceQuantity = detailModel.ProduceQuantity;
                    //detail.ManualProduceQuantity = detailModel.ManualProduceQuantity;
                    //detail.ProductId = detailModel.ProductId;
                    //detail.ReasonId = detailModel.ReasonId;
                    //detail.Comment = detailModel.Comment;
                    //detail.NoError = detail.NoError;
                    detail.Hide = false;

                    if (isDataChanged)
                        SaveDetailRevision(detail);
                    
                    if (!detail.IsNoData())
                        IsNoData = false;

                    db.SaveChanges();
                }
            }
            group.IsNoData = IsNoData;
            return group;
        }

        [Authorize]
        [HttpPost]
        public HourGoalModel HideSessionFactory(HourGoalModel group)
        {
            foreach (var gd in group.GoalDetails)
            {
                
                var goaldetail = db.GoalDetails.Find(gd.Id);
                if (goaldetail != null)
                {
                    //gd.Hide = true;
                    goaldetail.Hide = true;
                }
            }

            db.SaveChanges();
            return group;
        }

        public void SaveDetailRevision(GoalDetail detail)
        {
            var revision = new GoalDetailRevision
            {
                DateTime = DateTime.Now,
                TotalMinutes = detail.TotalMinutes,
                //Comment = detail.Comment,
                NoEmployee = detail.NoEmployee,
                //ProduceQuantity = detail.ProduceQuantity,
                //Quantity = detail.Quantity,
                //ReasonId = detail.ReasonId,
                UserId = User.Identity.GetUserId(),
                //ProductId = detail.ProductId
            };
            detail.Revisions.Add(revision);
        }

    }
}

