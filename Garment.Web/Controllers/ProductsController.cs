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

namespace Garment.Web.Controllers
{
    public class ProductsController : Controller
    {
        private GarmentContext db = new GarmentContext();

        // GET: Products
        public ActionResult Index()
        {
            return View(db.Products.ToList());
        }

        // GET: Products/Details/5
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Product product = db.Products.Find(id);
            if (product == null)
            {
                return HttpNotFound();
            }
            var productDetails = new ProductDetailsModel();
            //var productGoalDetails = db.GoalDetails.Where(gd => gd.ProductId == product.ProductId).ToList();
            productDetails.ProductInfo = new ProductInfo
            {
                Duration = product.Duration,
                Gender = product.Gender == 0 ? "Nam" : "Nữ",
                Lot = product.Lot,
                Name = product.Name,
                ProductId = product.ProductId,
                Quantity = product.Quantity,
                ProductType = product.ProductType.Name,
                //ProduceQuantity = productGoalDetails.Sum(d => d.ProduceQuantity)
            };
            //productDetails.ProductDateViews = productGoalDetails.GroupBy(gd => gd.Goal.GoalDate).Select(g =>
            //  new ProductDateView
            //  {
            //      Date = g.Key,
            //      ProduceQuantity = g.Sum(gd=>gd.ProduceQuantity),
            //      Quantity = g.Sum(gd => gd.Quantity),
            //      ProductGoalViews = g.GroupBy(gd => gd.Goal.Team).Select(g2 => new ProductGoalView
            //      {
            //          TeamName = g2.Key.Name,
            //          Quantity = g2.Sum(gd=>gd.Quantity),
            //          ProduceQuantity = g2.Sum(gd=>gd.ProduceQuantity)
            //      }
                  
            //      ).ToList()
            //}).ToList();
            return View(productDetails);
        }

        // GET: Products/Create
        public ActionResult Create()
        {
            ViewBag.ProductTypeId = new SelectList(db.ProductTypes, "Id", "Name");
            return View();
        }

        // POST: Products/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Product product)
        {
            if (ModelState.IsValid)
            {
                db.Products.Add(product);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.ProductTypeId = new SelectList(db.ProductTypes, "Id", "Name", product.ProductTypeId);
            return View(product);
        }

        // GET: Products/Edit/5
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Product product = db.Products.Where(p => p.ProductId == id).FirstOrDefault();
            if (product == null)
            {
                return HttpNotFound();
            }
            ViewBag.ProductTypeId = new SelectList(db.ProductTypes, "Id", "Name", product.ProductTypeId);
            return View(product);
        }

        // POST: Products/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Product product)
        {
            if (ModelState.IsValid)
            {
                db.Entry(product).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.ProductTypeId = new SelectList(db.ProductTypes, "Id", "Name", product.ProductTypeId);
            return View(product);
        }

        // GET: Products/Delete/5
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Product product = db.Products.Where(p => p.ProductId == id).FirstOrDefault();
            if (product == null)
            {
                return HttpNotFound();
            }
            return PartialView(product);
        }

        // POST: Products/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id, string confirmText)
        {
            Product product = db.Products.Where(p => p.ProductId == id).FirstOrDefault();
            var success = false;
            string error = "";
            if (product == null)
            {
                error = "Không tìm thấy video";
            }
            else if (confirmText.ToLower() != "đồng ý")
            {
                error = "Chuỗi nhập vào chưa đúng";
            }
            else
            {
                db.Products.Remove(product);
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
