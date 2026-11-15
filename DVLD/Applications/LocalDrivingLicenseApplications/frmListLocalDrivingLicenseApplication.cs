using DVLD.AppointmentTests;
using DVLD.Licenses;
using DVLD_Business.Applications;
using DVLD_Business.LocalDrivingLicenseApplications;
using DVLD_Business.ManageTestTypes;
using System;
using System.ComponentModel;
using System.Data;
using System.Windows.Forms;
using DVLD.GlobalClasses;
using DVLD_Shared;
using DVLD_Business.Drivers;

namespace DVLD.LocalDrivingLicenseApplications
{
    public partial class frmListLocalDrivingLicenseApplication : Form
    {
        private enum enStatus { All = -1, New = 1, Cancelled = 2, Completed = 3 };

        private class FilterItem
        {
            public string ColumnName { get; }
            public string LabelName { get; set; }
            public enFilterType FilterType { get; }

            public FilterItem(string columnName, string labelName, enFilterType filterType)
            {
                ColumnName = columnName;
                LabelName = labelName;
                FilterType = filterType;
            }

            public override string ToString()
            {
                return ColumnName;
            }
        }

        private class FilterStatusItem
        {
            public string DisplayName { get; set; }
            public enStatus Status { get; }

            public FilterStatusItem(string displayName, enStatus status)
            {
                DisplayName = displayName;
                Status = status;
            }

            public override string ToString()
            {
                return DisplayName;
            }
        }

        DataTable _dtLocalLicenseApplication { get; set; } = null;
        DataView _viewLocalLicenseApplication { get; set; } = null;

        private FilterItem _selectedFilter = null;
        private FilterStatusItem _selectedStatus = null;

        public frmListLocalDrivingLicenseApplication()
        {
            InitializeComponent();
        }

        private void _LoadLocalLicenseApplicationList()
        {
            _dtLocalLicenseApplication = clsLocalDrivingLicenseApplication.GetAllLocalDrivingLicenseApplicationWithDetails();
            lblNumberOfRecords.Text = _dtLocalLicenseApplication.Rows.Count.ToString();
            _viewLocalLicenseApplication = _dtLocalLicenseApplication.DefaultView;
            dgvLocalLicenseApplicationList.DataSource = _viewLocalLicenseApplication;
        }

        private void _ApplyCurrentFilter()
        {
            if (_selectedFilter == null || _selectedFilter.FilterType == enFilterType.None)
                return;

            if (_selectedFilter.FilterType == enFilterType.Status)
                _ApplyStatusFilter();
            else
                _ApplyFilter();
        }

        private void _RefreshLocalLicenseApplicationList()
        {
            _LoadLocalLicenseApplicationList();
            _ApplyCurrentFilter();
        }

        private void _ConfigControls()
        {
            if (dgvLocalLicenseApplicationList.Columns.Count == 0)
                return;

            dgvLocalLicenseApplicationList.Columns["ApplicationStatus"].Visible = false;

            dgvLocalLicenseApplicationList.Columns[0].HeaderText = "L.D.L. AppID";
            dgvLocalLicenseApplicationList.Columns[0].Width = 120;

            dgvLocalLicenseApplicationList.Columns[1].HeaderText = "Driving Class";
            dgvLocalLicenseApplicationList.Columns[1].Width = 270;

            dgvLocalLicenseApplicationList.Columns[2].HeaderText = "National No";
            dgvLocalLicenseApplicationList.Columns[2].Width = 130;

            dgvLocalLicenseApplicationList.Columns[3].HeaderText = "Full Name";
            dgvLocalLicenseApplicationList.Columns[3].Width = 400;

            dgvLocalLicenseApplicationList.Columns[4].HeaderText = "Application Date";
            dgvLocalLicenseApplicationList.Columns[4].Width = 170;

            dgvLocalLicenseApplicationList.Columns[5].HeaderText = "Passed Tests";
            dgvLocalLicenseApplicationList.Columns[5].Width = 140;

            dgvLocalLicenseApplicationList.Columns[6].HeaderText = "Status";
            dgvLocalLicenseApplicationList.Columns[6].Width = 100;
            dgvLocalLicenseApplicationList.Columns[6].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;

            dgvLocalLicenseApplicationList.ColumnHeadersDefaultCellStyle.Alignment =
                DataGridViewContentAlignment.MiddleCenter;

            int[] centerColumns = { 0, 4, 5, 6 };

            foreach (int index in centerColumns)
            {
                dgvLocalLicenseApplicationList.Columns[index].DefaultCellStyle.Alignment =
                    DataGridViewContentAlignment.MiddleCenter;
            }

        }

        private void _FillFilterBy()
        {
            cbFilterBy.Items.Add(new FilterItem("None", "None", enFilterType.None));
            cbFilterBy.Items.Add(new FilterItem("LocalDrivingLicenseApplicationID", "L.D.L. AppID", enFilterType.Number));
            cbFilterBy.Items.Add(new FilterItem("NationalNo", "National No", enFilterType.Text));
            cbFilterBy.Items.Add(new FilterItem("FullName", "Full Name", enFilterType.Text));
            cbFilterBy.Items.Add(new FilterItem("ApplicationStatus", "Status", enFilterType.Status));

            cbFilterBy.DisplayMember = "LabelName";
            cbFilterBy.ValueMember = "ColumnName";
            cbFilterBy.SelectedIndex = 0;
        }

        private void _FillFilterStatus()
        {
            cbApplicationStatus.Items.Add(new FilterStatusItem("All", enStatus.All));
            cbApplicationStatus.Items.Add(new FilterStatusItem("New", enStatus.New));
            cbApplicationStatus.Items.Add(new FilterStatusItem("Cancelled", enStatus.Cancelled));
            cbApplicationStatus.Items.Add(new FilterStatusItem("Completed", enStatus.Completed));

            cbApplicationStatus.SelectedIndex = 0;
            cbApplicationStatus.Visible = false;
        }

        private void frmListLocalDrivingLicenseApplication_Load(object sender, EventArgs e)
        {
            _LoadLocalLicenseApplicationList();
            _FillFilterBy();
            _FillFilterStatus();
            _ConfigControls();
        }
        
        private void _ResetList()
        {
            _viewLocalLicenseApplication.RowFilter = null;
            lblNumberOfRecords.Text = _viewLocalLicenseApplication.Count.ToString();
            _selectedStatus = null;
        }

        private void _ShowStatusUser(bool show)
        {
            cbApplicationStatus.Visible = show;
            txtFilter.Visible = !show;
        }

        private void cbFilterBy_SelectedIndexChanged(object sender, EventArgs e)
        {
            _selectedFilter = cbFilterBy.SelectedItem as FilterItem;

            if (_selectedFilter == null)
                return;

            _ResetList();

            if (_selectedFilter.FilterType == enFilterType.None)
            {
                txtFilter.Visible = false;
                cbApplicationStatus.Visible = false;
                return;
            }

            if (_selectedFilter.FilterType == enFilterType.Status)
            {
                _ShowStatusUser(true);
                cbApplicationStatus.SelectedIndex = 0;
                return;
            }

            _ShowStatusUser(false);
            txtFilter.Clear();
            txtFilter.Select();
        }

        private void _ApplyStatusFilter()
        {
            if (_selectedStatus == null || _selectedStatus.Status == enStatus.All)
            {
                _ResetList();
                return;
            }

            _viewLocalLicenseApplication.RowFilter = $"[ApplicationStatus] = {(int)_selectedStatus.Status}";
            lblNumberOfRecords.Text = _viewLocalLicenseApplication.Count.ToString();
        }

        private void _ApplyFilter()
        {
            if (string.IsNullOrWhiteSpace(txtFilter.Text))
            {
                _ResetList();
                return;
            }

            if (_selectedFilter.FilterType == enFilterType.Number)
            {
                if (!int.TryParse(txtFilter.Text.Trim(), out int result))
                {
                    txtFilter.Clear();
                    return;
                }

                _viewLocalLicenseApplication.RowFilter = $"[{_selectedFilter.ColumnName}] = {result}";
            }
            else if (_selectedFilter.FilterType == enFilterType.Text)
            {
                string searchValue = txtFilter.Text.Trim().Replace("'", "''");
                _viewLocalLicenseApplication.RowFilter = $"[{_selectedFilter.ColumnName}] LIKE '{searchValue}%'";
            }

            lblNumberOfRecords.Text = _viewLocalLicenseApplication.Count.ToString();
        }

        private void txtFilter_TextChanged(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtFilter.Text))
            {
                _ResetList();
                return;
            }

            _ApplyFilter();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void txtFilter_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = (
                _selectedFilter.FilterType == enFilterType.Number &&
                !(Char.IsDigit(e.KeyChar) || Char.IsControl(e.KeyChar))
            );
        }

        private void cbApplicationStatus_SelectedIndexChanged(object sender, EventArgs e)
        {
            _selectedStatus = cbApplicationStatus.SelectedItem as FilterStatusItem;

            if (_selectedStatus == null)
                return;

            _ApplyStatusFilter();
        }

        private void _AfterLocalDrivingLicenseApplicationSaved(int value)
        {
            _RefreshLocalLicenseApplicationList();
        }

        private int? GetSelectedApplicationId()
        {
            object cellValue = dgvLocalLicenseApplicationList.CurrentRow?.Cells[0].Value;

            if (cellValue == null)
                return null;

            return Convert.ToInt32(cellValue);
        }

        private void _AddNewLocalLicenseApplication()
        {
            using (var frm = new frmAddEditLocalDrivingLicenseApplication())
            {
                frm.OnLocalDrivingLicenseApplicationSaved += _AfterLocalDrivingLicenseApplicationSaved;
                frm.ShowDialog();
            }
        }

        private void ShowMessageError(enDeleteResult deleteResult)
        {
            switch (deleteResult)
            {
                case enDeleteResult.HasRelatedData:

                    MessageBox.Show(
                        "Application was not deleted because it has data linked to it.",
                        "Delete Failed",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Error
                    );

                    break;

                case enDeleteResult.NotFound:

                    MessageBox.Show(
                        "Application was not found.",
                        "Delete Failed",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Error
                    );

                    break;

                case enDeleteResult.Error:

                    MessageBox.Show(
                        "An unexpected error occurred while deleting the application.",
                        "Error",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Error
                    );

                    break;
            }
        }

        private void _DeleteApplication()
        {
            int? applicationId = GetSelectedApplicationId();

            if (applicationId == null)
                return;

            DialogResult confirmDelete = MessageBox.Show(
                $"Are you sure you want to delete application [{applicationId.Value}]",
                "Confirm Delete",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Warning,
                MessageBoxDefaultButton.Button2
            );

            if (confirmDelete == DialogResult.No)
                return;

            enDeleteResult deleteResult;
            deleteResult = clsLocalDrivingLicenseApplication.Delete(applicationId.Value);

            if (deleteResult != enDeleteResult.Success)
            {
                ShowMessageError(deleteResult);
                return;
            }

            MessageBox.Show(
               "Application Deleted Successfully.",
               "Successful",
               MessageBoxButtons.OK,
               MessageBoxIcon.Information
           );

            _RefreshLocalLicenseApplicationList();
        }

        private void _CancelApplication()
        {
            int? applicationId = GetSelectedApplicationId();

            if (applicationId == null)
                return;


            DialogResult confirmDelete = MessageBox.Show(
                $"Are you sure you want to cancel this application",
                "Confirm Cancel",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Warning,
                MessageBoxDefaultButton.Button2
            );

            if (confirmDelete == DialogResult.No)
                return;

            bool isCancelled = clsLocalDrivingLicenseApplication.CancelApplication(applicationId.Value);

            if(!isCancelled)
            {
                MessageBox.Show(
                    "An unexpected error occurred while cancelling the application.",
                    "Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error
                );

                return;
            }

            MessageBox.Show(
               "Application Cancelled Successfully.",
               "Successful",
               MessageBoxButtons.OK,
               MessageBoxIcon.Information
            );

            _RefreshLocalLicenseApplicationList();
        }

        private void _EditApplication()
        {
            int? applicationId = GetSelectedApplicationId();

            if (applicationId == null)
                return;

            using (var frm = new frmAddEditLocalDrivingLicenseApplication(applicationId.Value))
            {
                frm.OnLocalDrivingLicenseApplicationSaved += _AfterLocalDrivingLicenseApplicationSaved;
                frm.ShowDialog();
            }
        }

        private void _ScheduleVisionTest()
        {
            int? applicationId = GetSelectedApplicationId();

            if (applicationId == null)
                return;

            using (var frm = new frmTestAppointment(applicationId.Value, enTestType.VisionTest))
            {
                frm.OnTestSaved += _AfterLocalDrivingLicenseApplicationSaved;
                frm.ShowDialog();
            }
        }

        private void _ScheduleWrittenTest()
        {
            int? applicationId = GetSelectedApplicationId();

            if (applicationId == null)
                return;

            using (var frm = new frmTestAppointment(applicationId.Value, enTestType.WrittenTest))
            {
                frm.OnTestSaved += _AfterLocalDrivingLicenseApplicationSaved;
                frm.ShowDialog();
            }
        }

        private void _ScheduleStreetTest()
        {
            int? applicationId = GetSelectedApplicationId();

            if (applicationId == null)
                return;

            using (var frm = new frmTestAppointment(applicationId.Value, enTestType.StreetTest))
            {
                frm.OnTestSaved += _AfterLocalDrivingLicenseApplicationSaved;
                frm.ShowDialog();
            }
        }

        private void _HandleScheduleTests(int passedTestCount)
        {
            tsmScheduleTests.Enabled = (passedTestCount < 3);

            tsmScheduleVisionTest.Enabled = (passedTestCount == 0);
            tsmScheduleWrittenTest.Enabled = (passedTestCount == 1);
            tsmScheduleStreetTest.Enabled = (passedTestCount == 2);
        }

        private void _HandleCancelledStatus()
        {
            tsmEditApplication.Enabled = false;
            tsmDeleteApplication.Enabled = false;
            tsmCancelApplication.Enabled = false;
            tsmScheduleTests.Enabled = false;
            tsmIssueDrivingLicense.Enabled = false;
            tsmShowLicense.Enabled = false;
        }

        private void _HandleCompletedStatus()
        {
            tsmEditApplication.Enabled = false;
            tsmDeleteApplication.Enabled = false;
            tsmCancelApplication.Enabled = false;
            tsmScheduleTests.Enabled = false;
            tsmIssueDrivingLicense.Enabled = false;
        }

        private void cmServiceLocalLincese_Opening(object sender, CancelEventArgs e)
        {
            int? localApplicationId = GetSelectedApplicationId();

            if (localApplicationId == null)
            {
                e.Cancel = true;
                return;
            }

            clsLocalDrivingLicenseApplication localApp = clsLocalDrivingLicenseApplication.FindLocalDrivingLicenseApplication(
                localApplicationId.Value
            );

            if (localApp == null)
            {
                e.Cancel = true;
                return;
            }

            int passedTestCount = Convert.ToInt32(dgvLocalLicenseApplicationList.CurrentRow.Cells["PassedTestCount"].Value);
            bool isLicenseIssued = localApp.IsLicenseIssued();
 
            tsmShowApplicationDetails.Enabled = true;
            tsmShowPersonLicenseHistory.Enabled = true;

            if (localApp.ApplicationStatus == enApplicationStatus.Cancelled)
            {
                _HandleCancelledStatus();
                return; 
            }

            if (localApp.ApplicationStatus == enApplicationStatus.Completed)
            {
                _HandleCompletedStatus();
                tsmShowLicense.Enabled = isLicenseIssued;
                return; 
            }

            bool hasAppointment = localApp.HasAnyAppointment();

            tsmEditApplication.Enabled = !hasAppointment;
            tsmDeleteApplication.Enabled = !hasAppointment;
            tsmCancelApplication.Enabled = true;

            _HandleScheduleTests(passedTestCount);

            tsmIssueDrivingLicense.Enabled = (passedTestCount == 3 && !isLicenseIssued);
            tsmShowLicense.Enabled = isLicenseIssued;
        }

        private void btnAddNewLocalLicenseApplication_Click(object sender, EventArgs e)
        {
            _AddNewLocalLicenseApplication();
        }

        private void tsmEditApplication_Click(object sender, EventArgs e)
        {
            _EditApplication();
        }

        private void tsmDeleteApplication_Click(object sender, EventArgs e)
        {
            _DeleteApplication();
        }

        private void tsmCancelApplication_Click(object sender, EventArgs e)
        {
            _CancelApplication();
        }

        private void tsmScheduleVisionTest_Click(object sender, EventArgs e)
        {
            _ScheduleVisionTest();
        }

        private void tsmScheduleWrittenTest_Click(object sender, EventArgs e)
        {
            _ScheduleWrittenTest();
        }

        private void tsmScheduleStreetTest_Click(object sender, EventArgs e)
        {
            _ScheduleStreetTest();
        }

        private void tsmIssueDrivingLicense_Click(object sender, EventArgs e)
        {
            int? applicationId = GetSelectedApplicationId();

            if (applicationId == null)
                return;

            using (var frm = new frmIssueDriverLicenseForTheFirstTime(applicationId.Value))
            {
                frm.OnLicenseIssued += _AfterIssuedLocalDrivingLicense;
                frm.ShowDialog();
            }
        }

        private void _AfterIssuedLocalDrivingLicense(int value)
        {
            _RefreshLocalLicenseApplicationList();
        }

        private void tsmShowLicense_Click(object sender, EventArgs e)
        {
            int? applicationId = GetSelectedApplicationId();

            if (applicationId == null)
                return;

            int? licenseId = clsLocalDrivingLicenseApplication.FindLocalDrivingLicenseApplication(
                applicationId.Value   
            ).GetActiveLicenseID();

            if (licenseId == null)
            {
                MessageBox.Show(
                    "Not found active licese for this application",
                    "Not Allowed",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error
                );

                return;
            }

            using (var frm = new frmShowLicenseInfo(licenseId.Value))
            {
                frm.ShowDialog();
            }
        }

        private void tsmShowPersonLicenseHistory_Click(object sender, EventArgs e)
        {
            int? applicationId = GetSelectedApplicationId();

            if (applicationId == null)
                return;

            clsLocalDrivingLicenseApplication application = clsLocalDrivingLicenseApplication.FindLocalDrivingLicenseApplication(
                applicationId.Value
            );

            if (application == null)
            {
                MessageBox.Show(   
                    "Error, can't find the local driving license application.",
                    "Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error
                );

                return;
            }

            if(!clsDriver.IsDriverExistForPersonId(application.PersonId))
            {
                MessageBox.Show(
                    "This person is not registered as a driver.",
                    "Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error
                );

                return;
            }

            using (var frm = new frmShowPersonLicenseHistory(application.PersonId))
            {
                frm.ShowDialog();
            }
        }

        private void tsmShowApplicationDetails_Click(object sender, EventArgs e)
        {
            int? applicationId = GetSelectedApplicationId();

            if (applicationId == null)
                return;



            using (var frm = new frmLocalDrivingLicenseApplicationInfo(applicationId.Value, true))
            {
                frm.ShowDialog();
            }
        }
    }
}
