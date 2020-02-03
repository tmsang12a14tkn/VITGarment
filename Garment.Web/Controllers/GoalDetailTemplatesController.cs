using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Data.Models;
using Data.DataAccessLayer;
using Data.ViewModels;

namespace Garment.Web.Controllers
{
    public class GoalDetailTemplatesController : Controller
    {
        private GarmentContext db = new GarmentContext();

        // GET: GoalDetailTemplates
        public ActionResult Index(DayOfWeek? dayOfWeek)
        {
            DayOfWeek dayOfWeekModel = dayOfWeek.HasValue ? dayOfWeek.Value : DayOfWeek.Monday;
            return View(dayOfWeekModel);
        }

        public PartialViewResult DowList(DayOfWeek dayOfWeek) //
        {

            var goaldetailTemplates = db.GoalDetailTemplates.Where(p => p.DayOfWeek == dayOfWeek).OrderBy(p => p.StartCountTime).ToList();
            var view = new DowScheduleView
            {
                GoalDetailTemplates = goaldetailTemplates,
                DayOfWeek = dayOfWeek
            };
            return PartialView(view);
        }

        // GET: GoalDetailTemplates/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            GoalDetailTemplate goalDetailTemplate = db.GoalDetailTemplates.Find(id);
            if (goalDetailTemplate == null)
            {
                return HttpNotFound();
            }
            return View(goalDetailTemplate);
        }

        // GET: GoalDetailTemplates/Create
        public ActionResult Create(DayOfWeek dayOfWeek)
        {
            return View(new GoalDetailTemplate { DayOfWeek = dayOfWeek});
        }

        // POST: GoalDetailTemplates/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,StartCountTime,EndCountTime,DayOfWeek,SessionId, Minute")] GoalDetailTemplate goalDetailTemplate)
        {
            if (ModelState.IsValid)
            {
                db.GoalDetailTemplates.Add(goalDetailTemplate);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(goalDetailTemplate);
        }

        // GET: GoalDetailTemplates/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            GoalDetailTemplate goalDetailTemplate = db.GoalDetailTemplates.Find(id);
            if (goalDetailTemplate == null)
            {
                return HttpNotFound();
            }
            return View(goalDetailTemplate);
        }

        // POST: GoalDetailTemplates/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,StartCountTime,EndCountTime,DayOfWeek,SessionId, Minute")] GoalDetailTemplate goalDetailTemplate)
        {
            if (ModelState.IsValid)
            {
                db.Entry(goalDetailTemplate).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(goalDetailTemplate);
        }

        // GET: GoalDetailTemplates/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            GoalDetailTemplate goalDetailTemplate = db.GoalDetailTemplates.Find(id);
            if (goalDetailTemplate == null)
            {
                return HttpNotFound();
            }
            return PartialView(goalDetailTemplate);
        }

        // POST: GoalDetailTemplates/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id, string confirmText)
        {
            var error = "";
            if (confirmText.ToLower() == "đồng ý")
            {
                GoalDetailTemplate goalDetailTemplate = db.GoalDetailTemplates.Find(id);
                db.GoalDetailTemplates.Remove(goalDetailTemplate);
                db.SaveChanges();
                return Json(new { success = 1, id = id });
            }
            else
            {
                error = "Chuỗi nhập vào không đúng";
            }
            return Json(new { success = 0, content = error });
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
