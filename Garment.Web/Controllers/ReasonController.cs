using Data.DataAccessLayer;
using Data.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace Garment.Web.Controllers
{
    public class ReasonController : Controller
    {
        private GarmentContext db = new GarmentContext();

        // GET: Products
        public ActionResult Index()
        {
            return View(db.Reasons.ToList());
        }

        // GET: Products/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Products/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Name, Order, ColorCode, ErrorCode")] Reason item)
        {
            if (ModelState.IsValid)
            {
                db.Reasons.Add(item);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(item);
        }

        // GET: Products/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Reason item = db.Reasons.Find(id);
            if (item == null)
            {
                return HttpNotFound();
            }
            return View(item);
        }

        // POST: Products/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Name, Order, ColorCode, ErrorCode")] Reason item)
        {
            if (ModelState.IsValid)
            {
                db.Entry(item).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(item);
        }

        // GET: Products/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Reason item = db.Reasons.Find(id);
            if (item == null)
            {
                return HttpNotFound();
            }
            return PartialView(item);
        }

        // POST: Products/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id, string confirmText)
        {
            Reason item = db.Reasons.Find(id);
            var success = false;
            string error = "";
            if (item == null)
            {
                error = "Không tìm thấy video";
            }
            else if (confirmText.ToLower() != "đồng ý")
            {
                error = "Chuỗi nhập vào chưa đúng";
            }
            else
            {
                db.Reasons.Remove(item);
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