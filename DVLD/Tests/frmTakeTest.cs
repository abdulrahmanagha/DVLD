using DVLD.GlobalClasses;
using DVLD.Tests.Controls;
using DVLD_Business.LocalDrivingLicenseApplications;
using DVLD_Business.ManageTestTypes;
using DVLD_Business.TestAppointments;
using DVLD_DataAccess.Tests;
using System;
using System.Windows.Forms;
using static DVLD.Tests.Controls.ctrlScheduledTest;

namespace DVLD.Tests
{
    public partial class frmTakeTest : Form
    {
        public event Action<int> OnTestSaved;

        protected virtual void TestSaved(int testId)
        {
            OnTestSaved?.Invoke(testId);
        }

        private clsTest _test = null;
        private clsTestAppointment _appointment = null;
        private int? _appointmentId = null;
        private clsLocalDrivingLicenseApplication _localApplication = null;
        private clsTestType _testTypeInfo = null;

        public frmTakeTest(
            int appointmentId, 
            clsLocalDrivingLicenseApplication localApplication,
            clsTestType testType
        )
        {
            InitializeComponent();
            _appointmentId = appointmentId;
            _localApplication = localApplication;
            _testTypeInfo = testType;
            _PrepareTitle();
        }

        private void _PrepareTitle()
        {
            string typeName = _testTypeInfo.GetTestTypeName();
            string title = $"Schedule {typeName} Test";
            ctrlTestTitle1.LoadTypeWithTitle(title, _testTypeInfo.TestTypeId);
            this.Text = title;
        }

        private void _PrepareScheduleTest()
        {
            ctrlScheduledTest1.LoadInfo(
                _appointment,
                _localApplication,
                _testTypeInfo,
                ctrlScheduledTest.enCreatedMode.TakeTest,
                ctrlScheduledTest.enMode.Update
            );
        }

        private void _PrepareTest()
        {
            _test = new clsTest();
            _test.TestAppointmentId = _appointment.TestAppointmentId;
            _test.CreatedByUserId = clsGlobal.CurrentUser.UserId; 
        }

        private void _FillData()
        {
            _test = clsTest.FindTestByTestAppointment(_appointmentId.Value);

            if(_test ==  null)
            {
                MessageBox.Show(
                     "This form will be closed, because NO test appointment with Id = " + _appointmentId.Value,
                     "Error",
                     MessageBoxButtons.OK,
                     MessageBoxIcon.Error
                 );

                Close();
            }

            ctrlScheduledTest1.SetTestId = _test.TestId;

            if (_test.TestResult)
                rbPass.Checked = _test.TestResult;
            else
                rbFail.Checked = _test.TestResult;

            txtNotes.Text = _test.Notes?.ToString();
        }

        private void _AppointmentIsLock()
        {
            _FillData();
            lblLocked.Text = "Person already sat for the test, appointment locked.";
            ctrlScheduledTest1.EnableDate = false;
            btnSave.Enabled = false;
            gbResult.Enabled = false;
            txtNotes.Enabled = false;
            ctrlScheduledTest1.ShowRetakeTestInfo = true;
        }

        private void _PrepareTestAppointment()
        {
            _appointment = clsTestAppointment.Find(_appointmentId.Value);
            
            if (_appointment == null)
            {
                MessageBox.Show(
                     "This form will be closed, because NO test appointment with Id = " + _appointmentId.Value,
                     "Error",
                     MessageBoxButtons.OK,
                     MessageBoxIcon.Error
                 );
                Close();
            }
        }

        private void _PrepareForm()
        {
            _PrepareTestAppointment();
            _PrepareTest();
            _PrepareScheduleTest();
        }

        private void frmTakeTest_Load(object sender, EventArgs e)
        {
            _PrepareForm();

            if (_appointment.IsLocked)
            {
                _AppointmentIsLock();
            }
        }

        private void _SetDataToSave()
        {
            _test.TestResult = (rbPass.Checked);
            _test.Notes = string.IsNullOrEmpty(txtNotes.Text) ? null : txtNotes.Text;
        }

        private void _AddNewTest()
        {
            ctrlScheduledTest1.SetTestId = _test.TestId;

            MessageBox.Show("Data Saved Successfully",
                "Done",
                MessageBoxButtons.OK,
                MessageBoxIcon.Information
            );

        }

        private bool _PerformSave()
        {
            _SetDataToSave();

            bool isSaved = _test.Save();
            
            if (isSaved)
            {
                _AddNewTest();
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

            return isSaved;
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show(
                "Are you sure you want to save? " 
                + "After thent you can't change the Pass/Fail results after you save!",
                "Confirm",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Warning
            );

            if (result != DialogResult.Yes)
                return;

            bool isSaved = _PerformSave();

            if (isSaved)
            {
                OnTestSaved(_test.TestId);
                Close();
            }
        }
    }
}
