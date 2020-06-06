using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APNCarSaleDataService.Models
{
    public class APN_IdentityRole
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public virtual ICollection<APN_User> Users { get; set; }
    }
}
