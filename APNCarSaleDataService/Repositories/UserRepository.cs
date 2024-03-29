﻿using APN_Data_Service.DataAccess;
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
                                    UserId = DataRow.Field<int>("UserId"),
                                    FirstName = DataRow.Field<string>("FirstName"),
                                    Email = DataRow.Field<string>("Email"),
                                    Password = DataRow.Field<string>("Password"),
                                    PasswordSalt = DataRow.Field<string>("PasswordSalt")
                                }).ToList();
        }

        public List<APN_User> GetAllData()
        {
            return users;
        }

        public APN_User GetUniqueData(int id)
        {
            var user = users.FirstOrDefault(x => x.UserId == id);
            return user;
        }

        public void UpdateRecord(int id, APN_User user)
        {
            SqlConnection conn = db.GetConnection();
            SqlCommand cmd = conn.CreateCommand();
            cmd.CommandText = "Execute pr_ORA_UpdateUser @id,@name,@email,@phone";
            cmd.Parameters.Add("@id", SqlDbType.Int).Value = id;
            cmd.Parameters.Add("@name", SqlDbType.VarChar, 100).Value = user.FirstName;
            cmd.Parameters.Add("@email", SqlDbType.VarChar, 100).Value = user.Email;
            cmd.ExecuteNonQuery();
            conn.Close();
        }

        public void SaveData(APN_User user)
        {
            SqlConnection conn = db.GetConnection();
            SqlCommand cmd = conn.CreateCommand();
            cmd.CommandText = "Execute pr_APN_AddUser @name,@email,@password,@passwordSalt,@userType,@createdDate,@isActive";
            cmd.Parameters.Add("@name", SqlDbType.VarChar, 150).Value = user.FirstName;
            cmd.Parameters.Add("@email", SqlDbType.VarChar, 150).Value = user.Email;
            cmd.Parameters.Add("@password", SqlDbType.VarChar, -1).Value = user.Password;
            cmd.Parameters.Add("@passwordSalt", SqlDbType.VarChar, -1).Value = user.PasswordSalt;
            cmd.Parameters.Add("@userType", SqlDbType.VarChar, -1).Value = user.UserType;
            cmd.Parameters.Add("@createdDate", SqlDbType.DateTime).Value = user.CreatedDate;
            cmd.Parameters.Add("@isActive", SqlDbType.Bit).Value = user.IsActive;
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
