using DVLD_Business.LocalDrivingLicenseApplications;
using DVLD_Business.ManageTestTypes;
using DVLD_Business.TestAppointments;
using System;
using System.Windows.Forms;

namespace DVLD.Tests.Controls
{
    public partial class ctrlScheduledTest : UserControl
    {
        public enum enCreatedMode { Schedule, TakeTest }
        public enum enMode { Update, AddNew };

        private clsTestType _testTypeInfo = null;
        private clsTestAppointment _appointment = null;
        private clsLocalDrivingLicenseApplication _localApplication = null;
        private enCreatedMode _createdMode = enCreatedMode.Schedule;
        private enMode _modeStatus = enMode.Update;

        public ctrlScheduledTest()
        {
            InitializeComponent();
            _ResetDefaultValues();
            _SetDefaultValues();
        }

        public bool EnableDate
        {
            set
            {
                dtAppointmentDate.Enabled = value;
            }
        }


        public bool EnableRetakeTestInfo
        {
            set
            {
                gbRetakeTestInfo.Enabled = value;
            }
        }

        public bool ShowRetakeTestInfo
        {
            set
            {
                gbRetakeTestInfo.Visible = value;
            }
        }

        private void _ResetDefaultValues()
        {
            lblDLApplicationId.Text = "N\\A";
            lblClassType.Text = "N\\A";
            lblName.Text = "N\\A";
            lblTrial.Text = "N\\A";
            lblAppointmentDate.Text = "N\\A";
            lblFees.Text = "N\\A";
            lblTestId.Text = "N\\A"; 
        }

        private void _SetDefaultValues()
        {
            dtAppointmentDate.Value = DateTime.Today;
            //dtAppointmentDate.MinDate = DateTime.Today;
        }

        public DateTime GetSelectedDate
        {
            get
            {
                return dtAppointmentDate.Value;
            }
        }

        private void _FillData()
        {
            gbTestType.Text = _testTypeInfo.GetTestTypeName() + " Test";
            lblDLApplicationId.Text = _appointment.LocalDrivingLicenseApplicationId.ToString();
            lblClassType.Text = _localApplication.LicenseClassInfo.ClassName;
            lblName.Text = _localApplication.PersonInfo.GetName();
            lblFees.Text = _testTypeInfo.TestTypeFees.ToString();
            lblTrial.Text = _localApplication.TotalTrialsPerTest(_testTypeInfo.TestTypeId).ToString();

            if (_createdMode == enCreatedMode.TakeTest)
            {
                lblAppointmentDate.Text = _appointment.AppointmentDate.ToShortDateString();
                lblTestId.Text = "Not Taken Yet";
            }
        }

        public int SetTestId
        {
            set
            {
                lblTestId.Text = value.ToString();
            }
        }

        private void _ApplyModeSettings()
        {
            bool isSchedule = (_createdMode == enCreatedMode.Schedule);

            dtAppointmentDate.Visible = isSchedule;
            lblAppointmentDate.Visible = !isSchedule;
            gbRetakeTestInfo.Visible = !isSchedule;
            gbRetakeTestInfo.Visible = !isSchedule;
        }

        private void _HandleAppointmentDateConstraints()
        {
            if (_modeStatus == enMode.AddNew)
            {
                dtAppointmentDate.MinDate = DateTime.Today;
                dtAppointmentDate.Value = DateTime.Now;
            }
            else  
            {
                dtAppointmentDate.Value = _appointment.AppointmentDate;

                 if (_appointment.AppointmentDate < DateTime.Today)
                    dtAppointmentDate.MinDate = _appointment.AppointmentDate;
                else
                    dtAppointmentDate.MinDate = DateTime.Today;
            }
        }

        public void LoadInfo(
            clsTestAppointment appointment, 
            clsLocalDrivingLicenseApplication localApplication,
            clsTestType testType,
            enCreatedMode createdMode,
            enMode modeStatus
        )
        {
            _appointment = appointment;
            _localApplication = localApplication;
            _createdMode = createdMode;
            _modeStatus = modeStatus;
            _testTypeInfo = testType;

            _HandleAppointmentDateConstraints();
            _ApplyModeSettings();
            _FillData();        
        }
    }
}
