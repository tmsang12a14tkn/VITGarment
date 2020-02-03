using Data.DataAccessLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Garment.Web.Controllers
{
    public class AnalyticsController : Controller
    {
        private GarmentContext db = new GarmentContext();
        // GET: Analytics
        public ActionResult Chart()
        {
            var model = db.Factories.ToList();
            return View(model);
        }
        public ActionResult TeamChart(int id)
        {
            var team = db.Teams.Find(id);
            return View(team);
        }
    }
}