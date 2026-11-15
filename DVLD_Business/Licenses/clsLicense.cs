using DVLD_Business.Applications;
using DVLD_Business.DetainedLicenses;
using DVLD_Business.Drivers;
using DVLD_Business.LicenseClasses;
using DVLD_Business.Manage_Types;
using DVLD_DataAccess.Licenses;
using DVLD_Shared;
using System;
using System.Data;

namespace DVLD_Business.Licenses
{
    public class clsLicense
    {
        private enum enMode { Update = 0, AddNew = 1 };

        public int LicenseId { get; set; }
        public int ApplicationId { get; set; }
        public int DriverId { get; set; }
        public int LicenseClassId { get; set; }
        public DateTime IssueDate { get; set; }
        public DateTime ExpirationDate { get; set; }
        public string Notes { get; set; }
        public decimal PaidFees { get; set; }
        public bool IsActive { get; set; }
        public enIssueReason IssueReason { get; set; }
        public int CreatedByUserId { get; set; }

        public clsDriver DriverInfo = null;
        public clsLicenseClass LicenseClassInfo = null;

        private enMode _mode = enMode.AddNew;

        public bool IsDetained
        {
            get
            {
                return clsDetainedLicense.IsLicenseDetained(this.LicenseId);
            }
        }

        public clsLicense()
        {
            _mode = enMode.AddNew;
        }
        
        private clsLicense(LicenseDTO license)
        {
            LicenseId = license.LicenseId;
            ApplicationId = license.ApplicationId;
            DriverId = license.DriverId;
            LicenseClassId = license.LicenseClassId;
            IssueDate = license.IssueDate;
            ExpirationDate = license.ExpirationDate;
            Notes = license.Notes;
            PaidFees = license.PaidFees;
            IsActive = license.IsActive;
            IssueReason = (enIssueReason)license.IssueReason;
            CreatedByUserId = license.CreatedByUserId;

            DriverInfo = clsDriver.Find(DriverId);
            LicenseClassInfo = clsLicenseClass.Find(LicenseClassId);
            _mode = enMode.Update;
        }

        public static clsLicense Find(int licenseId)
        {
            LicenseDTO license = new LicenseDTO
            {
                LicenseId = licenseId,
            };

            bool isFoud = clsLicenseData.GetLicenseInfoByLicenseId(license);

            if (isFoud)
            {
                return new clsLicense(license);
            }
            else
            {
                return null;
            }
        }

        public static clsLicense FindLicesneByApplicationId(int applicationId)
        {
            LicenseDTO license = new LicenseDTO
            {
                ApplicationId = applicationId
            };

            bool isFoud = clsLicenseData.GetLicenseInfoByApplicationId(license);

            if (isFoud)
            {
                return new clsLicense(license);
            }
            else
            {
                return null;
            }
        }

        public string IssueReasonString()
        {
            switch(IssueReason)
            {
                case enIssueReason.FirstTime:
                    return "First Time";

                case enIssueReason.Renew:
                    return "Renew";

                case enIssueReason.ReplacementForDamaged:
                    return "Replacement For Damaged";

                case enIssueReason.ReplacementForLost:
                    return "Replacement For Lost";

                default:
                    return "First Time";
            }
        }

        private bool _AddNewLicense()
        {
            LicenseDTO license = new LicenseDTO
            {
                ApplicationId = this.ApplicationId,
                DriverId = this.DriverId,
                LicenseClassId = this.LicenseClassId,
                IssueDate = this.IssueDate,
                ExpirationDate = this.ExpirationDate,
                Notes = this.Notes,
                PaidFees = this.PaidFees,
                IsActive = this.IsActive,
                IssueReason = (int)this.IssueReason,
                CreatedByUserId = this.CreatedByUserId,
            };

            LicenseId = clsLicenseData.AddNewLicense(license);
            return LicenseId != -1;
        }

        private bool _UpdateLicense()
        {
            LicenseDTO license = new LicenseDTO
            {
                LicenseId = this.LicenseId,
                ApplicationId = this.ApplicationId,
                DriverId = this.DriverId,
                LicenseClassId = this.LicenseClassId,
                IssueDate = this.IssueDate,
                ExpirationDate = this.ExpirationDate,
                Notes = this.Notes,
                PaidFees = this.PaidFees,
                IsActive = this.IsActive,
                IssueReason = (int)this.IssueReason,
                CreatedByUserId = this.CreatedByUserId,
            };

            return clsLicenseData.UpdateLicense(license);
        }

        public bool Save()
        {
            switch (_mode)
            {
                case enMode.AddNew:
                    {
                        if (_AddNewLicense())
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
                        return _UpdateLicense();
                    }
            }

            return false;
        }

        public static enDeleteResult Delete(int licenseId)
        {
            return clsLicenseData.DeleteLicense(licenseId);
        }

        public static DataTable GetAllLicenseDriver(int driverId)
        {
            return clsLicenseData.GetDriverLicenses(driverId);
        }

        public static bool IsLicenseExistByPersonID(int personId, int licenseClassId)
        {
            return clsLicenseData.IsLicenseExistByPersonID(personId, licenseClassId);
        }

        public static int GetActiveLicenseIDByPersonID(int personId, int licenseClassId)
        {
            return clsLicenseData.GetActiveLicenseIDByPersonID(personId, licenseClassId);
        }

        public bool DeactivateLicense()
        {
            return clsLicenseData.DeactivateLicense(this.LicenseId);
        }

        public bool IsLicenseExpired()
        {
            return (this.ExpirationDate < DateTime.Now);
        }

        public clsLicense RenewLicense(string notes, int createdByUserId)
        {
            clsApplicationType applicationType = clsApplicationType.Find(
                enApplicationType.RenewDrivingLicense
            );

            clsLicense newLicense = RenewLicense(
                notes,
                createdByUserId, 
                applicationType
            );

            return newLicense;
        }

        public clsLicense RenewLicense(string notes, int createdByUserId, clsApplicationType applicationType)
        {
            if (!this.IsActive || this.ExpirationDate > DateTime.Now)
            {
                return null;
            }

            if(this.IsDetained)
            {
                return null;
            }

            clsApplication application = new clsApplication();
            application.ApplicationDate = DateTime.Now;
            application.PersonId = this.DriverInfo.PersonId;
            application.ApplicationTypeId = (int)applicationType.ApplicationTypeId;
            application.ApplicationStatus = enApplicationStatus.Completed;
            application.LastStatusDate = DateTime.Now;
            application.PaidFees = applicationType.ApplicationTypeFees;
            application.CreatedByUserId = createdByUserId;

            if (!application.Save())
            {
                return null;
            }

            clsLicense newLicense = new clsLicense();
            newLicense.ApplicationId = application.ApplicationId;
            newLicense.DriverId = this.DriverId;
            newLicense.LicenseClassId = this.LicenseClassId;
            newLicense.IssueDate = DateTime.Now;
            newLicense.ExpirationDate = DateTime.Now.AddYears(
                this.LicenseClassInfo.DefaultValidityLength
            );
            newLicense.Notes = notes;
            newLicense.PaidFees = this.LicenseClassInfo.ClassFees;
            newLicense.IsActive = true;
            newLicense.IssueReason = enIssueReason.Renew;
            newLicense.CreatedByUserId = createdByUserId;

            if (!newLicense.Save())
            {
                clsApplication.Delete(application.ApplicationId);
                return null;
            }

            if (!DeactivateLicense())
            {
                Delete(newLicense.LicenseId);
                clsApplication.Delete(application.ApplicationId);
                return null;
            }

            return newLicense;
        }

        public clsLicense ReplaceLicense(string notes, int createdByUserId, enApplicationType applicationTypeId)
        {
            clsApplicationType applicationType = clsApplicationType.Find(
                applicationTypeId
            );

            clsLicense newLicense = ReplaceLicense(
                notes,
                createdByUserId,
                applicationType
            );

            return newLicense;
        }

        public clsLicense ReplaceLicense(string notes, int createdByUserId, clsApplicationType applicationType)
        {
            if (!this.IsActive || this.IsLicenseExpired())
            {
                return null;
            }

            if(this.IsDetained)
            {
                return null;
            }

            clsApplication application = new clsApplication();
            application.ApplicationDate = DateTime.Now;
            application.PersonId = this.DriverInfo.PersonId;
            application.ApplicationTypeId = (int)applicationType.ApplicationTypeId;
            application.ApplicationStatus = enApplicationStatus.Completed;
            application.LastStatusDate = DateTime.Now;
            application.PaidFees = applicationType.ApplicationTypeFees;
            application.CreatedByUserId = createdByUserId;

            if (!application.Save())
            {
                return null;
            }

            clsLicense newLicense = new clsLicense();
            newLicense.ApplicationId = application.ApplicationId;
            newLicense.DriverId = this.DriverId;
            newLicense.LicenseClassId = this.LicenseClassId;
            newLicense.IssueDate = DateTime.Now;
            newLicense.ExpirationDate = this.ExpirationDate;
            newLicense.PaidFees = this.LicenseClassInfo.ClassFees;
            newLicense.IsActive = true;
            newLicense.IssueReason = (applicationType.ApplicationTypeId == enApplicationType.ReplaceDamagedDrivingLicense) ?
                enIssueReason.ReplacementForDamaged : enIssueReason.ReplacementForLost;
            newLicense.CreatedByUserId = createdByUserId;
            newLicense.Notes = notes;

            if (!newLicense.Save())
            {
                clsApplication.Delete(application.ApplicationId);
                return null;
            }

            if (!DeactivateLicense())
            {
                Delete(newLicense.LicenseId);
                clsApplication.Delete(application.ApplicationId);
                return null;
            }

            return newLicense;
        }

        public int? DetainLicense(decimal fees, int createdByUserId)
        {
            if (this.IsDetained || !this.IsActive)
            {
                return null;
            }

            clsDetainedLicense detaineLicense = new clsDetainedLicense();
            detaineLicense.LicenseId = this.LicenseId;
            detaineLicense.DetainDate = DateTime.Now;
            detaineLicense.FineFees = fees;
            detaineLicense.CreatedByUserId = createdByUserId;
            detaineLicense.IsReleased = false;

            if (!detaineLicense.Save())
            {
                return null;
            }

            return detaineLicense.DetainId;
        }

        public bool ReleaseLicense(int detainedId, ref int? applicationId, int releasedByUserId)
        {
            clsApplicationType applicationType = clsApplicationType.Find(
                enApplicationType.ReleaseDetainedDrivingLicsense
            );

            return ReleaseLicense(
                detainedId,
                ref applicationId, 
                releasedByUserId,
                applicationType
            );
        }

        public bool ReleaseLicense(int detainedId, ref int? applicationId, int releasedByUserId, clsApplicationType applicationType)
        {
            applicationId = null;

            if (!(this.IsDetained && this.IsActive))
            {
                return false;
            }

            if(applicationType == null)
            {
                return false;
            }    
            
            clsApplication application = new clsApplication();
            application.ApplicationDate = DateTime.Now;
            application.PersonId = this.DriverInfo.PersonId;
            application.ApplicationTypeId = (int)enApplicationType.ReleaseDetainedDrivingLicsense;
            application.ApplicationStatus = enApplicationStatus.Completed;
            application.LastStatusDate = DateTime.Now;
            application.PaidFees = applicationType.ApplicationTypeFees;
            application.CreatedByUserId = releasedByUserId;

            if (!application.Save())
            {
                return false;
            }

            applicationId = application.ApplicationId;

            bool isReleased = clsDetainedLicense.ReleaseDetinedLicense(
                detainedId,
                applicationId.Value,
                releasedByUserId
            );

            if (!isReleased)
            {
                clsApplication.Delete(application.ApplicationId);
            }

            return isReleased;
        }
    }
}
