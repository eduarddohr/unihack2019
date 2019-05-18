using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace Unihack.Web
{
    public class SQLCommands
    {
        public static SqlCommand AddCollectorManagerAssociation(string collectorId, string managerId)
        {
            SqlCommand cmd = new SqlCommand();

            string str = "insert into [dbo].[ManagerCollectorAssociation] (ManagerId, ColectorId) values (@mManagerId, @mColectorId)";
            cmd.CommandText = str;
            cmd.Parameters.Add("@mColectorId", SqlDbType.NVarChar).Value = collectorId;
            cmd.Parameters.Add("@mManagerId", SqlDbType.NVarChar).Value = managerId;

            return cmd;
        }

        public static SqlCommand AddZone(string zone)
        {
            SqlCommand cmd = new SqlCommand();

            string str = "insert into [dbo].[Zone] (Name) values (@mZone)";
            cmd.CommandText = str;
            cmd.Parameters.Add("@mZone", SqlDbType.NVarChar).Value = zone;

            return cmd;
        }

        public static SqlCommand AddManagerZoneAssociation(string managerId, int zoneId)
        {
            SqlCommand cmd = new SqlCommand();

            string str = "insert into [dbo].[ManagerArea] (ManagerId, ColectorId) values (@mManagerId, @mZone)";
            cmd.CommandText = str;
            cmd.Parameters.Add("@mManagerId", SqlDbType.NVarChar).Value = managerId;
            cmd.Parameters.Add("@mZone", SqlDbType.NVarChar).Value = zoneId;

            return cmd;
        }

        public static SqlCommand GetUserEmail(string email)
        {
            SqlCommand cmd = new SqlCommand();

            string str = "select * from [dbo].[AspNetUsers] where Email = @Email";
            cmd.CommandText = str;
            cmd.Parameters.Add("@Email", SqlDbType.NVarChar).Value = email;

            return cmd;
        }

        public static SqlCommand GetZoneName(string zoneName)
        {
            SqlCommand cmd = new SqlCommand();

            string str = "select * from [dbo].[Zone] where Name = @Name";
            cmd.CommandText = str;
            cmd.Parameters.Add("@Name", SqlDbType.NVarChar).Value = zoneName;

            return cmd;
        }

       
       
    }
}