using System;
using System.Data;
using System.Data.SqlClient;
using DVLD_Shared;
using DVLD_Shared.Utility;

namespace DVLD_DataAccess.LicenseClasses
{
    public class clsLicenseClassData
    {
        public static bool GetLicenseClassById(LicenseClassDTO licenseClassDTO)
        {
            bool isFound = false;
            string query = @"
                SELECT LicenseClassID
                     , ClassName
                     , ClassDescription
                     , MinimumAllowedAge
                     , DefaultValidityLength
                     , ClassFees
                FROM LicenseClasses
                WHERE LicenseClassID = @LicenseClassID;
            ";

            try
            {
                using (SqlConnection connection = new SqlConnection(clsSettings.ConnectionString))
                using (SqlCommand cmd = new SqlCommand(query, connection))
                {
                    cmd.Parameters.Add("@LicenseClassID", SqlDbType.Int).Value = licenseClassDTO?.LicenseClassId;
                    connection.Open();

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            licenseClassDTO.LicenseClassId = (int)reader["LicenseClassID"];
                            licenseClassDTO.ClassName = (string)reader["ClassName"];
                            licenseClassDTO.ClassDescription = (string)reader["ClassDescription"];
                            licenseClassDTO.MinimumAllowedAge = (byte)reader["MinimumAllowedAge"];
                            licenseClassDTO.DefaultValidityLength = (byte)reader["DefaultValidityLength"];
                            licenseClassDTO.ClassFees = (decimal)reader["ClassFees"];
                            isFound = true;
                        }
                        else
                        {
                            isFound = false;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                clsEventLogger.LogEvent(ex, $"LicenseClassID: {licenseClassDTO?.LicenseClassId}");
                isFound = false;
            }

            return isFound;
        }

        public static bool UpdateLicenseClass(LicenseClassDTO licenseClassDTO)
        {
            int rowsAffected = 0;
            string query = @"
                UPDATE LicenseClasses
                SET ClassName = @ClassName
                  , ClassDescription = @ClassDescription
                  , MinimumAllowedAge = @MinimumAllowedAge
                  , DefaultValidityLength = @DefaultValidityLength
                  , ClassFees = @ClassFees
                WHERE LicenseClassID = @LicenseClassID;
            ";

            try
            {
                using (SqlConnection connection = new SqlConnection(clsSettings.ConnectionString))
                using (SqlCommand cmd = new SqlCommand(query, connection))
                {
                    cmd.Parameters.Add("@LicenseClassID", SqlDbType.Int).Value = licenseClassDTO.LicenseClassId;
                    cmd.Parameters.Add("@ClassName", SqlDbType.NVarChar, 50).Value = licenseClassDTO.ClassName;
                    cmd.Parameters.Add("@ClassDescription", SqlDbType.NVarChar, 500).Value = licenseClassDTO.ClassDescription;
                    cmd.Parameters.Add("@MinimumAllowedAge", SqlDbType.TinyInt).Value = licenseClassDTO.MinimumAllowedAge;
                    cmd.Parameters.Add("@DefaultValidityLength", SqlDbType.TinyInt).Value = licenseClassDTO.DefaultValidityLength;
                    cmd.Parameters.Add("@ClassFees", SqlDbType.SmallMoney).Value = licenseClassDTO.ClassFees;

                    connection.Open();
                    rowsAffected = cmd.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                clsEventLogger.LogEvent(
                    ex,
                    $"LicenseClassID: {licenseClassDTO?.LicenseClassId}, ClassName: {licenseClassDTO?.ClassName}"
                );
                rowsAffected = 0;
            }

            return (rowsAffected > 0);
        }

        public static DataTable GetAllLicenseClass()
        {
            DataTable dt = new DataTable();
            string query = @"
                SELECT LicenseClassID
                     , ClassName
                     , ClassDescription
                     , MinimumAllowedAge
                     , DefaultValidityLength
                     , ClassFees
                FROM LicenseClasses;
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

        public static int GetDefaultValidityDate(int licenseClassId)
        {
            int defaultValidityDate = -1;
            string query = @"
                SELECT DefaultValidityLength
                FROM LicenseClasses
                WHERE LicenseClassID = @LicenseClassID;
            ";

            try
            {
                using (SqlConnection connection = new SqlConnection(clsSettings.ConnectionString))
                using (SqlCommand cmd = new SqlCommand(query, connection))
                {
                    cmd.Parameters.Add("@LicenseClassID", SqlDbType.Int).Value = licenseClassId;
                    connection.Open();

                    object result = cmd.ExecuteScalar();

                    if (result != null && result != DBNull.Value)
                    {
                        defaultValidityDate = Convert.ToInt32(result);
                    }
                }
            }
            catch (Exception ex)
            {
                clsEventLogger.LogEvent(ex, $"LicenseClassID: {licenseClassId}");
                defaultValidityDate = -1;
            }

            return defaultValidityDate;
        }

        public static int GetMinimumAllowedAge(int licenseClassId)
        {
            int minimumAllowedAge = -1;
            string query = @"
                SELECT MinimumAllowedAge
                FROM LicenseClasses
                WHERE LicenseClassID = @LicenseClassID;
            ";

            try
            {
                using (SqlConnection connection = new SqlConnection(clsSettings.ConnectionString))
                using (SqlCommand cmd = new SqlCommand(query, connection))
                {
                    cmd.Parameters.Add("@LicenseClassID", SqlDbType.Int).Value = licenseClassId;
                    connection.Open();

                    object result = cmd.ExecuteScalar();

                    if (result != null && result != DBNull.Value)
                    {
                        minimumAllowedAge = Convert.ToInt32(result);
                    }
                }
            }
            catch (Exception ex)
            {
                clsEventLogger.LogEvent(ex, $"LicenseClassID: {licenseClassId}");
                minimumAllowedAge = -1;
            }

            return minimumAllowedAge;
        }
    }
}