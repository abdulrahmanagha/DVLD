using DVLD_Business.LocalDrivingLicenseApplications;
using DVLD_DataAccess.TestAppointments;
using DVLD_Shared;
using System;
using System.Data;

namespace DVLD_Business.TestAppointments
{
    public class clsTestAppointment
    {
        private enum enMode { Update = 0, AddNew = 1 };

        public int TestAppointmentId { get; set; }
        public enTestType TestTypeId { get; set; }
        public int LocalDrivingLicenseApplicationId { get; set; }
        public DateTime AppointmentDate { get; set; }
        public decimal PaidFees { get; set; }
        public int CreatedByUserId { get; set; }
        public bool IsLocked { get; set; }
        public int? RetakeTestApplicationId { get; set; }
        private enMode _mode = enMode.AddNew;

        public int TestID
        {
            get { return _GetTestId(); }

        }

        public clsTestAppointment()
        {
            _mode = enMode.AddNew;
        }

        public clsTestAppointment(TestAppointmentDTO testAppointmentDTO)
        {
            TestAppointmentId = testAppointmentDTO.TestAppointmentId;
            TestTypeId = (enTestType)testAppointmentDTO.TestTypeId;
            LocalDrivingLicenseApplicationId = testAppointmentDTO.LocalDrivingLicenseApplicationId;
            AppointmentDate = testAppointmentDTO.AppointmentDate;
            PaidFees = testAppointmentDTO.PaidFees;
            CreatedByUserId = testAppointmentDTO.CreatedByUserId;
            IsLocked = testAppointmentDTO.IsLocked;
            RetakeTestApplicationId = testAppointmentDTO.RetakeTestApplicationId;
            _mode = enMode.Update;
        }

        public static clsTestAppointment Find(int testAppointmentId)
        {
            TestAppointmentDTO testAppointmentDTO = new TestAppointmentDTO
            {
                TestAppointmentId = testAppointmentId
            };

            bool isFoud = clsTestAppointmentData.GetTestAppointmentInfoById(testAppointmentDTO);

            if (isFoud)
            {
                return new clsTestAppointment(testAppointmentDTO);
            }
            else
            {
                return null;
            }
        }

        private bool _AddTestAppointment()
        {
            TestAppointmentDTO testAppointmentDTO = new TestAppointmentDTO
            {
                TestTypeId = (int)this.TestTypeId,
                LocalDrivingLicenseApplicationId = this.LocalDrivingLicenseApplicationId,
                AppointmentDate = this.AppointmentDate,
                PaidFees = this.PaidFees,
                CreatedByUserId = this.CreatedByUserId,
                IsLocked = this.IsLocked,
                RetakeTestApplicationId = this.RetakeTestApplicationId,
            };

            if (clsLocalDrivingLicenseApplication.IsThereAnActiveScheduledTest
                (this.LocalDrivingLicenseApplicationId, this.TestTypeId
            ))
                return false;

            TestAppointmentId = clsTestAppointmentData.AddNewTestAppointment(testAppointmentDTO);
            return TestAppointmentId != -1;
        }

        private bool _UpdateTestAppointment()
        {
            TestAppointmentDTO testAppointmentDTO = new TestAppointmentDTO
            {
                TestAppointmentId = this.TestAppointmentId,
                TestTypeId = (int)this.TestTypeId,
                LocalDrivingLicenseApplicationId = this.LocalDrivingLicenseApplicationId,
                AppointmentDate = this.AppointmentDate,
                PaidFees = this.PaidFees,
                CreatedByUserId = this.CreatedByUserId,
                IsLocked = this.IsLocked,
                RetakeTestApplicationId = this.RetakeTestApplicationId,
            };

            return clsTestAppointmentData.UpdateTestAppointment(testAppointmentDTO);
        }

        public bool Save()
        {
            switch (_mode)
            {
                case enMode.AddNew:
                    {
                        if (_AddTestAppointment())
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
                        return _UpdateTestAppointment();
                    }
            }

            return false;
        }
        
        public static enDeleteResult Delete(int testAppointmentId)
        {
            return clsTestAppointmentData.DeleteTestAppointment(testAppointmentId);
        }

        public static DataTable GetAllAppointmentTestForLocalApplication(int localApplication)
        {
            return clsTestAppointmentData.GetAllAppointmentTestForLocalApplication(localApplication);
        }

        public static DataTable GetAllAppointmentTestForLocalApplication(
            int localApplication,
            enTestType testType
        )
        {
            return clsTestAppointmentData.GetApplicationTestAppointmentsPerTestType(localApplication, (int)testType);
        }

        public bool LockAppointment()
        {
            return clsTestAppointmentData.LockAppointment(this.TestAppointmentId);
        }

        public static bool LockAppointment(int appointmentId)
        {
            return clsTestAppointmentData.LockAppointment(appointmentId);
        }

        public static clsTestAppointment GetLastTestAppointment(
            int localDrivingLicesesApplication,
            enTestType testType
        )
        {
            TestAppointmentDTO testAppointmentDTO = new TestAppointmentDTO()
            {
                TestTypeId = (int)testType
            };

            bool isFound =  clsTestAppointmentData.GetLastTestAppointment(
                localDrivingLicesesApplication,
                testAppointmentDTO
            );

            if(isFound )
            {
                return new clsTestAppointment();
            }
            else
                return null;
        }

        public int _GetTestId()
        {
            return clsTestAppointmentData.GetTestId(this.TestAppointmentId);
        }

        public static DataTable GetApplicationTestAppointmentsPerTestType(int localApplication, int testType)
        {
            return clsTestAppointmentData.GetApplicationTestAppointmentsPerTestType(
                localApplication,
                testType
            );
        }

        public static bool HasAnyAppointment(int localApplication)
        {
            return clsTestAppointmentData.HasAnyAppointment(
                localApplication
            );
        }
    }
}
