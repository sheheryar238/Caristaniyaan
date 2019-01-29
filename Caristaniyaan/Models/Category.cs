using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Caristaniyaan.Models
{
    public class Category
    {
        public int Id { get; set; }

        [Required]
        public string name { get; set; }

        [Required]
        public DateTime date { get; set; }
    }
}