using APNCarSaleDataService.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace APN_Car_Sale.Models
{
    public class ViewModel
    {
        public IEnumerable<APN_Vehicle> vehicles { get; set; }
        public IEnumerable<APN_Category> categories { get; set; }
        public IEnumerable<APN_SubCategory> subcategories { get; set; }
        public IEnumerable<HttpPostedFileBase> File { get; set; }
    }


    public class CommonViewModel
    {
        public APN_Vehicle Vehicle { get; set; }
        public string ModelName { get; set; }
        public IEnumerable<HttpPostedFileBase> files { get; set; }
        public IEnumerable<APN_Files> File { get; set; }
    }
}