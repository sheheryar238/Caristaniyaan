using Caristaniyaan.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Caristaniyaan.Dto
{
    public class subCategoryViewModal
    {
        [Required]
        [Display(Name ="Select Category")]
        public int CategoryId { get; set; }
        public Category Category { get; set; }

        [Required]
        [Display(Name = "Sub Category Name")]
        public string name { get; set; }

        public IEnumerable<Category> categories { get; set; }

        [Required]
        public DateTime date { get; set; }
    }
}