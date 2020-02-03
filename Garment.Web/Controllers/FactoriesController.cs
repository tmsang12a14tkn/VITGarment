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
using Data.ViewModels;

namespace Garment.Web.Controllers
{
    [Authorize]
    public class FactoriesController : Controller
    {
        private GarmentContext db = new GarmentContext();

        // GET: Factories
        public ActionResult Index()
        {
            return View(db.Factories.ToList());
        }

        // GET: Factories/Details/5
        public ActionResult Details(int? id, DateTime? date)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            if (date == null)
                date = DateTime.Now.Date;
            //Factory factory = db.Factories.Find(id);
            //if (factory == null)
            //{
            //    return HttpNotFound();
            //}
            ViewBag.date = date.Value;
            return View(id);
        }

        // GET: Factories/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Factories/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(HttpPostedFileBase profileFile,[Bind(Include = "Id,Name,ShortDescription,FullDescription")] Factory factory)
        {
            if (profileFile != null && profileFile.ContentLength > 0)
            {
                string fileName = Convert.ToInt32((DateTime.Now - new DateTime(2010, 01, 01)).TotalSeconds) + "_" + profileFile.FileName;
                string folder = "uploads/factorypicture";
                //string filePath = System.Configuration.ConfigurationManager.AppSettings[currentDomain] + @"\" + folder + @"\" + fileName;
                string filePath = System.IO.Path.Combine(Server.MapPath(@"~/" + folder), fileName);
                profileFile.SaveAs(filePath);

                factory.Picture = "/" + folder + "/" + fileName;
            }
            else
            {
                factory.Picture = "/Content/assets/xn1.jpg";
            }
            ModelState.Clear();
            TryValidateModel(factory);
            if (ModelState.IsValid)
            {
                db.Factories.Add(factory);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(factory);
        }

        // GET: Factories/Edit/5
        public ActionResult Edit(int? id, string tab = "info")
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Factory factory = db.Factories.Find(id);
            if (factory == null)
            {
                return HttpNotFound();
            }
            ViewBag.tab = tab;
            return View(factory);
        }

        // POST: Factories/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(HttpPostedFileBase profileFile, [Bind(Include = "Id,Name,ShortDescription,FullDescription")] Factory factory)
        {
            if (profileFile != null && profileFile.ContentLength > 0)
            {
                string fileName = Convert.ToInt32((DateTime.Now - new DateTime(2010, 01, 01)).TotalSeconds) + "_" + profileFile.FileName;
                string folder = "uploads/factorypicture";
                string filePath = System.IO.Path.Combine(Server.MapPath(@"~/" + folder), fileName);
                profileFile.SaveAs(filePath);

                factory.Picture = "/" + folder + "/" + fileName;
            }
            else if (String.IsNullOrEmpty(factory.Picture))
            {
                factory.Picture = "/Content/assets/xn1.jpg";
            }

            ModelState.Clear();
            TryValidateModel(factory);
            if (ModelState.IsValid)
            {
                db.Entry(factory).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(factory);
        }

        // GET: Factories/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Factory factory = db.Factories.Find(id);
            if (factory == null)
            {
                return HttpNotFound();
            }
            return PartialView(factory);
        }

        // POST: Factories/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id, string confirmText)
        {
            Factory factory = db.Factories.Find(id);
            var success = false;
            string error = "";
            if (factory == null)
            {
                error = "Không tìm thấy video";
            }
            else if (confirmText.ToLower() != "đồng ý")
            {
                error = "Chuỗi nhập vào chưa đúng";
            }
            else
            {
                if (factory.Picture != "/Content/assets/xn1.jpg")
                {
                    string filePath = Server.MapPath(factory.Picture);
                    if (System.IO.File.Exists(filePath))
                    {
                        System.IO.File.Delete(filePath);
                    }
                    
                }

                db.Factories.Remove(factory);
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
