using DVLD_DataAccess.Users;
using DVLD_Shared;
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
        public string Password { get; set; } = null;
        public bool IsActive { get; set; } = false;

        private enMode _mode = enMode.Update;

        public clsUser() 
        {
            UserId = -1;
            _mode = enMode.AddNew;
        }

        private clsUser(UserDTO userDTO, clsPerson person)
        {
            UserId = userDTO.UserID;
            UserName = userDTO.UserName;
            Password = userDTO.Password;
            IsActive = userDTO.IsActive;
            PersonId = userDTO.PersonID;

            if(person != null)
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

        public static clsUser Find(string username, string password)
        {
            UserDTO userDTO = new UserDTO
            {
                UserName = username,
                Password = password
            };

            bool isFoud = clsUserData.GetUserInfoByUserNameAndPassword(userDTO);

            if (isFoud)
            {
                clsPerson person = null;

                if (userDTO.PersonID.HasValue)
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
    
        private bool _AddNewUser()
        {
            UserDTO userDTO = new UserDTO
            {
                UserName = this.UserName,
                Password = this.Password,
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
                Password = this.Password,
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

        public static bool IsUserExistForPersonId(int personId)
        {
            return clsUserData.IsUserExistForPersonId(personId);
        }
    }
}
