using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Caristaniyaan.Models
{
    public class Product
    {
        public int Id { get; set; }

        [Required]
        public int BrandId { get; set; }
        public Brand Brand { get; set; }

        [Required]
        public string name { get; set; }

        [Required]
        public int price { get; set; }

        [Required]
        public int whileSalePrice { get; set; }

        [Required]
        public string color { get; set; }

        [Required]
        public int quantity { get; set; }

        public int? model { get; set; }

        [Required]
        public string details { get; set; }

        [Required]
        public Status status { get; set; }

        [Required]
        public string image_url { get; set; }

        [Required]
        public int priority { get; set; }

        [Required]
        public DateTime date { get; set; }
    }

    public enum Status {
        pending,
        delivered
    }
}