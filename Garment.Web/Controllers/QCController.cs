using Data.DataAccessLayer;
using Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Garment.Web.Controllers
{
    [Authorize]
    public class QCController : Controller
    {
        private GarmentContext db = new GarmentContext();
        // GET: QC
        public ActionResult Index()
        {
            return View(db.QCs.Where(qc=>qc.IsDeleted == false).ToList());
        }

        public ActionResult _Create()
        {
            return PartialView("_Create");
        }
        [HttpPost]
        public ActionResult _Create(QC qc)
        {
            if (ModelState.IsValid)
            {
                db.QCs.Add(qc);
                qc.Visible = true;
                db.SaveChanges();
                return _Details(qc.Id);
            }
            else
                return PartialView(qc);
        }

        public ActionResult _Details(int id)
        {
            return PartialView("_Details", db.QCs.Find(id));
        }
        public ActionResult _Edit(int id)
        {

            return PartialView("_Edit", db.QCs.Find(id));
        }
        [HttpPost]
        public ActionResult _Edit(QC qc)
        {
            if (ModelState.IsValid)
            {
                db.Entry(qc).State = System.Data.Entity.EntityState.Modified;
                db.SaveChanges();
                return _Details(qc.Id);
            }
            else
                return PartialView(qc);
        }

        public ActionResult _Delete(int? id)
        {
            return PartialView("_Delete", db.QCs.Find(id));
        }
        [HttpPost]
        public ActionResult _Delete(int id)
        {
            var qc = db.QCs.Find(id);
            if (qc != null)
            {
                qc.IsDeleted = true;
                db.Entry(qc).State = System.Data.Entity.EntityState.Modified;
                db.SaveChanges();
                return Json(new { success = true, id = id });
            }
            return Json(new { success = false });
        }
        [HttpPost]
        public ActionResult ChangeVisibleStatus(int id)
        {
            var qc = db.QCs.Find(id);
            if (qc != null)
            {
                qc.Visible = !qc.Visible;
                db.Entry(qc).State = System.Data.Entity.EntityState.Modified;
                db.SaveChanges();
            }
            return _Details(qc.Id);
        }
    }

}