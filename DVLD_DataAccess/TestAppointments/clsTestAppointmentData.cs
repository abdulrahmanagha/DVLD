using DVLD_Shared;
using DVLD_Shared.Utility;
using System;
using System.Data;
using System.Data.SqlClient;

namespace DVLD_DataAccess.TestAppointments
{
    public static class clsTestAppointmentData
    {
        public static bool GetTestAppointmentInfoById(TestAppointmentDTO testAppointmentDTO)
        {
            bool isFound = false;
            string query = @"         
                SELECT TestAppointmentID
                     , TestTypeID
                     , LocalDrivingLicenseApplicationID
                     , AppointmentDate
                     , PaidFees
                     , CreatedByUserID
                     , IsLocked
                     , RetakeTestApplicationID
                FROM TestAppointments
                WHERE TestAppointmentID = @TestAppointmentID;
            ";

            try
            {
                using (SqlConnection connection = new SqlConnection(clsSettings.ConnectionString))
                using (SqlCommand cmd = new SqlCommand(query, connection))
                {
                    cmd.Parameters.Add("@TestAppointmentID", SqlDbType.Int).Value = testAppointmentDTO?.TestAppointmentId;

                    connection.Open();
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            testAppointmentDTO.TestAppointmentId = (int)reader["TestAppointmentID"];
                            testAppointmentDTO.TestTypeId = (int)reader["TestTypeID"];
                            testAppointmentDTO.LocalDrivingLicenseApplicationId = (int)reader["LocalDrivingLicenseApplicationID"];
                            testAppointmentDTO.AppointmentDate = (DateTime)reader["AppointmentDate"];
                            testAppointmentDTO.PaidFees = (decimal)reader["PaidFees"];
                            testAppointmentDTO.CreatedByUserId = (int)reader["CreatedByUserID"];
                            testAppointmentDTO.IsLocked = (bool)reader["IsLocked"];
                            testAppointmentDTO.RetakeTestApplicationId = reader["RetakeTestApplicationID"] as int?;
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
                clsEventLogger.LogEvent(ex, $"TestAppointmentID: {testAppointmentDTO?.TestAppointmentId}");
                isFound = false;
            }

            return isFound;
        }

        public static bool GetLastTestAppointment(int localDrivingLicenseApplication, TestAppointmentDTO testAppointmentDTO)
        {
            bool isFound = false;
            string query = @"         
                SELECT TOP(1) TestAppointmentID
                     , TestTypeID
                     , LocalDrivingLicenseApplicationID
                     , AppointmentDate
                     , PaidFees
                     , CreatedByUserID
                     , IsLocked
                     , RetakeTestApplicationID
                FROM TestAppointments
                WHERE LocalDrivingLicenseApplicationID = @LocalDrivingLicenseApplicationID
                  AND TestTypeID = @TestTypeID
                ORDER BY TestAppointmentID DESC;
            ";

            try
            {
                using (SqlConnection connection = new SqlConnection(clsSettings.ConnectionString))
                using (SqlCommand cmd = new SqlCommand(query, connection))
                {
                    cmd.Parameters.Add("@LocalDrivingLicenseApplicationID", SqlDbType.Int).Value = localDrivingLicenseApplication;
                    cmd.Parameters.Add("@TestTypeID", SqlDbType.Int).Value = testAppointmentDTO.TestTypeId;

                    connection.Open();
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            testAppointmentDTO.TestAppointmentId = (int)reader["TestAppointmentID"];
                            testAppointmentDTO.TestTypeId = (int)reader["TestTypeID"];
                            testAppointmentDTO.LocalDrivingLicenseApplicationId = (int)reader["LocalDrivingLicenseApplicationID"];
                            testAppointmentDTO.AppointmentDate = (DateTime)reader["AppointmentDate"];
                            testAppointmentDTO.PaidFees = (decimal)reader["PaidFees"];
                            testAppointmentDTO.CreatedByUserId = (int)reader["CreatedByUserID"];
                            testAppointmentDTO.IsLocked = (bool)reader["IsLocked"];
                            testAppointmentDTO.RetakeTestApplicationId = reader["RetakeTestApplicationID"] as int?;
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
                clsEventLogger.LogEvent(
                    ex,
                    $"LocalDrivingLicenseApplicationID: {localDrivingLicenseApplication}, TestTypeID: {testAppointmentDTO?.TestTypeId}"
                );
                isFound = false;
            }

            return isFound;
        }

        public static int AddNewTestAppointment(TestAppointmentDTO testAppointmentDTO)
        {
            int testAppointment = -1;
            string query = @"
                INSERT INTO TestAppointments
                    (TestTypeID, LocalDrivingLicenseApplicationID, AppointmentDate, PaidFees
                     , CreatedByUserID, IsLocked, RetakeTestApplicationID)
                VALUES
                    (@TestTypeID, @LocalDrivingLicenseApplicationID, @AppointmentDate, @PaidFees
                     , @CreatedByUserID, @IsLocked, @RetakeTestApplicationID);

                SELECT CAST(SCOPE_IDENTITY() AS INT);
            ";

            try
            {
                using (SqlConnection connection = new SqlConnection(clsSettings.ConnectionString))
                using (SqlCommand cmd = new SqlCommand(query, connection))
                {
                    cmd.Parameters.Add("@TestTypeID", SqlDbType.Int).Value = testAppointmentDTO.TestTypeId;
                    cmd.Parameters.Add("@LocalDrivingLicenseApplicationID", SqlDbType.Int).Value = testAppointmentDTO.LocalDrivingLicenseApplicationId;
                    cmd.Parameters.Add("@AppointmentDate", SqlDbType.SmallDateTime).Value = testAppointmentDTO.AppointmentDate;
                    cmd.Parameters.Add("@PaidFees", SqlDbType.SmallMoney).Value = testAppointmentDTO.PaidFees;
                    cmd.Parameters.Add("@CreatedByUserID", SqlDbType.Int).Value = testAppointmentDTO.CreatedByUserId;
                    cmd.Parameters.Add("@IsLocked", SqlDbType.Bit).Value = testAppointmentDTO.IsLocked;
                    cmd.Parameters.Add("@RetakeTestApplicationID", SqlDbType.Int).Value = (object)testAppointmentDTO.RetakeTestApplicationId ?? DBNull.Value;

                    connection.Open();
                    object result = cmd.ExecuteScalar();

                    if (result != null && result != DBNull.Value)
                    {
                        testAppointment = Convert.ToInt32(result);
                    }
                }
            }
            catch (Exception ex)
            {
                clsEventLogger.LogEvent(
                    ex,
                    $"LocalDrivingLicenseApplicationID: {testAppointmentDTO?.LocalDrivingLicenseApplicationId}, " +
                    $"TestTypeID: {testAppointmentDTO?.TestTypeId}, CreatedByUserID: {testAppointmentDTO?.CreatedByUserId}"
                );
                testAppointment = -1;
            }

            return testAppointment;
        }

        public static bool UpdateTestAppointment(TestAppointmentDTO testAppointmentDTO)
        {
            int rowsAffected = 0;
            string query = @"
                UPDATE TestAppointments
                SET TestTypeID = @TestTypeID
                  , LocalDrivingLicenseApplicationID = @LocalDrivingLicenseApplicationID
                  , AppointmentDate = @AppointmentDate
                  , PaidFees = @PaidFees
                  , CreatedByUserID = @CreatedByUserID
                  , IsLocked = @IsLocked
                  , RetakeTestApplicationID = @RetakeTestApplicationID
                WHERE TestAppointmentID = @TestAppointmentID;
            ";

            try
            {
                using (SqlConnection connection = new SqlConnection(clsSettings.ConnectionString))
                using (SqlCommand cmd = new SqlCommand(query, connection))
                {
                    cmd.Parameters.Add("@TestAppointmentID", SqlDbType.Int).Value = testAppointmentDTO.TestAppointmentId;
                    cmd.Parameters.Add("@TestTypeID", SqlDbType.Int).Value = testAppointmentDTO.TestTypeId;
                    cmd.Parameters.Add("@LocalDrivingLicenseApplicationID", SqlDbType.Int).Value = testAppointmentDTO.LocalDrivingLicenseApplicationId;
                    cmd.Parameters.Add("@AppointmentDate", SqlDbType.SmallDateTime).Value = testAppointmentDTO.AppointmentDate;
                    cmd.Parameters.Add("@PaidFees", SqlDbType.SmallMoney).Value = testAppointmentDTO.PaidFees;
                    cmd.Parameters.Add("@CreatedByUserID", SqlDbType.Int).Value = testAppointmentDTO.CreatedByUserId;
                    cmd.Parameters.Add("@IsLocked", SqlDbType.Bit).Value = testAppointmentDTO.IsLocked;
                    cmd.Parameters.Add("@RetakeTestApplicationID", SqlDbType.Int).Value = (object)testAppointmentDTO.RetakeTestApplicationId ?? DBNull.Value;

                    connection.Open();
                    rowsAffected = cmd.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                clsEventLogger.LogEvent(
                    ex,
                    $"TestAppointmentID: {testAppointmentDTO?.TestAppointmentId}, " +
                    $"LocalDrivingLicenseApplicationID: {testAppointmentDTO?.LocalDrivingLicenseApplicationId}, " +
                    $"TestTypeID: {testAppointmentDTO?.TestTypeId}"
                );
                rowsAffected = 0;
            }

            return (rowsAffected > 0);
        }

        public static enDeleteResult DeleteTestAppointment(int testAppointment)
        {
            int rowsAffected = 0;
            string query = @"            
                DELETE FROM TestAppointments
                WHERE TestAppointmentID = @TestAppointmentID;
            ";

            try
            {
                using (SqlConnection connection = new SqlConnection(clsSettings.ConnectionString))
                using (SqlCommand cmd = new SqlCommand(query, connection))
                {
                    cmd.Parameters.Add("@TestAppointmentID", SqlDbType.Int).Value = testAppointment;

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

                clsEventLogger.LogEvent(ex, $"SQL Error #{ex.Number} | TestAppointmentID: {testAppointment}");
                return enDeleteResult.Error;
            }
            catch (Exception ex)
            {
                clsEventLogger.LogEvent(ex, $"TestAppointmentID: {testAppointment}");
                return enDeleteResult.Error;
            }

            return enDeleteResult.Success;
        }

        public static DataTable GetAllAppointmentTestForLocalApplication(int localApplication)
        {
            DataTable dt = new DataTable();
            string query = @"          
                SELECT TestAppointmentID
                     , AppointmentDate
                     , PaidFees
                     , IsLocked
                FROM TestAppointments
                WHERE LocalDrivingLicenseApplicationID = @LocalDrivingLicenseApplicationID;
            ";

            try
            {
                using (SqlConnection connection = new SqlConnection(clsSettings.ConnectionString))
                using (SqlCommand cmd = new SqlCommand(query, connection))
                {
                    cmd.Parameters.Add("@LocalDrivingLicenseApplicationID", SqlDbType.Int).Value = localApplication;
                    connection.Open();
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        dt.Load(reader);
                    }
                }
            }
            catch (Exception ex)
            {
                clsEventLogger.LogEvent(ex, $"LocalDrivingLicenseApplicationID: {localApplication}");
                dt = null;
            }

            return dt;
        }

        public static DataTable GetApplicationTestAppointmentsPerTestType(int localApplication, int testType)
        {
            DataTable dt = new DataTable();
            string query = @"          
                SELECT TestAppointmentID
                     , AppointmentDate
                     , PaidFees
                     , IsLocked
                FROM TestAppointments
                WHERE LocalDrivingLicenseApplicationID = @LocalDrivingLicenseApplicationID
                  AND TestTypeID = @TestTypeID;
            ";

            try
            {
                using (SqlConnection connection = new SqlConnection(clsSettings.ConnectionString))
                using (SqlCommand cmd = new SqlCommand(query, connection))
                {
                    cmd.Parameters.Add("@LocalDrivingLicenseApplicationID", SqlDbType.Int).Value = localApplication;
                    cmd.Parameters.Add("@TestTypeID", SqlDbType.Int).Value = testType;
                    connection.Open();

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        dt.Load(reader);
                    }
                }
            }
            catch (Exception ex)
            {
                clsEventLogger.LogEvent(
                    ex,
                    $"LocalDrivingLicenseApplicationID: {localApplication}, TestTypeID: {testType}"
                );
                dt = null;
            }

            return dt;
        }

        public static bool LockAppointment(int testAppointmentId)
        {
            int rowsAffected = 0;
            string query = @"
                UPDATE TestAppointments
                SET IsLocked = 1
                WHERE TestAppointmentID = @TestAppointmentID;
            ";

            try
            {
                using (SqlConnection connection = new SqlConnection(clsSettings.ConnectionString))
                using (SqlCommand cmd = new SqlCommand(query, connection))
                {
                    cmd.Parameters.Add("@TestAppointmentID", SqlDbType.Int).Value = testAppointmentId;

                    connection.Open();
                    rowsAffected = cmd.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                clsEventLogger.LogEvent(ex, $"TestAppointmentID: {testAppointmentId}");
                rowsAffected = 0;
            }

            return (rowsAffected > 0);
        }

        public static int GetTestId(int testAppointmentId)
        {
            int testId = -1;
            string query = @"
                SELECT TestID 
                FROM Tests 
                WHERE TestAppointmentID = @TestAppointmentID;
            ";

            try
            {
                using (SqlConnection connection = new SqlConnection(clsSettings.ConnectionString))
                using (SqlCommand cmd = new SqlCommand(query, connection))
                {
                    cmd.Parameters.Add("@TestAppointmentID", SqlDbType.Int).Value = testAppointmentId;
                    connection.Open();

                    object result = cmd.ExecuteScalar();

                    if (result != null && result != DBNull.Value)
                    {
                        testId = Convert.ToInt32(result);
                    }
                }
            }
            catch (Exception ex)
            {
                clsEventLogger.LogEvent(ex, $"TestAppointmentID: {testAppointmentId}");
                testId = -1;
            }

            return testId;
        }

        public static bool HasAnyAppointment(int localDrivingLicenseApplicationId)
        {
            bool isFound = false;
            string query = @"
                SELECT TOP (1) 1
                FROM TestAppointments AS TA
                WHERE LocalDrivingLicenseApplicationID = @LocalDrivingLicenseApplicationID;
            ";

            try
            {
                using (SqlConnection connection = new SqlConnection(clsSettings.ConnectionString))
                using (SqlCommand cmd = new SqlCommand(query, connection))
                {
                    cmd.Parameters.Add("@LocalDrivingLicenseApplicationID", SqlDbType.Int).Value = localDrivingLicenseApplicationId;
                    connection.Open();

                    object result = cmd.ExecuteScalar();
                    isFound = (result != null);
                }
            }
            catch (Exception ex)
            {
                clsEventLogger.LogEvent(ex, $"LocalDrivingLicenseApplicationID: {localDrivingLicenseApplicationId}");
                isFound = false;
            }

            return isFound;
        }
    }
}