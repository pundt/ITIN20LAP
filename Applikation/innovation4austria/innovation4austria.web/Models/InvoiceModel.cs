using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace innovation4austria.web.Models
{
    public class InvoiceModel
    {
        public int ID { get; set; }
        public string CompanyName { get; set; }

        public string InvoicePeriod { get; set; }

        public double Sum { get; set; }
    }
}