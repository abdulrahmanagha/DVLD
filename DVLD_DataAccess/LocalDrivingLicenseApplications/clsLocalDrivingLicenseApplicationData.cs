using DVLD_Shared;
using DVLD_Shared.Utility;
using System;
using System.Data;
using System.Data.SqlClient;

namespace DVLD_DataAccess.LocalDrivingLicenseApplications
{
    public class clsLocalDrivingLicenseApplicationData
    {
        public static bool GetLocalDrivingLicenseApplicationInfoById(
            int localDrivingLicenseApplicationId,
            ref int applicationId,
            ref int licenseClassId)
        {
            bool isFound = false;
            string query = @"             
                SELECT LocalDrivingLicenseApplicationID
                     , ApplicationID
                     , LicenseClassID
                FROM LocalDrivingLicenseApplications
                WHERE LocalDrivingLicenseApplicationID = @LocalDrivingLicenseApplicationID;
            ";

            try
            {
                using (SqlConnection connection = new SqlConnection(clsSettings.ConnectionString))
                using (SqlCommand cmd = new SqlCommand(query, connection))
                {
                    cmd.Parameters.Add("@LocalDrivingLicenseApplicationID", SqlDbType.Int).Value = localDrivingLicenseApplicationId;

                    connection.Open();
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            applicationId = (int)reader["ApplicationID"];
                            licenseClassId = (int)reader["LicenseClassID"];
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
                clsEventLogger.LogEvent(ex, $"LocalDrivingLicenseApplicationID: {localDrivingLicenseApplicationId}");
                isFound = false;
            }

            return isFound;
        }

        public static bool GetLocalDrivingLicenseApplicationInfoByApplicationId(
            ref int localDrivingLicenseApplicationId,
            int applicationId,
            ref int licenseClassId)
        {
            bool isFound = false;
            string query = @"             
                SELECT LocalDrivingLicenseApplicationID
                     , ApplicationID
                     , LicenseClassID
                FROM LocalDrivingLicenseApplications
                WHERE ApplicationID = @ApplicationID;
            ";

            try
            {
                using (SqlConnection connection = new SqlConnection(clsSettings.ConnectionString))
                using (SqlCommand cmd = new SqlCommand(query, connection))
                {
                    cmd.Parameters.Add("@ApplicationID", SqlDbType.Int).Value = applicationId;

                    connection.Open();
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            localDrivingLicenseApplicationId = (int)reader["LocalDrivingLicenseApplicationID"];
                            licenseClassId = (int)reader["LicenseClassID"];
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
                clsEventLogger.LogEvent(ex, $"ApplicationID: {applicationId}");
                isFound = false;
            }

            return isFound;
        }

        public static int AddNewLocalDrivingLicenseApplication(int applicationId, int licenseClassId)
        {
            int localDrivingLicenseApplicationId = -1;
            string query = @"
                INSERT INTO LocalDrivingLicenseApplications
                    (ApplicationID, LicenseClassID)
                VALUES 
                    (@ApplicationID, @LicenseClassID);

                SELECT CAST(SCOPE_IDENTITY() AS INT);
            ";

            try
            {
                using (SqlConnection connection = new SqlConnection(clsSettings.ConnectionString))
                using (SqlCommand cmd = new SqlCommand(query, connection))
                {
                    cmd.Parameters.Add("@ApplicationID", SqlDbType.Int).Value = applicationId;
                    cmd.Parameters.Add("@LicenseClassID", SqlDbType.Int).Value = licenseClassId;

                    connection.Open();
                    object result = cmd.ExecuteScalar();

                    if (result != null && result != DBNull.Value)
                    {
                        localDrivingLicenseApplicationId = Convert.ToInt32(result);
                    }
                }
            }
            catch (Exception ex)
            {
                clsEventLogger.LogEvent(
                    ex,
                    $"ApplicationID: {applicationId}, LicenseClassID: {licenseClassId}"
                );
                localDrivingLicenseApplicationId = -1;
            }

            return localDrivingLicenseApplicationId;
        }

        public static bool UpdateLocalDrivingLicenseApplication(
            int localDrivingLicenseApplicationId,
            int applicationID,
            int licenseClassId)
        {
            int rowsAffected = 0;
            string query = @"
                UPDATE LocalDrivingLicenseApplications
                SET ApplicationID = @ApplicationID
                  , LicenseClassID = @LicenseClassID
                WHERE LocalDrivingLicenseApplicationID = @LocalDrivingLicenseApplicationID;
            ";

            try
            {
                using (SqlConnection connection = new SqlConnection(clsSettings.ConnectionString))
                using (SqlCommand cmd = new SqlCommand(query, connection))
                {
                    cmd.Parameters.Add("@LocalDrivingLicenseApplicationID", SqlDbType.Int).Value = localDrivingLicenseApplicationId;
                    cmd.Parameters.Add("@ApplicationID", SqlDbType.Int).Value = applicationID;
                    cmd.Parameters.Add("@LicenseClassID", SqlDbType.Int).Value = licenseClassId;

                    connection.Open();
                    rowsAffected = cmd.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                clsEventLogger.LogEvent(
                    ex,
                    $"LocalDrivingLicenseApplicationID: {localDrivingLicenseApplicationId}, ApplicationID: {applicationID}, LicenseClassID: {licenseClassId}"
                );
                rowsAffected = 0;
            }

            return (rowsAffected > 0);
        }

        public static enDeleteResult DeleteLocalDrivingLicenseApplication(int localDrivingLicenseApplicationId)
        {
            int rowsAffected = 0;
            string query = @"
                DELETE FROM LocalDrivingLicenseApplications
                WHERE LocalDrivingLicenseApplicationID = @LocalDrivingLicenseApplicationID;
            ";

            try
            {
                using (SqlConnection connection = new SqlConnection(clsSettings.ConnectionString))
                using (SqlCommand cmd = new SqlCommand(query, connection))
                {
                    cmd.Parameters.Add("@LocalDrivingLicenseApplicationID", SqlDbType.Int).Value = localDrivingLicenseApplicationId;

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

                clsEventLogger.LogEvent(ex, $"SQL Error #{ex.Number} | LocalDrivingLicenseApplicationID: {localDrivingLicenseApplicationId}");
                return enDeleteResult.Error;
            }
            catch (Exception ex)
            {
                clsEventLogger.LogEvent(ex, $"LocalDrivingLicenseApplicationID: {localDrivingLicenseApplicationId}");
                return enDeleteResult.Error;
            }

            return enDeleteResult.Success;
        }

        public static DataTable GetAllLocalDrivingLicenseApplications()
        {
            DataTable dt = new DataTable();
            string query = @"
                SELECT LocalDrivingLicenseApplicationID
                     , ApplicationID
                     , LicenseClassID
                FROM LocalDrivingLicenseApplications;
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

        public static DataTable GetAllLocalDrivingLicenseApplicationsWithDetails()
        {
            DataTable dt = new DataTable();
            string query = @"         
                SELECT LocalDrivingLicenseApplicationID
                     , ClassName
                     , NationalNo
                     , FullName
                     , ApplicationDate
                     , PassedTestCount
                     , ApplicationStatusName
                     , ApplicationStatus
                FROM LocalDrivingLicenseApplications_View;
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

        public static int GetFailedTestCount(
            int localDrivingLicenseApplicationId,
            int testType)
        {
            int failTestCount = 0;
            string query = @"
                SELECT COUNT(*) AS FailedTestCount
                FROM TestAppointments AS TA
                JOIN Tests AS T
                    ON T.TestAppointmentID = TA.TestAppointmentID
                WHERE TA.LocalDrivingLicenseApplicationID = @LocalDrivingLicenseApplicationID
                  AND TA.TestTypeID = @TestTypeID
                  AND T.TestResult = 0;
            ";

            try
            {
                using (SqlConnection connection = new SqlConnection(clsSettings.ConnectionString))
                using (SqlCommand cmd = new SqlCommand(query, connection))
                {
                    cmd.Parameters.Add("@LocalDrivingLicenseApplicationID", SqlDbType.Int).Value = localDrivingLicenseApplicationId;
                    cmd.Parameters.Add("@TestTypeID", SqlDbType.Int).Value = testType;

                    connection.Open();
                    object result = cmd.ExecuteScalar();

                    if (result != null && result != DBNull.Value)
                    {
                        failTestCount = Convert.ToInt32(result);
                    }
                }
            }
            catch (Exception ex)
            {
                clsEventLogger.LogEvent(
                    ex,
                    $"LocalDrivingLicenseApplicationID: {localDrivingLicenseApplicationId}, TestTypeID: {testType}"
                );
                failTestCount = -1;
            }

            return failTestCount;
        }

        public static int TotalTrialsPerTest(int localDrivingLicenseApplicationId, int testTypeId)
        {
            int trials = 0;
            string query = @"
                SELECT COUNT(TestID) AS TotalTrialsPerTest
                FROM LocalDrivingLicenseApplications AS LDL
                JOIN TestAppointments AS TA
                    ON TA.LocalDrivingLicenseApplicationID = LDL.LocalDrivingLicenseApplicationID
                JOIN Tests AS T
                    ON T.TestAppointmentID = TA.TestAppointmentID
                WHERE LDL.LocalDrivingLicenseApplicationID = @LocalDrivingLicenseApplicationID
                  AND TA.TestTypeID = @TestTypeID;
            ";

            try
            {
                using (SqlConnection connection = new SqlConnection(clsSettings.ConnectionString))
                using (SqlCommand cmd = new SqlCommand(query, connection))
                {
                    cmd.Parameters.Add("@LocalDrivingLicenseApplicationID", SqlDbType.Int).Value = localDrivingLicenseApplicationId;
                    cmd.Parameters.Add("@TestTypeID", SqlDbType.Int).Value = testTypeId;

                    connection.Open();
                    object result = cmd.ExecuteScalar();

                    if (result != null && result != DBNull.Value)
                    {
                        trials = Convert.ToInt32(result);
                    }
                }
            }
            catch (Exception ex)
            {
                clsEventLogger.LogEvent(
                    ex,
                    $"LocalDrivingLicenseApplicationID: {localDrivingLicenseApplicationId}, TestTypeID: {testTypeId}"
                );
                trials = -1;
            }

            return trials;
        }

        public static bool IsThereAnActiveScheduledTest(int localApplicationId, int testTypeId)
        {
            bool isFound = false;
            string query = @"            
                SELECT 1
                FROM TestAppointments
                WHERE LocalDrivingLicenseApplicationID = @LocalDrivingLicenseApplicationID
                  AND TestTypeID = @TestTypeID
                  AND IsLocked = 0;
            ";

            try
            {
                using (SqlConnection connection = new SqlConnection(clsSettings.ConnectionString))
                using (SqlCommand cmd = new SqlCommand(query, connection))
                {
                    cmd.Parameters.Add("@LocalDrivingLicenseApplicationID", SqlDbType.Int).Value = localApplicationId;
                    cmd.Parameters.Add("@TestTypeID", SqlDbType.Int).Value = testTypeId;

                    connection.Open();
                    object result = cmd.ExecuteScalar();
                    isFound = (result != null);
                }
            }
            catch (Exception ex)
            {
                clsEventLogger.LogEvent(
                    ex,
                    $"LocalDrivingLicenseApplicationID: {localApplicationId}, TestTypeID: {testTypeId}"
                );
                isFound = false;
            }

            return isFound;
        }

        public static bool DoesPassTestType(int localApplicationId, int testTypeId)
        {
            bool isPassed = false;
            string query = @"
                SELECT TOP(1) 1
                FROM Tests AS T
                INNER JOIN TestAppointments AS TA 
                    ON T.TestAppointmentID = TA.TestAppointmentID
                WHERE TA.LocalDrivingLicenseApplicationID = @LocalDrivingLicenseApplicationID
                  AND TA.TestTypeID = @TestTypeID
                  AND T.TestResult = 1;
            ";

            try
            {
                using (SqlConnection connection = new SqlConnection(clsSettings.ConnectionString))
                using (SqlCommand cmd = new SqlCommand(query, connection))
                {
                    cmd.Parameters.Add("@LocalDrivingLicenseApplicationID", SqlDbType.Int).Value = localApplicationId;
                    cmd.Parameters.Add("@TestTypeID", SqlDbType.Int).Value = testTypeId;

                    connection.Open();
                    object result = cmd.ExecuteScalar();
                    isPassed = (result != null);
                }
            }
            catch (Exception ex)
            {
                clsEventLogger.LogEvent(
                    ex,
                    $"LocalDrivingLicenseApplicationID: {localApplicationId}, TestTypeID: {testTypeId}"
                );
                isPassed = false;
            }

            return isPassed;
        }
    }
}