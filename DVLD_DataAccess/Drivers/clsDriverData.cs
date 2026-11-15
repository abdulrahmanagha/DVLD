using DVLD_DataAccess;
using DVLD_Shared;
using DVLD_Shared.Utility;
using System;
using System.Data;
using System.Data.SqlClient;

namespace DVLD_Business.Drivers
{
    public static class clsDriverData
    {
        public static bool GetDriverInfoByDriverId(DriverDTO driverDTO)
        {
            bool isFound = false;
            string query = @"                    
                SELECT DriverID
                     , PersonID
                     , CreatedByUserID
                     , CreatedDate
                FROM Drivers
                WHERE DriverID = @DriverID;
            ";

            try
            {
                using (SqlConnection connection = new SqlConnection(clsSettings.ConnectionString))
                using (SqlCommand cmd = new SqlCommand(query, connection))
                {
                    cmd.Parameters.Add("@DriverID", SqlDbType.Int).Value = driverDTO?.DriverId;

                    connection.Open();
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            driverDTO.PersonId = (int)reader["PersonID"];
                            driverDTO.CreatedByUserId = (int)reader["CreatedByUserID"];
                            driverDTO.CreatedDate = (DateTime)reader["CreatedDate"];
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
                clsEventLogger.LogEvent(ex, $"DriverID: {driverDTO?.DriverId}");
                isFound = false;
            }

            return isFound;
        }

        public static bool GetDriverInfoByPersonId(DriverDTO driverDTO)
        {
            bool isFound = false;
            string query = @"                    
                SELECT DriverID
                     , PersonID
                     , CreatedByUserID
                     , CreatedDate
                FROM Drivers
                WHERE PersonID = @PersonID;
            ";

            try
            {
                using (SqlConnection connection = new SqlConnection(clsSettings.ConnectionString))
                using (SqlCommand cmd = new SqlCommand(query, connection))
                {
                    cmd.Parameters.Add("@PersonID", SqlDbType.Int).Value = driverDTO.PersonId;

                    connection.Open();
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            driverDTO.DriverId = (int)reader["DriverID"];
                            driverDTO.CreatedByUserId = (int)reader["CreatedByUserID"];
                            driverDTO.CreatedDate = (DateTime)reader["CreatedDate"];
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
                clsEventLogger.LogEvent(ex, $"PersonID: {driverDTO?.PersonId}");
                isFound = false;
            }

            return isFound;
        }

        public static int AddNewDriver(DriverDTO driverDTO)
        {
            int driverId = -1;
            string query = @"
                INSERT INTO Drivers
                    (PersonID, CreatedByUserID, CreatedDate)
                VALUES
                    (@PersonID, @CreatedByUserID, @CreatedDate);

                SELECT CAST(SCOPE_IDENTITY() AS INT);
            ";

            try
            {
                using (SqlConnection connection = new SqlConnection(clsSettings.ConnectionString))
                using (SqlCommand cmd = new SqlCommand(query, connection))
                {
                    cmd.Parameters.Add("@PersonID", SqlDbType.Int).Value = driverDTO.PersonId;
                    cmd.Parameters.Add("@CreatedByUserID", SqlDbType.Int).Value = driverDTO.CreatedByUserId;
                    cmd.Parameters.Add("@CreatedDate", SqlDbType.SmallDateTime).Value = DateTime.Now;

                    connection.Open();
                    object result = cmd.ExecuteScalar();

                    if (result != null && result != DBNull.Value)
                    {
                        driverId = Convert.ToInt32(result);
                    }
                }
            }
            catch (Exception ex)
            {
                clsEventLogger.LogEvent(
                    ex,
                    $"PersonID: {driverDTO?.PersonId}, CreatedByUserID: {driverDTO?.CreatedByUserId}"
                );

                driverId = -1;
            }

            return driverId;
        }

        public static bool UpdateDriver(DriverDTO driverDTO)
        {
            int rowsAffected = 0;
            string query = @"               
                UPDATE Drivers
                SET PersonID = @PersonID
                  , CreatedByUserID = @CreatedByUserID
                  , CreatedDate = @CreatedDate
                WHERE DriverID = @DriverID;
            ";

            try
            {
                using (SqlConnection connection = new SqlConnection(clsSettings.ConnectionString))
                using (SqlCommand cmd = new SqlCommand(query, connection))
                {
                    cmd.Parameters.Add("@DriverID", SqlDbType.Int).Value = driverDTO.DriverId;
                    cmd.Parameters.Add("@PersonID", SqlDbType.Int).Value = driverDTO.PersonId;
                    cmd.Parameters.Add("@CreatedByUserID", SqlDbType.Int).Value = driverDTO.CreatedByUserId;
                    cmd.Parameters.Add("@CreatedDate", SqlDbType.SmallDateTime).Value = driverDTO.CreatedDate;

                    connection.Open();
                    rowsAffected = cmd.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                clsEventLogger.LogEvent(
                    ex,
                    $"DriverID: {driverDTO?.DriverId}, PersonID: {driverDTO?.PersonId}"
                );
                rowsAffected = 0;
            }

            return (rowsAffected > 0);
        }

        public static enDeleteResult DeleteDriver(int driverId)
        {
            int rowsAffected = 0;
            string query = @"             
                DELETE FROM Drivers
                WHERE DriverID = @DriverID;
            ";

            try
            {
                using (SqlConnection connection = new SqlConnection(clsSettings.ConnectionString))
                using (SqlCommand cmd = new SqlCommand(query, connection))
                {
                    cmd.Parameters.Add("@DriverID", SqlDbType.Int).Value = driverId;

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

                clsEventLogger.LogEvent(ex, $"SQL Error #{ex.Number} | DriverID: {driverId}");
                return enDeleteResult.Error;
            }
            catch (Exception ex)
            {
                clsEventLogger.LogEvent(ex, $"DriverID: {driverId}");
                return enDeleteResult.Error;
            }

            return enDeleteResult.Success;
        }

        public static bool IsDriverExist(int driverId)
        {
            bool isFound = false;
            string query = @"
                SELECT 1
                FROM Drivers
                WHERE DriverID = @DriverID;
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

        public static bool IsDriverExistForPersonId(int personId)
        {
            bool isFound = false;
            string query = @"
                SELECT 1
                FROM Drivers
                WHERE PersonID = @PersonID;
            ";

            try
            {
                using (SqlConnection connection = new SqlConnection(clsSettings.ConnectionString))
                using (SqlCommand cmd = new SqlCommand(query, connection))
                {
                    cmd.Parameters.Add("@PersonID", SqlDbType.Int).Value = personId;

                    connection.Open();
                    object result = cmd.ExecuteScalar();
                    isFound = (result != null);
                }
            }
            catch (Exception ex)
            {
                clsEventLogger.LogEvent(ex, $"PersonID: {personId}");
                isFound = false;
            }

            return isFound;
        }

        public static DataTable GetAllDrivers()
        {
            DataTable dt = new DataTable();
            string query = @"
                SELECT DriverID
                     , PersonID
                     , CreatedByUserID
                     , CreatedDate
                FROM Drivers;
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

        public static DataTable GetAllDriversWithDetails()
        {
            DataTable dt = new DataTable();
            string query = @"
                SELECT DriverID
                     , PersonID
                     , FullName
                     , CreatedDate
                     , ActiveLicense
                     , HasInternationalLicense
                FROM Drivers_View;
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
