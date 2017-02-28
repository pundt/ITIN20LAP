using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace innovation4austria.web.Models
{
    public class RoomFilterModel
    {
        [Required]
        [DataType(DataType.Date)]
        public string Start { get; set; }
        [Required]
        [DataType(DataType.Date)]
        public string End { get; set; }

        public List<FacilityModel> Facilities{ get; set; }
    }
}