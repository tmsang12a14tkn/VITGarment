using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Data.Models;
using Garment.Web.Models;
using Data.DataAccessLayer;
using Data.ViewModels;

namespace Garment.Web.Controllers
{
    public class TeamsController : Controller
    {
        private GarmentContext db = new GarmentContext();

        // GET: Teams
        public ActionResult Index(int factoryid = 0)
        {
            ViewBag.factories = db.Factories.ToList();
            ViewBag.factoryid = factoryid;
            return View(factoryid != 0 ? db.Teams.Where(t => t.FactoryId == factoryid).ToList() : db.Teams.OrderBy(t => t.Factory.Name).ToList());
        }

        // GET: Teams/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Team team = db.Teams.Find(id);
            if (team == null)
            {
                return HttpNotFound();
            }
            return View(team);
        }

        // GET: Teams/Create
        public ActionResult Create()
        {
            ViewBag.FactoryId = new SelectList(db.Factories, "Id", "Name");
            return View();
        }

        // POST: Teams/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Name,FactoryId,NoEmployee,Order")] TeamModel team)
        {
            if (ModelState.IsValid)
            {
                Team t = new Team { 
                    Name = team.Name,
                    FactoryId = team.FactoryId,
                    NoEmployee = team.NoEmployee,
                    Order = team.Order
                };
                db.Teams.Add(t);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.FactoryId = new SelectList(db.Factories, "Id", "Name", team.FactoryId);
            return View(team);
        }

        // GET: Teams/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Team team = db.Teams.Find(id);
            if (team == null)
            {
                return HttpNotFound();
            }
            ViewBag.FactoryId = new SelectList(db.Factories, "Id", "Name", team.FactoryId);
            return View(new TeamModel { Id = team.Id, Name = team.Name, NoEmployee = team.NoEmployee, FactoryId = team.FactoryId, Order = team.Order});
        }

        // POST: Teams/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Name,FactoryId,NoEmployee,Order")] TeamModel team)
        {
            if (ModelState.IsValid)
            {
                Team t = new Team
                {
                    Id = team.Id,
                    Name = team.Name,
                    FactoryId = team.FactoryId,
                    NoEmployee = team.NoEmployee,
                    Order = team.Order
                };
                db.Entry(t).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.FactoryId = new SelectList(db.Factories, "Id", "Name", team.FactoryId);
            return View(team);
        }

        // GET: Teams/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Team team = db.Teams.Find(id);
            if (team == null)
            {
                return HttpNotFound();
            }
            return PartialView(team);
        }

        // POST: Teams/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id, string confirmText)
        {
            Team team = db.Teams.Find(id);
            var success = false;
            string error = "";
            if (team == null)
            {
                error = "Không tìm thấy video";
            }
            else if (confirmText.ToLower() != "đồng ý")
            {
                error = "Chuỗi nhập vào chưa đúng";
            }
            else
            {
                db.Goals.RemoveRange(team.Goals);
                db.Teams.Remove(team);
                db.SaveChanges();
                success = true;
            }

            return Json(new { success = success, id = id, error = error });
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
