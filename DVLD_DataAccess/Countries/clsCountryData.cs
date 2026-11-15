using System;
using System.Data;
using System.Data.SqlClient;
using DVLD_Shared.Utility;

namespace DVLD_DataAccess
{
    public class clsCountryData
    {
        public static bool GetCountryInfoById(int CountryId, ref string CountryName)
        {
            bool isFound = false;
            string query = @"
                SELECT CountryName
                FROM Countries
                WHERE CountryID = @CountryID;
            ";

            try
            {
                using (SqlConnection connection = new SqlConnection(clsSettings.ConnectionString))
                using (SqlCommand cmd = new SqlCommand(query, connection))
                {
                    cmd.Parameters.Add("@CountryID", SqlDbType.Int).Value = CountryId;
                    connection.Open();

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            isFound = true;
                            CountryName = (string)reader["CountryName"];
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                clsEventLogger.LogEvent(ex, $"CountryID: {CountryId}");
                isFound = false;
            }

            return isFound;
        }

        public static bool GetCountryInfoByName(ref int CountryId, string CountryName)
        {
            bool isFound = false;
            string query = @"
                SELECT CountryID
                FROM Countries
                WHERE CountryName = @CountryName;
            ";

            try
            {
                using (SqlConnection connection = new SqlConnection(clsSettings.ConnectionString))
                using (SqlCommand cmd = new SqlCommand(query, connection))
                {
                    cmd.Parameters.Add("@CountryName", SqlDbType.NVarChar, 50).Value = CountryName;
                    connection.Open();

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            isFound = true;
                            CountryId = (int)reader["CountryID"];
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                clsEventLogger.LogEvent(ex, $"CountryName: {CountryName}");
                isFound = false;
            }

            return isFound;
        }

        public static int AddNewCountry(string countryName)
        {
            int countryId = -1;
            string query = @"
                INSERT INTO Countries (CountryName)
                VALUES (@CountryName);
                SELECT CAST(SCOPE_IDENTITY() AS INT);
            ";

            try
            {
                using (SqlConnection connection = new SqlConnection(clsSettings.ConnectionString))
                using (SqlCommand cmd = new SqlCommand(query, connection))
                {
                    cmd.Parameters.Add("@CountryName", SqlDbType.NVarChar, 50).Value = countryName;

                    connection.Open();
                    object result = cmd.ExecuteScalar();

                    if (result != null && result != DBNull.Value)
                    {
                        countryId = Convert.ToInt32(result);
                    }
                }
            }
            catch (Exception ex)
            {
                clsEventLogger.LogEvent(ex, $"CountryName: {countryName}");
                countryId = -1;
            }

            return countryId;
        }

        public static bool UpdateCountry(int countryId, string countryName)
        {
            int rowsAffected = 0;
            string query = @"
                UPDATE Countries
                SET CountryName = @CountryName
                WHERE CountryID = @CountryID;
            ";

            try
            {
                using (SqlConnection connection = new SqlConnection(clsSettings.ConnectionString))
                using (SqlCommand cmd = new SqlCommand(query, connection))
                {
                    cmd.Parameters.Add("@CountryID", SqlDbType.Int).Value = countryId;
                    cmd.Parameters.Add("@CountryName", SqlDbType.NVarChar, 50).Value = countryName;

                    connection.Open();
                    rowsAffected = cmd.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                clsEventLogger.LogEvent(ex, $"CountryID: {countryId}, CountryName: {countryName}");
                rowsAffected = 0;
            }

            return (rowsAffected > 0);
        }

        public static bool DeleteCountry(int countryId)
        {
            int rowsAffected = 0;
            string query = @"
                DELETE FROM Countries
                WHERE CountryID = @CountryID;
            ";

            try
            {
                using (SqlConnection connection = new SqlConnection(clsSettings.ConnectionString))
                using (SqlCommand cmd = new SqlCommand(query, connection))
                {
                    cmd.Parameters.Add("@CountryID", SqlDbType.Int).Value = countryId;

                    connection.Open();
                    rowsAffected = cmd.ExecuteNonQuery();
                }
            }
            catch (SqlException ex)
            {
                if (ex.Number != 547)
                {
                    clsEventLogger.LogEvent(ex, $"SQL Error #{ex.Number} | CountryID: {countryId}");
                }
                rowsAffected = 0;
            }
            catch (Exception ex)
            {
                clsEventLogger.LogEvent(ex, $"CountryID: {countryId}");
                rowsAffected = 0;
            }

            return (rowsAffected > 0);
        }

        public static bool DeleteCountry(string countryName)
        {
            int rowsAffected = 0;
            string query = @"
                DELETE FROM Countries
                WHERE CountryName = @CountryName;
            ";

            try
            {
                using (SqlConnection connection = new SqlConnection(clsSettings.ConnectionString))
                using (SqlCommand cmd = new SqlCommand(query, connection))
                {
                    cmd.Parameters.Add("@CountryName", SqlDbType.NVarChar, 50).Value = countryName;

                    connection.Open();
                    rowsAffected = cmd.ExecuteNonQuery();
                }
            }
            catch (SqlException ex)
            {
                if (ex.Number != 547)
                {
                    clsEventLogger.LogEvent(ex, $"SQL Error #{ex.Number} | CountryName: {countryName}");
                }
                rowsAffected = 0;
            }
            catch (Exception ex)
            {
                clsEventLogger.LogEvent(ex, $"CountryName: {countryName}");
                rowsAffected = 0;
            }

            return (rowsAffected > 0);
        }

        public static bool IsExistCountry(int countryId)
        {
            bool isFound = false;
            string query = @"
                SELECT 1
                FROM Countries
                WHERE CountryId = @CountryId;
            ";

            try
            {
                using (SqlConnection connection = new SqlConnection(clsSettings.ConnectionString))
                using (SqlCommand cmd = new SqlCommand(query, connection))
                {
                    cmd.Parameters.Add("@CountryId", SqlDbType.Int).Value = countryId;

                    connection.Open();
                    object result = cmd.ExecuteScalar();
                    isFound = (result != null);
                }
            }
            catch (Exception ex)
            {
                clsEventLogger.LogEvent(ex, $"CountryID: {countryId}");
                isFound = false;
            }

            return isFound;
        }

        public static bool IsExistCountry(string countryName)
        {
            bool isFound = false;
            string query = @"
                SELECT 1
                FROM Countries
                WHERE CountryName = @CountryName;
            ";

            try
            {
                using (SqlConnection connection = new SqlConnection(clsSettings.ConnectionString))
                using (SqlCommand cmd = new SqlCommand(query, connection))
                {
                    cmd.Parameters.Add("@CountryName", SqlDbType.NVarChar, 50).Value = countryName;

                    connection.Open();
                    object result = cmd.ExecuteScalar();
                    isFound = (result != null);
                }
            }
            catch (Exception ex)
            {
                clsEventLogger.LogEvent(ex, $"CountryName: {countryName}");
                isFound = false;
            }

            return isFound;
        }

        public static DataTable GetAllCountries()
        {
            DataTable dt = new DataTable();
            string query = @"
                SELECT CountryID
                     , CountryName
                FROM Countries;
            ";

            try
            {
                using (SqlConnection connection = new SqlConnection(clsSettings.ConnectionString))
                using (SqlCommand cmd = new SqlCommand(query, connection))
                {
                    connection.Open();
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        dt.Load(reader);
                    }
                }
            }
            catch (Exception ex)
            {
                clsEventLogger.LogEvent(ex);
                dt = null;
            }

            return dt;
        }
    }
}