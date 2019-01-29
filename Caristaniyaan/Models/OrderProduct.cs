using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Caristaniyaan.Models
{
    public class OrderProduct
    {
        public int Id { get; set; }

        [Required]
        public int OrderId { get; set; }
        public Order Order { get; set; }

        [Required]
        public int ProductId { get; set; }
        public Product Product { get; set; }

        [Required]
        public int quantity { get; set; }

        [Required]
        public int currentPrice { get; set; }

        [Required]
        public DateTime date { get; set; }
    }
}