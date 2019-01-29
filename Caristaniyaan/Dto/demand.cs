using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Caristaniyaan.Dto
{
    public class demand
    {

        [Required]
        [MaxLength(4)]
        [MinLength(4)]
        [RegularExpression("^(19|20)[0-9][0-9]", ErrorMessage = "Modal year must be a valid year")]
        [Display(Name = "Modal Year")]
        public string modalYear { get; set; }

        [Required]
        [MaxLength(100)]
        [Display(Name = "Car Info")]
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
        [Display(Name = "Item or Service name")]
        [MaxLength(50)]
        public string itemName { get; set; }

        [Required]
        [Display(Name = "Details")]
        [MaxLength(500)]
        public string itemDetail { get; set; }

        [Required]
        [Display(Name = "Image")]
        public HttpPostedFileWrapper itemImage { get; set; }
    }
}