using unihackAPI.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace ResoApi
{
    public class SQLCommands
    {
        public static SqlCommand GetUsers()
        {
            SqlCommand cmd = new SqlCommand();

            string str = "select * from [dbo].[AspNetUsers]";
            cmd.CommandText = str;

            return cmd;        
        }
        #region Bin
        public static SqlCommand GetBins()
        {
            SqlCommand cmd = new SqlCommand();

            string str = @"select B.Id, B.Capacity, B.Latitude, B.Longitude, B.Name, B.Type, B.Zone, MA.ManagerId, U.Name as ManagerName from [dbo].[Bins] B
                            inner join [dbo].[ManagerArea] MA on B.Zone = MA.Zone
                            inner join [dbo].[AspNetUsers] U on MA.ManagerId = U.Id";
            cmd.CommandText = str;

            return cmd;
        }
        public static SqlCommand GetBin(Guid Id)
        {
            SqlCommand cmd = new SqlCommand();

            string str = "select * from [dbo].[Bins] where Id = @ID";
            cmd.CommandText = str;
            cmd.Parameters.Add("@ID", SqlDbType.UniqueIdentifier).Value = Id;

            return cmd;
        }
        public static SqlCommand AddBin(BinModel model)
        {
            SqlCommand cmd = new SqlCommand();

            string str = "insert into [dbo].[Bins] (Id, Name, Type, Latitude, Longitude, Capacity, Zone) values (@mId, @mName, @mType, @mLatitude, @mLongitude, @mCapacity, @mZone)";
            cmd.CommandText = str;
            cmd.Parameters.Add("@mId", SqlDbType.UniqueIdentifier).Value = model.Id;
            cmd.Parameters.Add("@mName", SqlDbType.NVarChar).Value = model.Name;
            cmd.Parameters.Add("@mType", SqlDbType.Int).Value = model.Type;
            cmd.Parameters.Add("@mLatitude", SqlDbType.Float).Value = model.Latitude;
            cmd.Parameters.Add("@mLongitude", SqlDbType.Float).Value = model.Longitude;
            cmd.Parameters.Add("@mCapacity", SqlDbType.Float).Value = model.Capacity;
            cmd.Parameters.Add("@mZone", SqlDbType.Int).Value = model.Zone;

            return cmd;
        }
        public static SqlCommand DeleteBin(Guid Id)
        {
            SqlCommand cmd = new SqlCommand();

            string str = "delete from [dbo].[Bins] where Id = @ID";
            cmd.CommandText = str;
            cmd.Parameters.Add("@ID", SqlDbType.UniqueIdentifier).Value = Id;

            return cmd;
        }
        public static SqlCommand GetCollectorsByBin(Guid Id)
        {
            SqlCommand cmd = new SqlCommand();
            string str = @"SELECT BUA.[UserId] as Id, U.Name as Name FROM[dbo].[BinUserAssociation] BUA
                             inner join[dbo].[AspNetUsers] U on BUA.UserId = U.Id where BinId = @mID";
            cmd.CommandText = str;
            cmd.Parameters.Add("@mId", SqlDbType.UniqueIdentifier).Value = Id;

            return cmd;
        }
        #endregion Bin

    }
}