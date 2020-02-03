using System;
using System.Globalization;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;
using Garment.Web.Models;
using Data.Models;
using Data.ViewModels;
using Data.DataAccessLayer;
using System.Collections.Generic;

namespace Garment.Web.Controllers
{
    [Authorize]
    public class AccountController : Controller
    {
        private ApplicationSignInManager _signInManager;
        private ApplicationUserManager _userManager;
        private GarmentContext context;

        public AccountController()
        {
            context = new GarmentContext();
        }

        public AccountController(ApplicationUserManager userManager, ApplicationSignInManager signInManager )
        {
            UserManager = userManager;
            SignInManager = signInManager;
        }

        public ApplicationSignInManager SignInManager
        {
            get
            {
                return _signInManager ?? HttpContext.GetOwinContext().Get<ApplicationSignInManager>();
            }
            private set 
            { 
                _signInManager = value; 
            }
        }

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

        //
        // GET: /Account/Login
        [AllowAnonymous]
        public ActionResult Login(string returnUrl)
        {
            ViewBag.ReturnUrl = returnUrl;
            return View();
        }

        //
        // POST: /Account/Login
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Login(LoginModel model, string returnUrl)
        {
            if (ModelState.IsValid)
            {
                var user = await UserManager.FindAsync(model.Email, model.Password);

                if (user != null)
                {
                    await SignInManager.SignInAsync(user, true, model.RememberMe);//(user, model.RememberMe);
                    if (String.IsNullOrEmpty(returnUrl))
                        return RedirectToAction("Index", "Home");
                    return RedirectToLocal(returnUrl);
                }
                else
                    ModelState.AddModelError("", "Tên đăng nhập hoặc mật khẩu không đúng.");
            }

            // If we got this far, something failed, redisplay form
            return View(model);
        }


        //
        // GET: /Account/Register
        [AllowAnonymous]
        public ActionResult Register()
        {
            return View();
        }

        //
        // POST: /Account/Register
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public ActionResult Register(RegisterModel model)
        {
            if (ModelState.IsValid)
            {

                if (!context.Users.Any(u => u.UserName == model.Username))
                {
                    var hasher = new PasswordHasher();

                    var user = new ApplicationUser
                    {
                        UserName = model.Username,
                        PasswordHash = hasher.HashPassword(model.Password),
                        Email = "",
                        EmailConfirmed = true,
                        SecurityStamp = Guid.NewGuid().ToString()
                    };

                    //foreach (var roleid in model.Roles)
                    //{
                    //    user.Roles.Add(new IdentityUserRole { RoleId = roleid, UserId = user.Id });
                    //}

                    context.Users.Add(user);
                    context.SaveChanges();
                }
                return RedirectToAction("Index", "Home");
            }

            // If we got this far, something failed, redisplay form
            return View(model);
        }

        public ActionResult Delete(string id)
        {
            return PartialView(context.Users.FirstOrDefault(u => u.Id == id));
        }

        [ValidateAntiForgeryToken]
        [HttpPost, ActionName("Delete")]
        public JsonResult DeleteConfirmed(string id, string confirmText)
        {
            ApplicationUser user = context.Users.FirstOrDefault(u => u.Id == id);
            context.Users.Attach(user);
            context.Entry(user).Collection(u => u.Roles).Load();
            var success = false;
            string error = "";
            if (user == null)
            {
                error = "Không tìm thấy người dùng này";
            }
            else if (confirmText.ToLower() != "đồng ý")
            {
                error = "Chuỗi nhập vào chưa đúng";
            }
            else
            {
                var roles = UserManager.GetRoles(user.Id);
                if (roles.Contains("Admin"))
                {
                    error = "Không có quyền xóa tài khoản Admin";
                }
                else
                {
                    //remove login
                    var logins = user.Logins;
                    foreach (var login in logins.ToList())
                    {
                        UserManager.RemoveLogin(login.UserId, new UserLoginInfo(login.LoginProvider, login.ProviderKey));
                    }
                    //remove role
                    foreach (var role in roles)
                    {
                        UserManager.RemoveFromRole(user.Id, role);
                    }
                    //remove user
                    context.Users.Remove(user);
                    context.SaveChanges();
                    success = true;
                }
            }

            return Json(new { success = success, id = id, error = error });
        }

        [Authorize(Roles = "Admin")]
        public ActionResult ResetPassword(string userId)
        {
            if (User.IsInRole("Admin") || User.Identity.GetUserId() == userId)
            {
                var model = new ResetPasswordModel { UserId = userId };
                return View(model);
            }

            return RedirectToAction("Management", "Admin");
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> ResetPassword(ResetPasswordModel model)
        {
            if (ModelState.IsValid)
            {

                await UserManager.RemovePasswordAsync(model.UserId);

                var result = await UserManager.AddPasswordAsync(model.UserId, model.NewPassword);
                if (result.Succeeded)
                {
                    return RedirectToAction("Management", "Account");
                }
                else
                {
                    foreach (string e in result.Errors)
                        ModelState.AddModelError("", e);
                }
            }
            return View(model);
        }

        public ActionResult ChangePassword(string userId)
        {
            if (User.Identity.GetUserId() == userId || User.IsInRole("Admin"))
            {
                var model = new ChangePasswordModel { UserId = userId };
                return View(model);
            }

            //if (User.IsInRole("Admin"))
            //    return RedirectToAction("Management", "Account");
            return RedirectToAction("Index", "Account");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> ChangePassword(ChangePasswordModel model)
        {
            if (ModelState.IsValid)
            {
                //_context.Entry(article).State = EntityState.Modified;
                var result = await UserManager.ChangePasswordAsync(model.UserId, model.OldPassword, model.NewPassword);
                if (result.Succeeded)
                {
                    //if (User.IsInRole("Admin"))
                    //    return RedirectToAction("Management", "Admin");
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    foreach (string e in result.Errors)
                        ModelState.AddModelError("", e);
                }
            }
            return View(model);
        }

        public ActionResult Management()
        {
            return View();
        }

        public JsonResult GetUsers()
        {
            var users = context.Users.AsEnumerable()
                                                .Select(spec => new
                                                {
                                                    Id = spec.Id,
                                                    UserName = spec.UserName,
                                                    Email = spec.Email,
                                                    //Role = UserManager.GetRoles(spec.Id).Count > 0 ? UserManager.GetRoles(spec.Id)[0] : "",  
                                                    Role = GetStringRoles(spec.Id)
                                                }).ToList();
            var total = users.Count;
            return Json(new { users, total }, JsonRequestBehavior.AllowGet);
        }

        //
        // POST: /Account/LogOff
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult LogOff()
        {
            AuthenticationManager.SignOut(DefaultAuthenticationTypes.ApplicationCookie);
            return RedirectToAction("Index", "Home");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (_userManager != null)
                {
                    _userManager.Dispose();
                    _userManager = null;
                }

                if (_signInManager != null)
                {
                    _signInManager.Dispose();
                    _signInManager = null;
                }
            }

            base.Dispose(disposing);
        }

        private string GetStringRoles(string userid)
        {
            List<string> lstRoles = UserManager.GetRoles(userid).ToList();
            string result = "";
            foreach (var role in lstRoles)
                result += role + ", ";
            return result.Trim().TrimEnd(',');
        }

        #region Helpers
        // Used for XSRF protection when adding external logins
        private const string XsrfKey = "XsrfId";

        private IAuthenticationManager AuthenticationManager
        {
            get
            {
                return HttpContext.GetOwinContext().Authentication;
            }
        }

        private void AddErrors(IdentityResult result)
        {
            foreach (var error in result.Errors)
            {
                ModelState.AddModelError("", error);
            }
        }

        private ActionResult RedirectToLocal(string returnUrl)
        {
            if (Url.IsLocalUrl(returnUrl))
            {
                return Redirect(returnUrl);
            }
            return RedirectToAction("Index", "Home");
        }

        internal class ChallengeResult : HttpUnauthorizedResult
        {
            public ChallengeResult(string provider, string redirectUri)
                : this(provider, redirectUri, null)
            {
            }

            public ChallengeResult(string provider, string redirectUri, string userId)
            {
                LoginProvider = provider;
                RedirectUri = redirectUri;
                UserId = userId;
            }

            public string LoginProvider { get; set; }
            public string RedirectUri { get; set; }
            public string UserId { get; set; }

            public override void ExecuteResult(ControllerContext context)
            {
                var properties = new AuthenticationProperties { RedirectUri = RedirectUri };
                if (UserId != null)
                {
                    properties.Dictionary[XsrfKey] = UserId;
                }
                context.HttpContext.GetOwinContext().Authentication.Challenge(properties, LoginProvider);
            }
        }
        #endregion
    }
}