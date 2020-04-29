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
    public class SubCategoryRepository : IRepository<APN_SubCategory, int>
    {
        //APN user list
        private List<APN_SubCategory> subcategorys = new List<APN_SubCategory>();

        //Db reference
        private DatabaseConnection db = new DatabaseConnection();

        public SubCategoryRepository()
        {
            SqlConnection conn = db.GetConnection();
            SqlDataAdapter da = new SqlDataAdapter("pr_APN_LoadSubCategory", conn);
            da.SelectCommand.CommandType = CommandType.StoredProcedure;
            DataSet dat_set = new DataSet();
            da.Fill(dat_set);
            conn.Close();

            subcategorys = dat_set.Tables[0].AsEnumerable().Select(
                                DataRow => new APN_SubCategory
                                {
                                    SId = DataRow.Field<int>("id"),
                                    SName = DataRow.Field<string>("subname"),
                                    SDescription = DataRow.Field<string>("description"),
                                    name = DataRow.Field<string>("name"),
                                    Cid = DataRow.Field<int>("Cid")
                                }).ToList();
        }

        public List<APN_SubCategory> GetAllData()
        {
            return subcategorys;
        }

        public APN_SubCategory GetUniqueData(int id)
        {
            var category = subcategorys.FirstOrDefault(x => x.SId == id);
            return category;
        }

        public void SaveData(APN_SubCategory category)
        {
            SqlConnection conn = db.GetConnection();
            SqlCommand cmd = conn.CreateCommand();
            cmd.CommandText = "Execute pr_APN_AddSubCategory @name,@description,@cid";
            cmd.Parameters.Add("@name", SqlDbType.VarChar, 100).Value = category.SName;
            cmd.Parameters.Add("@description", SqlDbType.VarChar, 100).Value = category.SDescription;
            cmd.Parameters.Add("@cid", SqlDbType.Int).Value = category.Cid;
            cmd.ExecuteNonQuery();
            conn.Close();
        }

        public void UpdateRecord(int id, APN_SubCategory category)
        {
            SqlConnection conn = db.GetConnection();
            SqlCommand cmd = conn.CreateCommand();
            cmd.CommandText = "Execute pr_APN_UpdateSubCategory @id,@name,@description,@cid";
            cmd.Parameters.Add("@id", SqlDbType.VarChar, 100).Value = id;
            cmd.Parameters.Add("@name", SqlDbType.VarChar, 100).Value = category.SName;
            cmd.Parameters.Add("@description", SqlDbType.VarChar, 100).Value = category.SDescription;
            cmd.Parameters.Add("@cid", SqlDbType.Int).Value = category.Cid;
            cmd.ExecuteNonQuery();
            conn.Close();
        }
        public void DeleteRecord(int id)
        {
            SqlConnection conn = db.GetConnection();
            SqlCommand cmd = conn.CreateCommand();
            cmd.CommandText = "Execute pr_APN_DeleteSubCategory @id";
            cmd.Parameters.Add("@id", SqlDbType.Int).Value = id;
            cmd.ExecuteNonQuery();
            conn.Close();
        }
    }
}
