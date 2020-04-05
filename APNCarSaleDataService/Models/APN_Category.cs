using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APNCarSaleDataService.Models
{
    public class APN_Category
    {
        public int id { get; set; }
        public string name { get; set; }
        public string description { get; set; }

    }

    public class APN_SubCategory : APN_Category
    {
        public int SId { get; set; }
        public string SName { get; set; }
        public string SDescription { get; set; }
    }
}
