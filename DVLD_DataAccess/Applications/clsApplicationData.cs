using DVLD_Shared;
using DVLD_Shared.Utility;
using System;
using System.Data;
using System.Data.SqlClient;

namespace DVLD_DataAccess.Applications
{
    public class clsApplicationData
    {
        public static bool GetApplicationInfoById(ApplicationDTO applicationDTO)
        {
            bool isFound = false;
            string query = @"          
                SELECT ApplicationID
                     , ApplicantPersonID
                     , ApplicationDate
                     , ApplicationTypeID
                     , ApplicationStatus
                     , LastStatusDate
                     , PaidFees
                     , CreatedByUserID
                FROM Applications
                WHERE ApplicationID = @ApplicationID;
            ";

            try
            {
                using (SqlConnection connection = new SqlConnection(clsSettings.ConnectionString))
                using (SqlCommand cmd = new SqlCommand(query, connection))
                {
                    cmd.Parameters.Add("@ApplicationID", SqlDbType.Int).Value = applicationDTO?.ApplicationId;
                    connection.Open();

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            applicationDTO.PersonId = (int)reader["ApplicantPersonID"];
                            applicationDTO.ApplicationDate = (DateTime)reader["ApplicationDate"];
                            applicationDTO.ApplicationTypeId = (int)reader["ApplicationTypeID"];
                            applicationDTO.ApplicationStatus = reader.GetByte(reader.GetOrdinal("ApplicationStatus"));
                            applicationDTO.LastStatusDate = (DateTime)reader["LastStatusDate"];
                            applicationDTO.PaidFees = (decimal)reader["PaidFees"];
                            applicationDTO.CreatedByUserId = (int)reader["CreatedByUserID"];
                            isFound = true;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                clsEventLogger.LogEvent(ex, $"ApplicationID: {applicationDTO?.ApplicationId}");
                isFound = false;
            }

            return isFound;
        }

        public static int AddNewApplication(ApplicationDTO applicationDTO)
        {
            int applicationId = -1;
            string query = @"                
                INSERT INTO Applications
                    (ApplicantPersonID, ApplicationDate, ApplicationTypeID, ApplicationStatus
                    , LastStatusDate, PaidFees, CreatedByUserID)
                VALUES
                    (@ApplicantPersonID, @ApplicationDate, @ApplicationTypeID, @ApplicationStatus
                    , @LastStatusDate, @PaidFees, @CreatedByUserID);

                SELECT CAST(SCOPE_IDENTITY() AS INT);
            ";

            try
            {
                using (SqlConnection connection = new SqlConnection(clsSettings.ConnectionString))
                using (SqlCommand cmd = new SqlCommand(query, connection))
                {
                    cmd.Parameters.Add("@ApplicantPersonID", SqlDbType.Int).Value = applicationDTO.PersonId;
                    cmd.Parameters.Add("@ApplicationDate", SqlDbType.DateTime).Value = DateTime.Now;
                    cmd.Parameters.Add("@ApplicationTypeID", SqlDbType.Int).Value = applicationDTO.ApplicationTypeId;
                    cmd.Parameters.Add("@ApplicationStatus", SqlDbType.TinyInt).Value = (byte)applicationDTO.ApplicationStatus;
                    cmd.Parameters.Add("@LastStatusDate", SqlDbType.DateTime).Value = applicationDTO.LastStatusDate;
                    cmd.Parameters.Add("@PaidFees", SqlDbType.SmallMoney).Value = applicationDTO.PaidFees;
                    cmd.Parameters.Add("@CreatedByUserID", SqlDbType.Int).Value = applicationDTO.CreatedByUserId;

                    connection.Open();
                    object result = cmd.ExecuteScalar();

                    if (result != null && result != DBNull.Value)
                    {
                        applicationId = Convert.ToInt32(result);
                    }
                }
            }
            catch (Exception ex)
            {
                clsEventLogger.LogEvent(
                    ex,
                    $"PersonID: {applicationDTO?.PersonId}, AppTypeID: {applicationDTO?.ApplicationTypeId}, CreatedByUserID: {applicationDTO?.CreatedByUserId}"
                );
                applicationId = -1;
            }

            return applicationId;
        }

        public static bool UpdateApplication(ApplicationDTO applicationDTO)
        {
            int rowsAffected = 0;
            string query = @"        
                UPDATE Applications
                SET ApplicantPersonID = @ApplicantPersonID
                  , ApplicationTypeID = @ApplicationTypeID
                  , ApplicationStatus = @ApplicationStatus
                  , LastStatusDate = @LastStatusDate
                  , PaidFees = @PaidFees
                  , CreatedByUserID = @CreatedByUserID
                WHERE ApplicationID = @ApplicationID;
            ";

            try
            {
                using (SqlConnection connection = new SqlConnection(clsSettings.ConnectionString))
                using (SqlCommand cmd = new SqlCommand(query, connection))
                {
                    cmd.Parameters.Add("@ApplicationID", SqlDbType.Int).Value = applicationDTO.ApplicationId;
                    cmd.Parameters.Add("@ApplicantPersonID", SqlDbType.Int).Value = applicationDTO.PersonId;
                    cmd.Parameters.Add("@ApplicationTypeID", SqlDbType.Int).Value = applicationDTO.ApplicationTypeId;
                    cmd.Parameters.Add("@ApplicationStatus", SqlDbType.TinyInt).Value = (byte)applicationDTO.ApplicationStatus;
                    cmd.Parameters.Add("@LastStatusDate", SqlDbType.DateTime).Value = applicationDTO.LastStatusDate;
                    cmd.Parameters.Add("@PaidFees", SqlDbType.SmallMoney).Value = applicationDTO.PaidFees;
                    cmd.Parameters.Add("@CreatedByUserID", SqlDbType.Int).Value = applicationDTO.CreatedByUserId;

                    connection.Open();
                    rowsAffected = cmd.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                clsEventLogger.LogEvent(ex, $"ApplicationID: {applicationDTO?.ApplicationId}");
                rowsAffected = 0;
            }

            return (rowsAffected > 0);
        }

        public static enDeleteResult DeleteApplication(int applicationId)
        {
            int rowsAffected = 0;
            string query = @"
                DELETE FROM Applications
                WHERE ApplicationID = @ApplicationID;
            ";

            try
            {
                using (SqlConnection connection = new SqlConnection(clsSettings.ConnectionString))
                using (SqlCommand cmd = new SqlCommand(query, connection))
                {
                    cmd.Parameters.Add("@ApplicationID", SqlDbType.Int).Value = applicationId;

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

                clsEventLogger.LogEvent(ex, $"SQL Error #{ex.Number} | ApplicationID: {applicationId}");
                return enDeleteResult.Error;
            }
            catch (Exception ex)
            {
                clsEventLogger.LogEvent(ex, $"ApplicationID: {applicationId}");
                return enDeleteResult.Error;
            }

            return enDeleteResult.Success;
        }

        public static DataTable GetAllApplications()
        {
            DataTable dt = new DataTable();
            string query = @"
                SELECT ApplicationID
                     , ApplicantPersonID
                     , ApplicationDate
                     , ApplicationTypeID
                     , ApplicationStatus
                     , LastStatusDate
                     , PaidFees
                     , CreatedByUserID
                FROM Applications;
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

        public static bool UpdateApplicationStatus(int applicationId, int applicationStatus)
        {
            int rowsAffected = 0;
            string query = @"
                UPDATE Applications
                SET ApplicationStatus = @ApplicationStatus
                  , LastStatusDate = @LastStatusDate
                WHERE ApplicationID = @ApplicationID;
            ";

            try
            {
                using (SqlConnection connection = new SqlConnection(clsSettings.ConnectionString))
                using (SqlCommand cmd = new SqlCommand(query, connection))
                {
                    cmd.Parameters.Add("@ApplicationID", SqlDbType.Int).Value = applicationId;
                    cmd.Parameters.Add("@ApplicationStatus", SqlDbType.TinyInt).Value = (byte)applicationStatus;
                    cmd.Parameters.Add("@LastStatusDate", SqlDbType.DateTime).Value = DateTime.Now;

                    connection.Open();
                    rowsAffected = cmd.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                clsEventLogger.LogEvent(ex, $"ApplicationID: {applicationId}, NewStatus: {applicationStatus}");
                rowsAffected = 0;
            }

            return (rowsAffected > 0);
        }

        public static bool IsApplicationExist(int applicationId)
        {
            bool isFound = false;
            string query = @"
                SELECT 1
                FROM Applications
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
                    isFound = (result != null);
                }
            }
            catch (Exception ex)
            {
                clsEventLogger.LogEvent(ex, $"ApplicationID: {applicationId}");
                isFound = false;
            }

            return isFound;
        }

        //public static bool DoesPersonHaveActiveApplication(int personId, int applicationTypeId)
        //{
        //    return GetActiveApplicationId(personId, applicationTypeId) != -1;
        //}

        public static int GetActiveApplicationIDForLicenseClass(int personId, int applicationTypeId, int licenseClassId)
        {
            int applicationId = -1;
            string query = @"
                SELECT APP.ApplicationID
                FROM LocalDrivingLicenseApplications AS LDL
                JOIN Applications AS App
                    ON App.ApplicationID = LDL.ApplicationID
                WHERE APP.ApplicantPersonID = @ApplicantPersonID
                    AND APP.ApplicationTypeID = @ApplicationTypeID
                    AND LDL.LicenseClassID = @LicenseClassID
                    AND APP.ApplicationStatus = 1;
            ";

            try
            {
                using (SqlConnection connection = new SqlConnection(clsSettings.ConnectionString))
                using (SqlCommand cmd = new SqlCommand(query, connection))
                {
                    cmd.Parameters.Add("@ApplicantPersonID", SqlDbType.Int).Value = personId;
                    cmd.Parameters.Add("@ApplicationTypeID", SqlDbType.Int).Value = applicationTypeId;
                    cmd.Parameters.Add("@LicenseClassID", SqlDbType.Int).Value = licenseClassId;

                    connection.Open();
                    object result = cmd.ExecuteScalar();

                    if (result != null && result != DBNull.Value)
                    {
                        applicationId = Convert.ToInt32(result);
                    }
                }
            }
            catch (Exception ex)
            {
                clsEventLogger.LogEvent(
                    ex,
                    $"PersonID: {personId}, AppTypeID: {applicationTypeId}, LicenseClassID: {licenseClassId}"
                );
                applicationId = -1;
            }

            return applicationId;
        }
    }
}
