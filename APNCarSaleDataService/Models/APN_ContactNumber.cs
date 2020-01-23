using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APNCarSaleDataService.Models
{
    public class APN_ContactNumber
    {
        public int Id { get; set; }
        public string Number { get; set; }
        public IEnumerable<APN_Category> Categories { get; set; }
    }
}
