using System.Data.SqlClient;
using System.Diagnostics;
using OpenPOS_APP.Factory.Database;
using OpenPOS_APP.Settings;

namespace OpenPOS_APP.Services
{
    public static class DatabaseService
    {
        private static string _connectionString;
        public static SqlConnection Dbcontext { get; private set; }
        public static void Initialize()
        {
            SetConnectionString();
            Seeder.Initialize();
        }

        public static void SetConnectionString()
        {
            _connectionString = ApplicationSettings.DbSett.connection_string;
            Dbcontext = new SqlConnection
            {
                ConnectionString = _connectionString
            };
        }
        public static bool Execute(SqlCommand command)
        {
            try
            {
                SqlConnection connection = new SqlConnection(GetConnectionString());
                command.Connection = connection;
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                reader.Close();
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                System.Diagnostics.Debug.WriteLine(ex.Message);
                CloseConnection();
                return false;
            }
        }

        public static T ExecuteSingle<T>(SqlCommand command)
        {
            using (SqlConnection connection = new SqlConnection(GetConnectionString()))
            {
                command.Connection = connection;
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                T result;
                try
                {
                   result = getObject<T>(reader);
                }

                finally
                {
                    reader.Close();
                }
                return result;
            }
        }

        public static List<T> Execute<T>(SqlCommand command)
        {
            using (SqlConnection connection = new SqlConnection(GetConnectionString()))
            {
                System.Diagnostics.Debug.WriteLine(command.CommandText);
                command.Connection = connection;
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();

                try
                {
                    return GetList<T>(reader);
                }
                finally
                {
                    reader.Close();
                }
            }
        }

        private static T getObject<T>(SqlDataReader reader)
        {
            var type = typeof(T);
            T obj = (T)Activator.CreateInstance(type);
            while (reader.Read())
            {
                foreach (var prop in type.GetProperties())
                { 
                    var propType = prop.PropertyType;
                    try
                   {
                      if (propType.IsGenericType && propType.GetGenericTypeDefinition().Equals(typeof(Nullable<>)))
                      {
                         if (reader[prop.Name] == null)
                         {
                            prop.SetValue(obj, null, null);
                         }
                         else
                         {
                             propType = Nullable.GetUnderlyingType(propType);
                             prop.SetValue(obj, Convert.ChangeType(reader[prop.Name].ToString(), propType));
                         }
                      }
                      else
                      {
                         prop.SetValue(obj, Convert.ChangeType(reader[prop.Name].ToString(), propType));
                      }
                   }
                   catch (Exception e)
                   {
                      Console.WriteLine(e);
                      Debug.WriteLine(e);
                      throw;
                   }
                }
            }

            return obj;
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
                    try
                    {
                        if (propType.IsGenericType && propType.GetGenericTypeDefinition().Equals(typeof(Nullable<>)))
                        {
                            if (reader[prop.Name] == null)
                            {
                                prop.SetValue(obj, null, null);
                            }
                            propType = Nullable.GetUnderlyingType(propType);
                        }
                        else
                        {
                            prop.SetValue(obj, Convert.ChangeType(reader[prop.Name].ToString(), propType));
                        }
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine(e);
                        Debug.WriteLine(e);
                        throw;
                    }
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
            return _connectionString;
        }
    }
}