using DVLD_Shared;
using DVLD_Shared.Utility;
using System;
using System.Data;
using System.Data.SqlClient;

namespace DVLD_DataAccess.Licenses
{
    public class clsLicenseData
    {
        public static bool GetLicenseInfoByLicenseId(LicenseDTO licenseDTO)
        {
            bool isFound = false;
            string query = @"
                SELECT LicenseID
                     , ApplicationID
                     , DriverID
                     , LicenseClass
                     , IssueDate
                     , ExpirationDate
                     , Notes
                     , PaidFees
                     , IsActive
                     , IssueReason
                     , CreatedByUserID
                FROM Licenses
                WHERE LicenseID = @LicenseID;
            ";

            try
            {
                using (SqlConnection connection = new SqlConnection(clsSettings.ConnectionString))
                using (SqlCommand cmd = new SqlCommand(query, connection))
                {
                    cmd.Parameters.Add("@LicenseID", SqlDbType.Int).Value = licenseDTO?.LicenseId;

                    connection.Open();
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            licenseDTO.ApplicationId = (int)reader["ApplicationID"];
                            licenseDTO.DriverId = (int)reader["DriverID"];
                            licenseDTO.LicenseClassId = (int)reader["LicenseClass"];
                            licenseDTO.IssueDate = (DateTime)reader["IssueDate"];
                            licenseDTO.ExpirationDate = (DateTime)reader["ExpirationDate"];
                            licenseDTO.Notes = reader["Notes"] as string;
                            licenseDTO.PaidFees = (decimal)reader["PaidFees"];
                            licenseDTO.IsActive = (bool)reader["IsActive"];
                            licenseDTO.IssueReason = (byte)reader["IssueReason"];
                            licenseDTO.CreatedByUserId = (int)reader["CreatedByUserID"];
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
                clsEventLogger.LogEvent(ex, $"LicenseID: {licenseDTO?.LicenseId}");
                isFound = false;
            }

            return isFound;
        }

        public static bool GetLicenseInfoByApplicationId(LicenseDTO licenseDTO)
        {
            bool isFound = false;
            string query = @"
                SELECT LicenseID
                     , ApplicationID
                     , DriverID
                     , LicenseClass
                     , IssueDate
                     , ExpirationDate
                     , Notes
                     , PaidFees
                     , IsActive
                     , IssueReason
                     , CreatedByUserID
                FROM Licenses
                WHERE ApplicationID = @ApplicationID;
            ";

            try
            {
                using (SqlConnection connection = new SqlConnection(clsSettings.ConnectionString))
                using (SqlCommand cmd = new SqlCommand(query, connection))
                {
                    cmd.Parameters.Add("@ApplicationID", SqlDbType.Int).Value = licenseDTO.ApplicationId;

                    connection.Open();
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            licenseDTO.LicenseId = (int)reader["LicenseID"];
                            licenseDTO.ApplicationId = (int)reader["ApplicationID"];
                            licenseDTO.DriverId = (int)reader["DriverID"];
                            licenseDTO.LicenseClassId = (int)reader["LicenseClass"];
                            licenseDTO.IssueDate = (DateTime)reader["IssueDate"];
                            licenseDTO.ExpirationDate = (DateTime)reader["ExpirationDate"];
                            licenseDTO.Notes = reader["Notes"] as string;
                            licenseDTO.PaidFees = (decimal)reader["PaidFees"];
                            licenseDTO.IsActive = (bool)reader["IsActive"];
                            licenseDTO.IssueReason = (byte)reader["IssueReason"];
                            licenseDTO.CreatedByUserId = (int)reader["CreatedByUserID"];
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
                clsEventLogger.LogEvent(ex, $"ApplicationID: {licenseDTO?.ApplicationId}");
                isFound = false;
            }

            return isFound;
        }

        public static int AddNewLicense(LicenseDTO licenseDTO)
        {
            int licenseId = -1;
            string query = @"
                INSERT INTO Licenses
                    (ApplicationID, DriverID, LicenseClass, IssueDate, ExpirationDate
                     , Notes, PaidFees, IsActive, IssueReason, CreatedByUserID)
                VALUES
                    (@ApplicationID, @DriverID, @LicenseClass, @IssueDate, @ExpirationDate
                     , @Notes, @PaidFees, @IsActive, @IssueReason, @CreatedByUserID);

                SELECT CAST(SCOPE_IDENTITY() AS INT);
            ";

            try
            {
                using (SqlConnection connection = new SqlConnection(clsSettings.ConnectionString))
                using (SqlCommand cmd = new SqlCommand(query, connection))
                {
                    cmd.Parameters.Add("@ApplicationID", SqlDbType.Int).Value = licenseDTO.ApplicationId;
                    cmd.Parameters.Add("@DriverID", SqlDbType.Int).Value = licenseDTO.DriverId;
                    cmd.Parameters.Add("@LicenseClass", SqlDbType.Int).Value = licenseDTO.LicenseClassId;
                    cmd.Parameters.Add("@IssueDate", SqlDbType.DateTime).Value = licenseDTO.IssueDate;
                    cmd.Parameters.Add("@ExpirationDate", SqlDbType.DateTime).Value = licenseDTO.ExpirationDate;
                    cmd.Parameters.Add("@Notes", SqlDbType.NVarChar, 500).Value = (object)licenseDTO.Notes ?? DBNull.Value;
                    cmd.Parameters.Add("@PaidFees", SqlDbType.SmallMoney).Value = licenseDTO.PaidFees;
                    cmd.Parameters.Add("@IsActive", SqlDbType.Bit).Value = licenseDTO.IsActive;
                    cmd.Parameters.Add("@IssueReason", SqlDbType.TinyInt).Value = licenseDTO.IssueReason;
                    cmd.Parameters.Add("@CreatedByUserID", SqlDbType.Int).Value = licenseDTO.CreatedByUserId;

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
                    $"ApplicationID: {licenseDTO?.ApplicationId}, DriverID: {licenseDTO?.DriverId}, " +
                    $"LicenseClassID: {licenseDTO?.LicenseClassId}, CreatedByUserID: {licenseDTO?.CreatedByUserId}"
                );
                licenseId = -1;
            }

            return licenseId;
        }

        public static bool UpdateLicense(LicenseDTO licenseDTO)
        {
            int rowsAffected = 0;
            string query = @"            
                UPDATE Licenses
                SET ApplicationID = @ApplicationID
                  , DriverID = @DriverID
                  , LicenseClass = @LicenseClass
                  , IssueDate = @IssueDate
                  , ExpirationDate = @ExpirationDate
                  , Notes = @Notes
                  , PaidFees = @PaidFees
                  , IsActive = @IsActive
                  , IssueReason = @IssueReason
                  , CreatedByUserID = @CreatedByUserID
                WHERE LicenseID = @LicenseID;
            ";

            try
            {
                using (SqlConnection connection = new SqlConnection(clsSettings.ConnectionString))
                using (SqlCommand cmd = new SqlCommand(query, connection))
                {
                    cmd.Parameters.Add("@LicenseID", SqlDbType.Int).Value = licenseDTO.LicenseId;
                    cmd.Parameters.Add("@ApplicationID", SqlDbType.Int).Value = licenseDTO.ApplicationId;
                    cmd.Parameters.Add("@DriverID", SqlDbType.Int).Value = licenseDTO.DriverId;
                    cmd.Parameters.Add("@LicenseClass", SqlDbType.Int).Value = licenseDTO.LicenseClassId;
                    cmd.Parameters.Add("@IssueDate", SqlDbType.DateTime).Value = licenseDTO.IssueDate;
                    cmd.Parameters.Add("@ExpirationDate", SqlDbType.DateTime).Value = licenseDTO.ExpirationDate;
                    cmd.Parameters.Add("@Notes", SqlDbType.NVarChar, 500).Value = (object)licenseDTO.Notes ?? DBNull.Value;
                    cmd.Parameters.Add("@PaidFees", SqlDbType.SmallMoney).Value = licenseDTO.PaidFees;
                    cmd.Parameters.Add("@IsActive", SqlDbType.Bit).Value = licenseDTO.IsActive;
                    cmd.Parameters.Add("@IssueReason", SqlDbType.TinyInt).Value = licenseDTO.IssueReason;
                    cmd.Parameters.Add("@CreatedByUserID", SqlDbType.Int).Value = licenseDTO.CreatedByUserId;

                    connection.Open();
                    rowsAffected = cmd.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                clsEventLogger.LogEvent(
                    ex,
                    $"LicenseID: {licenseDTO?.LicenseId}, ApplicationID: {licenseDTO?.ApplicationId}, DriverID: {licenseDTO?.DriverId}"
                );
                rowsAffected = 0;
            }

            return (rowsAffected > 0);
        }

        public static enDeleteResult DeleteLicense(int licenseId)
        {
            int rowsAffected = 0;
            string query = @"
                DELETE FROM Licenses
                WHERE LicenseID = @LicenseID;
            ";

            try
            {
                using (SqlConnection connection = new SqlConnection(clsSettings.ConnectionString))
                using (SqlCommand cmd = new SqlCommand(query, connection))
                {
                    cmd.Parameters.Add("@LicenseID", SqlDbType.Int).Value = licenseId;

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

                clsEventLogger.LogEvent(ex, $"SQL Error #{ex.Number} | LicenseID: {licenseId}");
                return enDeleteResult.Error;
            }
            catch (Exception ex)
            {
                clsEventLogger.LogEvent(ex, $"LicenseID: {licenseId}");
                return enDeleteResult.Error;
            }

            return enDeleteResult.Success;
        }

        public static DataTable GetDriverLicenses(int diverId)
        {
            DataTable dt = new DataTable();
            string query = @"          
                SELECT L.LicenseID
                     , L.ApplicationID
                     , LC.ClassName
                     , L.IssueDate
                     , L.ExpirationDate
                     , L.IsActive
                FROM Licenses AS L
                JOIN LicenseClasses AS LC
                    ON LC.LicenseClassID = L.LicenseClass
                WHERE DriverID = @DriverID;
            ";

            try
            {
                using (SqlConnection connection = new SqlConnection(clsSettings.ConnectionString))
                using (SqlCommand cmd = new SqlCommand(query, connection))
                {
                    cmd.Parameters.Add("@DriverID", SqlDbType.Int).Value = diverId;

                    connection.Open();
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        dt.Load(reader);
                    }
                }
            }
            catch (Exception ex)
            {
                clsEventLogger.LogEvent(ex, $"DriverID: {diverId}");
                dt = null;
            }

            return dt;
        }

        public static bool IsLicenseExistByPersonID(int personId, int licenseClassId)
        {
            bool isFound = false;
            string query = @"
                SELECT 1 
                FROM Licenses L
                INNER JOIN Drivers D ON L.DriverID = D.DriverID
                WHERE D.PersonID = @PersonID 
                  AND L.LicenseClass = @LicenseClassID;
            ";

            try
            {
                using (SqlConnection connection = new SqlConnection(clsSettings.ConnectionString))
                using (SqlCommand cmd = new SqlCommand(query, connection))
                {
                    cmd.Parameters.Add("@PersonID", SqlDbType.Int).Value = personId;
                    cmd.Parameters.Add("@LicenseClassID", SqlDbType.Int).Value = licenseClassId;

                    connection.Open();
                    object result = cmd.ExecuteScalar();
                    isFound = (result != null);
                }
            }
            catch (Exception ex)
            {
                clsEventLogger.LogEvent(ex, $"PersonID: {personId}, LicenseClassID: {licenseClassId}");
                isFound = false;
            }

            return isFound;
        }

        public static int GetActiveLicenseIDByPersonID(int personId, int licenseClassId)
        {
            int licenseId = -1;
            string query = @"
                SELECT LicenseID
                FROM Licenses AS L
                JOIN Drivers AS D
                    ON D.DriverID = L.DriverID
                WHERE D.PersonID = @PersonID
                  AND L.LicenseClass = @LicenseClassID
                  AND L.IsActive = 1;
            ";

            try
            {
                using (SqlConnection connection = new SqlConnection(clsSettings.ConnectionString))
                using (SqlCommand cmd = new SqlCommand(query, connection))
                {
                    cmd.Parameters.Add("@PersonID", SqlDbType.Int).Value = personId;
                    cmd.Parameters.Add("@LicenseClassID", SqlDbType.Int).Value = licenseClassId;

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
                clsEventLogger.LogEvent(ex, $"PersonID: {personId}, LicenseClassID: {licenseClassId}");
                licenseId = -1;
            }

            return licenseId;
        }

        public static bool DeactivateLicense(int licenseId)
        {
            int rowsAffected = 0;
            string query = @"            
                UPDATE Licenses
                SET IsActive = 0
                WHERE LicenseID = @LicenseID;
            ";

            try
            {
                using (SqlConnection connection = new SqlConnection(clsSettings.ConnectionString))
                using (SqlCommand cmd = new SqlCommand(query, connection))
                {
                    cmd.Parameters.Add("@LicenseID", SqlDbType.Int).Value = licenseId;
                    connection.Open();

                    rowsAffected = cmd.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                clsEventLogger.LogEvent(ex, $"LicenseID: {licenseId}");
                rowsAffected = 0;
            }

            return (rowsAffected > 0);
        }

        public static int GetLicenseIdByApplicationId(int applicationId)
        {
            int licenseId = -1;
            string query = @"
                SELECT LicenseID 
                FROM Licenses 
                WHERE ApplicationID = @ApplicationID;
            ";

            try
            {
                using (SqlConnection connection = new SqlConnection(clsSettings.ConnectionString))
                using (SqlCommand cmd = new SqlCommand(query, connection))
                {
                    cmd.Parameters.Add("@ApplicationID", SqlDbType.Int).Value = applicationId;

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
                clsEventLogger.LogEvent(ex, $"ApplicationID: {applicationId}");
                licenseId = -1;
            }

            return licenseId;
        }
    }
}