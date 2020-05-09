using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace APNCarSaleDataService.Models
{
    /// <summary>
    /// Files upload
    /// </summary>
    public class APN_Files
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string ContentType { get; set; }
        public byte[] ImageBytes { get; set; }
        public int Cid { get; set; }
        public int Sid { get; set; }
    }
}
