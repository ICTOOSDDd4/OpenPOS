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
        public static SqlConnection dbcontext { get; private set; }
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
            using (SqlCommand command = new SqlCommand(query, dbcontext))
            {
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    System.Diagnostics.Debug.WriteLine(Environment.NewLine + "Retrieving data from database..." + Environment.NewLine);
                    System.Diagnostics.Debug.WriteLine("Retrieved records:");

                    //check if there are records
                    if (reader.HasRows)
                    {
                        return reader;
                        //while (reader.Read())
                        //{
                        //    string id = reader.GetInt32(0).ToString();
                        //    string name = reader.GetString(1);
                        //    string last_name = reader.GetString(2);

                        //    //display retrieved record
                        //    System.Diagnostics.Debug.WriteLine("{0},{1},{2}", id, name, last_name);
                        //}
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

            using (dbcontext = new SqlConnection())
            {
                dbcontext.ConnectionString = connectionString;

                dbcontext.Open();
            }
        }

        private static void CloseSQLConnection()
        {
            dbcontext.Close();
        }
        static private string GetConnectionString()
        {
            return "";
        }
    }
}
