using innovation4austria.web.App_GlobalResources;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace innovation4austria.web.Models
{
    public class CompanyModel
    {
        public string ID { get; set; }

        public string Name { get; set; }

        public string Street { get; set; }

        public string Number { get; set; }

        public string City { get; set; }

        public string Zip { get; set; }
    }
}