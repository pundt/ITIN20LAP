using innovation4austria.data;
using innovation4austria.logic;
using innovation4austria.web.App_GlobalResources;
using innovation4austria.web.Models;
using log4net;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace innovation4austria.web.Controllers
{
    [Authorize]
    public class RoomController : Controller
    {
        private static readonly ILog log = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        [HttpGet]
        public ActionResult Index()
        {
            log.Info("GET - Room - Index");
            RoomFilterModel model = new Models.RoomFilterModel();

            // get all buildings
            List<Building> buildings = BuildingAdministration.GetBuildings();
            model.Buildings = AutoMapperConfig.CommonMapper.Map<List<BuildingModel>>(buildings);
            List<Facility> facilities = FacilityAdministration.GetFacilities();
            model.Facilities = AutoMapperConfig.CommonMapper.Map<List<FacilityModel>>(facilities);
            List<data.Type> types = TypeAdministration.GetRoomTypes();
            model.Types = AutoMapperConfig.CommonMapper.Map<List<TypeModel>>(types);

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken()]
        public ActionResult Search(string start, string end, int[] idFacilities, int idBuilding, int idRoomType)
        {
            log.Info("POST - Room - Search");

            List<Room> searchResults = RoomAdministration.Search(start, end, idFacilities, idBuilding, idRoomType);
            List<SearchResultModel> model = AutoMapperConfig.CommonMapper.Map<List<SearchResultModel>>(searchResults);
            ViewBag.Start = start;
            ViewBag.End = end;

            return PartialView("_Search", model);
        }

        [HttpPost]
        public ActionResult Confirmation(string start, string end, int idRoom)
        {
            ConfirmationModel model = new ConfirmationModel();
            ActionResult result = null;

            List<Company> companies = CompanyAdministration.GetCompanies();
            Room room = RoomAdministration.Get(idRoom);
            if (room != null)
            {
                CultureInfo currentCulture = System.Threading.Thread.CurrentThread.CurrentCulture;
                model.Room = AutoMapperConfig.CommonMapper.Map<SearchResultModel>(room);
                model.Companies = AutoMapperConfig.CommonMapper.Map<List<CompanyModel>>(companies);

                DateTime startDate, endDate;
                if (DateTime.TryParse(start, currentCulture, DateTimeStyles.None, out startDate))
                    model.Start = startDate;
                if (DateTime.TryParse(end, currentCulture, DateTimeStyles.None, out endDate))
                    model.End = endDate;

                result = View(model);
            }
            else
            {
                TempData[Constants.Messages.WARNING] = ValidationMessages.InvalidRoomId;
                result = RedirectToAction("Index");
            }
            return result;
        }

        [HttpPost]
        public ActionResult Booking(string start, string end, int idRoom)
        {
            log.Info("POST - Room - Booking");

            /// booking for company currently logged on

            /// redirect to room overview/search

            return View();
        }

        [HttpPost]
        public ActionResult Booking(string start, string end, int idRoom, int idCompany)
        {
            log.Info("POST - Room - Booking");

            /// booking for selected company


            return View();
        }
    }
}