using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace innovation4austria.web.Controllers
{
    [Authorize]
    public class BookingController : Controller
    {
        public ActionResult Index()
        {
            /// shows all bookings of company currently logged on
           

            return View();
        }

        public ActionResult Cancel(int idBooking)
        {
            /// cancel selected booking
            /// if possible (check 3-days-deadline) 

            ///

            return View();
        }
    }
}