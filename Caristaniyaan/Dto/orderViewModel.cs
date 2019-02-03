using Caristaniyaan.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Caristaniyaan.Dto
{
    public class orderViewModel
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
        [Range(1, int.MaxValue)]
        [EnumDataType(typeof(Province))]
        public Province province { get; set; }

        [Required]
        [Display(Name = "Country")]
        [Range(1, int.MaxValue)]
        [EnumDataType(typeof(Country))]
        public Country countary { get; set; }

        [Required]
        public int total { get; set; }

        [Required]
        public string cartItems { get; set; }

        [Required]
        public Status status { get; set; }
    }

    public enum Province
    {
        Punjab = 1,
        Sindh = 2,
		Balochistan = 3,
		Kpk = 4,
		GilgitBaltistan = 5
	}

    public enum Country
    {
        Pakistan = 1
    }
}