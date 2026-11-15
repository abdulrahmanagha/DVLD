using DVLD_DataAccess.LicenseClasses;
using DVLD_Shared;
using System.Data;

namespace DVLD_Business.LicenseClasses
{
    public class clsLicenseClass
    {
        public int LicenseClassId { get; set; }
        public string ClassName { get; set; }
        public string ClassDescription { get; set; }
        public int MinimumAllowedAge { get; set; }
        public int DefaultValidityLength { get; set; }
        public decimal ClassFees { get; set; }

        private clsLicenseClass(LicenseClassDTO licenseClassDTO)
        {
            LicenseClassId = licenseClassDTO.LicenseClassId;
            ClassName = licenseClassDTO.ClassName;
            ClassDescription = licenseClassDTO.ClassDescription;
            MinimumAllowedAge = licenseClassDTO.MinimumAllowedAge;
            DefaultValidityLength = licenseClassDTO.DefaultValidityLength;
            ClassFees = licenseClassDTO.ClassFees;
        }

        public static clsLicenseClass Find(int licenseClassId)
        {
            LicenseClassDTO licenseClassDTO = new LicenseClassDTO
            {
                LicenseClassId = licenseClassId,
            };


            if (clsLicenseClassData.GetLicenseClassById(licenseClassDTO))
            {
                return new clsLicenseClass(
                    licenseClassDTO
                );
            }
            else
                return null;
        }

        private bool _UpdateLicenseClass()
        {
            LicenseClassDTO licenseClassDTO = new LicenseClassDTO
            {
                LicenseClassId = this.LicenseClassId,
                ClassName = this.ClassName,
                ClassDescription = this.ClassDescription,
                MinimumAllowedAge = this.MinimumAllowedAge,
                DefaultValidityLength = this.DefaultValidityLength,
                ClassFees = this.ClassFees,
            };

            return clsLicenseClassData.UpdateLicenseClass(
                licenseClassDTO
            );
        }

        public bool Save()
        {
            return _UpdateLicenseClass();
        }

        public static DataTable GetAllLicenseClasses()
        {
            return clsLicenseClassData.GetAllLicenseClass();
        }

        public static int GetDefaultValidityDate(int licenseClassId)
        {
            return clsLicenseClassData.GetDefaultValidityDate(licenseClassId);
        }

        public static int GetMinimumAllowedAge(int  licenseClassId)
        {
            return clsLicenseClassData.GetMinimumAllowedAge(licenseClassId);
        }
    }
}
