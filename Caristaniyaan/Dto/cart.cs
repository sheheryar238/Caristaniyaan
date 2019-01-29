using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Caristaniyaan.Dto
{
    public class cart
    {
        public int Id { get; set; }
        public string Pic { get; set; }
        public string Name { get; set; }
        public int Price { get; set; }
        public string Color { get; set; }
        public string Car { get; set; }
        public int Quantity { get; set; }
        public int Total { get; set; }
        public string Link { get; set; }

    }
}