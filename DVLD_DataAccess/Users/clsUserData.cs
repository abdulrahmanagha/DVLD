using DVLD_Shared;
using DVLD_Shared.Utility;
using System;
using System.Data;
using System.Data.SqlClient;

namespace DVLD_DataAccess.Users
{
    public static class clsUserData
    {
        public static bool GetUserInfoByUserId(UserDTO userDTO)
        {
            bool isFound = false;
            string query = @"                    
                SELECT UserID
                     , UserName
                     , Password
                     , IsActive
                     , PersonID
                FROM Users
                WHERE UserID = @UserID;
            ";

            try
            {
                using (SqlConnection connection = new SqlConnection(clsSettings.ConnectionString))
                using (SqlCommand cmd = new SqlCommand(query, connection))
                {
                    cmd.Parameters.Add("@UserID", SqlDbType.Int).Value = userDTO?.UserID;

                    connection.Open();
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            userDTO.UserID = (int)reader["UserID"];
                            userDTO.UserName = (string)reader["UserName"];
                            userDTO.Password = (string)reader["Password"];
                            userDTO.IsActive = (bool)reader["IsActive"];
                            userDTO.PersonID = reader["PersonID"] as int?;
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
                clsEventLogger.LogEvent(ex, $"UserID: {userDTO?.UserID}");
                isFound = false;
            }

            return isFound;
        }

        public static bool GetUserInfoByPersonId(UserDTO userDTO)
        {
            bool isFound = false;
            string query = @"                    
                SELECT UserID
                     , UserName
                     , Password
                     , IsActive
                     , PersonID
                FROM Users
                WHERE PersonID = @PersonID;
            ";

            try
            {
                using (SqlConnection connection = new SqlConnection(clsSettings.ConnectionString))
                using (SqlCommand cmd = new SqlCommand(query, connection))
                {
                    cmd.Parameters.Add("@PersonID", SqlDbType.Int).Value = userDTO.PersonID;

                    connection.Open();
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            userDTO.UserID = (int)reader["UserID"];
                            userDTO.UserName = (string)reader["UserName"];
                            userDTO.Password = (string)reader["Password"];
                            userDTO.IsActive = (bool)reader["IsActive"];
                            userDTO.PersonID = reader["PersonID"] as int?;
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
                clsEventLogger.LogEvent(ex, $"PersonID: {userDTO?.PersonID}");
                isFound = false;
            }

            return isFound;
        }

        public static bool GetUserInfoByUserName(UserDTO userDTO)
        {
            bool isFound = false;
            string query = @"
                SELECT UserID
                     , UserName
                     , Password
                     , IsActive
                     , PersonID
                FROM Users
                WHERE UserName = @UserName;
            ";

            try
            {
                using (SqlConnection connection = new SqlConnection(clsSettings.ConnectionString))
                using (SqlCommand cmd = new SqlCommand(query, connection))
                {
                    cmd.Parameters.Add("@UserName", SqlDbType.NVarChar, 20).Value = userDTO.UserName;

                    connection.Open();
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            userDTO.UserID = (int)reader["UserID"];
                            userDTO.UserName = (string)reader["UserName"];
                            userDTO.Password = (string)reader["Password"];
                            userDTO.IsActive = (bool)reader["IsActive"];
                            userDTO.PersonID = reader["PersonID"] as int?;
                            isFound = true;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                clsEventLogger.LogEvent(ex, $"UserName: {userDTO?.UserName}");
                isFound = false;
            }

            return isFound;
        }

        public static int AddNewUser(UserDTO userDTO)
        {
            int userId = -1;
            string query = @"
                INSERT INTO Users
                    (UserName, Password, IsActive, PersonID)
                VALUES
                    (@UserName, @Password, @IsActive, @PersonID);

                SELECT CAST(SCOPE_IDENTITY() AS INT);
            ";

            try
            {
                using (SqlConnection connection = new SqlConnection(clsSettings.ConnectionString))
                using (SqlCommand cmd = new SqlCommand(query, connection))
                {
                    cmd.Parameters.Add("@UserName", SqlDbType.NVarChar, 20).Value = userDTO.UserName;
                    cmd.Parameters.Add("@Password", SqlDbType.NVarChar, 255).Value = userDTO.Password;
                    cmd.Parameters.Add("@IsActive", SqlDbType.Bit).Value = userDTO.IsActive;
                    cmd.Parameters.Add("@PersonID", SqlDbType.Int).Value = (object)userDTO.PersonID ?? DBNull.Value;

                    connection.Open();
                    object result = cmd.ExecuteScalar();

                    if (result != null && result != DBNull.Value)
                    {
                        userId = Convert.ToInt32(result);
                    }
                }
            }
            catch (Exception ex)
            {
                clsEventLogger.LogEvent(
                    ex,
                    $"UserName: {userDTO?.UserName}, PersonID: {userDTO?.PersonID}"
                );
                userId = -1;
            }

            return userId;
        }

        public static bool UpdateUser(UserDTO userDTO)
        {
            int rowsAffected = 0;
            string query = @"               
                UPDATE Users
                SET UserName = @UserName
                  , Password = @Password
                  , IsActive = @IsActive
                  , PersonID = @PersonID
                WHERE UserID = @UserID;
            ";

            try
            {
                using (SqlConnection connection = new SqlConnection(clsSettings.ConnectionString))
                using (SqlCommand cmd = new SqlCommand(query, connection))
                {
                    cmd.Parameters.Add("@UserID", SqlDbType.Int).Value = userDTO.UserID;
                    cmd.Parameters.Add("@UserName", SqlDbType.NVarChar, 20).Value = userDTO.UserName;
                    cmd.Parameters.Add("@Password", SqlDbType.NVarChar, 255).Value = userDTO.Password;
                    cmd.Parameters.Add("@IsActive", SqlDbType.Bit).Value = userDTO.IsActive;
                    cmd.Parameters.Add("@PersonID", SqlDbType.Int).Value = (object)userDTO.PersonID ?? DBNull.Value;

                    connection.Open();
                    rowsAffected = cmd.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                clsEventLogger.LogEvent(
                    ex,
                    $"UserID: {userDTO?.UserID}, UserName: {userDTO?.UserName}, PersonID: {userDTO?.PersonID}"
                );
                rowsAffected = 0;
            }

            return (rowsAffected > 0);
        }

        public static enDeleteResult DeleteUser(int userId)
        {
            int rowsAffected = 0;
            string query = @"             
                DELETE FROM Users
                WHERE UserID = @UserID;
            ";

            try
            {
                using (SqlConnection connection = new SqlConnection(clsSettings.ConnectionString))
                using (SqlCommand cmd = new SqlCommand(query, connection))
                {
                    cmd.Parameters.Add("@UserID", SqlDbType.Int).Value = userId;

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

                clsEventLogger.LogEvent(ex, $"SQL Error #{ex.Number} | UserID: {userId}");
                return enDeleteResult.Error;
            }
            catch (Exception ex)
            {
                clsEventLogger.LogEvent(ex, $"UserID: {userId}");
                return enDeleteResult.Error;
            }

            return enDeleteResult.Success;
        }

        public static bool IsUserExist(int userId)
        {
            bool isFound = false;
            string query = @"
                SELECT 1
                FROM Users
                WHERE UserID = @UserID;
            ";

            try
            {
                using (SqlConnection connection = new SqlConnection(clsSettings.ConnectionString))
                using (SqlCommand cmd = new SqlCommand(query, connection))
                {
                    cmd.Parameters.Add("@UserID", SqlDbType.Int).Value = userId;

                    connection.Open();
                    object result = cmd.ExecuteScalar();
                    isFound = (result != null);
                }
            }
            catch (Exception ex)
            {
                clsEventLogger.LogEvent(ex, $"UserID: {userId}");
                isFound = false;
            }

            return isFound;
        }

        public static bool IsUserExist(string userName)
        {
            bool isFound = false;
            string query = @"
                SELECT 1
                FROM Users
                WHERE UserName = @UserName;
            ";

            try
            {
                using (SqlConnection connection = new SqlConnection(clsSettings.ConnectionString))
                using (SqlCommand cmd = new SqlCommand(query, connection))
                {
                    cmd.Parameters.Add("@UserName", SqlDbType.NVarChar, 20).Value = userName;

                    connection.Open();
                    object result = cmd.ExecuteScalar();
                    isFound = (result != null);
                }
            }
            catch (Exception ex)
            {
                clsEventLogger.LogEvent(ex, $"UserName: {userName}");
                isFound = false;
            }

            return isFound;
        }

        public static bool IsUserExist(string userName, int? excludeUserId)
        {
            bool isFound = false;
            string query = @"
                SELECT 1
                FROM Users
                WHERE UserName = @UserName
                AND UserID != @ExcludeUserID;
            ";

            try
            {
                using (SqlConnection connection = new SqlConnection(clsSettings.ConnectionString))
                using (SqlCommand cmd = new SqlCommand(query, connection))
                {
                    cmd.Parameters.Add("@UserName", SqlDbType.NVarChar, 20).Value = userName;
                    cmd.Parameters.Add("@ExcludeUserID", SqlDbType.Int).Value = excludeUserId;

                    connection.Open();
                    object result = cmd.ExecuteScalar();
                    isFound = (result != null);
                }
            }
            catch (Exception ex)
            {
                clsEventLogger.LogEvent(ex, $"UserName: {userName}");
                isFound = false;
            }

            return isFound;
        }

        public static bool IsUserExistForPersonId(int personId)
        {
            bool isFound = false;
            string query = @"
                SELECT 1
                FROM Users
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

        public static DataTable GetAllUsers()
        {
            DataTable dt = new DataTable();
            string query = @"
                SELECT UserID
                     , PersonID
                     , UserName
                     , IsActive
                FROM Users;
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

        public static DataTable GetAllUsersWithDetails()
        {
            DataTable dt = new DataTable();
            string query = @"            
                SELECT U.UserID
                     , P.PersonID
                     , (
                         P.FirstName + ' ' +
                         P.SecondName + ' ' +
                         COALESCE(P.ThirdName + ' ', '') +
                         P.LastName
                     ) AS FullName
                     , U.UserName
                     , U.IsActive
                FROM Users AS U
                JOIN People AS P
                    ON P.PersonID = U.PersonID; 
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

        public static bool ChangePassword(int userId, string newPassword)
        {
            int rowsAffected = 0;
            string query = @"
                UPDATE Users
                SET Password = @Password
                WHERE UserID = @UserID;
            ";

            try
            {
                using (SqlConnection connection = new SqlConnection(clsSettings.ConnectionString))
                using (SqlCommand cmd = new SqlCommand(query, connection))
                {
                    cmd.Parameters.Add("@UserID", SqlDbType.Int).Value = userId;
                    cmd.Parameters.Add("@Password", SqlDbType.NVarChar, 255).Value = newPassword;

                    connection.Open();
                    rowsAffected = cmd.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                clsEventLogger.LogEvent(ex, $"UserID: {userId}");
                rowsAffected = 0;
            }

            return (rowsAffected > 0);
        }
    }
}