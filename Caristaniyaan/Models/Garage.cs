using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Caristaniyaan.Models
{
    public class Garage
    {
        public int Id { get; set; }

        [Required]
        public string name { get; set; }

        [Required]
        public string model { get; set; }

        [Required]
        public string chassisno { get; set; }


        [Required]
        public string details { get; set; }

        [Required]
        public Status status { get; set; }

        [Required]
        public int priority { get; set; }

        [Required]
        public DateTime date { get; set; }
    }

}