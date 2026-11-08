using System;
using MySql.Data.MySqlClient;

namespace BillingSystem.Database
{
    public class DatabaseConnection
    {
        private static string SERVER => Environment.GetEnvironmentVariable("BILLINGSYSTEM_DB_HOST") ?? "127.0.0.1";
        private const uint PORT = 3306;
        private const string DATABASE = "BillingDB";
        private static string UID => Environment.GetEnvironmentVariable("BILLINGSYSTEM_DB_USER") ?? "billing_app";

        // Credentials are supplied through the BILLINGSYSTEM_DB_PASSWORD
        // environment variable and are never stored in source control.
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
