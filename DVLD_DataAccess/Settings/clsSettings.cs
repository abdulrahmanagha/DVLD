using System;
using System.Configuration;

namespace DVLD_DataAccess
{
    public class clsSettings
    {
        public static string ConnectionString { get; } = _GetConnectionString();

        private static string _GetConnectionString()
        {
            string connectionString = ConfigurationManager.ConnectionStrings["DVLDConnectionString"]?.ConnectionString;

            if (string.IsNullOrEmpty(connectionString))
            {
                throw new ConfigurationErrorsException("Connection string 'DVLDConnectionString' is not found in the configuration file.");
            }

            return connectionString;
        }
    }
}
