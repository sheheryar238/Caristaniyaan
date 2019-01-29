using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Caristaniyaan.Dto
{
    public class appointemnt
    {
        [Required]
        [MaxLength(4)]
        [MinLength(4)]
        [RegularExpression("^(19|20)[0-9][0-9]", ErrorMessage = "Modal year must be a valid year")]
        [Display(Name = "Modal Year")]
        public string modalYear { get; set; }

        [Required]
        [MaxLength(100)]
        [Display(Name = "Car info")]
        public string carInfo { get; set; }

        [Required]
        [MaxLength(20)]
        [Display(Name = "Name")]
        public string name { get; set; }

        [Required]
        [RegularExpression("[+][9][2][0-9]{3}[0-9]{7}", ErrorMessage = "Phone Number must be of proper format")]
        [Display(Name = "Phone Number")]
        [MaxLength(13)]
        public string phonenumber { get; set; }

        [Required]
        [EmailAddress]
        [Display(Name = "Email")]
        public string email { get; set; }

        [Required]
        [Display(Name = "Message")]
        [MaxLength(500)]
        public string message { get; set; }
    }
}