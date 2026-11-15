using DVLD_Shared;
using DVLD_Shared.Utility;
using System;
using System.Data;
using System.Data.SqlClient;

namespace DVLD_DataAccess.Tests
{
    public class clsTestData
    {
        public static bool GetTestInfoById(TestDTO testDTO)
        {
            bool isFound = false;
            string query = @"
                SELECT TestAppointmentID
                     , TestResult
                     , Notes
                     , CreatedByUserID
                FROM Tests
                WHERE TestID = @TestID;
            ";

            try
            {
                using (SqlConnection connection = new SqlConnection(clsSettings.ConnectionString))
                using (SqlCommand cmd = new SqlCommand(query, connection))
                {
                    cmd.Parameters.Add("@TestID", SqlDbType.Int).Value = testDTO?.TestId;

                    connection.Open();
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            testDTO.TestAppointmentId = (int)reader["TestAppointmentID"];
                            testDTO.TestResult = (bool)reader["TestResult"];
                            testDTO.Notes = reader["Notes"] as string;
                            testDTO.CreatedByUserId = (int)reader["CreatedByUserID"];
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
                clsEventLogger.LogEvent(ex, $"TestID: {testDTO?.TestId}");
                isFound = false;
            }

            return isFound;
        }

        public static bool GetTestInfoByAppointmentId(TestDTO testDTO)
        {
            bool isFound = false;
            string query = @"
                SELECT TestID
                     , TestAppointmentID
                     , TestResult
                     , Notes
                     , CreatedByUserID
                FROM Tests
                WHERE TestAppointmentID = @TestAppointmentID;
            ";

            try
            {
                using (SqlConnection connection = new SqlConnection(clsSettings.ConnectionString))
                using (SqlCommand cmd = new SqlCommand(query, connection))
                {
                    cmd.Parameters.Add("@TestAppointmentID", SqlDbType.Int).Value = testDTO.TestAppointmentId;

                    connection.Open();
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            testDTO.TestId = (int)reader["TestID"];
                            testDTO.TestResult = (bool)reader["TestResult"];
                            testDTO.Notes = reader["Notes"] as string;
                            testDTO.CreatedByUserId = (int)reader["CreatedByUserID"];
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
                clsEventLogger.LogEvent(ex, $"TestAppointmentID: {testDTO?.TestAppointmentId}");
                isFound = false;
            }

            return isFound;
        }

        public static bool GetLastTestByPersonAndTestTypeAndLicenseClass(
            int personId,
            int licenseClassId,
            int testTypeId,
            TestDTO testDTO)
        {
            bool isFound = false;
            string query = @"
                SELECT TOP (1) T.TestID
                     , T.TestAppointmentID
                     , T.TestResult
                     , T.Notes
                     , T.CreatedByUserID
                FROM Tests AS T
                JOIN TestAppointments AS TA
                    ON TA.TestAppointmentID = T.TestAppointmentID
                JOIN LocalDrivingLicenseApplications AS LDL
                    ON LDL.LocalDrivingLicenseApplicationID = TA.LocalDrivingLicenseApplicationID
                JOIN Applications AS APP
                    ON APP.ApplicationID = LDL.ApplicationID
                WHERE APP.ApplicantPersonID = @ApplicantPersonID
                  AND TA.TestTypeID = @TestTypeID
                  AND LDL.LicenseClassID = @LicenseClassID
                ORDER BY T.TestAppointmentID DESC;
            ";

            try
            {
                using (SqlConnection connection = new SqlConnection(clsSettings.ConnectionString))
                using (SqlCommand cmd = new SqlCommand(query, connection))
                {
                    cmd.Parameters.Add("@ApplicantPersonID", SqlDbType.Int).Value = personId;
                    cmd.Parameters.Add("@TestTypeID", SqlDbType.Int).Value = testTypeId;
                    cmd.Parameters.Add("@LicenseClassID", SqlDbType.Int).Value = licenseClassId;

                    connection.Open();
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            testDTO.TestId = (int)reader["TestID"];
                            testDTO.TestAppointmentId = (int)reader["TestAppointmentID"];
                            testDTO.TestResult = (bool)reader["TestResult"];
                            testDTO.Notes = reader["Notes"] as string;
                            testDTO.CreatedByUserId = (int)reader["CreatedByUserID"];
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
                    $"PersonID: {personId}, LicenseClassID: {licenseClassId}, TestTypeID: {testTypeId}"
                );
                isFound = false;
            }

            return isFound;
        }

        public static int AddNewTest(TestDTO testDTO)
        {
            int testId = -1;
            string query = @"
                INSERT INTO Tests
                    (TestAppointmentID, TestResult, Notes, CreatedByUserID)
                VALUES
                    (@TestAppointmentID, @TestResult, @Notes, @CreatedByUserID);

                UPDATE TestAppointments
                SET IsLocked = 1
                WHERE TestAppointmentID = @TestAppointmentID;

                SELECT CAST(SCOPE_IDENTITY() AS INT);
            ";

            try
            {
                using (SqlConnection connection = new SqlConnection(clsSettings.ConnectionString))
                using (SqlCommand cmd = new SqlCommand(query, connection))
                {
                    cmd.Parameters.Add("@TestAppointmentID", SqlDbType.Int).Value = testDTO.TestAppointmentId;
                    cmd.Parameters.Add("@TestResult", SqlDbType.Bit).Value = testDTO.TestResult;
                    cmd.Parameters.Add("@Notes", SqlDbType.NVarChar, 500).Value = (object)testDTO.Notes ?? DBNull.Value;
                    cmd.Parameters.Add("@CreatedByUserID", SqlDbType.Int).Value = testDTO.CreatedByUserId;

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
                clsEventLogger.LogEvent(
                    ex,
                    $"TestAppointmentID: {testDTO?.TestAppointmentId}, TestResult: {testDTO?.TestResult}, CreatedByUserID: {testDTO?.CreatedByUserId}"
                );
                testId = -1;
            }

            return testId;
        }

        public static bool UpdateTest(TestDTO testDTO)
        {
            int rowsAffected = 0;
            string query = @"    
                UPDATE Tests
                SET TestAppointmentID = @TestAppointmentID
                  , TestResult = @TestResult
                  , Notes = @Notes
                  , CreatedByUserID = @CreatedByUserID
                WHERE TestID = @TestID;
            ";

            try
            {
                using (SqlConnection connection = new SqlConnection(clsSettings.ConnectionString))
                using (SqlCommand cmd = new SqlCommand(query, connection))
                {
                    cmd.Parameters.Add("@TestID", SqlDbType.Int).Value = testDTO.TestId;
                    cmd.Parameters.Add("@TestAppointmentID", SqlDbType.Int).Value = testDTO.TestAppointmentId;
                    cmd.Parameters.Add("@TestResult", SqlDbType.Bit).Value = testDTO.TestResult;
                    cmd.Parameters.Add("@Notes", SqlDbType.NVarChar, 500).Value = (object)testDTO.Notes ?? DBNull.Value;
                    cmd.Parameters.Add("@CreatedByUserID", SqlDbType.Int).Value = testDTO.CreatedByUserId;

                    connection.Open();
                    rowsAffected = cmd.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                clsEventLogger.LogEvent(
                    ex,
                    $"TestID: {testDTO?.TestId}, TestAppointmentID: {testDTO?.TestAppointmentId}"
                );
                rowsAffected = 0;
            }

            return (rowsAffected > 0);
        }

        public static int GetPassedTestCount(int localDrivingLicenseApplicationId)
        {
            int passedTestCount = -1;
            string query = @"       
                SELECT COUNT(T.TestResult) AS NumberOfPassedTest
                FROM TestAppointments AS TA
                JOIN Tests AS T
                    ON T.TestAppointmentID = TA.TestAppointmentID
                WHERE TA.LocalDrivingLicenseApplicationID = @LocalDrivingLicenseApplicationID
                  AND T.TestResult = 1;
            ";

            try
            {
                using (SqlConnection connection = new SqlConnection(clsSettings.ConnectionString))
                using (SqlCommand cmd = new SqlCommand(query, connection))
                {
                    cmd.Parameters.Add("@LocalDrivingLicenseApplicationID", SqlDbType.Int).Value = localDrivingLicenseApplicationId;

                    connection.Open();
                    object result = cmd.ExecuteScalar();

                    if (result != null && result != DBNull.Value)
                    {
                        passedTestCount = Convert.ToInt32(result);
                    }
                }
            }
            catch (Exception ex)
            {
                clsEventLogger.LogEvent(ex, $"LocalDrivingLicenseApplicationID: {localDrivingLicenseApplicationId}");
                passedTestCount = -1;
            }

            return passedTestCount;
        }
    }
}