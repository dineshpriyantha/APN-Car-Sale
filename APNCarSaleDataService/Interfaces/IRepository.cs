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
    public interface IRepository<TEntity, Tpk> where TEntity : class
    {
        List<TEntity> GetAllData();
        TEntity GetUniqueData(Tpk id);
        void SaveData(TEntity entity);
        void DeleteRecord(Tpk id);
        void UpdateRecord(Tpk id, TEntity entity);
    }
}
