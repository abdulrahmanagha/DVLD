using DVLD.Tests;
using DVLD_Business.LocalDrivingLicenseApplications;
using DVLD_Business.ManageTestTypes;
using DVLD_Business.TestAppointments;
using DVLD_Business.Users;
using System;
using System.Data;
using System.Windows.Forms;
using DVLD_Shared;
using DVLD_DataAccess.Tests;

namespace DVLD.AppointmentTests
{
    public partial class frmTestAppointment : Form
    {
        public event Action<int> OnTestSaved;

        protected virtual void TestSaved(int testId)
        {
            OnTestSaved?.Invoke(testId);
        }

        private clsLocalDrivingLicenseApplication _localApplication = null;
        private clsTestType _testTypeInfo = null;
        private enTestType _testTypeId = enTestType.VisionTest;
        private int? _localApplicationId = null;
        DataTable _dtAppointments { get; set; } = null;
        DataView _viewAppointments { get; set; } = null;

        public frmTestAppointment(int? localApplicationId, enTestType testType)
        {
            InitializeComponent();
            _localApplicationId = localApplicationId;
            _testTypeId = testType;

            ctrlDrivingLicenseApplicationInfo1.ShowLicenseInfo = false;
        }

        private void _LoadAppointmentList()
        {
            _dtAppointments = clsTestAppointment.GetAllAppointmentTestForLocalApplication(
                _localApplicationId.Value,
                _testTypeId
            );

            lblNumberOfRecords.Text = _dtAppointments.Rows.Count.ToString();
            _viewAppointments = _dtAppointments.DefaultView;
            dgvAppointmentsList.DataSource = _viewAppointments;
        }

        private void _RefreshAppointmentList()
        {
            _LoadAppointmentList();
        }

        private void _ConfigControls()
        {
            if (dgvAppointmentsList.Columns.Count == 0)
                return;

            dgvAppointmentsList.Columns[0].HeaderText = "Appointment ID";
            dgvAppointmentsList.Columns[0].Width = 150;

            dgvAppointmentsList.Columns[1].HeaderText = "Appointment Date";
            dgvAppointmentsList.Columns[1].Width = 400;

            dgvAppointmentsList.Columns[2].HeaderText = "Paid Fees";
            dgvAppointmentsList.Columns[2].Width = 170;

            dgvAppointmentsList.Columns[3].HeaderText = "Is Locked";
            dgvAppointmentsList.Columns[3].Width = 200;

            dgvAppointmentsList.ColumnHeadersDefaultCellStyle.Alignment =
                DataGridViewContentAlignment.MiddleCenter;
            dgvAppointmentsList.Columns[3].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;

            int[] centerColumns = { 0, 1, 2, 3 };

            foreach (int index in centerColumns)
            {
                dgvAppointmentsList.Columns[index].DefaultCellStyle.Alignment =
                    DataGridViewContentAlignment.MiddleCenter;
            }
        }

        public void _PerpareTestType()
        {
            _testTypeInfo = clsTestType.Find(_testTypeId);

            if (_testTypeInfo == null)
            {
                return;
            }

            string typeName = _testTypeInfo.GetTestTypeName();
            string title = $"{typeName} Test Appointment";
            ctrlTestTitle1.LoadTypeWithTitle(title, _testTypeId);
            this.Text = title;
        }

        private bool _CheckHasActiveAppointment()
        {
            if (clsLocalDrivingLicenseApplication.IsThereAnActiveScheduledTest(
                _localApplicationId.Value,
                _testTypeId
            ))
            {
                MessageBox.Show(
                   "Person Already have an active appointment for test, "
                   + "You can't add new appointment.",
                   "Not Allowed",
                   MessageBoxButtons.OK,
                   MessageBoxIcon.Error
               );

                return true;
            }

            return false;
        }

        private bool _CheckPrviousTestConstraint()
        {
            switch(_testTypeId)
            {
                case enTestType.VisionTest:
                    return false;

                case enTestType.WrittenTest:
                    {
                        if(!clsLocalDrivingLicenseApplication.DoesPassTestType(_localApplicationId.Value, enTestType.VisionTest))
                        {
                            MessageBox.Show(
                                "Cannot Sechdule, Vision Test should be passed first",
                                "Not Allowed",
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Error
                            );

                            return true;
                        }

                        break;
                    }

                case enTestType.StreetTest:
                    {
                        if (!clsLocalDrivingLicenseApplication.DoesPassTestType(_localApplicationId.Value, enTestType.WrittenTest))
                        {
                            MessageBox.Show(
                                "Cannot Sechdule, Written Test should be passed first",
                                "Not Allowed",
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Error
                            );
                            return true;
                        }

                        break;
                    }
            }

            return false;
        }

        private bool _CheckPassedTestType()
        {
            if (_localApplication.DoesPassTestType(_testTypeId))
            {
                MessageBox.Show(
                      "Person already passed in his test before.",
                      "Not Allowed",
                      MessageBoxButtons.OK,
                      MessageBoxIcon.Error
                  );

                return true;
            }

            return false;
        }

        private bool _CheckViolateAddApointmentConstraints()
        {
            if (_CheckPrviousTestConstraint())
                return true;

            if (_CheckPassedTestType())
                return true;

            if (_CheckHasActiveAppointment()) 
                return true;

            return false;
        }

        private void _AddNewAppointment()
        {
            if (_CheckViolateAddApointmentConstraints())
            {
                return;
            }

            using (var frm = new frmAddEditAppointment(_localApplicationId, _testTypeId))
            {
                frm.OnApointmentSaved += AfterSaved;
                frm.ShowDialog();
            }
        }

        private void frmVisionTestAppointment_Load(object sender, EventArgs e)
        {
            _PerpareTestType();
            ctrlDrivingLicenseApplicationInfo1.LoadApplication(_localApplicationId.Value);
            _localApplication = ctrlDrivingLicenseApplicationInfo1.GetLocalApplication;
            _LoadAppointmentList();
            _ConfigControls();
        }

        private void btnAddNewAppointment_Click(object sender, EventArgs e)
        {
            _AddNewAppointment();
        }

        private void AfterSaved(int value)
        {
            _RefreshAppointmentList();
        }

        private int? GetSelectedAppointmentId()
        {
            object cellValue = dgvAppointmentsList.CurrentRow?.Cells[0].Value;

            if (cellValue == null)
                return null;

            return Convert.ToInt32(cellValue);
        }

        private void _EditAppointment()
        {
            int? appointmentId = GetSelectedAppointmentId();

            if (appointmentId == null)
                return;

            using (var frm = new frmAddEditAppointment(appointmentId))
            {
                frm.OnApointmentSaved += AfterSaved;
                frm.ShowDialog();
            }
        }

        private void tsmEditAppointment_Click(object sender, EventArgs e)
        {
            _EditAppointment();
        }

        private void tsmTakeTest_Click(object sender, EventArgs e)
        {
            int? appointmentId = GetSelectedAppointmentId();

            if (appointmentId == null)
                return;

            using (var frm = new frmTakeTest(appointmentId.Value, _localApplication, _testTypeInfo))
            {
                frm.OnTestSaved += AfterTakeTestResult;
                frm.ShowDialog();
            }

        }

        private void _RefreshTestResultInfo()
        {
            int passedTests = clsTest.GetPassedTestsCount(
                _localApplicationId.Value
            );

            ctrlDrivingLicenseApplicationInfo1.SetPassedTests = passedTests;
        }

        private void AfterTakeTestResult(int value)
        {
            _RefreshAppointmentList();
            _RefreshTestResultInfo();
            OnTestSaved(value);
        }
    }
}
