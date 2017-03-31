using innovation4austria.data;
using innovation4austria.logic;
using innovation4austria.web.Models;
using log4net;
using System;
using System.Collections.Generic;
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

            return PartialView("_Search", model);
        }
    }
}