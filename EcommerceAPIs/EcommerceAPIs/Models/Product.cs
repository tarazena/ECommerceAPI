using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EcommerceAPIs.Models
{
    public class Product
    {
        public string id { get; set; }
        public string name { get; set; }
        public string price { get; set; }
        public string memory { get; set; }
        public string company { get; set; }
        public List<string> images = new List<string>();
    }
}