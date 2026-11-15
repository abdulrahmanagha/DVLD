using System;
using System.Data;
using System.Data.SqlClient;
using DVLD_Shared.Utility;

namespace DVLD_DataAccess.Manage_Types
{
    public static class clsApplicationTypeData
    {
        public static bool GetApplicationTypeByInfoId(
            int applicationId,
            ref string applicationTypeTitle,
            ref decimal applicationTypeFees)
        {
            bool isFound = false;
            string query = @"           
                SELECT ApplicationTypeID
                     , ApplicationTypeTitle
                     , ApplicationFees 
                FROM ApplicationTypes
                WHERE ApplicationTypeID = @ApplicationTypeID;
            ";

            try
            {
                using (SqlConnection connection = new SqlConnection(clsSettings.ConnectionString))
                using (SqlCommand cmd = new SqlCommand(query, connection))
                {
                    cmd.Parameters.Add("@ApplicationTypeID", SqlDbType.Int).Value = applicationId;

                    connection.Open();
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            applicationTypeTitle = (string)reader["ApplicationTypeTitle"];
                            applicationTypeFees = (decimal)reader["ApplicationFees"];
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
                clsEventLogger.LogEvent(ex, $"ApplicationTypeID: {applicationId}");
                isFound = false;
            }

            return isFound;
        }

        public static bool UpdateApplicationType(
            int applicationId,
            string applicationTypeTitle,
            decimal applicationTypeFees)
        {
            int rowsAffected = 0;
            string query = @"          
                UPDATE ApplicationTypes
                SET ApplicationTypeTitle = @ApplicationTypeTitle
                  , ApplicationFees = @ApplicationFees            
                WHERE ApplicationTypeID = @ApplicationTypeID;
            ";

            try
            {
                using (SqlConnection connection = new SqlConnection(clsSettings.ConnectionString))
                using (SqlCommand cmd = new SqlCommand(query, connection))
                {
                    cmd.Parameters.Add("@ApplicationTypeID", SqlDbType.Int).Value = applicationId;
                    cmd.Parameters.Add("@ApplicationTypeTitle", SqlDbType.NVarChar, 150).Value = applicationTypeTitle;
                    cmd.Parameters.Add("@ApplicationFees", SqlDbType.SmallMoney).Value = applicationTypeFees;

                    connection.Open();
                    rowsAffected = cmd.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                clsEventLogger.LogEvent(
                    ex,
                    $"ApplicationTypeID: {applicationId}, Title: {applicationTypeTitle}, Fees: {applicationTypeFees}"
                );
                rowsAffected = 0;
            }

            return (rowsAffected > 0);
        }

        public static DataTable GetAllApplicationTypes()
        {
            DataTable dt = new DataTable();
            string query = @"
                SELECT ApplicationTypeID
                     , ApplicationTypeTitle
                     , ApplicationFees 
                FROM ApplicationTypes;
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