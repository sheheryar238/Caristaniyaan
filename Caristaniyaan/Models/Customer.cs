using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Caristaniyaan.Models
{
    public class Customer
    {
        public int CustomerId { get; set; }

        public string name { get; set; }

        public string email { get; set; }

        public string password { get; set; }

        public string address { get; set; }

        public Status status { get; set; }

        public DateTime date { get; set; }
    }
}