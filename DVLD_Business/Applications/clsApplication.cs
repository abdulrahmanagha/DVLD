using DVLD_Business.Manage_Types;
using DVLD_Business.Users;
using DVLD_DataAccess.Applications;
using DVLD_Shared;
using System;

namespace DVLD_Business.Applications
{
    public class clsApplication
    {
        protected enum enMode { AddNew, Update };

        public int ApplicationId { get; set; }
        public int PersonId { get; set; }
        public int ApplicationTypeId { get; set; }
        public DateTime ApplicationDate { get; set; }
        public DateTime LastStatusDate { get; set; }
        public enApplicationStatus ApplicationStatus { get; set; }
        public decimal PaidFees { get; set; }
        public int CreatedByUserId { get; set; }
        public clsPerson PersonInfo { get; set; }
        public clsUser CreatedByUserInfo { get; set; }
        public clsApplicationType ApplicationTypeInfo { get; set; }
        protected enMode _mode = enMode.AddNew;

        protected clsApplication(
            ApplicationDTO applicationDTO, 
            clsPerson person, 
            clsUser user,
            clsApplicationType applicationType
        )
        {
            ApplicationId = applicationDTO.ApplicationId;
            PersonId = applicationDTO.PersonId;
            ApplicationDate = applicationDTO.ApplicationDate;
            ApplicationTypeId = applicationDTO.ApplicationTypeId;
            ApplicationStatus = (enApplicationStatus)applicationDTO.ApplicationStatus;
            LastStatusDate = applicationDTO.LastStatusDate;
            CreatedByUserId = applicationDTO.CreatedByUserId;
            PaidFees = applicationDTO.PaidFees;

            if(person != null)
            {
                PersonInfo = person;
            }

            if(user != null)
            {
                CreatedByUserInfo = user;
            }

            if(applicationType != null)
            {
                ApplicationTypeInfo = applicationType;
            }

            _mode = enMode.Update;
        }

        protected clsApplication(clsApplication application)
        {
            ApplicationId = application.ApplicationId;
            PersonId = application.PersonId;
            ApplicationDate = application.ApplicationDate;
            ApplicationTypeId = application.ApplicationTypeId;
            ApplicationStatus = application.ApplicationStatus;
            LastStatusDate = application.LastStatusDate;
            CreatedByUserId = application.CreatedByUserId;
            PaidFees = application.PaidFees;

            PersonInfo = application.PersonInfo;
            ApplicationTypeInfo = application.ApplicationTypeInfo;
            CreatedByUserInfo = application.CreatedByUserInfo;
            _mode = enMode.Update;
        }

        public clsApplication()
        {
            _mode = enMode.AddNew;
        }

        public static clsApplication FindBaseApplication(int applicationId)
        {
            ApplicationDTO applicationDTO = new ApplicationDTO
            {
                ApplicationId = applicationId
            };

            bool isFound = clsApplicationData.GetApplicationInfoById(applicationDTO);

            if(isFound)
            {
                clsPerson person = clsPerson.Find(applicationDTO.PersonId);
                clsApplicationType applicationType = clsApplicationType.Find((enApplicationType)applicationDTO.ApplicationTypeId);
                clsUser user = clsUser.Find(applicationDTO.CreatedByUserId);
                return new clsApplication(applicationDTO, person, user, applicationType);
            }
            else
            {
                return null;
            }
        }

        private bool _AddNewApplication()
        {
            ApplicationDTO applicationDTO = new ApplicationDTO()
            {
                PersonId = this.PersonId,
                ApplicationDate = this.ApplicationDate,
                ApplicationTypeId = this.ApplicationTypeId,
                ApplicationStatus = (int)this.ApplicationStatus,
                LastStatusDate = this.LastStatusDate,
                PaidFees = this.PaidFees,
                CreatedByUserId = this.CreatedByUserId
            };

            ApplicationId = clsApplicationData.AddNewApplication(applicationDTO);
            return ApplicationId != -1;
        }

        private bool _UpdateApplication()
        {
            ApplicationDTO applicationDTO = new ApplicationDTO()
            {
                ApplicationId = this.ApplicationId,
                PersonId = this.PersonId,
                ApplicationTypeId = this.ApplicationTypeId,
                ApplicationStatus = (int)this.ApplicationStatus,
                LastStatusDate = this.LastStatusDate,
                PaidFees = this.PaidFees,
                CreatedByUserId = this.CreatedByUserId
            };

            return clsApplicationData.UpdateApplication(applicationDTO);
        }

        public virtual bool Save()
        {
            switch (_mode)
            {
                case enMode.AddNew:
                    {
                        if (_AddNewApplication())
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
                        return _UpdateApplication();
                    }
            }

            return false;
        }

        public static enDeleteResult Delete(int applicationId)
        {
            return clsApplicationData.DeleteApplication(applicationId);
        }
    
        public virtual bool CancelApplication()
        {
            return clsApplicationData.UpdateApplicationStatus(
                this.ApplicationId, (int)enApplicationStatus.Cancelled
            );
        }

        public static bool CancelApplication(int applicationId)
        {
            return clsApplicationData.UpdateApplicationStatus(
                applicationId, (int)enApplicationStatus.Cancelled
            );
        }

        public virtual bool CompleteApplication()
        {
            return clsApplicationData.UpdateApplicationStatus(
                this.ApplicationId, (int)enApplicationStatus.Completed
            );
        }

        public static bool CompleteApplication(int applicationId)
        {
            return clsApplicationData.UpdateApplicationStatus(
                applicationId, (int)enApplicationStatus.Completed
            );
        }

        public static bool IsApplicationExist(int applicationId)
        {
            return clsApplicationData.IsApplicationExist(applicationId);
        }

        //public static bool DoesPersonHaveActiveApplication(int personId, enApplicationType applicationTypeId)
        //{
        //    return clsApplicationData.DoesPersonHaveActiveApplication(personId, (int)applicationTypeId);
        //}

        //public bool DoesPersonHaveActiveApplication(enApplicationType applicationTypeId)
        //{
        //    return clsApplicationData.DoesPersonHaveActiveApplication(this.PersonId, (int)applicationTypeId);
        //}

        public static int? GetActiveApplicationIDForLicenseClass(
            int personId, 
            enApplicationType applicationTypeId, 
            int licenseClassId
        )
        {
            int? activeApplicationId = clsApplicationData.GetActiveApplicationIDForLicenseClass(
                personId,
                (int)applicationTypeId,
                licenseClassId
            );

            return activeApplicationId.Value != -1 ? activeApplicationId : null;
        }
    }
}
