using APN_Data_Service.DataAccess;
using APNCarSaleDataService.Interfaces;
using APNCarSaleDataService.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace APNCarSaleDataService.Repositories
{
    public class VehicleRepository : IRepository<APN_Vehicle, int>
    {
        private List<APN_Vehicle> vehicle = new List<APN_Vehicle>();

        private DatabaseConnection db = new DatabaseConnection();

        private SqlConnection conn;

        public VehicleRepository()
        {
            conn = db.GetConnection();
            SqlDataAdapter da = new SqlDataAdapter("pr_APN_LoadVehicle", conn);
            da.SelectCommand.CommandType = CommandType.StoredProcedure;
            DataSet ds = new DataSet();
            da.Fill(ds);
            conn.Close();

            vehicle = ds.Tables[0].AsEnumerable().Select(
                      DataRow => new APN_Vehicle
                      {
                          Id = DataRow.Field<int>("id"),
                          Brand = DataRow.Field<string>("brand"),
                          Model = DataRow.Field<string>("model"),
                          ModelYear = DataRow.Field<string>("modelYear"),
                          Price = DataRow.Field<string>("price"),
                          Description = DataRow.Field<string>("description"),
                          ContactNumber = DataRow.Field<string>("contactNumber"),
                          IsNegotiate = DataRow.Field<bool>("isNegotiate"),
                          HideNumber = DataRow.Field<bool>("hideNumber"),
                          Cid = DataRow.Field<int>("Cid"),
                          Subid = DataRow.Field<int>("Subid")
                      }).ToList();
        }

        public List<APN_Vehicle> GetAllData()
        {
            return vehicle;
        }

        public APN_Vehicle GetUniqueData(int id)
        {
            throw new NotImplementedException();
        }

        public void SaveData(APN_Vehicle vehicle)
        {
            conn.Open();
            SqlCommand cmd = conn.CreateCommand();
            cmd.CommandText = "Execute pr_APN_AddVehicle @brand,@model,@modelYear,@price,@description,@contactNumber,@isNegotiate,@hideNumber,@Cid,@Subid,@Email";
            cmd.Parameters.Add("@brand", SqlDbType.VarChar, 50).Value = vehicle.Brand != null ? vehicle.Brand : "";
            cmd.Parameters.Add("@model", SqlDbType.VarChar, 50).Value = vehicle.Model != null ? vehicle.Model : "";
            cmd.Parameters.Add("@modelYear", SqlDbType.VarChar, 20).Value = vehicle.ModelYear != null ? vehicle.ModelYear : "";
            cmd.Parameters.Add("@price", SqlDbType.VarChar, 20).Value = vehicle.Price != null ? vehicle.Price : "";
            cmd.Parameters.Add("@description", SqlDbType.VarChar, -1).Value = vehicle.Description != null ? vehicle.Description : "";
            cmd.Parameters.Add("@contactNumber", SqlDbType.VarChar, 20).Value = vehicle.ContactNumber != null ? vehicle.ContactNumber : "";
            cmd.Parameters.Add("@isNegotiate", SqlDbType.Bit).Value = vehicle.IsNegotiate;
            cmd.Parameters.Add("@hideNumber", SqlDbType.Bit).Value = vehicle.HideNumber;
            cmd.Parameters.Add("@Cid", SqlDbType.Int).Value = vehicle.Cid;
            cmd.Parameters.Add("@Subid", SqlDbType.Int).Value = vehicle.Subid;
            cmd.Parameters.Add("@Email", SqlDbType.VarChar, 50).Value = vehicle.Email != null ? vehicle.Email : "";
            int result = cmd.ExecuteNonQuery();
            conn.Close();

            if (result > 0)
            {
                foreach (var item in vehicle.File)
                {
                    SaveFileDetails(item);
                }
            }
        }

        public void SaveFileDetails(APN_Files file)
        {
            conn.Open();
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

        public byte[] ConvertToBytes(HttpPostedFileBase file)
        {
            byte[] imageBytes = null;
            BinaryReader reader = new BinaryReader(file.InputStream);
            imageBytes = reader.ReadBytes((int)file.ContentLength);
            return imageBytes;
        }

        public void DeleteRecord(int id)
        {
            throw new NotImplementedException();
        }

        public void UpdateRecord(int id, APN_Vehicle entity)
        {
            throw new NotImplementedException();
        }
    }
}
