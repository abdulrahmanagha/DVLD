using DVLD_Shared;
using DVLD_Shared.DTOs;
using DVLD_Shared.Utility;
using System;
using System.Data;
using System.Data.SqlClient;

namespace DVLD_DataAccess.InternationalLicenses
{
    public class clsInternationalLicenseData
    {
        public static bool GetInternationalLicenseInfoById(InternationalDTO internationalDTO)
        {
            bool isFound = false;
            string query = @"
                SELECT InternationalLicenseID
                     , ApplicationID
                     , DriverID
                     , IssuedUsingLocalLicenseID
                     , IssueDate
                     , ExpirationDate
                     , IsActive
                     , CreatedByUserID
                FROM InternationalLicenses
                WHERE InternationalLicenseID = @InternationalLicenseID;
            ";

            try
            {
                using (SqlConnection connection = new SqlConnection(clsSettings.ConnectionString))
                using (SqlCommand cmd = new SqlCommand(query, connection))
                {
                    cmd.Parameters.Add("@InternationalLicenseID", SqlDbType.Int).Value = internationalDTO?.InternationalLicenseId;

                    connection.Open();
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            internationalDTO.ApplicationId = (int)reader["ApplicationID"];
                            internationalDTO.DriverId = (int)reader["DriverID"];
                            internationalDTO.IssuedUsingLocalLicenseId = (int)reader["IssuedUsingLocalLicenseID"];
                            internationalDTO.IssueDate = (DateTime)reader["IssueDate"];
                            internationalDTO.ExpirationDate = (DateTime)reader["ExpirationDate"];
                            internationalDTO.IsActive = (bool)reader["IsActive"];
                            internationalDTO.CreatedByUserId = (int)reader["CreatedByUserID"];
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
                clsEventLogger.LogEvent(ex, $"InternationalLicenseID: {internationalDTO?.InternationalLicenseId}");
                isFound = false;
            }

            return isFound;
        }

        public static bool GetInternationalLicenseInfoByDriverId(InternationalDTO internationalDTO)
        {
            bool isFound = false;
            string query = @"
                SELECT InternationalLicenseID
                     , ApplicationID
                     , DriverID
                     , IssuedUsingLocalLicenseID
                     , IssueDate
                     , ExpirationDate
                     , IsActive
                     , CreatedByUserID
                FROM InternationalLicenses
                WHERE DriverID = @DriverID;
            ";

            try
            {
                using (SqlConnection connection = new SqlConnection(clsSettings.ConnectionString))
                using (SqlCommand cmd = new SqlCommand(query, connection))
                {
                    cmd.Parameters.Add("@DriverID", SqlDbType.Int).Value = internationalDTO.DriverId;

                    connection.Open();
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            internationalDTO.InternationalLicenseId = (int)reader["InternationalLicenseID"];
                            internationalDTO.ApplicationId = (int)reader["ApplicationID"];
                            internationalDTO.DriverId = (int)reader["DriverID"];
                            internationalDTO.IssuedUsingLocalLicenseId = (int)reader["IssuedUsingLocalLicenseID"];
                            internationalDTO.IssueDate = (DateTime)reader["IssueDate"];
                            internationalDTO.ExpirationDate = (DateTime)reader["ExpirationDate"];
                            internationalDTO.IsActive = (bool)reader["IsActive"];
                            internationalDTO.CreatedByUserId = (int)reader["CreatedByUserID"];
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
                clsEventLogger.LogEvent(ex, $"DriverID: {internationalDTO?.DriverId}");
                isFound = false;
            }

            return isFound;
        }

        public static bool GetInternationalLicenseInfoByLocalLicenseId(InternationalDTO internationalDTO)
        {
            bool isFound = false;
            string query = @"
                SELECT InternationalLicenseID
                     , ApplicationID
                     , DriverID
                     , IssuedUsingLocalLicenseID
                     , IssueDate
                     , ExpirationDate
                     , IsActive
                     , CreatedByUserID
                FROM InternationalLicenses
                WHERE IssuedUsingLocalLicenseID = @IssuedUsingLocalLicenseID;
            ";

            try
            {
                using (SqlConnection connection = new SqlConnection(clsSettings.ConnectionString))
                using (SqlCommand cmd = new SqlCommand(query, connection))
                {
                    cmd.Parameters.Add("@IssuedUsingLocalLicenseID", SqlDbType.Int).Value = internationalDTO.IssuedUsingLocalLicenseId;

                    connection.Open();
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            internationalDTO.InternationalLicenseId = (int)reader["InternationalLicenseID"];
                            internationalDTO.ApplicationId = (int)reader["ApplicationID"];
                            internationalDTO.DriverId = (int)reader["DriverID"];
                            internationalDTO.IssuedUsingLocalLicenseId = (int)reader["IssuedUsingLocalLicenseID"];
                            internationalDTO.IssueDate = (DateTime)reader["IssueDate"];
                            internationalDTO.ExpirationDate = (DateTime)reader["ExpirationDate"];
                            internationalDTO.IsActive = (bool)reader["IsActive"];
                            internationalDTO.CreatedByUserId = (int)reader["CreatedByUserID"];
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
                clsEventLogger.LogEvent(ex, $"IssuedUsingLocalLicenseID: {internationalDTO?.IssuedUsingLocalLicenseId}");
                isFound = false;
            }

            return isFound;
        }

        public static int AddNewInternationalLicense(InternationalDTO internationalDTO)
        {
            int licenseId = -1;
            string query = @"
                UPDATE InternationalLicenses
                SET IsActive = 0
                WHERE DriverID = @DriverID;
                
                INSERT INTO InternationalLicenses
                    (ApplicationID, DriverID, IssuedUsingLocalLicenseID, IssueDate, ExpirationDate
                        , IsActive, CreatedByUserID)
                VALUES
                    (@ApplicationID, @DriverID, @IssuedUsingLocalLicenseID, @IssueDate, @ExpirationDate
                        , @IsActive, @CreatedByUserID);

                SELECT CAST(SCOPE_IDENTITY() AS INT);
            ";

            try
            {
                using (SqlConnection connection = new SqlConnection(clsSettings.ConnectionString))
                using (SqlCommand cmd = new SqlCommand(query, connection))
                {
                    cmd.Parameters.Add("@ApplicationID", SqlDbType.Int).Value = internationalDTO.ApplicationId;
                    cmd.Parameters.Add("@DriverID", SqlDbType.Int).Value = internationalDTO.DriverId;
                    cmd.Parameters.Add("@IssuedUsingLocalLicenseID", SqlDbType.Int).Value = internationalDTO.IssuedUsingLocalLicenseId;
                    cmd.Parameters.Add("@IssueDate", SqlDbType.DateTime).Value = DateTime.Now;
                    cmd.Parameters.Add("@ExpirationDate", SqlDbType.DateTime).Value = internationalDTO.ExpirationDate;
                    cmd.Parameters.Add("@IsActive", SqlDbType.Bit).Value = internationalDTO.IsActive;
                    cmd.Parameters.Add("@CreatedByUserID", SqlDbType.Int).Value = internationalDTO.CreatedByUserId;

                    connection.Open();
                    object result = cmd.ExecuteScalar();

                    if (result != null && result != DBNull.Value)
                    {
                        licenseId = Convert.ToInt32(result);
                    }
                }
            }
            catch (Exception ex)
            {
                clsEventLogger.LogEvent(
                    ex,
                    $"ApplicationID: {internationalDTO?.ApplicationId}, DriverID: {internationalDTO?.DriverId}, LocalLicenseID: {internationalDTO?.IssuedUsingLocalLicenseId}"
                );
                licenseId = -1;
            }

            return licenseId;
        }

        public static bool UpdateInternationalLicense(InternationalDTO internationalDTO)
        {
            int rowsAffected = 0;
            string query = @"            
                UPDATE InternationalLicenses
                SET ApplicationID = @ApplicationID
                  , DriverID = @DriverID
                  , IssuedUsingLocalLicenseID = @IssuedUsingLocalLicenseID
                  , IssueDate = @IssueDate
                  , ExpirationDate = @ExpirationDate
                  , IsActive = @IsActive
                  , CreatedByUserID = @CreatedByUserID
                WHERE InternationalLicenseID = @InternationalLicenseID;
            ";

            try
            {
                using (SqlConnection connection = new SqlConnection(clsSettings.ConnectionString))
                using (SqlCommand cmd = new SqlCommand(query, connection))
                {
                    cmd.Parameters.Add("@InternationalLicenseID", SqlDbType.Int).Value = internationalDTO.InternationalLicenseId;
                    cmd.Parameters.Add("@ApplicationID", SqlDbType.Int).Value = internationalDTO.ApplicationId;
                    cmd.Parameters.Add("@DriverID", SqlDbType.Int).Value = internationalDTO.DriverId;
                    cmd.Parameters.Add("@IssuedUsingLocalLicenseID", SqlDbType.Int).Value = internationalDTO.IssuedUsingLocalLicenseId;
                    cmd.Parameters.Add("@IssueDate", SqlDbType.DateTime).Value = DateTime.Now;
                    cmd.Parameters.Add("@ExpirationDate", SqlDbType.DateTime).Value = internationalDTO.ExpirationDate;
                    cmd.Parameters.Add("@IsActive", SqlDbType.Bit).Value = internationalDTO.IsActive;
                    cmd.Parameters.Add("@CreatedByUserID", SqlDbType.Int).Value = internationalDTO.CreatedByUserId;

                    connection.Open();
                    rowsAffected = cmd.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                clsEventLogger.LogEvent(
                    ex,
                    $"InternationalLicenseID: {internationalDTO?.InternationalLicenseId}, DriverID: {internationalDTO?.DriverId}"
                );
                rowsAffected = 0;
            }

            return (rowsAffected > 0);
        }

        public static enDeleteResult DeleteLicense(int internationalLicenseId)
        {
            int rowsAffected = 0;
            string query = @"
                DELETE FROM InternationalLicenses
                WHERE InternationalLicenseID = @InternationalLicenseID;
            ";

            try
            {
                using (SqlConnection connection = new SqlConnection(clsSettings.ConnectionString))
                using (SqlCommand cmd = new SqlCommand(query, connection))
                {
                    cmd.Parameters.Add("@InternationalLicenseID", SqlDbType.Int).Value = internationalLicenseId;

                    connection.Open();
                    rowsAffected = cmd.ExecuteNonQuery();

                    if (rowsAffected == 0)
                        return enDeleteResult.NotFound;
                }
            }
            catch (SqlException ex)
            {
                if (ex.Number == 547)
                {
                    return enDeleteResult.HasRelatedData;
                }

                clsEventLogger.LogEvent(ex, $"SQL Error #{ex.Number} | InternationalLicenseID: {internationalLicenseId}");
                return enDeleteResult.Error;
            }
            catch (Exception ex)
            {
                clsEventLogger.LogEvent(ex, $"InternationalLicenseID: {internationalLicenseId}");
                return enDeleteResult.Error;
            }

            return enDeleteResult.Success;
        }

        public static bool HasDriverActiveInternationalLicense(int driverId)
        {
            bool isFound = false;
            string query = @"
                SELECT 1
                FROM InternationalLicenses
                WHERE DriverID = @DriverID
                  AND IsActive = 1
                  AND GETDATE() BETWEEN IssueDate AND ExpirationDate;
            ";

            try
            {
                using (SqlConnection connection = new SqlConnection(clsSettings.ConnectionString))
                using (SqlCommand cmd = new SqlCommand(query, connection))
                {
                    cmd.Parameters.Add("@DriverID", SqlDbType.Int).Value = driverId;
                    connection.Open();

                    object result = cmd.ExecuteScalar();
                    isFound = (result != null);
                }
            }
            catch (Exception ex)
            {
                clsEventLogger.LogEvent(ex, $"DriverID: {driverId}");
                isFound = false;
            }

            return isFound;
        }

        public static DataTable GetInternationalLicenses()
        {
            DataTable dt = new DataTable();
            string query = @"          
                SELECT InternationalLicenseID
                     , ApplicationID
                     , DriverID
                     , IssuedUsingLocalLicenseID
                     , IssueDate
                     , ExpirationDate
                     , IsActive
                FROM InternationalLicenses;
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

        public static DataTable GetAllInternationalLicenseDriver(int driverId)
        {
            DataTable dt = new DataTable();
            string query = @"          
                SELECT InternationalLicenseID
                     , ApplicationID
                     , DriverID
                     , IssuedUsingLocalLicenseID
                     , IssueDate
                     , ExpirationDate
                     , IsActive
                FROM InternationalLicenses
                WHERE DriverID = @DriverID;
            ";

            try
            {
                using (SqlConnection connection = new SqlConnection(clsSettings.ConnectionString))
                using (SqlCommand cmd = new SqlCommand(query, connection))
                {
                    cmd.Parameters.Add("@DriverID", SqlDbType.Int).Value = driverId;
                    connection.Open();
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        dt.Load(reader);
                    }
                }
            }
            catch (Exception ex)
            {
                clsEventLogger.LogEvent(ex, $"DriverID: {driverId}");
                dt = null;
            }

            return dt;
        }
    }
}