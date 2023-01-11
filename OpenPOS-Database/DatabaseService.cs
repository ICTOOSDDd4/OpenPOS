using System.Data.SqlClient;
using System.Diagnostics;
using OpenPOS_Database.Factory.Database;
using OpenPOS_Settings;

namespace OpenPOS_Database
{
    //General service handler
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
                ConnectionString = _connectionString,
            };
        }

        /// <summary>
        /// Executes SqlCommand without return value (delete and update)
        /// </summary>
        /// <param name="command">SqlCommand drafted in services</param>
        /// <returns>Bool for succeeded or not</returns>
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
                Debug.WriteLine(ex.Message);
                CloseConnection();
                return false;
            }
        }

        /// <summary>
        /// Executes SqlCommand for single return value (find by id)
        /// </summary>
        /// <typeparam name="T">The generic type for finding what model to use</typeparam>
        /// <param name="command">SqlCommand drafted in services</param>
        /// <returns>Model of the generic type given for T</returns>
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

        /// <summary>
        /// Executes SqlCommand for list of return values (get all or get by filter)
        /// </summary>
        /// <typeparam name="T">The generic type for finding what model to use</typeparam>
        /// <param name="command">SqlCommand drafted in services</param>
        /// <returns>List of the generic type given for T</returns>
        public static List<T> Execute<T>(SqlCommand command)
        {
            using (SqlConnection connection = new SqlConnection(GetConnectionString()))
            {
                Debug.WriteLine(command.CommandText);
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

        /// <summary>
        /// Creates the instance of the model given and sets its values by the database data returned
        /// </summary>
        /// <typeparam name="T">The generic type for finding what model to use</typeparam>
        /// <param name="reader">SqlDataReader returned from opening the database connection</param>
        /// <returns>Model of the generic type given for T</returns>
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
                        if (propType.IsGenericType && propType.GetGenericTypeDefinition() == typeof(Nullable<>))
                        {
                            if (reader[prop.Name] == null || reader[prop.Name] == DBNull.Value || reader[prop.Name].ToString() == "")
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

        /// <summary>
        /// Creates a list of the model given and sets its values by the database data returned
        /// </summary>
        /// <typeparam name="T">The generic type for finding what model to use</typeparam>
        /// <param name="reader">SqlDataReader returned from opening the database connection</param>
        /// <returns>List of the generic type given for T</returns>
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