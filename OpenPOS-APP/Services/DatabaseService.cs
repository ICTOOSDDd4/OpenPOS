using OpenPOS_APP.Models;
using System;
using System.Collections.Generic;
using System.Data;
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
            string connectionString = GetConnectionString();
            Dbcontext = new SqlConnection
            {
                ConnectionString = connectionString
            };
        }

        public static void Execute(string query)
        {
            try
            {
                Dbcontext.Open();
                SqlCommand command = new SqlCommand(query, Dbcontext);
                SqlDataReader reader = command.ExecuteReader();
                CloseConnection();
            } catch (Exception ex) { 
                System.Diagnostics.Debug.WriteLine(ex.Message);
                CloseConnection();
            }

        }

        public static T ExecuteSingle<T>(string query)
        {
            try
            {
                Dbcontext.Open();
                using (SqlCommand command = new SqlCommand(query, Dbcontext))
                {
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        if (reader.HasRows)
                        {
                            var type = typeof(T);
                            T obj = (T)Activator.CreateInstance(type);
                            while (reader.Read())
                            {
                                foreach (var prop in type.GetProperties())
                                {
                                    var propType = prop.PropertyType;
                                    prop.SetValue(obj, Convert.ChangeType(reader[prop.Name].ToString(), propType));
                                }
                            }
                            CloseConnection();
                            return obj;
                        }
                        else
                        {
                            System.Diagnostics.Debug.WriteLine("No data found.");
                            CloseConnection();
                            throw new SqlNullValueException();
                        }
                    }
                }
            }
            catch (SqlException e)
            {
                System.Diagnostics.Debug.WriteLine($"Failed to execute {query}");
                CloseConnection();
                throw new Exception();
            }
        }

        public static List<T> Execute<T>(String query)
        {
            try
            {
                Dbcontext.Open();
                using (SqlCommand command = new SqlCommand(query, Dbcontext))
                {
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        if (reader.HasRows)
                        {
                            List<T> list = GetList<T>(reader);
                            CloseConnection();
                            return list;
                        }
                        else
                        {
                            System.Diagnostics.Debug.WriteLine("No data found.");
                            CloseConnection();
                            throw new SqlNullValueException();
                        }
                    }
                }
            }
            catch (SqlException e)
            {
                System.Diagnostics.Debug.WriteLine($"Failed to execute {query}");
                CloseConnection();
                throw new Exception();
            }

        }

        private static List<T> GetList<T>(SqlDataReader reader)
        {
            List<T> list = new List<T>();
            while (reader.Read())
            {
                var type = typeof(T);
                T obj = (T)Activator.CreateInstance(type);
                foreach (var prop in type.GetProperties())
                {
                    var propType = prop.PropertyType;
                    prop.SetValue(obj, Convert.ChangeType(reader[prop.Name].ToString(), propType));
                }
                list.Add(obj);
            }
            return list;
        }

        public static void CloseConnection()
        {
            Dbcontext.Close();
        }
        static private string GetConnectionString()
        {
            // Contains Connection String
            // Delete this string whenever you commit your repo to git
            return "";
        }
    }
}
