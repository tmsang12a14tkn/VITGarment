using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Garment.Web.Controllers
{
    public class ReportController : Controller
    {
        // GET: Report
        public ActionResult ProductByTeam(DateTime begin, DateTime end)
        {
            return View();
        }
    }
}