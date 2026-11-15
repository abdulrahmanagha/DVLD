using DVLD_Business.Applications;
using DVLD_Business.Drivers;
using DVLD_DataAccess.InternationalLicenses;
using DVLD_Shared;
using DVLD_Shared.DTOs;
using System;
using System.Data;

namespace DVLD_Business.InternationalLicenses
{
    public class clsInternationalLicense : clsApplication
    {
        public int InternationalLicenseId { get; set; }
        public int DriverId { get; set; }
        public int IssuedUsingLocalLicenseId { get; set; }
        public DateTime IssueDate { get; set; }
        public DateTime ExpirationDate { get; set; }
        public bool IsActive { get; set; }

        private clsDriver _driverInfo = null;

        public clsDriver DriverInfo
        {
            get
            {
                if( _driverInfo == null )
                {
                    _driverInfo = clsDriver.Find(this.DriverId);
                }

                return _driverInfo;
            }
        }

        public clsInternationalLicense() : base()
        {
            InternationalLicenseId = -1;
            DriverId = -1;
            IssuedUsingLocalLicenseId = -1;
            _mode = enMode.AddNew;
        }

        private clsInternationalLicense(InternationalDTO internationalDTO, clsApplication application)
            : base(application)
        {
            InternationalLicenseId = internationalDTO.InternationalLicenseId;
            DriverId = internationalDTO.DriverId;
            IssuedUsingLocalLicenseId = internationalDTO.IssuedUsingLocalLicenseId;
            IssueDate = internationalDTO.IssueDate;
            ExpirationDate = internationalDTO.ExpirationDate;
            IsActive = internationalDTO.IsActive;

            _mode = enMode.Update;
        }

        public static clsInternationalLicense FindInternationalLicense(int internationalLicenseId)
        {
            InternationalDTO internationalDTO = new InternationalDTO()
            {
                InternationalLicenseId = internationalLicenseId
            };

            bool isFound = clsInternationalLicenseData.GetInternationalLicenseInfoById(
                internationalDTO
            );

            if (!isFound)
                return null;

            clsApplication application = clsApplication.FindBaseApplication(internationalDTO.ApplicationId);

            if (application == null)
                return null;

            return new clsInternationalLicense(
                internationalDTO,
                application
            );
        }

        private bool _AddNewInternationalLicense()
        {
            InternationalDTO internationalDTO = new InternationalDTO()
            {
                ApplicationId = this.ApplicationId,
                DriverId = this.DriverId,
                IssuedUsingLocalLicenseId = this.IssuedUsingLocalLicenseId,
                IssueDate = this.IssueDate,
                ExpirationDate = this.ExpirationDate,
                IsActive = this.IsActive,
                CreatedByUserId = this.CreatedByUserId
            };

            InternationalLicenseId = clsInternationalLicenseData.AddNewInternationalLicense(internationalDTO);
            return InternationalLicenseId != -1;
        }

        private bool _UpdateInternationalLicense()
        {
            InternationalDTO internationalDTO = new InternationalDTO()
            {
                InternationalLicenseId = this.InternationalLicenseId,
                ApplicationId = this.ApplicationId,
                DriverId = this.DriverId,
                IssuedUsingLocalLicenseId = this.IssuedUsingLocalLicenseId,
                IssueDate = this.IssueDate,
                ExpirationDate = this.ExpirationDate,
                IsActive = this.IsActive,
                CreatedByUserId = this.CreatedByUserId
            };

            return clsInternationalLicenseData.UpdateInternationalLicense(internationalDTO);
        }

        public override bool Save()
        {
            switch (_mode)
            {
                case enMode.AddNew:
                    {
                        if (!base.Save()) 
                            return false;

                        return _AddNewInternationalLicense();
                    }

                case enMode.Update:
                    {
                        if (!base.Save())
                            return false;

                        return _UpdateInternationalLicense();
                    }
            }

            return false;
        }

        public static enDeleteResult DeleteInternationalLicense(int internationalLicenseId)
        {
            return clsInternationalLicenseData.DeleteLicense(internationalLicenseId);
        }

        public static bool HasDriverInternationalLicense(int driverId)
        {
            return clsInternationalLicenseData.HasDriverActiveInternationalLicense(driverId);
        }

        public static DataTable GetInternationalLicenses()
        {
            return clsInternationalLicenseData.GetInternationalLicenses();
        }

        public static DataTable GetAllInternationalLicenseDriver(int driverId)
        {
            return clsInternationalLicenseData.GetAllInternationalLicenseDriver(driverId);
        }
    }
}
