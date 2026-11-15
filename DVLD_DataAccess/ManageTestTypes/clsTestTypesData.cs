using System;
using System.Data;
using System.Data.SqlClient;
using DVLD_Shared.Utility;

namespace DVLD_DataAccess.ManageTestTypes
{
    public class clsTestTypesData
    {
        public static bool GetTestTypeById(
            int testTypeId,
            ref string testTypeTitle,
            ref string testTypeDescription,
            ref decimal testTypeFees)
        {
            bool isFound = false;
            string query = @"             
                SELECT TestTypeTitle
                     , TestTypeDescription
                     , TestTypeFees
                FROM TestTypes
                WHERE TestTypeID = @TestTypeID;
            ";

            try
            {
                using (SqlConnection connection = new SqlConnection(clsSettings.ConnectionString))
                using (SqlCommand cmd = new SqlCommand(query, connection))
                {
                    cmd.Parameters.Add("@TestTypeID", SqlDbType.Int).Value = testTypeId;
                    connection.Open();

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            testTypeTitle = (string)reader["TestTypeTitle"];
                            testTypeDescription = (string)reader["TestTypeDescription"];
                            testTypeFees = (decimal)reader["TestTypeFees"];
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
                clsEventLogger.LogEvent(ex, $"TestTypeID: {testTypeId}");
                isFound = false;
            }

            return isFound;
        }

        public static bool UpdateTestType(
            int testTypeId,
            string testTypeTitle,
            string testTypeDescription,
            decimal testTypeFees)
        {
            int rowsAffected = 0;
            string query = @"           
                UPDATE TestTypes
                SET TestTypeTitle = @TestTypeTitle
                  , TestTypeDescription = @TestTypeDescription
                  , TestTypeFees = @TestTypeFees
                WHERE TestTypeID = @TestTypeID;
            ";

            try
            {
                using (SqlConnection connection = new SqlConnection(clsSettings.ConnectionString))
                using (SqlCommand cmd = new SqlCommand(query, connection))
                {
                    cmd.Parameters.Add("@TestTypeID", SqlDbType.Int).Value = testTypeId;
                    cmd.Parameters.Add("@TestTypeTitle", SqlDbType.NVarChar, 100).Value = testTypeTitle;
                    cmd.Parameters.Add("@TestTypeDescription", SqlDbType.NVarChar, 500).Value = testTypeDescription;
                    cmd.Parameters.Add("@TestTypeFees", SqlDbType.SmallMoney).Value = testTypeFees;

                    connection.Open();
                    rowsAffected = cmd.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                clsEventLogger.LogEvent(
                    ex,
                    $"TestTypeID: {testTypeId}, Title: {testTypeTitle}, Fees: {testTypeFees}"
                );
                rowsAffected = 0;
            }

            return (rowsAffected > 0);
        }

        public static DataTable GetAllTestTypes()
        {
            DataTable dt = new DataTable();
            string query = @"          
                SELECT TestTypeID
                     , TestTypeTitle
                     , TestTypeDescription
                     , TestTypeFees
                FROM TestTypes;
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