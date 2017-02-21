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

            // VARIANTE user defined MAPPING
            // mapping von List<Company> auf List<CompanyModel>
            List<CompanyModel> model = new List<CompanyModel>();

            foreach (var company in allCompanies)
            {
                model.Add(new CompanyModel()
                {
                    ID = company.ID,
                    Name = company.Name,
                    Number = company.Number,
                    Street = company.Street,
                    Zip = company.Zip,
                    City = company.City
                });
            }

            // Variante AUTOMAPPER
            //List<CompanyModel> model = AutoMapperConfig.CommonMapper.Map<List<CompanyModel>>(allCompanies);

            return View(model);
        }

        [HttpGet]
        public ActionResult Detail(int id)
        {
            log.Info("GET - Company - Detail");

            // lade Daten aus logic
            Company company = CompanyAdministration.GetCompany(id);
            List<User> companyUsers = UserAdministration.GetCompanyUsers(id);

            // übertrage werte in Model Objekt
            CompanyDetailModel model = new CompanyDetailModel();

            // Variante 1 - manuelles mapping
            //model.Company = new CompanyModel()
            //{
            //    ID = company.ID,
            //    Name = company.Name,
            //    Zip = company.Zip,
            //    Street = company.Street,
            //    City = company.City
            //};

            //model.CompanyUsers = new List<ProfileDataModel>();
            //foreach (var companyUser in companyUsers)
            //{
            //    model.CompanyUsers.Add(new ProfileDataModel()
            //    {
            //        ID = companyUser.ID,
            //        Firstname = companyUser.FirstName,
            //        Lastname = companyUser.LastName,
            //        Active = companyUser.Active,
            //        Username = companyUser.Username
            //    });
            //}

            // Variante 2 - mapping via AutoMapper
            model.Company = AutoMapperConfig.CommonMapper.Map<CompanyModel>(company);
            model.CompanyUsers = AutoMapperConfig.CommonMapper.Map<List<ProfileDataModel>>(companyUsers);


            // übergib alles an view
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult SaveDetail(CompanyModel model)
        {
            if (ModelState.IsValid)
            {
                if (CompanyAdministration.SaveCompanyData(model.ID, model.Name, model.Street, model.Number, model.Zip, model.City))
                {
                    TempData[Constants.Messages.SUCCESS] = ValidationMessages.SaveSuccess;
                }
                else
                {
                    TempData[Constants.Messages.ERROR] = ValidationMessages.SaveError;
                }
            }
            else
            {
                TempData[Constants.Messages.WARNING] = ValidationMessages.CompanyDataInvalid;
            }

            return RedirectToAction("Detail", new { id = model.ID });
        }

        [HttpGet]
        public ActionResult Employee(int id = 0)
        {
            log.Info("GET - Company - Employee()");

            if (id <= 0)
                return HttpNotFound("Invalid Employee ID");

            User employee = UserAdministration.GetUser(id);
            EmployeeDataModel dataModel = AutoMapperConfig.InnovationMapper.Map<EmployeeDataModel>(employee);
            EmployeeModel model = new EmployeeModel()
            {
                EmployeeData = dataModel,
                EmployeePassword = new EmployeePasswordModel()
            };

            return View(model);
        }

        [HttpPost]
        [Authorize]
        [ValidateAntiForgeryToken]
        public ActionResult SaveEmployeeData(EmployeeDataModel model)
        {
            log.Info("POST - Company - SaveEmployeeData(EmployeeDataModel)");

            if (ModelState.IsValid)
            {
                // call logic and modify employeeData
                try
                {
                    ProfileChangeResult result = UserAdministration.SaveEmployeeData(model.ID, model.Firstname, model.Lastname);
                    if (result == ProfileChangeResult.Success)
                    {

                        TempData[Constants.Messages.SUCCESS] = ValidationMessages.SaveSuccess;
                    }
                    else
                    {
                        TempData[Constants.Messages.ERROR] = ValidationMessages.SaveError;
                    }
                }
                catch (Exception)
                {
                    TempData[Constants.Messages.ERROR] = ValidationMessages.SaveError;
                }
            }
            else
            {
                TempData[Constants.Messages.WARNING] = ValidationMessages.ProfileDataInvalid;
            }

            return RedirectToAction("Employee", new { id = model.ID });
        }

        [HttpPost]
        [Authorize]
        [ValidateAntiForgeryToken]
        public ActionResult SaveEmployeePassword(EmployeePasswordModel model)
        {
            log.Info("POST - Company - SaveEmployeePassword(EmployeePasswordModel)");

            if (ModelState.IsValid)
            {
                // call logic and modify employee password
                if (UserAdministration.ChangePassword(model.ID, model.OldPassword, model.NewPassword) == PasswordChangeResult.Success)
                {

                    TempData[Constants.Messages.SUCCESS] = ValidationMessages.SaveSuccess;
                }
                else
                {
                    TempData[Constants.Messages.ERROR] = ValidationMessages.SaveError;
                }
            }
            else
            {
                TempData[Constants.Messages.WARNING] = ValidationMessages.ProfilePasswordInvalid;
            }

            return RedirectToAction("Employee", new { id = model.ID });
        }

        [HttpGet]
        public ActionResult CreateEmployee(int id)
        {
            log.Info("GET - Company - CreateEmployee(id)");

            CreateEmployeeModel model = new CreateEmployeeModel()
            {
                CompanyID = id
            };

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreateEmployee(CreateEmployeeModel model)
        {
            log.Info("POST - Company - CreateEmployee(CreateEmployeeModel)");
            ActionResult result = View(model);

            if (ModelState.IsValid)
            {
                if (UserAdministration.CreateCompanyUser(model.Username, model.NewPassword, model.Firstname, model.Lastname, model.CompanyID))
                {
                    TempData[Constants.Messages.SUCCESS] = ValidationMessages.SaveSuccess;
                    result = RedirectToAction("Detail", new { id = model.CompanyID });
                }
                else
                {
                    TempData[Constants.Messages.ERROR] = ValidationMessages.SaveError;

                }  
            }
            else
            {
                TempData[Constants.Messages.WARNING] = ValidationMessages.CreateEmployeeInvalid;
            }

            return result;
        }
    }
}