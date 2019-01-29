using Caristaniyaan.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Caristaniyaan.Dto
{
    public class orderDetailViewModel
    {
        public int Id { get; set; }

        [Required]
        [Display(Name = "First Name")]
        public string fname { get; set; }

        [Required]
        [Display(Name = "Last Name")]
        public string lname { get; set; }

        [Required]
        [EmailAddress]
        [Display(Name = "Email")]
        public string email { get; set; }

        [Required]
        [RegularExpression("[+][9][2][0-9]{3}[0-9]{7}", ErrorMessage = "Phone Number must be of proper format")]
        [Display(Name = "Phone Number")]
        [MaxLength(13)]
        public string phoneno { get; set; }

        [Required]
        [Display(Name = "Address")]
        public string address { get; set; }

        [Required]
        [Display(Name = "City")]
        public string city { get; set; }

        [Required]
        [Display(Name = "Post Code")]
        public int postcode { get; set; }

        [Required]
        [Display(Name = "Province")]
        public string province { get; set; }

        [Required]
        [Display(Name = "Country")]
        public string countary { get; set; }

        [Required]
        public DateTime date { get; set; }

        [Required]
        public Status status { get; set; }
        public OrderProduct[] orderProducts { get; set; }
    }
}