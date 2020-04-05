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
    public class UserRepository : IRepository<APN_User, int>
    {
        //APN user list
        private List<APN_User> users = new List<APN_User>();

        //Db reference
        private DatabaseConnection db = new DatabaseConnection();

        public UserRepository()
        {
            SqlConnection conn = db.GetConnection();
            SqlDataAdapter da = new SqlDataAdapter("pr_APN_LoadUser", conn);
            da.SelectCommand.CommandType = CommandType.StoredProcedure;
            DataSet dat_set = new DataSet();
            da.Fill(dat_set);
            conn.Close();

            users = dat_set.Tables[0].AsEnumerable().Select(
                                DataRow => new APN_User
                                {
                                    id = DataRow.Field<int>("id"),
                                    name = DataRow.Field<string>("name"),
                                    email = DataRow.Field<string>("email")
                                }).ToList();
        }

        public List<APN_User> GetAllData()
        {
            return users;
        }

        public APN_User GetUniqueData(int id)
        {
            var user = users.FirstOrDefault(x => x.id == id);
            return user;
        }

        public void UpdateRecord(int id, APN_User user)
        {
            SqlConnection conn = db.GetConnection();
            SqlCommand cmd = conn.CreateCommand();
            cmd.CommandText = "Execute pr_ORA_UpdateUser @id,@name,@email,@phone";
            cmd.Parameters.Add("@id", SqlDbType.Int).Value = id;
            cmd.Parameters.Add("@name", SqlDbType.VarChar, 100).Value = user.name;
            cmd.Parameters.Add("@email", SqlDbType.VarChar, 100).Value = user.email;
            cmd.ExecuteNonQuery();
            conn.Close();
        }

        public void SaveData(APN_User user)
        {
            SqlConnection conn = db.GetConnection();
            SqlCommand cmd = conn.CreateCommand();
            cmd.CommandText = "Execute pr_ORA_AddUser @name,@email,@phone";
            cmd.Parameters.Add("@name", SqlDbType.VarChar, 100).Value = user.name;
            cmd.Parameters.Add("@email", SqlDbType.VarChar, 100).Value = user.email;
            cmd.ExecuteNonQuery();
            conn.Close();
        }

        public void DeleteRecord(int id)
        {
            SqlConnection conn = db.GetConnection();
            SqlCommand cmd = conn.CreateCommand();
            cmd.CommandText = "Execute pr_ORA_DeleteUer @id";
            cmd.Parameters.Add("@id", SqlDbType.Int).Value = id;
            cmd.ExecuteNonQuery();
            conn.Close();
        }
    }
}
