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
    [Authorize(Roles = Constants.Roles.INNOVATION4AUSTRIA)]
    public class CompanyController : Controller
    {
        private static readonly ILog log = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        /// <summary>
        /// will show all companies
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult Index()
        {
            log.Info("GET - Company - Index");


            List<Company> allCompanies = CompanyAdministration.GetCompanies();
            List<CompanyModel> model = AutoMapperConfig.CommonMapper.Map<List<CompanyModel>>(allCompanies);

            return View(model);
        }
    }
}