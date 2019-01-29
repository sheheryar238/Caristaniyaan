using Caristaniyaan.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Caristaniyaan.Dto
{
    public class productViewModal
    {
        [Required]
        public int Id { get; set; }

        [Required]
        [Display(Name = "Brand")]
        public int BrandId { get; set; }

        [Required]
        [Display(Name = "Product Name")]
        public string name { get; set; }

        [Required]
        [Display(Name = "Product Price")]
        public int price { get; set; }

        [Required]
        [Display(Name = "Product Whole Sale Price")]
        public int whileSalePrice { get; set; }

        [Required]
        [Display(Name = "Product Color")]
        public string color { get; set; }

        [Required]
        [Display(Name = "Product Quantity")]
        public int quantity { get; set; }

        [Display(Name = "Product Model")]
        public int? car { get; set; }

        [Required]
        [Display(Name = "Product Detail")]
        public string details { get; set; }

        [Required]
        [Display(Name = "Status")]
        public Status status { get; set; }

        [Required]
        [Display(Name = "Product Image")]
        public HttpPostedFileWrapper image { get; set; }

        [Required]
        [Display(Name = "Priority")]
        public int priority { get; set; }

        [Required]
        public DateTime date { get; set; }
        public string imageurl { get; set; }

        public IEnumerable<Brand> brand { get; set; }
    }
}