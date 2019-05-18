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

            string str = @"select B.Id, B.Capacity, B.Latitude, B.Longitude, B.Name, B.Type, B.Zone, Z.Name as ZoneName, MA.ManagerId, U.Name as ManagerName from [dbo].[Bins] B
                            inner join [dbo].[ManagerArea] MA on B.Zone = MA.Zone
                            inner join [dbo].[AspNetUsers] U on MA.ManagerId = U.Id
							inner join [dbo].[Zone] Z on B.Zone= Z.ZoneId";
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

        public static SqlCommand GetBinsByZone(int Id)
        {
            SqlCommand cmd = new SqlCommand();

            string str = @"select B.Id, B.Capacity, B.Latitude, B.Longitude, B.Name, B.Type, B.Zone, Z.Name as ZoneName, MA.ManagerId, U.Name as ManagerName from [dbo].[Bins] B
                            inner join [dbo].[ManagerArea] MA on B.Zone = MA.Zone
                            inner join [dbo].[AspNetUsers] U on MA.ManagerId = U.Id
							inner join [dbo].[Zone] Z on B.Zone= Z.ZoneId
                            where B.Zone=@mId;";
            cmd.CommandText = str;
            cmd.Parameters.Add("@mId", SqlDbType.Int).Value = Id;

            return cmd;
        }
        public static SqlCommand GetBinsByManager(string Id)
        {
            SqlCommand cmd = new SqlCommand();

            string str = @"select bins.Id, bins.Name, bins.Type, bins.Latitude, bins.Longitude, bins.Capacity, manager.ManagerId, users.Name as ManagerName, zone.ZoneId, zone.Name as ZoneName
                            from [dbo].[ManagerArea] as manager
                            inner join [dbo].[Bins] as bins
                            on manager.Zone = bins.Zone

                            inner join [dbo].[AspNetUsers] as users
                            on manager.ManagerId = users.Id

                            inner join [dbo].[Zone] as zone
                            on manager.Zone = zone.ZoneId
                            where ManagerId=@mId;";
            cmd.CommandText = str;
            cmd.Parameters.Add("@mId", SqlDbType.NVarChar).Value = Id;

            return cmd;
        }

        public static SqlCommand UpdateBin(Guid Id, float Capacity)
        {
            SqlCommand cmd = new SqlCommand();
            string str = @"update [dbo].[Bins] set Capacity=@mCapacity where Id=@mId";
            cmd.CommandText = str;
            cmd.Parameters.Add("@mId", SqlDbType.UniqueIdentifier).Value = Id;
            cmd.Parameters.Add("@mCapacity", SqlDbType.Float).Value = Capacity;

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
        
        public static SqlCommand GetCollectorsByManager(string Id)
        {
            SqlCommand cmd = new SqlCommand();
            string str = @"select MCA.ColectorId as Id, U.Name as Name from [dbo].[ManagerCollectorAssociation] MCA
                inner join [dbo].[AspNetUsers] U on MCA.ColectorId = U.Id
                where  ManagerId=@mId";
            cmd.Parameters.Add("@mId", SqlDbType.NVarChar).Value = Id;
            cmd.CommandText = str;

            return cmd;
        }

        #endregion Bin

        public static SqlCommand GetManagers()
        {
            SqlCommand cmd = new SqlCommand();
            string str = @"select U.Id, U.Name, U.Email, Ma.Zone as ZoneId, Z.Name as ZoneName from [dbo].[AspNetUsers] U
                inner join [dbo].[ManagerArea] MA on U.Id = MA.ManagerId
                inner join [dbo].[Zone] Z on MA.Zone = Z.ZoneId";
            cmd.CommandText = str;

            return cmd;
        }

    }
}