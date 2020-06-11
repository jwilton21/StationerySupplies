using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Policy;
using System.Threading.Tasks;

namespace StationerySupplies.WebSite.Models
{
    public class Product
    {
        public String Id { get; set; }
        public String Maker { get; set; }
        public  String Image { get; set; }

        public String Url { get; set; }
        public String Title { get; set; }
        public String Description { get; set; }
        public int[] Ratings { get; set; }

    }
}
