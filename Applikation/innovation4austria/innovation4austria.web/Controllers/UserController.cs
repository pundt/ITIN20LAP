using innovation4austria.data;
using innovation4austria.logic;
using innovation4austria.web;
using innovation4austria.web.App_GlobalResources;
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
            ProfileDataModel dataModel = AutoMapperConfig.CommonMapper.Map<ProfileDataModel>(currentUser);
            ProfileModel model = new ProfileModel()
            {
                ProfileData = dataModel,
                ProfilePassword = new ProfilePasswordModel()
            };

            return View("Profile", model);
        }

        [HttpPost]
        [Authorize]
        [ValidateAntiForgeryToken]
        public ActionResult SaveProfileData(ProfileDataModel model)
        {
            log.Info("POST - User - SaveProfileData(ProfileDataModel)");

            if (ModelState.IsValid)
            {
                // call logic and modify profileData
                try
                {
                    ProfileChangeResult result = UserAdministration.SaveProfileData(User.Identity.Name, model.Firstname, model.Lastname);
                    if (result == ProfileChangeResult.Success)
                    {

                        TempData[Constants.SUCCESS_MESSAGE] = ValidationMessages.SaveSuccess;
                    }
                    else
                    {
                        TempData[Constants.ERROR_MESSAGE] = ValidationMessages.SaveError;
                    }
                }
                catch (Exception)
                {
                    TempData[Constants.ERROR_MESSAGE] = ValidationMessages.SaveError;
                }
            }
            else
            {
                TempData[Constants.WARNING_MESSAGE] = ValidationMessages.ProfileDataInvalid;
            }

            return RedirectToAction("ProfileData");
        }

        [HttpPost]
        [Authorize]
        [ValidateAntiForgeryToken]
        public ActionResult SaveProfilePassword(ProfilePasswordModel model)
        {
            log.Info("POST - User - SaveProfilePassword(ProfilePasswordModel)");

            if (ModelState.IsValid)
            {
                // call logic and modify profileData
                if (UserAdministration.ChangePassword(User.Identity.Name, model.OldPassword, model.NewPassword) == PasswordChangeResult.Success)
                {

                    TempData[Constants.SUCCESS_MESSAGE] = ValidationMessages.SaveSuccess;
                }
                else
                {
                    TempData[Constants.ERROR_MESSAGE] = ValidationMessages.SaveError;
                }
            }
            else
            {
                TempData[Constants.WARNING_MESSAGE] = ValidationMessages.ProfilePasswordInvalid;
            }

            return RedirectToAction("ProfileData");
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