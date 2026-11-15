using DVLD_Shared;
using DVLD_Shared.DTOs;
using DVLD_Shared.Utility;
using System;
using System.Data;
using System.Data.SqlClient;

namespace DVLD_DataAccess.DetainedLicenses
{
    public class clsDetainedLicenseData
    {
        public static bool GetDetainLicenseInfoById(DetainedLicensesDTO detainedLicensesDTO)
        {
            bool isFound = false;
            string query = @"
                SELECT LicenseID
                     , DetainDate
                     , FineFees
                     , CreatedByUserID
                     , IsReleased
                     , ReleaseDate
                     , ReleasedByUserID
                     , ReleaseApplicationID
                FROM DetainedLicenses
                WHERE DetainID = @DetainID;
            ";

            try
            {
                using (SqlConnection connection = new SqlConnection(clsSettings.ConnectionString))
                using (SqlCommand cmd = new SqlCommand(query, connection))
                {
                    cmd.Parameters.Add("@DetainID", SqlDbType.Int).Value = detainedLicensesDTO?.DetainId;

                    connection.Open();
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            detainedLicensesDTO.LicenseId = (int)reader["LicenseID"];
                            detainedLicensesDTO.DetainDate = (DateTime)reader["DetainDate"];
                            detainedLicensesDTO.FineFees = (decimal)reader["FineFees"];
                            detainedLicensesDTO.CreatedByUserId = (int)reader["CreatedByUserID"];
                            detainedLicensesDTO.IsReleased = (bool)reader["IsReleased"];
                            detainedLicensesDTO.ReleaseDate = reader["ReleaseDate"] as DateTime?;
                            detainedLicensesDTO.ReleasedByUserId = reader["ReleasedByUserID"] as int?;
                            detainedLicensesDTO.ReleaseApplicationId = reader["ReleaseApplicationID"] as int?;
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
                clsEventLogger.LogEvent(ex, $"DetainID: {detainedLicensesDTO?.DetainId}");
                isFound = false;
            }

            return isFound;
        }

        public static bool GetDetainLicenseInfoByLicenseId(DetainedLicensesDTO detainedLicensesDTO)
        {
            bool isFound = false;
            string query = @"
                SELECT TOP(1) DetainID
                     , LicenseID
                     , DetainDate
                     , FineFees
                     , CreatedByUserID
                     , IsReleased
                     , ReleaseDate
                     , ReleasedByUserID
                     , ReleaseApplicationID
                FROM DetainedLicenses
                WHERE LicenseID = @LicenseID
                    AND IsReleased = 0
                ORDER BY DetainID DESC;
            ";

            try
            {
                using (SqlConnection connection = new SqlConnection(clsSettings.ConnectionString))
                using (SqlCommand cmd = new SqlCommand(query, connection))
                {
                    cmd.Parameters.Add("@LicenseID", SqlDbType.Int).Value = detainedLicensesDTO.LicenseId;

                    connection.Open();
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            detainedLicensesDTO.DetainId = (int)reader["DetainID"];
                            detainedLicensesDTO.LicenseId = (int)reader["LicenseID"];
                            detainedLicensesDTO.DetainDate = (DateTime)reader["DetainDate"];
                            detainedLicensesDTO.FineFees = (decimal)reader["FineFees"];
                            detainedLicensesDTO.CreatedByUserId = (int)reader["CreatedByUserID"];
                            detainedLicensesDTO.IsReleased = (bool)reader["IsReleased"];
                            detainedLicensesDTO.ReleaseDate = reader["ReleaseDate"] as DateTime?;
                            detainedLicensesDTO.ReleasedByUserId = reader["ReleasedByUserID"] as int?;
                            detainedLicensesDTO.ReleaseApplicationId = reader["ReleaseApplicationID"] as int?;
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
                clsEventLogger.LogEvent(ex, $"LicenseID: {detainedLicensesDTO?.LicenseId}");
                isFound = false;
            }

            return isFound;
        }

        public static int AddNewDetainLicense(DetainedLicensesDTO detainedLicensesDTO)
        {
            int detainId = -1;
            string query = @"
                INSERT INTO DetainedLicenses
                    (LicenseID, DetainDate, FineFees, CreatedByUserID, IsReleased
                      , ReleaseDate, ReleasedByUserID, ReleaseApplicationID)
                VALUES
                    (@LicenseID, @DetainDate, @FineFees, @CreatedByUserID, @IsReleased
                      , @ReleaseDate, @ReleasedByUserID, @ReleaseApplicationID);

                SELECT CAST(SCOPE_IDENTITY() AS INT);
            ";

            try
            {
                using (SqlConnection connection = new SqlConnection(clsSettings.ConnectionString))
                using (SqlCommand cmd = new SqlCommand(query, connection))
                {
                    cmd.Parameters.Add("@LicenseID", SqlDbType.Int).Value = detainedLicensesDTO.LicenseId;
                    cmd.Parameters.Add("@DetainDate", SqlDbType.SmallDateTime).Value = detainedLicensesDTO.DetainDate;
                    cmd.Parameters.Add("@FineFees", SqlDbType.SmallMoney).Value = detainedLicensesDTO.FineFees;
                    cmd.Parameters.Add("@CreatedByUserID", SqlDbType.Int).Value = detainedLicensesDTO.CreatedByUserId;
                    cmd.Parameters.Add("@IsReleased", SqlDbType.Bit).Value = detainedLicensesDTO.IsReleased;
                    cmd.Parameters.Add("@ReleaseDate", SqlDbType.SmallDateTime).Value = (object)detainedLicensesDTO.ReleaseDate ?? DBNull.Value;
                    cmd.Parameters.Add("@ReleasedByUserID", SqlDbType.Int).Value = (object)detainedLicensesDTO.ReleasedByUserId ?? DBNull.Value;
                    cmd.Parameters.Add("@ReleaseApplicationID", SqlDbType.Int).Value = (object)detainedLicensesDTO.ReleaseApplicationId ?? DBNull.Value;

                    connection.Open();
                    object result = cmd.ExecuteScalar();

                    if (result != null && result != DBNull.Value)
                    {
                        detainId = Convert.ToInt32(result);
                    }
                }
            }
            catch (Exception ex)
            {
                clsEventLogger.LogEvent(
                    ex,
                    $"LicenseID: {detainedLicensesDTO?.LicenseId}, CreatedByUserID: {detainedLicensesDTO?.CreatedByUserId}"
                );
                detainId = -1;
            }

            return detainId;
        }

        public static bool UpdateDetainLicense(DetainedLicensesDTO detainedLicensesDTO)
        {
            int rowsAffected = 0;
            string query = @"            
                UPDATE DetainedLicenses
                SET LicenseID = @LicenseID
                  , DetainDate = @DetainDate
                  , FineFees = @FineFees
                  , CreatedByUserID = @CreatedByUserID
                  , IsReleased = @IsReleased
                  , ReleaseDate = @ReleaseDate
                  , ReleasedByUserID = @ReleasedByUserID
                  , ReleaseApplicationID = @ReleaseApplicationID
                WHERE DetainID = @DetainID;
            ";

            try
            {
                using (SqlConnection connection = new SqlConnection(clsSettings.ConnectionString))
                using (SqlCommand cmd = new SqlCommand(query, connection))
                {
                    cmd.Parameters.Add("@DetainID", SqlDbType.Int).Value = detainedLicensesDTO.DetainId;
                    cmd.Parameters.Add("@LicenseID", SqlDbType.Int).Value = detainedLicensesDTO.LicenseId;
                    cmd.Parameters.Add("@DetainDate", SqlDbType.SmallDateTime).Value = detainedLicensesDTO.DetainDate;
                    cmd.Parameters.Add("@FineFees", SqlDbType.SmallMoney).Value = detainedLicensesDTO.FineFees;
                    cmd.Parameters.Add("@CreatedByUserID", SqlDbType.Int).Value = detainedLicensesDTO.CreatedByUserId;
                    cmd.Parameters.Add("@IsReleased", SqlDbType.Bit).Value = detainedLicensesDTO.IsReleased;
                    cmd.Parameters.Add("@ReleaseDate", SqlDbType.SmallDateTime).Value = (object)detainedLicensesDTO.ReleaseDate ?? DBNull.Value;
                    cmd.Parameters.Add("@ReleasedByUserID", SqlDbType.Int).Value = (object)detainedLicensesDTO.ReleasedByUserId ?? DBNull.Value;
                    cmd.Parameters.Add("@ReleaseApplicationID", SqlDbType.Int).Value = (object)detainedLicensesDTO.ReleaseApplicationId ?? DBNull.Value;

                    connection.Open();
                    rowsAffected = cmd.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                clsEventLogger.LogEvent(
                    ex,
                    $"DetainID: {detainedLicensesDTO?.DetainId}, LicenseID: {detainedLicensesDTO?.LicenseId}"
                );
                rowsAffected = 0;
            }

            return (rowsAffected > 0);
        }

        public static enDeleteResult DeleteDetainLicense(int detainId)
        {
            int rowsAffected = 0;
            string query = @"
                DELETE FROM DetainedLicenses
                WHERE DetainID = @DetainID;
            ";

            try
            {
                using (SqlConnection connection = new SqlConnection(clsSettings.ConnectionString))
                using (SqlCommand cmd = new SqlCommand(query, connection))
                {
                    cmd.Parameters.Add("@DetainID", SqlDbType.Int).Value = detainId;

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

                clsEventLogger.LogEvent(ex, $"SQL Error #{ex.Number} | DetainID: {detainId}");
                return enDeleteResult.Error;
            }
            catch (Exception ex)
            {
                clsEventLogger.LogEvent(ex, $"DetainID: {detainId}");
                return enDeleteResult.Error;
            }

            return enDeleteResult.Success;
        }

        public static bool IsLicenseDetained(int licenseId)
        {
            bool isDetained = false;
            string query = @"
                SELECT 1
                FROM DetainedLicenses
                WHERE LicenseID = @LicenseID
                  AND IsReleased = 0;
            ";

            try
            {
                using (SqlConnection connection = new SqlConnection(clsSettings.ConnectionString))
                using (SqlCommand cmd = new SqlCommand(query, connection))
                {
                    cmd.Parameters.Add("@LicenseID", SqlDbType.Int).Value = licenseId;
                    connection.Open();

                    object result = cmd.ExecuteScalar();
                    isDetained = (result != null);
                }
            }
            catch (Exception ex)
            {
                clsEventLogger.LogEvent(ex, $"LicenseID: {licenseId}");
                isDetained = false;
            }

            return isDetained;
        }

        public static bool ReleaseDetainedLicense(
            int detainId,
            int applicationId,
            int releasedByUserId
        )
        {
            int rowsAffected = 0;
            string query = @"            
                UPDATE DetainedLicenses
                SET IsReleased = 1
                  , ReleaseDate = @ReleaseDate
                  , ReleasedByUserID = @ReleasedByUserID
                  , ReleaseApplicationID = @ReleaseApplicationID
                WHERE DetainID = @DetainID;
            ";

            try
            {
                using (SqlConnection connection = new SqlConnection(clsSettings.ConnectionString))
                using (SqlCommand cmd = new SqlCommand(query, connection))
                {
                    cmd.Parameters.Add("@DetainID", SqlDbType.Int).Value = detainId;
                    cmd.Parameters.Add("@ReleaseDate", SqlDbType.SmallDateTime).Value = DateTime.Now;
                    cmd.Parameters.Add("@ReleasedByUserID", SqlDbType.Int).Value = releasedByUserId;
                    cmd.Parameters.Add("@ReleaseApplicationID", SqlDbType.Int).Value = applicationId;

                    connection.Open();
                    rowsAffected = cmd.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                clsEventLogger.LogEvent(
                    ex,
                    $"DetainID: {detainId}, ReleaseApplicationID: {applicationId}, ReleasedByUserID: {releasedByUserId}"
                );

                rowsAffected = 0;
            }

            return (rowsAffected > 0);
        }

        public static DataTable GetAllDetainedLicenses()
        {
            DataTable dt = new DataTable();
            string query = @"
                SELECT DetainID
                     , LicenseID
                     , DetainDate
                     , IsReleased
                     , FineFees
                     , ReleaseDate
                     , NationalNo
                     , FullName
                     , ReleaseApplicationID
                     , PersonID
                FROM DetainedLicenses_View;
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
        }}
    }
