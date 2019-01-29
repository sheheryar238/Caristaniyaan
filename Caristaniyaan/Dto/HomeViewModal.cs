using Caristaniyaan.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Caristaniyaan.Dto
{
    public class HomeViewModal
    {
        public IEnumerable<Product> product { get; set; }
    }
}