using System;
using MySql.Data.MySqlClient;

namespace BillingSystem.Database
{
    public class DatabaseConnection
    {
        private const string SERVER = "127.0.0.1";
        private const uint PORT = 3306;
        private const string DATABASE = "BillingDB";
        private const string UID = "billing_app";

        // Retains the password currently configured in your project.
        // If it is not the MySQL root password, update this constant.
        private static string PASSWORD =>
            Environment.GetEnvironmentVariable("BILLINGSYSTEM_DB_PASSWORD") ?? "";

        private static string ConnectionString =>
            $"Server={SERVER};Port={PORT};Database={DATABASE};Uid={UID};Pwd={PASSWORD};";

        public static string LastConnectionError { get; private set; } = "";

        public static MySqlConnection GetConnection()
        {
            return new MySqlConnection(ConnectionString);
        }

        public static bool TestConnection()
        {
            try
            {
                using (var conn = GetConnection())
                {
                    conn.Open();
                }

                LastConnectionError = "";
                return true;
            }
            catch (Exception ex)
            {
                LastConnectionError = ex.ToString();
                return false;
            }
        }
    }
}
