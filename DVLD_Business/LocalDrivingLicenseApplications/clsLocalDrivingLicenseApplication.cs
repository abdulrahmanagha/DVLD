using DVLD_Business.Applications;
using DVLD_Business.Drivers;
using DVLD_Business.LicenseClasses;
using DVLD_Business.Licenses;
using DVLD_Business.TestAppointments;
using DVLD_DataAccess.Licenses;
using DVLD_DataAccess.LocalDrivingLicenseApplications;
using DVLD_DataAccess.Tests;
using DVLD_Shared;
using System;
using System.Data;

namespace DVLD_Business.LocalDrivingLicenseApplications
{
    public class clsLocalDrivingLicenseApplication : clsApplication
    {
        public int LocalDrivingLicenseApplicationId { get; set; }
        public int LicenseClassId { get; set; }

        public clsLicenseClass LicenseClassInfo { get; set; }

        public clsLocalDrivingLicenseApplication() : base() 
        {
            LocalDrivingLicenseApplicationId = -1;
            ApplicationId = -1;
            LicenseClassId = -1;
            _mode = enMode.AddNew;
        }

        private clsLocalDrivingLicenseApplication(int localDrivingLicenseApplicationId, 
            int licenseClassId, clsLicenseClass licenseClass ,clsApplication application)
            : base(application)
        {
            LocalDrivingLicenseApplicationId = localDrivingLicenseApplicationId;
            LicenseClassId = licenseClassId;
            LicenseClassInfo = licenseClass;
            _mode = enMode.Update;
        }

        public static clsLocalDrivingLicenseApplication FindLocalDrivingLicenseApplication(int localDrivingLicenseApplicationId)
        {
            int applicationId = -1, licenseClassId = -1;

            bool isFound = clsLocalDrivingLicenseApplicationData.GetLocalDrivingLicenseApplicationInfoById(
                localDrivingLicenseApplicationId, ref applicationId, ref licenseClassId);

            if(!isFound)
                return null;

            clsApplication application = clsApplication.FindBaseApplication(applicationId);

            if(application == null) 
                return null;

            clsLicenseClass licenseClass = clsLicenseClass.Find(licenseClassId);

            return new clsLocalDrivingLicenseApplication(
                    localDrivingLicenseApplicationId,
                    licenseClassId,
                    licenseClass,
                    application
            );
        }

        private bool _AddNewLocalDrivingLicenseApplication()
        {
            LocalDrivingLicenseApplicationId = clsLocalDrivingLicenseApplicationData.AddNewLocalDrivingLicenseApplication (
                ApplicationId,
                LicenseClassId
            );

            return LocalDrivingLicenseApplicationId != -1;
        }

        private bool _UpdateLocalDrivingLicenseApplication()
        {
            return clsLocalDrivingLicenseApplicationData.UpdateLocalDrivingLicenseApplication(
                LocalDrivingLicenseApplicationId,
                ApplicationId,
                LicenseClassId
            );
        }

        public override bool Save()
        {
            switch (_mode)
            {
                case enMode.AddNew:
                    {
                        if (!base.Save()) // must here because not change MODE by base 
                            return false;

                        return _AddNewLocalDrivingLicenseApplication();
                    }

                case enMode.Update:
                    {
                        if (!base.Save()) 
                            return false;

                        return _UpdateLocalDrivingLicenseApplication();
                    }
            }

            return false;
        }

        private static enDeleteResult _DeleteLocalDrivingLicenseApplication(int localDrivingLicenseApplicationId, int applicationId)
        {
            enDeleteResult deleteResult = clsLocalDrivingLicenseApplicationData.DeleteLocalDrivingLicenseApplication(
                    localDrivingLicenseApplicationId
            );

            if (deleteResult != enDeleteResult.Success)
                return deleteResult;

            return clsApplication.Delete(applicationId);
        }

        public static enDeleteResult Delete(int localDrivingLicenseApplicationId)
        {
            clsLocalDrivingLicenseApplication localDrivingLicenseApplication = FindLocalDrivingLicenseApplication(
                localDrivingLicenseApplicationId
            );

            if(localDrivingLicenseApplication == null)
                return enDeleteResult.Error;

            return _DeleteLocalDrivingLicenseApplication(
                localDrivingLicenseApplication.LocalDrivingLicenseApplicationId,
                localDrivingLicenseApplication.ApplicationId
            );
        }

        public static enDeleteResult Delete(int localDrivingLicenseApplicationId, int applicationId)
        {
            return _DeleteLocalDrivingLicenseApplication(
                localDrivingLicenseApplicationId,
                applicationId
            );
        }

        public static DataTable GetAllLocalDrivingLicenseApplication()
        {
            return clsLocalDrivingLicenseApplicationData.GetAllLocalDrivingLicenseApplications();
        }

        public static DataTable GetAllLocalDrivingLicenseApplicationWithDetails()
        {
            return clsLocalDrivingLicenseApplicationData.GetAllLocalDrivingLicenseApplicationsWithDetails();
        }

        public static new bool CancelApplication(int localDrivingLicenseApplicationId)
        {
            clsLocalDrivingLicenseApplication localDrivingLicenseApplication = FindLocalDrivingLicenseApplication(
                localDrivingLicenseApplicationId
            );

            if (localDrivingLicenseApplication == null)
                return false;

            return clsApplication.CancelApplication(
                localDrivingLicenseApplication.ApplicationId
            );
        }

        public static new bool CompleteApplication(int localDrivingLicenseApplicationId)
        {
            clsLocalDrivingLicenseApplication localDrivingLicenseApplication = FindLocalDrivingLicenseApplication(
                localDrivingLicenseApplicationId
            );

            if (localDrivingLicenseApplication == null)
                return false;

            return clsApplication.CompleteApplication(
                localDrivingLicenseApplication.ApplicationId
            );
        }

        public static bool CanRetake(int localApplication, enTestType testType)
        {
            if( clsLocalDrivingLicenseApplicationData.DoesPassTestType(
                localApplication,
                (int)testType
            ))
            {
                return false;
            }

            int faildTestCount = clsLocalDrivingLicenseApplicationData.GetFailedTestCount(
                localApplication,
                (int)testType
            );
            
            return faildTestCount > 0;
        }

        public int? GetActiveLicenseID()
        {
            int? licenseId = clsLicense.GetActiveLicenseIDByPersonID(this.PersonId, this.LicenseClassId);
            return (licenseId != -1) ? licenseId : null;
        }

        public static bool IsThereAnActiveScheduledTest(int localApplicationId, enTestType testTypeId)
        {
            return clsLocalDrivingLicenseApplicationData.IsThereAnActiveScheduledTest(
                localApplicationId,
                (int)testTypeId
            );
        }

        public static bool DoesPassTestType(int localApplicationId, enTestType testTypeId)
        {
            return clsLocalDrivingLicenseApplicationData.DoesPassTestType(
                localApplicationId,
                (int)testTypeId
            );
        }

        public bool DoesPassTestType(enTestType testTypeId)
        {
            return clsLocalDrivingLicenseApplicationData.DoesPassTestType(
                this.LocalDrivingLicenseApplicationId,
                (int)testTypeId
            );
        }

        public static int TotalTrialsPerTest(int localApplicationId, enTestType testTypeId)
        {
            return clsLocalDrivingLicenseApplicationData.TotalTrialsPerTest(
                localApplicationId,
                (int)testTypeId
            );
        }

        public int TotalTrialsPerTest(enTestType testTypeId)
        {
            return clsLocalDrivingLicenseApplicationData.TotalTrialsPerTest(
                this.LocalDrivingLicenseApplicationId,
                (int)testTypeId
            );
        }

        public clsTest GetLastTestPerTestType(enTestType testTypeId)
        {
            return clsTest.FindLastTestPerPersonAndLicenseClass(
                this.PersonId,
                this.LicenseClassId,
                testTypeId
            );
        }

        public bool IsLicenseIssued()
        {
            return clsLicenseData.GetLicenseIdByApplicationId(this.ApplicationId) != -1;
        }

        public int? GetIssuedLicenseId()
        {
            int? licenseId = clsLicenseData.GetLicenseIdByApplicationId(this.ApplicationId);
            return (licenseId != -1) ? licenseId : null;
        }

        public int? IssueLicenseForTheFirstTime(string notes, int createdByUserId)
        {
            if (IsLicenseIssued())
                return null;

            if(this.ApplicationStatus != enApplicationStatus.New)
                return null;

            clsDriver driver = clsDriver.FindByPersonId(PersonId);

            if(driver == null)
            {
                driver = new clsDriver();
                driver.PersonId = PersonId;
                driver.CreatedByUserId = createdByUserId;

                if(!driver.Save())
                {
                    return null;
                }
            }

            clsLicense license = new clsLicense();
            license.DriverId = driver.DriverId;
            license.LicenseClassId = this.LicenseClassId;
            license.IssueDate = DateTime.Now;
            license.ExpirationDate = DateTime.Now.AddYears(
                this.LicenseClassInfo.DefaultValidityLength
            );
            license.Notes = notes;
            license.IsActive = true;
            license.IssueReason = enIssueReason.FirstTime;
            license.CreatedByUserId = createdByUserId;
            license.ApplicationId = this.ApplicationId;
            license.PaidFees = this.LicenseClassInfo.ClassFees;

            if(!license.Save())
            {
                clsDriver.Delete(driver.DriverId);
                return null;
            }

            this.CompleteApplication();
            return license.LicenseId;
        }

        public bool IsPassedAllTests()
        {
            return clsTest.GetPassedTestsCount(this.LocalDrivingLicenseApplicationId) >= 3;
        }

        public bool HasAnyAppointment()
        {
            return clsTestAppointment.HasAnyAppointment(
                this.LocalDrivingLicenseApplicationId
            );
        }

        public static bool HasAnyAppointment(int localDrivingLicenseApplicationId)
        {
            return clsTestAppointment.HasAnyAppointment(
                localDrivingLicenseApplicationId
            );
        }
 
    }
}
