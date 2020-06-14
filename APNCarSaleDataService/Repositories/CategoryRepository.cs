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
    public class CategoryRepository : IRepository<APN_Category, int>
    {
        //APN user list
        private List<APN_Category> categorys = new List<APN_Category>();

        //Db reference
        private DatabaseConnection db = new DatabaseConnection();

        public CategoryRepository()
        {
            SqlConnection conn = db.GetConnection();
            SqlDataAdapter da = new SqlDataAdapter("pr_APN_LoadCategory", conn);
            da.SelectCommand.CommandType = CommandType.StoredProcedure;
            DataSet dat_set = new DataSet();
            da.Fill(dat_set);
            conn.Close();

            categorys = dat_set.Tables[0].AsEnumerable().Select(
                                DataRow => new APN_Category
                                {
                                    id = DataRow.Field<int>("id"),
                                    name = DataRow.Field<string>("name"),
                                    description = DataRow.Field<string>("description"),
                                    priority = DataRow.Field<int>("priority")
                                }).ToList();
        }

        public List<APN_Category> GetAllData()
        {
            return categorys;
        }

        public APN_Category GetUniqueData(int id)
        {
            var category = categorys.FirstOrDefault(x => x.id == id);
            return category;
        }

        public void SaveData(APN_Category category)
        {
            SqlConnection conn = db.GetConnection();
            SqlCommand cmd = conn.CreateCommand();
            cmd.CommandText = "Execute pr_APN_AddCategory @name,@description,@priority";
            cmd.Parameters.Add("@name", SqlDbType.VarChar, 100).Value = category.name;
            cmd.Parameters.Add("@description", SqlDbType.VarChar, 100).Value = category.description;
            cmd.Parameters.Add("@priority", SqlDbType.Int).Value = category.priority;
            cmd.ExecuteNonQuery();
            conn.Close();
        }

        public void UpdateRecord(int id, APN_Category category)
        {
            SqlConnection conn = db.GetConnection();
            SqlCommand cmd = conn.CreateCommand();
            cmd.CommandText = "Execute pr_APN_UpdateCategory @id,@name,@description,@priority";
            cmd.Parameters.Add("@id", SqlDbType.VarChar, 100).Value = id;
            cmd.Parameters.Add("@name", SqlDbType.VarChar, 100).Value = category.name;
            cmd.Parameters.Add("@description", SqlDbType.VarChar, 100).Value = category.description;
            cmd.Parameters.Add("@priority", SqlDbType.Int).Value = category.priority;
            cmd.ExecuteNonQuery();
            conn.Close();
        }
        public void DeleteRecord(int id)
        {
            SqlConnection conn = db.GetConnection();
            SqlCommand cmd = conn.CreateCommand();
            cmd.CommandText = "Execute pr_APN_DeleteCategory @id";
            cmd.Parameters.Add("@id", SqlDbType.VarChar, 100).Value = id;
            cmd.ExecuteNonQuery();
            conn.Close();
        }

    }
}
