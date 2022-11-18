using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data.SqlTypes;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpenPOS_APP.Services
{
    internal static class DatabaseService
    {
        public static SqlConnection Dbcontext { get; private set; }
        public static void Initialize()
        {
            OpenSqlConnection();
        }
        public static void CloseConnection()
        {
            CloseSQLConnection();
        }

        public static SqlDataReader Execute(String query)
        {
            Dbcontext.Open();
            using (SqlCommand command = new SqlCommand(query, Dbcontext))
            {
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    System.Diagnostics.Debug.WriteLine(Environment.NewLine + "Retrieving data from database..." + Environment.NewLine);
                    System.Diagnostics.Debug.WriteLine("Retrieved records:");

                    //check if there are records
                    if (reader.HasRows)
                    {
                        return reader;
                    }
                    else
                    {
                        System.Diagnostics.Debug.WriteLine("No data found.");
                        throw new SqlNullValueException();
                    }
                }
            }
        }

        private static void OpenSqlConnection()
        {
            string connectionString = GetConnectionString();
            Dbcontext = new SqlConnection
            {
                ConnectionString = connectionString
            };
            //using (Dbcontext = new SqlConnection())
            //{
            //    Dbcontext.ConnectionString = connectionString;

            //    Dbcontext.Open();
            //}
        }

        private static void CloseSQLConnection()
        {
            Dbcontext.Close();
        }
        static private string GetConnectionString()
        {
            return "";
        }
    }
}
