using innovation4austria.web.App_GlobalResources;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace innovation4austria.web.Models
{
    public class LoginModel
    {
        [Required(AllowEmptyStrings = false, 
            ErrorMessageResourceType = typeof(ValidationMessages), 
            ErrorMessageResourceName = Validation.REQUIRED)]
        [EmailAddress(ErrorMessageResourceName = Validation.DATATYPE_MAIL,
            ErrorMessageResourceType = typeof(ValidationMessages))]
        [StringLength(100, 
            ErrorMessageResourceName = Validation.MAXLENGTH,
            ErrorMessageResourceType = typeof(ValidationMessages))]
        [Display(ResourceType = typeof(DisplayNames), Name = nameof(DisplayNames.Username))]
        public string Username { get; set; }

        [Required(AllowEmptyStrings = false,
            ErrorMessageResourceType = typeof(ValidationMessages),
            ErrorMessageResourceName = Validation.REQUIRED)]
        [RegularExpression("[a-zA-Z0-9!]{5,}", 
            ErrorMessageResourceType = typeof(ValidationMessages),
            ErrorMessageResourceName = Validation.REGULAR_EXPRESSION_PASSWORD)]
        [StringLength(50,
            ErrorMessageResourceName = Validation.MAXLENGTH,
            ErrorMessageResourceType = typeof(ValidationMessages))]
        [Display(ResourceType = typeof(DisplayNames), Name = nameof(DisplayNames.Password))]
        [DataType(DataType.Password)]
        public string Password { get; set; }
    }
}