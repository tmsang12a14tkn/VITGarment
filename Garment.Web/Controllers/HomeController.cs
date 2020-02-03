using Data.DataAccessLayer;
using Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Garment.Web.Controllers
{
    public class HomeController : Controller
    {
        private GarmentContext db = new GarmentContext();

        
        public ActionResult Index()
        {
            
            return View();
        }

        [Route("man-hinh/{teamId}")]
        public ActionResult DailyGoal(int teamId = 1)
        {
            Team team = db.Teams.Find(teamId);
            return View(team);
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        [Authorize]
        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}