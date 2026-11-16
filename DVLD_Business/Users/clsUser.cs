using DVLD_DataAccess.Users;
using DVLD_Shared;
using DVLD_Shared.Utility;
using System;
using System.Data;

namespace DVLD_Business.Users
{
    public class clsUser
    {
        private enum enMode { Update = 0, AddNew = 1 };

        public clsPerson PersonInfo { get; set; } = null;
        public int? PersonId { get; set; } = null;
        public int UserId { get; set;}
        public string UserName { get; set; } = null;
        public bool IsActive { get; set; } = false;

        private enMode _mode = enMode.Update;
        private string _passwordHash = null;

        public clsUser(string plainPassword) 
        {
            if(string.IsNullOrWhiteSpace(plainPassword))
                throw new ArgumentException("Password cannot be null or empty.", nameof(plainPassword));

            UserId = -1;
            _passwordHash = clsPasswordHasher.HashPassword(plainPassword);
            _mode = enMode.AddNew;
        }

        private clsUser(UserDTO userDTO, clsPerson person)
        {
            UserId = userDTO.UserID;
            UserName = userDTO.UserName;
            IsActive = userDTO.IsActive;
            PersonId = userDTO.PersonID;
            _passwordHash = userDTO.Password;

            if (person != null)
            {
                PersonInfo = person;
            }

            _mode = enMode.Update;
        }

        public static clsUser Find(int userId)
        {
            UserDTO userDTO = new UserDTO
            {
                UserID = userId
            };

            bool isFoud = clsUserData.GetUserInfoByUserId(userDTO);

            if(isFoud)
            {
                clsPerson person = null;
                
                if(userDTO.PersonID.HasValue)
                {
                    person = clsPerson.Find(userDTO.PersonID.Value);
                }
               
                return new clsUser(userDTO, person);
            }
            else
            {
                return null;
            }
        }

        public static clsUser FindByPersonId(int personId)
        {
            UserDTO userDTO = new UserDTO
            {
                PersonID = personId
            };

            bool isFound = clsUserData.GetUserInfoByPersonId(userDTO);

            if(isFound)
            {
                clsPerson person = clsPerson.Find(userDTO.PersonID.Value);
                return new clsUser(userDTO, person);
            }
            else
                return null;
        }

        public static clsUser Find(string username, string plainPassword)
        {
            UserDTO userDTO = new UserDTO 
            { 
                UserName = username 
            };

            if (!clsUserData.GetUserInfoByUserName(userDTO))
                return null;

            if (!clsPasswordHasher.VerifyPassword(plainPassword, userDTO.Password))
                return null;

            clsPerson person = userDTO.PersonID.HasValue ? clsPerson.Find(userDTO.PersonID.Value) : null;
            return new clsUser(userDTO, person);
        }
    
        public bool VerifyPassword(string plainPassword)
        {
            return clsPasswordHasher.VerifyPassword(plainPassword, _passwordHash);
        }

        public bool ChangePassword(string currentPlainPassword, string newPlainPassword)
        {
            if(_mode != enMode.Update)
                throw new InvalidOperationException("Cannot change password for an unsaved user.");

            if(string.IsNullOrWhiteSpace(newPlainPassword))
                throw new ArgumentException("New password cannot be null or empty.", nameof(newPlainPassword));

            if (!VerifyPassword(currentPlainPassword))
                return false;

            string newHash = clsPasswordHasher.HashPassword(newPlainPassword);

            if(clsUserData.ChangePassword(this.UserId, newHash))
            {
                _passwordHash = newHash;
                return true;
            }


            return false;
        }

        private bool _AddNewUser()
        {
            UserDTO userDTO = new UserDTO
            {
                UserName = this.UserName,
                Password = _passwordHash,
                IsActive = this.IsActive,
                PersonID = this.PersonId
            };

            UserId = clsUserData.AddNewUser(userDTO);
            return UserId != -1;
        }

        private bool _UpdateUser()
        {
            UserDTO userDTO = new UserDTO
            {
                UserID = this.UserId,
                UserName = this.UserName,
                Password = _passwordHash,
                IsActive = this.IsActive,
                PersonID = this.PersonId
            };

            return clsUserData.UpdateUser(userDTO);
        }

        public bool Save()
        {
            switch (_mode)
            {
                case enMode.AddNew:
                    {
                        if (_AddNewUser())
                        {
                            this._mode = enMode.Update;
                            return true;
                        }
                        else
                        {
                            return false;
                        }
                    }

                case enMode.Update:
                    {
                        return _UpdateUser();
                    }
            }

            return false;
        }

        public static enDeleteResult Delete(int userId)
        {
            return clsUserData.DeleteUser(userId);
        }

        public static DataTable GetAllUsers()
        {
            return clsUserData.GetAllUsersWithDetails();
        }

        public static bool IsUserExist(int userId)
        {
            return clsUserData.IsUserExist(userId);
        }

        public static bool IsUserExist(string userName)
        {
            return clsUserData.IsUserExist(userName);
        }

        public static bool IsUserExist(string userName, int excludeUserId)
        {
            return clsUserData.IsUserExist(userName, excludeUserId);
        }

        public static bool IsUserExistForPersonId(int personId)
        {
            return clsUserData.IsUserExistForPersonId(personId);
        }
    }
}
