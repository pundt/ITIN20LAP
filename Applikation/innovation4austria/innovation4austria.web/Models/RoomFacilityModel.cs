using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace innovation4austria.web.Models
{
    public class RoomFacilityModel
    {
        public int ID_Room { get; set; }
        public int ID_Facility { get; set; }

        public int Quantity { get; set; }

        public FacilityModel Facility { get; set; }
    }
}