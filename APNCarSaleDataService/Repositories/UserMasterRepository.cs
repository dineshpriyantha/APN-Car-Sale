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
    public class UserMasterRepository : IRepository<APN_UserMaster, int>
    {
        private List<APN_UserMaster> users = new List<APN_UserMaster>();

        //Db reference
        private DatabaseConnection db = new DatabaseConnection();

        public UserMasterRepository()
        {
            SqlConnection conn = db.GetConnection();
            SqlDataAdapter da = new SqlDataAdapter("pr_APN_LoadUserMaster", conn);
            da.SelectCommand.CommandType = CommandType.StoredProcedure;
            DataSet dat_set = new DataSet();
            da.Fill(dat_set);
            conn.Close();

            users = dat_set.Tables[0].AsEnumerable().Select(
                                DataRow => new APN_UserMaster
                                {
                                    UserID = DataRow.Field<int>("userID"),
                                    UserName = DataRow.Field<string>("userName"),
                                    UserPassword = DataRow.Field<string>("userPassword"),
                                    UserRoles = DataRow.Field<string>("userRoles"),
                                    UserEmailID = DataRow.Field<string>("userEmailID")
                                }).ToList();
        }


        public List<APN_UserMaster> GetAllData()
        {
            return users;
        }

        public APN_UserMaster GetUniqueData(int id)
        {
            var user = users.FirstOrDefault(x => x.UserID == id);
            return user;
        }

        public void SaveData(APN_UserMaster entity)
        {
            throw new NotImplementedException();
        }

        public void DeleteRecord(int id)
        {
            throw new NotImplementedException();
        }

        public void UpdateRecord(int id, APN_UserMaster entity)
        {
            throw new NotImplementedException();
        }
    }
}
