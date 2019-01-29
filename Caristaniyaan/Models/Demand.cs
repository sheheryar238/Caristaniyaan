using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Caristaniyaan.Models
{
    public class Demand
    {
        public int Id { get; set; }

        [Required]
        [MaxLength(4)]
        [MinLength(4)]
        [RegularExpression("^(19|20)[0-9][0-9]", ErrorMessage = "Modal year must be a valid year")]
        public string modalYear { get; set; }

        [Required]
        [MaxLength(100)]
        public string carInfo { get; set; }

        [Required]
        [MaxLength(20)]
        public string name { get; set; }

        [Required]
        [RegularExpression("[+][9][2][0-9]{3}[0-9]{7}", ErrorMessage = "Phone Number must be of proper format")]
        [Display(Name = "Phone Number")]
        [MaxLength(13)]
        public string phonenumber { get; set; }

        [Required]
        [EmailAddress]
        public string email { get; set; }

        [Required]
        [Display(Name = "Phone Number")]
        [MaxLength(50)]
        public string itemName { get; set; }

        [Required]
        [Display(Name = "Phone Number")]
        [MaxLength(500)]
        public string itemDetail { get; set; }

        [Required]
        public string itemImage { get; set; }

        [Required]
        public DateTime date { get; set; }
    }
}