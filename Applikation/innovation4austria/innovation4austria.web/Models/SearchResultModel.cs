using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace innovation4austria.web.Models
{
    public class SearchResultModel
    {
        public int ID { get; set; }
        public string RoomName { get; set; }

        public string Size { get; set; }

        public string PricePerDay { get; set; }

        public BuildingModel Building { get; set; }

        public TypeModel Type { get; set; }

        public List<RoomFacilityModel> AllRoomFacilities { get; set; }
    }
}