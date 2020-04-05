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
    class CategoryRepository : IRepository<APN_Category, int>
    {
        //APN user list
        private List<APN_Category> categorys = new List<APN_Category>();

        //Db reference
        private DatabaseConnection db = new DatabaseConnection();

        public CategoryRepository()
        {
            SqlConnection conn = db.GetConnection();
            SqlDataAdapter da = new SqlDataAdapter("pr_APN_LoadUser", conn);
            da.SelectCommand.CommandType = CommandType.StoredProcedure;
            DataSet dat_set = new DataSet();
            da.Fill(dat_set);
            conn.Close();

            categorys = dat_set.Tables[0].AsEnumerable().Select(
                                DataRow => new APN_Category
                                {
                                    id = DataRow.Field<int>("id"),
                                    name = DataRow.Field<string>("name"),
                                    description = DataRow.Field<string>("description")
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
            cmd.CommandText = "Execute pr_ORA_AddUser @name,@email,@phone";
            cmd.Parameters.Add("@name", SqlDbType.VarChar, 100).Value = category.name;
            cmd.Parameters.Add("@email", SqlDbType.VarChar, 100).Value = category.description;
            cmd.ExecuteNonQuery();
            conn.Close();
        }

        public void UpdateRecord(int id, APN_Category entity)
        {
            throw new NotImplementedException();
        }
        public void DeleteRecord(int id)
        {
            throw new NotImplementedException();
        }

    }
}
