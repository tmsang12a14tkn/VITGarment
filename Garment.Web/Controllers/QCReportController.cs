using Data.DataAccessLayer;
using Data.Models;
using Data.ViewModels;
using Garment.Web.Common;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Garment.Web.Controllers
{
    public class QCReportController : Controller
    {
        private GarmentContext db = new GarmentContext();
        // GET: QCReport
        public ActionResult _QCReportPreview(int qcId)
        {
            var qc = db.QCs.Find(qcId);
            return PartialView("_QCReportPreview", qc);
        }

        [Authorize]
        public ActionResult FactoryCreate(int factoryId, DateTime? date)
        {
            var factory = db.Factories.Find(factoryId);
            if (date == null) date = DateTime.Now.Date;
            ViewBag.Date = date;

            return View(factory);
        }
        public ActionResult _QCReportListForTeam(int teamId, DateTime date)
        {
            var sessionQCReports = new List<SessionQCReportView>();
            var qcReports = db.QCReports.Where(qcr => qcr.TeamId == teamId && qcr.Date == date).ToList();
            var qcTeams = db.QCTeams.Where(qct => qct.TeamId == teamId && qct.From <= date && (qct.To == null || qct.To.Value >= date)).ToList();

            var goal = db.Goals.FirstOrDefault(g => g.TeamId == teamId && g.GoalDate == date);
            var sessionHours = db.GoalDetails.Where(gd => gd.GoalId == goal.Id).GroupBy(gd => gd.SessionOrder);
            //kiem tra, them qc chua dc tao
            foreach (var sessionHour in sessionHours)
            {

                var sessionOrder = sessionHour.Key;
                var sessionQCReport = new SessionQCReportView()
                {
                    SessionOrder = sessionOrder,
                    ProductQCReports = new List<ProductQCReportView>()
                };
                var products = sessionHour.SelectMany(h => h.ProductDetails).Select(p => p.ProductId).Distinct();
                foreach(var productId in products)
                {
                    var productQCReport = new ProductQCReportView
                    {
                        ProductId = productId,
                        QCReports = new List<QCReport>()
                    };
                    foreach (var qcTeam in qcTeams)
                    {
                        var qcReport = qcReports.FirstOrDefault(qcr => qcr.SessionOrder == sessionOrder && qcr.ProductId == productId && qcr.QCId == qcTeam.QCId);
                        if (qcReport == null)
                        {
                            qcReport = new QCReport
                            {
                                QCId = qcTeam.QCId,
                                QC = qcTeam.QC,
                                TeamId = qcTeam.TeamId,
                                Date = date,
                                SessionOrder = sessionOrder,
                                ProductId = productId,
                                IsEmpty = true
                            };
                        }
                        productQCReport.QCReports.Add(qcReport);
                    }
                    sessionQCReport.ProductQCReports.Add(productQCReport);
                }

                sessionQCReports.Add(sessionQCReport);
            }

            //ViewBag.teamId = teamId;
            var model = new TeamQCReportView
            {
                TeamId = teamId,
                TeamName = db.Teams.Find(teamId).Name,
                SessionQCReports = sessionQCReports
            };
            return PartialView(model);
        }

        public ActionResult _ChartForTeam(int teamId, DateTime date)
        {
            var qcReports = db.QCReports.Where(qcr => qcr.TeamId == teamId && qcr.Date == date).ToList();

            ViewBag.teamId = teamId;
            return PartialView(qcReports);
        }

        public ActionResult _Details(int teamId, DateTime date, int order, string productId, int qcId)
        {
            var qcReport = db.QCReports.Find(teamId, date, order, productId, qcId);
            if (qcReport == null)
            {
                qcReport = new QCReport
                {
                    QCId = qcId,
                    QC = db.QCs.Find(qcId),
                    Date = date,
                    TeamId = teamId,
                    ProductId = productId,
                    SessionOrder = order,
                    Team = db.Teams.Find(teamId)
                };
            }
            return PartialView(qcReport);
        }

        public ActionResult _Edit(int teamId, DateTime date, int order, string productId, int qcId)
        {
            var qcReport = db.QCReports.Find(teamId, date, order, productId, qcId);
            if(qcReport == null)
            {
                qcReport = new QCReport
                {
                    QCId = qcId,
                    QC = db.QCs.Find(qcId),
                    Date = date,
                    TeamId = teamId,
                    Team = db.Teams.Find(teamId)
                };
            }
            return PartialView(qcReport);
        }
        [HttpPost]
        public ActionResult _Edit(QCReport qcReport)
        {
            if (qcReport.Errors != null)
            {
                foreach (var error in qcReport.Errors.ToList())
                {
                    if (!Utilities.IsNullOrDefault(error) && !error.IsDeleted)
                    {
                        db.Entry(error).State = error.Id == 0 ? EntityState.Added : EntityState.Modified;
                    }
                    else
                    {
                        qcReport.Errors.Remove(error);
                        if(error.Id!=0)
                            db.Entry(error).State = EntityState.Deleted;
                    }
                }
            }

            if (qcReport.Specifications != null)
            {
                foreach (var spec in qcReport.Specifications.ToList())
                {
                    //Xử lý SpecDetails
                    if (spec.QCSpecDetails != null)
                    {
                        foreach (var specDetail in spec.QCSpecDetails.ToList())
                        {
                            specDetail.QCSpecificationId = spec.Id;
                            if (!Utilities.IsNullOrDefault(specDetail) && !specDetail.IsDeleted)
                            {
                                db.Entry(specDetail).State = specDetail.Id == 0 ? EntityState.Added : EntityState.Modified;
                            }
                            else
                            {
                                spec.QCSpecDetails.Remove(specDetail);
                                if (specDetail.Id != 0)
                                    db.Entry(specDetail).State = EntityState.Deleted;
                            }
                        }
                    }

                    if (!spec.IsDeleted)
                    {
                        db.Entry(spec).State = spec.Id == 0 ? EntityState.Added : EntityState.Modified;
                    }
                    else
                    {
                        qcReport.Specifications.Remove(spec);
                        if (spec.Id != 0)
                            db.Entry(spec).State = EntityState.Deleted;
                    }
                }
            }

            if (db.QCReports.Any(qcr=>qcr.TeamId == qcReport.TeamId && qcr.Date == qcReport.Date && qcr.QCId == qcReport.QCId))
            {
                db.Entry(qcReport).State = EntityState.Modified;
            }
            else
            {
                db.Entry(qcReport).State = EntityState.Added;
            }
            
            db.SaveChanges();
            return Json(new { success = true, teamId = qcReport.TeamId, date = qcReport.Date.ToString("dd/MM/yyyy") });
        }
    }
}