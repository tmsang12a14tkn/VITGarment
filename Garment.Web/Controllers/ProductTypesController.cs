using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Data.DataAccessLayer;
using Data.Models;

namespace Garment.Web.Controllers
{
    public class ProductTypesController : Controller
    {
        private GarmentContext db = new GarmentContext();

        // GET: ProductTypes
        public ActionResult Index()
        {
            return View(db.ProductTypes.ToList());
        }

        // GET: ProductTypes/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ProductType productType = db.ProductTypes.Find(id);
            if (productType == null)
            {
                return HttpNotFound();
            }
            return View(productType);
        }

        // GET: ProductTypes/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: ProductTypes/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Name")] ProductType productType)
        {
            if (ModelState.IsValid)
            {
                db.ProductTypes.Add(productType);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(productType);
        }

        // GET: ProductTypes/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ProductType productType = db.ProductTypes.Find(id);
            if (productType == null)
            {
                return HttpNotFound();
            }
            return View(productType);
        }

        // POST: ProductTypes/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Name")] ProductType productType)
        {
            if (ModelState.IsValid)
            {
                db.Entry(productType).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(productType);
        }

        // GET: ProductTypes/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ProductType productType = db.ProductTypes.Find(id);
            if (productType == null)
            {
                return HttpNotFound();
            }
            return PartialView(productType);
        }

        // POST: ProductTypes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id, string confirmText)
        {
            ProductType productType = db.ProductTypes.Find(id);
            var success = false;
            string error = "";
            if (productType == null)
            {
                error = "Không tìm thấy video";
            }
            else if (confirmText.ToLower() != "đồng ý")
            {
                error = "Chuỗi nhập vào chưa đúng";
            }
            else
            {
                db.ProductTypes.Remove(productType);
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
