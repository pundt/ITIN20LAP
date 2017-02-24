using innovation4austria.logic;
using innovation4austria.web.App_GlobalResources;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.UI;

namespace innovation4austria.web.Controllers
{
    [OutputCache(Location = OutputCacheLocation.None, NoStore = true)]
    public class ValidationController : Controller
    {

        public JsonResult IsUsernameAvailable(string username)
        {
            if (UserAdministration.GetUser(username)==null)
                return Json(true, JsonRequestBehavior.AllowGet);
            
            return Json(ValidationMessages.UsernameNotAvailable, JsonRequestBehavior.AllowGet);
        }

    }
}