using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpenPOS_APP.Services
{
    internal static class DatabaseService
    {
        public static void Initialize()
        {
            OpenSqlConnection();
        }

        private static void OpenSqlConnection()
        {
            string connectionString = GetConnectionString();

            using (SqlConnection connection = new SqlConnection())
            {
                connection.ConnectionString = connectionString;

                connection.Open();

                String sql = "SELECT * FROM [dbo].[user]";

                using (SqlCommand command = new SqlCommand(sql, connection))
                {
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        System.Diagnostics.Debug.WriteLine(Environment.NewLine + "Retrieving data from database..." + Environment.NewLine);
                        System.Diagnostics.Debug.WriteLine("Retrieved records:");

                        //check if there are records
                        if (reader.HasRows)
                        {
                            while (reader.Read())
                            {
                                string id = reader.GetInt32(0).ToString();
                                string name = reader.GetString(1);
                                string last_name = reader.GetString(2);

                                //display retrieved record
                                System.Diagnostics.Debug.WriteLine("{0},{1},{2}",id, name, last_name);
                            }
                        }
                        else
                        {
                            System.Diagnostics.Debug.WriteLine("No data found.");
                        }
                    }
                }
                System.Diagnostics.Debug.WriteLine("State: {0}", connection.State);
                System.Diagnostics.Debug.WriteLine("ConnectionString: {0}",
                    connection.ConnectionString);
            }
        }

        static private string GetConnectionString()
        {
            // To avoid storing the connection string in your code,
            // you can retrieve it from a configuration file.
            return "";
        }
    }
}
