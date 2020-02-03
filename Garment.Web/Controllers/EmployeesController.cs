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
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace Garment.Web.Controllers
{
    public class EmployeesController : Controller
    {
        private GarmentContext db = new GarmentContext();

        // GET: Employees
        public ActionResult Index(int teamId = 0)
        {
            var employees = teamId == 0 ? db.Employees.Include(e => e.Team): db.Employees.Where(e=>e.TeamId == teamId);
            ViewBag.Teams = new SelectList(db.Teams, "Id", "Name", teamId);
            ViewBag.users = db.Users.ToList();
            return View(employees.ToList());
        }

        // GET: Employees/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Employee employee = db.Employees.Find(id);
            if (employee == null)
            {
                return HttpNotFound();
            }
            return View(employee);
        }

        // GET: Employees/Create
        public ActionResult Create()
        {
            ViewBag.TeamId = new SelectList(db.Teams, "Id", "Name");
            //ViewBag.PossibleRoles = db.Roles.OrderByDescending(r => r.Name).ToList();
            return View();
        }

        // POST: Employees/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(EmployeeRegisterModel empreg)
        {
            if (ModelState.IsValid)
            {
                Employee emp = new Employee();
                emp.Name = empreg.Name;
                emp.TeamId = empreg.TeamId;
                emp = db.Employees.Add(emp);
                db.SaveChanges();

                if(!string.IsNullOrEmpty(empreg.Username))
                {
                    if (!db.Users.Any(u => u.UserName == empreg.Username))
                    {
                        var hasher = new PasswordHasher();

                        var user = new ApplicationUser
                        {
                            EmployeeId = emp.Id,
                            UserName = empreg.Username,
                            PasswordHash = hasher.HashPassword(empreg.Password),
                            Email = "",
                            EmailConfirmed = true,
                            SecurityStamp = Guid.NewGuid().ToString()
                        };

                        //foreach (var roleid in empreg.Roles)
                        //{
                        //    user.Roles.Add(new IdentityUserRole { RoleId = roleid, UserId = user.Id });
                        //}

                        db.Users.Add(user);
                        db.SaveChanges();
                    }
                }
                return RedirectToAction("Index");
            }

            ViewBag.TeamId = new SelectList(db.Teams, "Id", "Name", empreg.TeamId);
            //ViewBag.PossibleRoles = db.Roles.OrderByDescending(r => r.Name).ToList();
            return View(empreg);
        }

        // GET: Employees/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Employee employee = db.Employees.Find(id);
            if (employee == null)
            {
                return HttpNotFound();
            }

            ApplicationUser user = db.Users.Where(u => u.EmployeeId == employee.Id).FirstOrDefault();

            EmployeeRegisterModel empreg = new EmployeeRegisterModel();
            empreg.haveAccount = user != null;
            empreg.Id = employee.Id;
            empreg.Name = employee.Name;
            empreg.TeamId = employee.TeamId;
            empreg.Username = user != null ? user.UserName : "";
            ViewBag.TeamId = new SelectList(db.Teams, "Id", "Name", employee.TeamId);
            //ViewBag.PossibleRoles = db.Roles.OrderByDescending(r => r.Name).ToList();
            return View(empreg);
        }

        // POST: Employees/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(EmployeeRegisterModel empreg)
        {
            if (ModelState.IsValid)
            {
                Employee emp = new Employee();
                emp.Id = empreg.Id;
                emp.Name = empreg.Name;
                emp.TeamId = empreg.TeamId;
                db.Entry(emp).State = EntityState.Modified;
                db.SaveChanges();

                ApplicationUser userCheck = db.Users.Where(u => u.EmployeeId == emp.Id).FirstOrDefault();
                if (userCheck == null)
                {
                    if (!string.IsNullOrEmpty(empreg.Username) && !string.IsNullOrEmpty(empreg.Password))
                    {
                        if (!db.Users.Any(u => u.UserName == empreg.Username))
                        {
                            var hasher = new PasswordHasher();

                            var user = new ApplicationUser
                            {
                                EmployeeId = emp.Id,
                                UserName = empreg.Username,
                                PasswordHash = hasher.HashPassword(empreg.Password),
                                Email = "",
                                EmailConfirmed = true,
                                SecurityStamp = Guid.NewGuid().ToString()
                            };

                            db.Users.Add(user);
                            db.SaveChanges();
                        }
                        else
                        {
                            ModelState.AddModelError("", "Tên đăng nhập đã có người sử dụng !");
                            ViewBag.TeamId = new SelectList(db.Teams, "Id", "Name", empreg.TeamId);
                            return View(empreg);
                        }
                    }
                    
                }else
                {

                    userCheck.UserName = empreg.Username;
                    db.Entry(userCheck).State = EntityState.Modified;
                    db.SaveChanges();
                }

                return RedirectToAction("Index");
            }
            ViewBag.TeamId = new SelectList(db.Teams, "Id", "Name", empreg.TeamId);
            //ViewBag.PossibleRoles = db.Roles.OrderByDescending(r => r.Name).ToList();
            return View(empreg);
        }

        public ActionResult DeleteAccount(string username, int empId)
        {
            ApplicationUser userCheck = db.Users.Where(u => u.EmployeeId == empId).FirstOrDefault();
            if (userCheck != null)
            {
                db.Users.Remove(userCheck);
                db.SaveChanges();
            }
            return RedirectToAction("Edit", new { id = empId });
        }

        // GET: Employees/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Employee employee = db.Employees.Find(id);
            if (employee == null)
            {
                return HttpNotFound();
            }
            return PartialView(employee);
        }

        // POST: Employees/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id, string confirmText)
        {
            Employee employee = db.Employees.Find(id);
            var success = false;
            string error = "";
            if (employee == null)
            {
                error = "Không tìm thấy video";
            }
            else if (confirmText.ToLower() != "đồng ý")
            {
                error = "Chuỗi nhập vào chưa đúng";
            }
            else
            {
                ApplicationUser userCheck = db.Users.Where(u => u.EmployeeId == id).FirstOrDefault();
                if (userCheck != null)
                {
                    db.Users.Remove(userCheck);
                    db.SaveChanges();
                }


                db.Employees.Remove(employee);
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
