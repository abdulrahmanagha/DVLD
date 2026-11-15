using DVLD_DataAccess.Manage_Types;
using DVLD_Shared;
using System;
using System.Data;

namespace DVLD_Business.Manage_Types
{
    public class clsApplicationType
    {
        public enApplicationType ApplicationTypeId { get; set; }
        public string ApplicationTypeTitle { get; set; }
        public decimal ApplicationTypeFees { get; set; }

        private clsApplicationType(enApplicationType applicationTypeId, string applicationTypeTitle, decimal applicationTypeFees)
        {
            ApplicationTypeId = applicationTypeId;
            ApplicationTypeTitle = applicationTypeTitle;
            ApplicationTypeFees = applicationTypeFees;
        }

        public static clsApplicationType Find(enApplicationType applicationTypeId)
        {
            string applicationTypeTitle = null;
            decimal applicationTypeFees = 0;

            if (
                clsApplicationTypeData.GetApplicationTypeByInfoId(
                    (int)applicationTypeId, 
                    ref applicationTypeTitle,
                    ref applicationTypeFees
                )
            )
            {
                return new clsApplicationType(
                    applicationTypeId,
                    applicationTypeTitle,
                    applicationTypeFees
                );
            }
            else
                return null;
        }

        public static DataTable GetAllApplicationTypes()
        {
            return clsApplicationTypeData.GetAllApplicationTypes();
        }

        private bool _UpdateApplicationType()
        {
            return clsApplicationTypeData.UpdateApplicationType(
                (int)ApplicationTypeId,
                ApplicationTypeTitle, 
                ApplicationTypeFees
            );
        }

        public bool Save()
        {
            return _UpdateApplicationType();
        }
    }
}
