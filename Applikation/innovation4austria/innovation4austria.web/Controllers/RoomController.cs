using innovation4austria.web.Models;
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
        [HttpGet]
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken()]
        public ActionResult Search(string start, string ende, int[] ausstattung, int standort, int art)
        {
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