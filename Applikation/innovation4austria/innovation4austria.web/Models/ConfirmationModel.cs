using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace innovation4austria.web.Models
{
    public class ConfirmationModel
    {
        public SearchResultModel Room { get; set; }
        public List<CompanyModel> Companies { get; set; }
        public DateTime Start { get; set; }
        public DateTime End { get; set; }
    }
}