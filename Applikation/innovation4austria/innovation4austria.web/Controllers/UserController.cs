using innovation4austria.data;
using innovation4austria.logic;
using innovation4austria.web;
using innovation4austria.web.Models;
using log4net;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace innovation4austria.web.Controllers
{
    public class UserController : Controller
    {
        private static readonly ILog log = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        [HttpGet]
        public ActionResult Login()
        {
            log.Info("GET - User - Login");

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login(LoginModel model)
        {
            log.Info("POST - User - Login(LoginModel)");

            if (ModelState.IsValid)
            {
                var logonResult = UserAdministration.Logon(model.Username, model.Password);
                log.Debug(logonResult);

                switch (logonResult)
                {
                    case LogonResult.LogonDataValid:
                        FormsAuthentication.SetAuthCookie(model.Username, true);
                        return RedirectToAction("Dashboard", "User");
                        break;
                    case LogonResult.LogonDataInvalid:
                    case LogonResult.UserInactive:
                    case LogonResult.UnkownUser:
                        TempData[Constants.WARNING_MESSAGE] = ValidationMessages.LogonDataInvalid;
                        break;
                    default:
                        break;
                }
            }
            return View();
        }

        [HttpGet]
        [Authorize]
        public ActionResult Dashboard()
        {
            log.Info("GET - User - Dashboard()");

            return View();
        }

        [HttpGet]
        [Authorize]
        public ActionResult ProfileData()
        {
            log.Info("GET - User - ProfileData()");

            User currentUser = UserAdministration.GetUser(User.Identity.Name);
            ProfileDataModel model = AutoMapperConfig.CommonMapper.Map<ProfileDataModel>(currentUser);


            return View(model);
        }

        [HttpPost]
        [Authorize]
        [ValidateAntiForgeryToken]
        public ActionResult Logout()
        {
            log.Info("POST - User - Logout()");
            FormsAuthentication.SignOut();

            return RedirectToAction("Login");
        }
    }
}