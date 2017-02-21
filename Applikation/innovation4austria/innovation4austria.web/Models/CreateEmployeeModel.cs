using innovation4austria.web.App_GlobalResources;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace innovation4austria.web.Models
{
    public class CreateEmployeeModel
    {
        public int CompanyID { get; set; }

        [Display(ResourceType = typeof(DisplayNames), Name = nameof(DisplayNames.Username))]
        [StringLength(20,
            ErrorMessageResourceName = Validation.MAXLENGTH,
            ErrorMessageResourceType = typeof(ValidationMessages))]
        [Editable(false)]
        public string Username { get; set; }

        [Display(ResourceType = typeof(DisplayNames), Name = nameof(DisplayNames.Firstname))]
        [StringLength(50,
            ErrorMessageResourceName = Validation.MAXLENGTH,
            ErrorMessageResourceType = typeof(ValidationMessages))]
        public string Firstname { get; set; }

        [Display(ResourceType = typeof(DisplayNames), Name = nameof(DisplayNames.Lastname))]
        [StringLength(75,
            ErrorMessageResourceName = Validation.MAXLENGTH,
            ErrorMessageResourceType = typeof(ValidationMessages))]
        public string Lastname { get; set; }

        [Display(ResourceType = typeof(DisplayNames), Name = nameof(DisplayNames.NewPassword))]
        [StringLength(20,
          ErrorMessageResourceName = Validation.MAXLENGTH,
          ErrorMessageResourceType = typeof(ValidationMessages))]
        [DataType(DataType.Password)]
        public string NewPassword { get; set; }

        [Display(ResourceType = typeof(DisplayNames), Name = nameof(DisplayNames.ConfirmPassword))]
        [StringLength(20,
            ErrorMessageResourceName = Validation.MAXLENGTH,
            ErrorMessageResourceType = typeof(ValidationMessages))]
        [DataType(DataType.Password)]
        public string ConfirmNewPassword { get; set; }
    }
}