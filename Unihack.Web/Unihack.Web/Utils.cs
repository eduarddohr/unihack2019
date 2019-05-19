using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;
using System.Reflection;
using System.Net.Mail;
using System.Net;

namespace Unihack.Web
{
    public static class Utils
    {
        public static string msSQLConnectionStringDesktopApps = "Server = tcp:db-test-stud.database.windows.net,1433;Initial Catalog = unihack; Persist Security Info=False;User ID =db_admin; Password=q!w2e3r4; MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout = 30";
        public static string ExecuteScalar(SqlCommand commd)
        {
            SqlConnection conn = new SqlConnection(msSQLConnectionStringDesktopApps);
            try
            {

                commd.Connection = conn;
                if (conn.State == ConnectionState.Closed)
                {
                    conn.Open();
                }
                var value = commd.ExecuteScalar();
                if (value != null)
                {
                    return value.ToString();
                }
                else
                { return ""; }

            }
            catch (Exception ex)
            { return "Error when getting data:" + ex.Message; }
            finally
            { conn.Close(); }
        }
        public static string ExecuteNonQuery(SqlCommand commd)
        {
            SqlConnection conn = new SqlConnection(msSQLConnectionStringDesktopApps);
            try
            {
                commd.Connection = conn;
                if (conn.State == ConnectionState.Closed)
                {
                    conn.Open();
                }
                var value = commd.ExecuteNonQuery();
                if (value != 0)
                {
                    return value.ToString();
                }
                else
                { return " "; }

            }
            catch (Exception ex)
            {
                Console.WriteLine("Eror "+ commd.CommandText + ", " + ex.Message);
                return "-1";
            }
            finally
            { conn.Close(); }
        }

        public static DataTable ExecuteTable(SqlCommand commd)

        {
            var tb = new DataTable();
            SqlConnection conn = new SqlConnection(msSQLConnectionStringDesktopApps);
            try
            {

                commd.Connection = conn;
                if (conn.State == ConnectionState.Closed)
                {
                    conn.Open();
                }
                using (SqlDataReader dr = commd.ExecuteReader())
                {
                    tb.Load(dr);
                    return tb;
                }

            }
            catch (Exception ex)
            {
                return tb;
            }
            finally
            { conn.Close(); }
        }

        public static List<T> DataTableToList<T>(this DataTable table) where T : class, new()
        {
            try
            {
                List<T> list = new List<T>();
                foreach (var row in table.AsEnumerable())
                {
                    T obj = new T();
                    foreach(var prop in obj.GetType().GetProperties())
                    {
                        try
                        {
                            PropertyInfo propertyInfo = obj.GetType().GetProperty(prop.Name);
                            propertyInfo.SetValue(obj, Convert.ChangeType(row[prop.Name], propertyInfo.PropertyType), null);
                        }
                        catch
                        {
                            continue;
                        }                
                    }
                    list.Add(obj);
                }
                return list;
            }
            catch
            {
                return null;
            }
        }
        public static void sendMail(string toEmail, string BodyEmail)
        {
            var fromAddress = new MailAddress("eduarddohr1@gmail.com", "SmartCollectors");
            var toAddress = new MailAddress(toEmail, "To Name");
            const string fromPassword = "19970424";
            const string subject = "Subject";
            string body = BodyEmail;

            var smtp = new SmtpClient
            {
                Host = "smtp.gmail.com",
                Port = 587,
                EnableSsl = true,
                DeliveryMethod = SmtpDeliveryMethod.Network,
                UseDefaultCredentials = false,
                Credentials = new NetworkCredential(fromAddress.Address, fromPassword)
            };
            using (var message = new MailMessage(fromAddress, toAddress)
            {
                Subject = subject,
                Body = body
            })
            {
                smtp.Send(message);
            }
        }

    }
}



