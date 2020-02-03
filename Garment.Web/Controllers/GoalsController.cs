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
using Microsoft.AspNet.Identity;
using Garment.Web.Helpers;
using Data.ViewModels;

namespace Garment.Web.Controllers
{
    public class GoalsController : Controller
    {
        private GarmentContext db = new GarmentContext();

        [Authorize]
        // GET: Goals
        public ActionResult Index(DateTime? date)
        {
            var now = DateTime.Now;
            var filterView = new GoalFilterView();
            
            if (!date.HasValue)
            {
                date = now;
                filterView.SelectDate = now;
            }
            var begin = date.Value.StartOfWeek(DayOfWeek.Monday);
            var end = begin.AddDays(6);

            var goals = db.Goals.Where(g => g.GoalDate >= begin && g.GoalDate <= end).GroupBy(g => new { g.GoalDate, g.Team.Factory })
                                .Select(gr => new GoalTabFilterView
                                {
                                    Factory = gr.Key.Factory,
                                    GoalDate = gr.Key.GoalDate,
                                    Goals = gr.ToList()
                                });

            filterView.Begin = begin;
            filterView.End = end;
            filterView.GoalTabFilterViews = goals.ToList();
            ViewBag.factories = db.Factories;
            return View(filterView);
        }

        //public ActionResult GetGoalDetailQuantity(int goalid)
        //{
        //    List<GoalDetailFilterView> goalDetails = db.GoalDetails.Where(d => d.GoalId == goalid).GroupBy(g => g.ProductId)
        //                                .Select(gr => new GoalDetailFilterView { 
        //                                    Quantity = gr.Sum(q => q.Quantity),
        //                                    ProductQuantity = gr.Sum(p => p.ProduceQuantity),
        //                                    ProductId = gr.Key
        //                                }).ToList();

        //    return PartialView("_GetGoalDetailQuantity", goalDetails);
        //}

        // GET: Goals/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Goal goal = db.Goals.Find(id);
            if (goal == null)
            {
                return HttpNotFound();
            }
            return View(goal);
        }
        [Route("nhap")]
        [Authorize]
        // GET: Goals/Create
        public ActionResult Create(int teamId, DateTime? date)
        {
            //ViewBag.TeamId = new SelectList(db.Teams, "Id", "Name");
            //var userId = User.Identity.GetUserId();
            //var user = db.Users.Find(userId);
            if(!date.HasValue)
                date = DateTime.Now.Date;
            var goal = db.Goals.FirstOrDefault(g => g.GoalDate == date.Value && g.TeamId == teamId);
            var team = db.Teams.Find(teamId);
            if (goal == null)
            {
                goal = new Goal
                {
                    GoalDate = date.Value,
                    TeamId = teamId,
                    Team = team,
                };
                //var employees = db.Employees.Where(e => e.TeamId == 1).ToList();
                //foreach(var template in db.GoalDetailTemplates.ToList())
                //{
                //    var goalDetail = new GoalDetail
                //    {
                //        StartCountTime = template.StartCountTime,
                //        EndCountTime = template.EndCountTime,
                //        Goal = goal
                //    };
                //    foreach(var employee in employees)
                //    {
                //        var produceHistory = new ProduceHistory
                //        {
                //            EmployeeId = employee.Id,
                //            GoalDetail = goalDetail
                //        };
                //    }
                //}
                //db.Goals.Add(goal);
                db.SaveChanges();
            }

            return View(goal);
        }

        [Authorize]
        public ActionResult FactoryCreate(int factoryId, DateTime? date)
        {
            var factory = db.Factories.Find(factoryId);
            if (date == null) date = DateTime.Now.Date;
            ViewBag.Date = date;

            return View(factory);
        }

        // POST: Goals/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,GoalDate,StartHour,EndHour,TeamId,ProductId")] Goal goal)
        {
            if (ModelState.IsValid)
            {
                db.Goals.Add(goal);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(goal);
        }

        // GET: Goals/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Goal goal = db.Goals.Find(id);
            if (goal == null)
            {
                return HttpNotFound();
            }
            return View(goal);
        }

        // POST: Goals/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,GoalDate,StartHour,EndHour,TeamId,ProductId")] Goal goal)
        {
            if (ModelState.IsValid)
            {
                db.Entry(goal).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(goal);
        }

        // GET: Goals/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Goal goal = db.Goals.Find(id);
            if (goal == null)
            {
                return HttpNotFound();
            }
            return View(goal);
        }

        // POST: Goals/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Goal goal = db.Goals.Find(id);
            db.Goals.Remove(goal);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult Admin(int teamId)
        {
            var team = db.Teams.Find(teamId);
            var dateNow = DateTime.Now.Date;
            var goals = db.Goals.Where(g=>g.TeamId == teamId && g.GoalDate <= dateNow).OrderByDescending(g=>g.GoalDate).Take(30).ToList();
            ViewBag.TeamName = team.Name;
            return View(goals);
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
