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
        [Required]
        public int ID { get; set; }

        [Required]
        [Display(ResourceType = typeof(DisplayNames), Name = nameof(DisplayNames.CompanyName))]
        [StringLength(20,
            ErrorMessageResourceName = Validation.MAXLENGTH,
            ErrorMessageResourceType = typeof(ValidationMessages))]
        public string Name { get; set; }

        [Display(ResourceType = typeof(DisplayNames), Name = nameof(DisplayNames.CompanyName))]
        [StringLength(20,
           ErrorMessageResourceName = Validation.MAXLENGTH,
           ErrorMessageResourceType = typeof(ValidationMessages))]
        public string Street { get; set; }

        [Display(ResourceType = typeof(DisplayNames), Name = nameof(DisplayNames.Number))]
        [StringLength(10,
           ErrorMessageResourceName = Validation.MAXLENGTH,
           ErrorMessageResourceType = typeof(ValidationMessages))]
        public string Number { get; set; }

        [Display(ResourceType = typeof(DisplayNames), Name = nameof(DisplayNames.City))]
        [StringLength(50,
           ErrorMessageResourceName = Validation.MAXLENGTH,
           ErrorMessageResourceType = typeof(ValidationMessages))]
        public string City { get; set; }

        [Display(ResourceType = typeof(DisplayNames), Name = nameof(DisplayNames.Zip))]
        [StringLength(10,
           ErrorMessageResourceName = Validation.MAXLENGTH,
           ErrorMessageResourceType = typeof(ValidationMessages))]
        public string Zip { get; set; }
    }
}