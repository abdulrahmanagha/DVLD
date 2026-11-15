using DVLD_Business.TestAppointments;
using DVLD_Shared;
using System;

namespace DVLD_DataAccess.Tests
{
    public class clsTest
    {
        private enum enMode { Update = 0, AddNew = 1 };

        public int TestId { get; set; }
        public int TestAppointmentId { get; set; }
        public bool TestResult { get; set; }
        public string Notes { get; set; }
        public int CreatedByUserId { get; set; }

        private enMode _mode = enMode.AddNew;

        public clsTest()
        {
            _mode = enMode.AddNew;
        }

        private clsTest(TestDTO testDTO)
        {
            TestId = testDTO.TestId;
            TestAppointmentId = testDTO.TestAppointmentId;
            TestResult = testDTO.TestResult;
            Notes = testDTO.Notes;
            CreatedByUserId = testDTO.CreatedByUserId;
            _mode = enMode.Update;
        }

        public static clsTest Find(int testId)
        {
            TestDTO testDTO = new TestDTO
            {
                TestId = testId
            };

            bool isFoud = clsTestData.GetTestInfoById(testDTO);

            if (isFoud)
            {
                return new clsTest(testDTO);
            }
            else
            {
                return null;
            }
        }

        public static clsTest FindTestByTestAppointment(int testAppointment)
        {
            TestDTO testDTO = new TestDTO
            {
                TestAppointmentId = testAppointment
            };

            bool isFoud = clsTestData.GetTestInfoByAppointmentId(testDTO);

            if (isFoud)
            {
                return new clsTest(testDTO);
            }
            else
            {
                return null;
            }
        }

        public static clsTest FindLastTestPerPersonAndLicenseClass(int personId, int licenseClassId, enTestType testTypeId)
        {
            TestDTO testDTO = new TestDTO();

            bool isFoud = clsTestData.GetLastTestByPersonAndTestTypeAndLicenseClass(
                personId,
                licenseClassId,
                (int)testTypeId,
                testDTO
            );

            if (isFoud)
            {
                return new clsTest(testDTO);
            }
            else
            {
                return null;
            }
        }

        private bool _AddTest()
        { 
            TestDTO testDTO = new TestDTO
            {
                TestAppointmentId = this.TestAppointmentId,
                TestResult = this.TestResult,
                Notes = this.Notes,
                CreatedByUserId = this.CreatedByUserId
            };

            TestId = clsTestData.AddNewTest(testDTO);
            return TestId != -1;
        }

        private bool _UpdateTest()
        {
            TestDTO testDTO = new TestDTO
            {
                TestId = this.TestId,
                TestAppointmentId = this.TestAppointmentId,
                TestResult = this.TestResult,
                Notes = this.Notes,
                CreatedByUserId = this.CreatedByUserId
            };

            return clsTestData.UpdateTest(testDTO);
        }

        public bool Save()
        {
            switch (_mode)
            {
                case enMode.AddNew:
                    {
                        if (_AddTest())
                        {
                            this._mode = enMode.Update;
                            return clsTestAppointment.LockAppointment(this.TestAppointmentId); ;
                        }
                        else
                        {
                            return false;
                        }
                    }

                case enMode.Update:
                    {
                        return _UpdateTest();
                    }
            }

            return false;
        }

        public static int GetPassedTestsCount(int localDrivingLicenseApplicationId)
        {
            return clsTestData.GetPassedTestCount(
                localDrivingLicenseApplicationId
            );
        }

    }
}
