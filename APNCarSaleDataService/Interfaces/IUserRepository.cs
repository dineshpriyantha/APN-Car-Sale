using APNCarSaleDataService.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APNCarSaleDataService.Interfaces
{
    /// <summary>
    /// APN User Interface
    /// </summary>
    public interface IUserRepository
    {
        List<APN_User> GetAllUsers();
        APN_User GetUser(int id);
    }
}
