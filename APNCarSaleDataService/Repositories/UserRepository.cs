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
    public class UserRepository : IUserRepository
    {
        //APN user list
        private List<APN_User> users = new List<APN_User>();
        //Db reference
        private DatabaseConnection db = new DatabaseConnection();
        public UserRepository()
        {
            SqlConnection conn = db.GetConnection();
            SqlDataAdapter da = new SqlDataAdapter("pr_ORA_LoadUser", conn);
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

        public List<APN_User> GetAllUsers()
        {
            return users;
        }

        public APN_User GetUser(int id)
        {
            throw new NotImplementedException();
        }
    }
}
