using DVLD_DataAccess.DetainedLicenses;
using DVLD_Shared;
using DVLD_Shared.DTOs;
using System;
using System.Data;

namespace DVLD_Business.DetainedLicenses
{
    public class clsDetainedLicense
    {
        private enum enMode { Update = 0, AddNew = 1 };

        public int DetainId { get; set; }
        public int LicenseId { get; set; }
        public DateTime DetainDate { get; set; }
        public decimal FineFees { get; set; }
        public int CreatedByUserId { get; set; }
        public bool IsReleased { get; set; }
        public DateTime? ReleaseDate { get; set; }
        public int? ReleasedByUserId { get; set; }
        public int? ReleaseApplicationId { get; set; }

        private enMode _mode = enMode.AddNew;

        public clsDetainedLicense()
        {
            _mode = enMode.AddNew;
        }

        private clsDetainedLicense(DetainedLicensesDTO detainedLicensesDTO)
        {
            DetainId = detainedLicensesDTO.DetainId;
            LicenseId = detainedLicensesDTO.LicenseId;
            DetainDate = detainedLicensesDTO.DetainDate;
            FineFees = detainedLicensesDTO.FineFees;
            CreatedByUserId = detainedLicensesDTO.CreatedByUserId;
            IsReleased = detainedLicensesDTO.IsReleased;
            ReleaseDate = detainedLicensesDTO.ReleaseDate;
            ReleasedByUserId = detainedLicensesDTO.ReleasedByUserId;
            ReleaseApplicationId = detainedLicensesDTO.ReleaseApplicationId;

            _mode = enMode.Update;
        }

        public static clsDetainedLicense Find(int detainId)
        {
            DetainedLicensesDTO detainedLicensesDTO = new DetainedLicensesDTO
            {
                DetainId = detainId,
            };

            bool isFoud = clsDetainedLicenseData.GetDetainLicenseInfoById(detainedLicensesDTO);

            if (isFoud)
            {
                return new clsDetainedLicense(detainedLicensesDTO);
            }
            else
            {
                return null;
            }
        }

        public static clsDetainedLicense FindByLicneseId(int licenseId)
        {
            DetainedLicensesDTO detainedLicensesDTO = new DetainedLicensesDTO
            {
                LicenseId = licenseId,
            };

            bool isFoud = clsDetainedLicenseData.GetDetainLicenseInfoByLicenseId(detainedLicensesDTO);

            if (isFoud)
            {
                return new clsDetainedLicense(detainedLicensesDTO);
            }
            else
            {
                return null;
            }
        }

        private bool _AddNewDetainLicense()
        {
            DetainedLicensesDTO detainedLicensesDTO = new DetainedLicensesDTO()
            {
                LicenseId = this.LicenseId,
                DetainDate = this.DetainDate,
                FineFees = this.FineFees,
                CreatedByUserId = this.CreatedByUserId,
                IsReleased = this.IsReleased,
                ReleaseDate = this.ReleaseDate,
                ReleasedByUserId = this.ReleasedByUserId,
                ReleaseApplicationId = this.ReleaseApplicationId
            };

            DetainId = clsDetainedLicenseData.AddNewDetainLicense(detainedLicensesDTO);
            return DetainId != -1;
        }

        private bool _UpdateDetainLicense()
        {
            DetainedLicensesDTO detainedLicensesDTO = new DetainedLicensesDTO()
            {
                DetainId = this.DetainId,
                LicenseId = this.LicenseId,
                DetainDate = this.DetainDate,
                FineFees = this.FineFees,
                CreatedByUserId = this.CreatedByUserId,
                IsReleased = this.IsReleased,
                ReleaseDate = this.ReleaseDate,
                ReleasedByUserId = this.ReleasedByUserId,
                ReleaseApplicationId = this.ReleaseApplicationId
            };

            return clsDetainedLicenseData.UpdateDetainLicense(detainedLicensesDTO);
        }

        public bool Save()
        {
            switch (_mode)
            {
                case enMode.AddNew:
                    {
                        if (_AddNewDetainLicense())
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
                        return _UpdateDetainLicense();
                    }
            }

            return false;
        }

        public static enDeleteResult Delete(int detainId)
        {
            return clsDetainedLicenseData.DeleteDetainLicense(detainId);
        }

        public static bool IsLicenseDetained(int licenseId)
        {
            return clsDetainedLicenseData.IsLicenseDetained(licenseId);
        }

        public static bool IsLicenseReleased(int licenseId)
        {
            return !clsDetainedLicenseData.IsLicenseDetained(licenseId);
        }

        public static bool ReleaseDetinedLicense(
            int detainedId, 
            int applicationId, 
            int releasedByUserId
        )
        {
            return clsDetainedLicenseData.ReleaseDetainedLicense(
                detainedId,
                applicationId,
                releasedByUserId
            );
        }

        public bool ReleaseDetinedLicense(int applicationId,int releasedByUserId)
        {
            return clsDetainedLicenseData.ReleaseDetainedLicense(
                this.DetainId,
                applicationId,
                releasedByUserId
            );
        }

        public static DataTable GetAllDetainedLicenses()
        {
            return clsDetainedLicenseData.GetAllDetainedLicenses();
        }
    }
}
