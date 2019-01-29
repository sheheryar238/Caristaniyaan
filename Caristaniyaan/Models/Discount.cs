using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Caristaniyaan.Models
{
    public class Discount
    {
        public int Id { get; set; }

        [Required]
        public int ProductId { get; set; }
        public Product Product { get; set; }

        [Required]
        public int discountAmount { get; set; }

        [Required]
        public DateTime startdate { get; set; }

        [Required]
        public DateTime enddate { get; set; }

        [Required]
        public DateTime date { get; set; }
    }
}