using System;
using System.Data;
using System.Data.SqlClient;
using DVLD_Shared;
using DVLD_Shared.Utility;

namespace DVLD_DataAccess
{
    public static class clsPersonData
    {
        public static bool GetPersonInfoById(PersonDTO personDTO)
        {
            bool isFound = false;
            string query = @"
                SELECT PersonID
                     , NationalNo
                     , FirstName
                     , SecondName
                     , ThirdName
                     , LastName
                     , DateOfBirth
                     , Gender
                     , Address
                     , Phone
                     , Email
                     , NationalityCountryID
                     , ImagePath
                     , C.CountryName AS CountryName
                FROM People AS P
                JOIN Countries AS C
                    ON P.NationalityCountryID = C.CountryID
                WHERE PersonID = @PersonID;
            ";

            try
            {
                using (SqlConnection connection = new SqlConnection(clsSettings.ConnectionString))
                using (SqlCommand cmd = new SqlCommand(query, connection))
                {
                    cmd.Parameters.Add("@PersonID", SqlDbType.Int).Value = personDTO?.PersonId;

                    connection.Open();
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            personDTO.PersonId = (int)reader["PersonID"];
                            personDTO.NationalNo = (string)reader["NationalNo"];
                            personDTO.FirstName = (string)reader["FirstName"];
                            personDTO.SecondName = (string)reader["SecondName"];
                            personDTO.ThirdName = reader["ThirdName"] as string;
                            personDTO.LastName = (string)reader["LastName"];
                            personDTO.DateOfBirth = (DateTime)reader["DateOfBirth"];
                            personDTO.Gender = (byte)reader["Gender"];
                            personDTO.Address = (string)reader["Address"];
                            personDTO.Phone = (string)reader["Phone"];
                            personDTO.Email = reader["Email"] as string;
                            personDTO.NationalityCountryId = (int)reader["NationalityCountryID"];
                            personDTO.ImagePath = reader["ImagePath"] as string;
                            personDTO.CountryName = (string)reader["CountryName"];
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
                clsEventLogger.LogEvent(ex, $"PersonID: {personDTO?.PersonId}");
                isFound = false;
            }

            return isFound;
        }

        public static bool GetPersonInfoByNationalNo(PersonDTO personDTO)
        {
            bool isFound = false;
            string query = @"
                SELECT PersonID
                     , NationalNo
                     , FirstName
                     , SecondName
                     , ThirdName
                     , LastName
                     , DateOfBirth
                     , Gender
                     , Address
                     , Phone
                     , Email
                     , NationalityCountryID
                     , ImagePath
                     , C.CountryName AS CountryName
                FROM People AS P
                JOIN Countries AS C
                    ON P.NationalityCountryID = C.CountryID
                WHERE NationalNo = @NationalNo;
            ";

            try
            {
                using (SqlConnection connection = new SqlConnection(clsSettings.ConnectionString))
                using (SqlCommand cmd = new SqlCommand(query, connection))
                {
                    cmd.Parameters.Add("@NationalNo", SqlDbType.NVarChar, 20).Value = personDTO.NationalNo;

                    connection.Open();
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            personDTO.PersonId = (int)reader["PersonID"];
                            personDTO.NationalNo = (string)reader["NationalNo"];
                            personDTO.FirstName = (string)reader["FirstName"];
                            personDTO.SecondName = (string)reader["SecondName"];
                            personDTO.ThirdName = reader["ThirdName"] as string;
                            personDTO.LastName = (string)reader["LastName"];
                            personDTO.DateOfBirth = (DateTime)reader["DateOfBirth"];
                            personDTO.Gender = (byte)reader["Gender"];
                            personDTO.Address = (string)reader["Address"];
                            personDTO.Phone = (string)reader["Phone"];
                            personDTO.Email = reader["Email"] as string;
                            personDTO.NationalityCountryId = (int)reader["NationalityCountryID"];
                            personDTO.ImagePath = reader["ImagePath"] as string;
                            personDTO.CountryName = (string)reader["CountryName"];
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
                clsEventLogger.LogEvent(ex, $"NationalNo: {personDTO?.NationalNo}");
                isFound = false;
            }

            return isFound;
        }

        public static int AddNewPerson(PersonDTO personDTO)
        {
            int personId = -1;
            string query = @"
                INSERT INTO People
                    (NationalNo, FirstName, SecondName, ThirdName, LastName, DateOfBirth
                    , Gender, Address, Phone, Email, NationalityCountryID, ImagePath)
                VALUES
                    (@NationalNo, @FirstName, @SecondName, @ThirdName, @LastName, @DateOfBirth
                    , @Gender, @Address, @Phone, @Email, @NationalityCountryID, @ImagePath);

                SELECT CAST(SCOPE_IDENTITY() AS INT);
            ";

            try
            {
                using (SqlConnection connection = new SqlConnection(clsSettings.ConnectionString))
                using (SqlCommand cmd = new SqlCommand(query, connection))
                {
                    cmd.Parameters.Add("@NationalNo", SqlDbType.NVarChar, 20).Value = personDTO.NationalNo;
                    cmd.Parameters.Add("@FirstName", SqlDbType.NVarChar, 20).Value = personDTO.FirstName;
                    cmd.Parameters.Add("@SecondName", SqlDbType.NVarChar, 20).Value = personDTO.SecondName;
                    cmd.Parameters.Add("@ThirdName", SqlDbType.NVarChar, 20).Value = (object)personDTO.ThirdName ?? DBNull.Value;
                    cmd.Parameters.Add("@LastName", SqlDbType.NVarChar, 20).Value = personDTO.LastName;
                    cmd.Parameters.Add("@DateOfBirth", SqlDbType.DateTime).Value = personDTO.DateOfBirth;
                    cmd.Parameters.Add("@Gender", SqlDbType.TinyInt).Value = personDTO.Gender;
                    cmd.Parameters.Add("@Address", SqlDbType.NVarChar, 500).Value = personDTO.Address;
                    cmd.Parameters.Add("@Phone", SqlDbType.NVarChar, 20).Value = personDTO.Phone;
                    cmd.Parameters.Add("@Email", SqlDbType.NVarChar, 50).Value = (object)personDTO.Email ?? DBNull.Value;
                    cmd.Parameters.Add("@NationalityCountryID", SqlDbType.Int).Value = personDTO.NationalityCountryId;
                    cmd.Parameters.Add("@ImagePath", SqlDbType.NVarChar, 250).Value = (object)personDTO.ImagePath ?? DBNull.Value;

                    connection.Open();
                    object result = cmd.ExecuteScalar();

                    if (result != null && result != DBNull.Value)
                    {
                        personId = Convert.ToInt32(result);
                    }
                }
            }
            catch (Exception ex)
            {
                clsEventLogger.LogEvent(
                    ex,
                    $"NationalNo: {personDTO?.NationalNo}, NationalityCountryID: {personDTO?.NationalityCountryId}"
                );
                personId = -1;
            }

            return personId;
        }

        public static bool UpdatePerson(PersonDTO personDTO)
        {
            int rowsAffected = 0;
            string query = @"    
                UPDATE People
                SET NationalNo = @NationalNo
                  , FirstName = @FirstName
                  , SecondName = @SecondName
                  , ThirdName = @ThirdName
                  , LastName = @LastName
                  , DateOfBirth = @DateOfBirth
                  , Gender = @Gender
                  , Address = @Address
                  , Phone = @Phone
                  , Email = @Email
                  , NationalityCountryID = @NationalityCountryID
                  , ImagePath = @ImagePath
                WHERE PersonID = @PersonID;
            ";

            try
            {
                using (SqlConnection connection = new SqlConnection(clsSettings.ConnectionString))
                using (SqlCommand cmd = new SqlCommand(query, connection))
                {
                    cmd.Parameters.Add("@PersonID", SqlDbType.Int).Value = personDTO.PersonId;
                    cmd.Parameters.Add("@NationalNo", SqlDbType.NVarChar, 20).Value = personDTO.NationalNo;
                    cmd.Parameters.Add("@FirstName", SqlDbType.NVarChar, 20).Value = personDTO.FirstName;
                    cmd.Parameters.Add("@SecondName", SqlDbType.NVarChar, 20).Value = personDTO.SecondName;
                    cmd.Parameters.Add("@ThirdName", SqlDbType.NVarChar, 20).Value = (object)personDTO.ThirdName ?? DBNull.Value;
                    cmd.Parameters.Add("@LastName", SqlDbType.NVarChar, 20).Value = personDTO.LastName;
                    cmd.Parameters.Add("@DateOfBirth", SqlDbType.DateTime).Value = personDTO.DateOfBirth;
                    cmd.Parameters.Add("@Gender", SqlDbType.TinyInt).Value = personDTO.Gender;
                    cmd.Parameters.Add("@Address", SqlDbType.NVarChar, 500).Value = personDTO.Address;
                    cmd.Parameters.Add("@Phone", SqlDbType.NVarChar, 20).Value = personDTO.Phone;
                    cmd.Parameters.Add("@Email", SqlDbType.NVarChar, 50).Value = (object)personDTO.Email ?? DBNull.Value;
                    cmd.Parameters.Add("@NationalityCountryID", SqlDbType.Int).Value = personDTO.NationalityCountryId;
                    cmd.Parameters.Add("@ImagePath", SqlDbType.NVarChar, 250).Value = (object)personDTO.ImagePath ?? DBNull.Value;

                    connection.Open();
                    rowsAffected = cmd.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                clsEventLogger.LogEvent(
                    ex,
                    $"PersonID: {personDTO?.PersonId}, NationalNo: {personDTO?.NationalNo}"
                );
                rowsAffected = 0;
            }

            return (rowsAffected > 0);
        }

        public static enDeleteResult DeletePerson(int personId)
        {
            int rowsAffected = 0;
            string query = @"             
                DELETE FROM People
                WHERE PersonID = @PersonID;
            ";

            try
            {
                using (SqlConnection connection = new SqlConnection(clsSettings.ConnectionString))
                using (SqlCommand cmd = new SqlCommand(query, connection))
                {
                    cmd.Parameters.Add("@PersonID", SqlDbType.Int).Value = personId;

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

                clsEventLogger.LogEvent(ex, $"SQL Error #{ex.Number} | PersonID: {personId}");
                return enDeleteResult.Error;
            }
            catch (Exception ex)
            {
                clsEventLogger.LogEvent(ex, $"PersonID: {personId}");
                return enDeleteResult.Error;
            }

            return enDeleteResult.Success;
        }

        public static enDeleteResult DeletePerson(string nationalNo)
        {
            int rowsAffected = 0;
            string query = @"             
                DELETE FROM People
                WHERE NationalNo = @NationalNo;
            ";

            try
            {
                using (SqlConnection connection = new SqlConnection(clsSettings.ConnectionString))
                using (SqlCommand cmd = new SqlCommand(query, connection))
                {
                    cmd.Parameters.Add("@NationalNo", SqlDbType.NVarChar, 20).Value = nationalNo;

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

                clsEventLogger.LogEvent(ex, $"SQL Error #{ex.Number} | NationalNo: {nationalNo}");
                return enDeleteResult.Error;
            }
            catch (Exception ex)
            {
                clsEventLogger.LogEvent(ex, $"NationalNo: {nationalNo}");
                return enDeleteResult.Error;
            }

            return enDeleteResult.Success;
        }

        public static bool IsExistPerson(int personId)
        {
            bool isFound = false;
            string query = @"
                SELECT 1
                FROM People
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

        public static bool IsExistPerson(string nationalNo)
        {
            bool isFound = false;
            string query = @"
                SELECT 1
                FROM People
                WHERE NationalNo = @NationalNo;
            ";

            try
            {
                using (SqlConnection connection = new SqlConnection(clsSettings.ConnectionString))
                using (SqlCommand cmd = new SqlCommand(query, connection))
                {
                    cmd.Parameters.Add("@NationalNo", SqlDbType.NVarChar, 20).Value = nationalNo;

                    connection.Open();
                    object result = cmd.ExecuteScalar();
                    isFound = (result != null);
                }
            }
            catch (Exception ex)
            {
                clsEventLogger.LogEvent(ex, $"NationalNo: {nationalNo}");
                isFound = false;
            }

            return isFound;
        }

        public static DataTable GetAllPeople()
        {
            DataTable dt = new DataTable();
            string query = @"            
                SELECT PersonID
                     , NationalNo
                     , FirstName
                     , SecondName
                     , ThirdName
                     , LastName
                     , (
                         CASE
                            WHEN Gender = 0 THEN 'Male'
                            ELSE 'Female'
                         END
                     ) AS [Gender]
                     , DateOfBirth
                     , CountryName
                     , Phone
                     , Email
                FROM People AS P
                JOIN Countries AS C
                    ON C.CountryID = P.NationalityCountryID;
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