using innovation4austria.web.App_GlobalResources;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace innovation4austria.web.Models
{
    public class ProfilePasswordModel
    {
        [Display(ResourceType = typeof(DisplayNames), Name = nameof(DisplayNames.OldPassword))]
        [StringLength(20,
            ErrorMessageResourceName = Validation.MAXLENGTH,
            ErrorMessageResourceType = typeof(ValidationMessages))]
        [DataType(DataType.Password)]
        public string OldPassword { get; set; }

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