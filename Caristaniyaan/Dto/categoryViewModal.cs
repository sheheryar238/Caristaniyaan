using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Caristaniyaan.Dto
{
    public class categoryViewModal
    {

        [Required]
        [Display(Name ="Category Name")]
        public string name { get; set; }
    }
}