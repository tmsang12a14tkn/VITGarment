using Data.DataAccessLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity.Owin;
using Data.ViewModels;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Data.Models;

namespace Garment.Web.Controllers
{
    public class RoleController : Controller
    {
        private GarmentContext context;
        private ApplicationUserManager _userManager;
        public ApplicationUserManager UserManager
        {
            get
            {
                return _userManager ?? HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();
            }
            private set
            {
                _userManager = value;
            }
        }
        public RoleController(ApplicationUserManager userManager)
        {
            UserManager = userManager;
        }

        public RoleController()
        {
            context = new GarmentContext();
        }


        [Authorize(Roles = "Admin")]
        public ActionResult Management()
        {
            return View();
        }

        [Authorize(Roles = "Admin")]
        public ActionResult Create()
        {
            return View();
        }

        //
        // POST: /Account/Register
        [HttpPost]
        [Authorize(Roles = "Admin")]
        [ValidateAntiForgeryToken]
        public ActionResult Create(CreateRoleModel model)
        {
            if (ModelState.IsValid)
            {
                var RoleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(context));
                // Create Admin Role
                IdentityResult roleResult;

                // Check to see if Role Exists, if not create it
                if (!RoleManager.RoleExists(model.Name))
                {
                    roleResult = RoleManager.Create(new IdentityRole(model.Name));
                    if (roleResult.Succeeded)
                        return RedirectToAction("Management");
                }
            }
            return View(model);
        }

        [Authorize(Roles = "Admin")]
        public ActionResult Edit(string id)
        {
            IdentityRole role = context.Roles.Where(r => r.Id == id).FirstOrDefault();
            CreateRoleModel rolemodel = AutoMapper.Mapper.Map<CreateRoleModel>(role);
            return View(rolemodel);
        }

        //
        // POST: /Reporter/Edit/5
        [Authorize(Roles = "Admin")]
        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Edit(CreateRoleModel model)
        {
            if (!String.IsNullOrEmpty(model.Name))
            {
                var role = AutoMapper.Mapper.Map<CreateRoleModel, IdentityRole>(model);
                context.Entry(role).State = System.Data.Entity.EntityState.Modified;
                context.SaveChanges();

                return RedirectToAction("Management");
            }
            return View(model);
        }


        [Authorize(Roles = "Admin")]
        public ActionResult Delete(string id)
        {
            IdentityRole role = context.Roles.Where(r => r.Id == id).FirstOrDefault();
            return PartialView(AutoMapper.Mapper.Map<IdentityRole, CreateRoleModel>(role));
        }

        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        [HttpPost, ActionName("Delete")]
        public JsonResult DeleteConfirmed(string id, string confirmText)
        {
            IdentityRole role = context.Roles.FirstOrDefault(u => u.Id == id);
            var success = false;
            string error = "";
            if (role == null)
            {
                error = "Không tìm thấy vai trò này";
            }
            else if (confirmText.ToLower() != "đồng ý")
            {
                error = "Chuỗi nhập vào chưa đúng";
            }
            else
            {
                var users = UserManager.Users.ToList();
                foreach (ApplicationUser u in users)
                {
                    if (UserManager.IsInRole(u.Id, role.Name))
                        UserManager.RemoveFromRole(u.Id, role.Name);
                }

                context.Roles.Remove(role);
                context.SaveChanges();
                success = true;
            }

            return Json(new { success = success, id = id, error = error });
        }

        public JsonResult GetRoles()
        {
            var Roles = context.Roles.AsEnumerable()
                                                .Select(spec => new
                                                {
                                                    Id = spec.Id,
                                                    Name = spec.Name,
                                                }).ToList();
            var total = Roles.Count;
            return Json(new { Roles, total }, JsonRequestBehavior.AllowGet);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                context.Dispose();
                context = null;
            }
            base.Dispose(disposing);
        }
    }
}