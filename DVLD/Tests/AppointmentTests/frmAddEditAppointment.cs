using DVLD.Tests.Controls;
using DVLD_Business.Applications;
using DVLD_Business.LocalDrivingLicenseApplications;
using DVLD_Business.Manage_Types;
using DVLD_Business.ManageTestTypes;
using DVLD_Business.TestAppointments;
using System;
using System.Windows.Forms;
using DVLD_Shared;
using DVLD.GlobalClasses;

namespace DVLD.Tests
{
    public partial class frmAddEditAppointment : Form
    {
        public event Action<int> OnApointmentSaved;

        protected virtual void ApointmentSaved(int appointmentId)
        {
            OnApointmentSaved?.Invoke(appointmentId);
        }

        public enum enAppointmentType { FirstAttempt, Retake }
        private enum enMode { Update, AddNew };

        private clsTestAppointment _appointment = null;
        private clsLocalDrivingLicenseApplication _localApplication = null;
        private clsTestType _testTypeInfo = null;
        private enTestType _testTypeId = enTestType.VisionTest;
        private int? _localApplicationId = null;
        private int? _appointmentTestId = null;
        private decimal retakeFees = 0;
        private enMode _mode;
        private enAppointmentType _appointmentType = enAppointmentType.FirstAttempt;
        private clsApplication _retakeApplication = null;

        public frmAddEditAppointment(
            int? localApplicationId, 
            enTestType testType
        )
        {
            InitializeComponent();
            _localApplicationId = localApplicationId;
            _testTypeId = testType;
            _mode = enMode.AddNew;
        }

        public frmAddEditAppointment(int? appointmentTest)
        {
            InitializeComponent();
            _appointmentTestId = appointmentTest;
            _mode = enMode.Update;
        }

        public void _PerpareTestType()
        {
            _testTypeInfo = clsTestType.Find(_testTypeId);

            if (_testTypeInfo == null)
            {
                MessageBox.Show(
                    "This form will be closed, because No Test Type with ID = " + (int)_testTypeId,
                    "Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error
                );

                this.Close();
                return;
            }

            string typeName = _testTypeInfo.GetTestTypeName();
            string title = "";

            if (_mode == enMode.AddNew) 
                title = $"Add {typeName} Test Appointment";
            else
                title = $"Update {typeName} Test Appointment";

            ctrlTestTitle1.LoadTypeWithTitle(title, _testTypeId);
            this.Text = title;
        }

        public void _PrepareLocalLicenseApplicationInfo()
        {
            _localApplication = clsLocalDrivingLicenseApplication.FindLocalDrivingLicenseApplication(
                _localApplicationId.Value
            );

            if (_localApplication == null)
            {
                MessageBox.Show(
                    "This form will be closed, because No Local Licesne Application with ID = " + _localApplicationId.Value,
                    "Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error
                );

                this.Close();
                return;
            }

        }

        public void _PrepareUpdateAppointment()
        {
            _appointment = clsTestAppointment.Find(_appointmentTestId.Value);

            if (_appointment == null)
            {
                MessageBox.Show(
                    "This form will be closed, because No Appointment Test with ID = " + _appointmentTestId.Value,
                    "Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error
                );

                this.Close();
                return;
            }

            _localApplicationId = _appointment.LocalDrivingLicenseApplicationId;
            _testTypeId = (enTestType)_appointment.TestTypeId;           
        }

        public void _PrepareAddNewAppointment()
        {
            _appointment = new clsTestAppointment();

            _appointment.TestTypeId = _testTypeId;
            _appointment.LocalDrivingLicenseApplicationId = _localApplication.LocalDrivingLicenseApplicationId;
            _appointment.IsLocked = false;
            _appointment.CreatedByUserId = clsGlobal.CurrentUser.UserId; 
        }

        private void _PrepareTestContext()
        {
            _PerpareTestType();
            _PrepareLocalLicenseApplicationInfo();
        }

        private void _PerpareAddNew()
        {
            _PrepareTestContext();
            _PrepareAddNewAppointment();
        }

        private void _PerpareUpdate()
        {
            _PrepareUpdateAppointment();
            _PrepareTestContext();
        }

        private void _PrepareMode()
        {
            if (_mode == enMode.AddNew)
            {
                _PerpareAddNew();
            }
            else
            {
                _PerpareUpdate();
            }
        }

        private void _PerpareScheduleTest()
        {
            DateTime oldDate = DateTime.Now;

            if (_mode == enMode.Update)
            {
                oldDate = _appointment.AppointmentDate;
            }

            ctrlScheduledTest.enMode mode = (
                    _mode == enMode.AddNew
            ) ? ctrlScheduledTest.enMode.AddNew : ctrlScheduledTest.enMode.Update;

            ctrlScheduledTest1.LoadInfo(
                _appointment,
                _localApplication,
                _testTypeInfo,
                ctrlScheduledTest.enCreatedMode.Schedule,
                mode
            );
        }

        private void _AppointmentIsLock()
        {
            lblLocked.Text = "Person already sat for the test, appointment locked.";
            ctrlScheduledTest1.EnableDate = false;
            btnSave.Enabled = false;
        }

        private void _AddNewRetakeApplication()
        {
            _retakeApplication = new clsApplication();

            _retakeApplication.CreatedByUserId = clsGlobal.CurrentUser.UserId;
            _retakeApplication.ApplicationStatus = enApplicationStatus.Completed;
            _retakeApplication.LastStatusDate = DateTime.Now;
            _retakeApplication.ApplicationTypeId = (int)enApplicationType.RetakeTest;
            _retakeApplication.ApplicationTypeInfo = clsApplicationType.Find(enApplicationType.RetakeTest);
            _retakeApplication.PersonId = _localApplication.PersonId;
            _retakeApplication.PaidFees = _retakeApplication.ApplicationTypeInfo.ApplicationTypeFees;
        }

        private void _FillDataRetakeTest()
        {
            gbRetakeApplication.Enabled = true;
            lblRetakeAppFees.Text = _retakeApplication.PaidFees.ToString();
            lblTotalFees.Text = (_testTypeInfo.TestTypeFees + _retakeApplication.PaidFees).ToString();
            lblRetakeApplicationId.Text = (_mode == enMode.Update) ? 
                _retakeApplication.ApplicationId.ToString() : "N\\A";

            ctrlTestTitle1.Title = $"Schedule Retake {_testTypeInfo.GetTestTypeName()} Test";
        }

        private void _PrepareRetakeApplication()
        {
            if(_mode == enMode.Update)
            {
                if (!_appointment.RetakeTestApplicationId.HasValue)
                    return;

                _retakeApplication = clsApplication.FindBaseApplication(_appointment.RetakeTestApplicationId.Value);
            }
            else
                _AddNewRetakeApplication();

            _FillDataRetakeTest();
        }

        private void _PerpareAppointmentType()
        {
            if(_mode == enMode.Update)
            {
                _appointmentType = (_appointment.RetakeTestApplicationId != null) ? 
                    enAppointmentType.Retake : enAppointmentType.FirstAttempt;
                return;
            }
            
            bool isRetake = clsLocalDrivingLicenseApplication.CanRetake(
                _localApplication.LocalDrivingLicenseApplicationId,
                _testTypeId
            );
    
            _appointmentType = (isRetake) ? enAppointmentType.Retake : enAppointmentType.FirstAttempt;
        }

        private void frmScheduleTests_Load(object sender, EventArgs e)
        {
            _PrepareMode();
            _PerpareAppointmentType();
            _PerpareScheduleTest();

            if (_appointmentType == enAppointmentType.Retake)
            {
                _PrepareRetakeApplication();
            }

            if (_appointment.IsLocked)
            {
                _AppointmentIsLock();
            }
        }

        private void _ConvertModeToUpdate()
        {
            _mode = enMode.Update;
        }

        private void _PerformAddNew()
        {
            MessageBox.Show("Data Saved Successfully",
                "Done",
                MessageBoxButtons.OK,
                MessageBoxIcon.Information
            );

            _ConvertModeToUpdate();
        }

        private void _PerformUpdate()
        {
            MessageBox.Show(
                "Data Updated Successfully",
                "Done",
                MessageBoxButtons.OK,
                MessageBoxIcon.Information
            );
        }

        private void _SetDataToSave()
        {
            _appointment.AppointmentDate = ctrlScheduledTest1.GetSelectedDate;
            _appointment.PaidFees = _testTypeInfo.TestTypeFees + retakeFees;

            if (_appointmentType == enAppointmentType.Retake)
            {
                _appointment.RetakeTestApplicationId = _retakeApplication.ApplicationId;
            }
        }

        private void _CreateRetakeApplication()
        {
            if (!_retakeApplication.Save())
            {
                MessageBox.Show(
                    "An error occurred.",
                    "Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error
                );

                Close();
            }
        }

        private bool _PerformSave()
        {
            if(_appointmentType == enAppointmentType.Retake)
            {
                _CreateRetakeApplication();
                lblRetakeApplicationId.Text = _retakeApplication.ApplicationId.ToString();
            }

            _SetDataToSave();

            if (_appointment.Save())
            {
                if (_mode == enMode.Update)
                {
                    _PerformUpdate();
                }
                else
                {
                    _PerformAddNew();
                }

                return true;
            }
            else
            {
                MessageBox.Show(
                    "Error, can't save data",
                    "Invalid",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error
                );
            }

            return false;
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            bool isSaved = _PerformSave();

            if (isSaved)
            {
                ApointmentSaved(_appointment.TestAppointmentId);
                Close();
            }
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
