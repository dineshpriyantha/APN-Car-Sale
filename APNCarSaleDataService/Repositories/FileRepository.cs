using APN_Data_Service.DataAccess;
using APNCarSaleDataService.Interfaces;
using APNCarSaleDataService.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APNCarSaleDataService.Repositories
{
    public class FileRepository : IRepository<APN_Files, int>
    {
        private List<APN_Files> vehicle = new List<APN_Files>();

        private DatabaseConnection db = new DatabaseConnection();
        public void DeleteRecord(int id)
        {
            throw new NotImplementedException();
        }

        public List<APN_Files> GetAllData()
        {
            throw new NotImplementedException();
        }

        public APN_Files GetUniqueData(int id)
        {
            throw new NotImplementedException();
        }

        public void SaveData(APN_Files entity)
        {
            //foreach (var item in entity)
            //{
            //    SaveFileDetails(item);
            //}
            throw new NotImplementedException();
        }

        public void SaveFileDetails(APN_Files file)
        {
            SqlConnection conn = db.GetConnection();
            SqlCommand cmd = conn.CreateCommand();
            cmd.CommandText = "Execute pr_APN_AddFiles @filename,@contentType,@imageBytes,@cid,@sid";
            cmd.Parameters.Add("@filename", SqlDbType.VarChar, 50).Value = file.Name;
            cmd.Parameters.Add("@contentType", SqlDbType.VarChar, 50).Value = file.ContentType;
            cmd.Parameters.Add("@imageBytes", SqlDbType.VarBinary, -1).Value = file.ImageBytes;
            cmd.Parameters.Add("@cid", SqlDbType.Int).Value = file.Cid;
            cmd.Parameters.Add("@sid", SqlDbType.Int).Value = file.Sid;
            cmd.ExecuteNonQuery();
            conn.Close();
        }

        public void UpdateRecord(int id, APN_Files entity)
        {
            throw new NotImplementedException();
        }
    }
}
