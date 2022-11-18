using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data.SqlTypes;
using System.Linq;
using System.Reflection;
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

        public static List<List<string>> Execute(String query, Object model)
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
                        List<List<string>> result = new List<List<string>>();
                        while (reader.Read())
                        {
                            int count = 0;
                            List<string> list = new List<string>();
                            foreach (PropertyInfo prop in model.GetType().GetProperties())
                            {
                                list.Add(reader[count].ToString());
                                count++;
                            }
                            result.Add(list);
                        }
                        return result;
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
