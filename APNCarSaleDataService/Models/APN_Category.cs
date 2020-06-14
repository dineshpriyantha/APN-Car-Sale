using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
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
        public int? priority { get; set; }
    }

    public class APN_SubCategory
    {
        public int SId { get; set; }
        public string SName { get; set; }
        public string SDescription { get; set; }
        public string name { get; set; }
        public int Cid { get; set; }
    }
}
