using Caristaniyaan.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Caristaniyaan.Dto
{
    public class brandViewModal
    {
        [Required]
        [Display(Name ="Select Sub Cateogry")]
        public int SubCategoryId { get; set; }
        public SubCategory SubCategory { get; set; }

        [Required]
        [Display(Name = "Brand Name")]
        public string name { get; set; }

        public IEnumerable<SubCategory> subcategories { get; set; }

    }
}