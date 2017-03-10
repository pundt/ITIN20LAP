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

            // gehe in die Datenbank
            model.Buildings = new List<BuildingModel>();
            for (int i = 0; i < 10; i++)
            {
                model.Buildings.Add(new BuildingModel()
                {
                    ID = i,
                    Number = "number " + i,
                    Street = "street " + i
                });
            }

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken()]
        public ActionResult Search(string start, string end, int[] idFacilities, int idBuilding, int idRoomType)
        {
            log.Info("POST - Room - Search");            

            List<SearchResultModel> model = new List<SearchResultModel>();
            for (int i = 0; i < 10; i++)
            {
                model.Add(new SearchResultModel()
                {
                    Description = "result" + i,
                    Room_ID = i
                });
            }

            return PartialView("_Search", model);
        }
    }
}