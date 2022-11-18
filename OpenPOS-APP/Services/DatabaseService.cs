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
        public static SqlConnection dbcontext { get; private set; }
        public static void Initialize()
        {
            OpenSqlConnection();
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
        static private string GetConnectionString()
        {
            return "";
        }
    }
}
