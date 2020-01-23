using APNCarSaleDataService.Interfaces;
using APNCarSaleDataService.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APNCarSaleDataService.Repositories
{
    public class FilesRepository : IRepository<APN_Files, int>
    {
        public List<APN_Files> GetAllData()
        {
            throw new NotImplementedException();
        }

        public APN_Files GetUniqueData(int id)
        {
            throw new NotImplementedException();
        }

        public void SaveData()
        {
            throw new NotImplementedException();
        }
    }
}
