using Data.DataAccessLayer;
using Data.Models;
using Data.ViewModels;
using System;
using System.Data.Entity;
using System.Linq;
using System.Web.Mvc;

namespace Garment.Web.Controllers
{
    public class QCTeamController : Controller
    {
        private GarmentContext db = new GarmentContext();
        // GET: QCTeam
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult _QCTeamList(int teamId)
        {
            ViewBag.teamId = teamId;
            var qcTeams = db.QCTeams.Where(qct => qct.TeamId == teamId).ToList();
            return PartialView("_QCTeamList", qcTeams);
        }
        //[Route("QCTeam/Create/{teamId:int}")]
        public ActionResult _Create(int teamId)
        {
            ViewBag.qcs = db.QCs.Where(qc => qc.IsDeleted == false && qc.Visible == true).ToList();
            return PartialView("_Create", new QCTeamModel { TeamId = teamId, From = DateTime.Now.Date});
        }
        [HttpPost]
        public ActionResult _CreateOrEdit(QCTeamModel qcTeamModel)
        {
            if(ModelState.IsValid)
            {
                if (qcTeamModel.QCId == 0)
                {
                    return Json(new { success = false, id = qcTeamModel.TeamId, error = "Chưa chọn QC" });
                }
                if (qcTeamModel.ApplyAll)
                {
                    var factoryId = db.Teams.Find(qcTeamModel.TeamId).FactoryId;
                    var teams = db.Teams.Where(team => team.FactoryId == factoryId).ToList();
                    foreach(var team in teams)
                    {
                        var qcTeam = new QCTeam
                        {
                            QCId = qcTeamModel.QCId,
                            TeamId = team.Id,
                            From = qcTeamModel.From,
                            To = qcTeamModel.To
                        };
                        if (!db.QCTeams.Any(qct => qct.TeamId ==team.Id && qct.QCId == qcTeamModel.QCId))
                            db.QCTeams.Add(qcTeam);
                        else
                            db.Entry(qcTeam).State = EntityState.Modified;
                    }
                }
                else
                {
                    var qcTeam = new QCTeam
                    {
                        QCId = qcTeamModel.QCId,
                        TeamId = qcTeamModel.TeamId,
                        From = qcTeamModel.From,
                        To = qcTeamModel.To
                    };
                    if (!db.QCTeams.Any(qct=>qct.TeamId == qcTeamModel.TeamId && qct.QCId == qcTeamModel.QCId))
                        db.QCTeams.Add(qcTeam);
                    else
                        db.Entry(qcTeam).State = EntityState.Modified;
                }
                db.SaveChanges();
                return Json(new { success = true , id = qcTeamModel.TeamId, applyAll = qcTeamModel.ApplyAll});
            }
            return Json(new { success = false });
        }
        public ActionResult _Edit(int teamId, int qcId)
        {
            var qcTeam = db.QCTeams.Find(teamId, qcId);
            var qcTeamModel = new QCTeamModel
            {
                QCId = qcTeam.QCId,
                TeamId = qcTeam.TeamId,
                From = qcTeam.From,
                To = qcTeam.To
            };
            return PartialView("_Edit", qcTeamModel);
        }
        public ActionResult _Delete(int teamId, int qcId)
        {
            var qcTeam = db.QCTeams.Find(teamId, qcId);
           
            return PartialView("_Delete", qcTeam);
        }
        [HttpPost]
        public ActionResult _DeleteConfirm(int teamId, int qcId)
        {
            var qcTeam = db.QCTeams.Find(teamId, qcId);
            db.Entry(qcTeam).State = EntityState.Deleted;
            db.SaveChanges();

            return Json(new { success = true, id = teamId});
        }


    }
}