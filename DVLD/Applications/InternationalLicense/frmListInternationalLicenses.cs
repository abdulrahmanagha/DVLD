using DVLD.GlobalClasses;
using DVLD.Licenses;
using DVLD.People.Controls;
using DVLD_Business.Drivers;
using DVLD_Business.InternationalLicenses;
using DVLD_Business.Licenses;
using DVLD_Shared;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DVLD.Applications.InternationalLicense
{
    public partial class frmListInternationalLicenses : Form
    {
        public frmListInternationalLicenses()
        {
            InitializeComponent();
        }

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

        DataTable _dtInternationalLicenseApplication { get; set; } = null;
        DataView _viewInternationalLicenseApplication { get; set; } = null;

        private FilterItem _selectedFilter = null;
        private FilterStatusItem _selectedStatus = null;

        private void _LoadInternationalLicenseApplicationList()
        {
            _dtInternationalLicenseApplication = clsInternationalLicense.GetInternationalLicenses();
            lblNumberOfRecords.Text = _dtInternationalLicenseApplication.Rows.Count.ToString();
            _viewInternationalLicenseApplication = _dtInternationalLicenseApplication.DefaultView;
            dgvInternationalLicenseApplicationList.DataSource = _viewInternationalLicenseApplication;
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

        private void _RefreshInternationalLicenseApplicationList()
        {
            _LoadInternationalLicenseApplicationList();
            _ApplyCurrentFilter();
        }

        private void _ConfigControls()
        {
            if (dgvInternationalLicenseApplicationList.Columns.Count == 0)
                return;

            dgvInternationalLicenseApplicationList.Columns[0].HeaderText = "Int. License ID";
            dgvInternationalLicenseApplicationList.Columns[0].Width = 130;

            dgvInternationalLicenseApplicationList.Columns[1].HeaderText = "Application ID";
            dgvInternationalLicenseApplicationList.Columns[1].Width = 140;

            dgvInternationalLicenseApplicationList.Columns[2].HeaderText = "Driver ID";
            dgvInternationalLicenseApplicationList.Columns[2].Width = 140;

            dgvInternationalLicenseApplicationList.Columns[3].HeaderText = "L. License ID";
            dgvInternationalLicenseApplicationList.Columns[3].Width = 140;

            dgvInternationalLicenseApplicationList.Columns[4].HeaderText = "Issue Date";
            dgvInternationalLicenseApplicationList.Columns[4].Width = 200;

            dgvInternationalLicenseApplicationList.Columns[5].HeaderText = "Expiration Date";
            dgvInternationalLicenseApplicationList.Columns[5].Width = 200;

            dgvInternationalLicenseApplicationList.Columns[6].HeaderText = "Is Active";
            dgvInternationalLicenseApplicationList.Columns[6].Width = 140;
            dgvInternationalLicenseApplicationList.Columns[6].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;

            dgvInternationalLicenseApplicationList.ColumnHeadersDefaultCellStyle.Alignment =
                DataGridViewContentAlignment.MiddleCenter;

            int[] centerColumns = { 0, 1, 2, 3, 4, 5 };

            foreach (int index in centerColumns)
            {
                dgvInternationalLicenseApplicationList.Columns[index].DefaultCellStyle.Alignment =
                    DataGridViewContentAlignment.MiddleCenter;
            }

        }

        private void frmListInternationalLicenses_Load(object sender, EventArgs e)
        {
            _LoadInternationalLicenseApplicationList();
            _FillFilterBy();
            _FillFilterStatus();
            _ConfigControls();
        }

        private void _FillFilterBy()
        {
            cbFilterBy.Items.Add(new FilterItem("None", "None", enFilterType.None));
            cbFilterBy.Items.Add(new FilterItem("InternationalLicenseID", "Int. License ID", enFilterType.Number));
            cbFilterBy.Items.Add(new FilterItem("ApplicationID", "Application ID", enFilterType.Number));
            cbFilterBy.Items.Add(new FilterItem("IsActive", "Is Active", enFilterType.Status));

            cbFilterBy.DisplayMember = "LabelName";
            cbFilterBy.ValueMember = "ColumnName";
            cbFilterBy.SelectedIndex = 0;
        }

        private void _FillFilterStatus()
        {
            cbApplicationStatus.Items.Add(new FilterStatusItem("All", enStatus.All));
            cbApplicationStatus.Items.Add(new FilterStatusItem("Active", enStatus.Active));
            cbApplicationStatus.Items.Add(new FilterStatusItem("InActive", enStatus.Inactive));

            cbApplicationStatus.SelectedIndex = 0;
            cbApplicationStatus.Visible = false;
        }

        private void frmListLocalDrivingLicenseApplication_Load(object sender, EventArgs e)
        {
            _LoadInternationalLicenseApplicationList();
            _FillFilterBy();
            _FillFilterStatus();
            _ConfigControls();
        }

        private void _ResetList()
        {
            _viewInternationalLicenseApplication.RowFilter = null;
            lblNumberOfRecords.Text = _viewInternationalLicenseApplication.Count.ToString();
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

            _viewInternationalLicenseApplication.RowFilter = $"[IsActive] = {(int)_selectedStatus.Status}";
            lblNumberOfRecords.Text = _viewInternationalLicenseApplication.Count.ToString();
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

                _viewInternationalLicenseApplication.RowFilter = $"[{_selectedFilter.ColumnName}] = {result}";
            }
            else if (_selectedFilter.FilterType == enFilterType.Text)
            {
                string searchValue = txtFilter.Text.Trim().Replace("'", "''");
                _viewInternationalLicenseApplication.RowFilter = $"[{_selectedFilter.ColumnName}] LIKE '{searchValue}%'";
            }

            lblNumberOfRecords.Text = _viewInternationalLicenseApplication.Count.ToString();
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

        private int? GetSelectedApplicationId()
        {
            object cellValue = dgvInternationalLicenseApplicationList.CurrentRow?.Cells[0].Value;

            if (cellValue == null)
                return null;

            return Convert.ToInt32(cellValue);
        }

        private void btnAddNewLocalLicenseApplication_Click(object sender, EventArgs e)
        {
            using (var frm = new frmNewInternationalLicenseApplication())
            {
                frm.OnInternationalLicenseApplicationSaved += Frm_OnInternationalLicenseApplicationSaved;
                frm.ShowDialog();
            }
        }

        private void Frm_OnInternationalLicenseApplicationSaved(int obj)
        {
            _RefreshInternationalLicenseApplicationList();
        }

        private void tsmShowPersonDetails_Click(object sender, EventArgs e)
        {
            object result = dgvInternationalLicenseApplicationList.CurrentRow?.Cells["DriverId"].Value;

            if(result == null) 
                return;

            int? driverId = (int)result;

            clsDriver driver = clsDriver.Find(driverId.Value);

            using (var frm = new frmShowPersonInfo(driver.PersonId))
            {
                frm.ShowDialog();
            }
        }

        private void tsmShowLicense_Click(object sender, EventArgs e)
        {
            int? applicationId = GetSelectedApplicationId();

            if (applicationId == null)
                return;

            using (var frm = new frmShowInternationalLicenseInfo(applicationId.Value))
            {
                frm.ShowDialog();
            }
        }

        private void tsmShowPersonLicenseHistory_Click(object sender, EventArgs e)
        {
            object result = dgvInternationalLicenseApplicationList.CurrentRow?.Cells["DriverId"].Value;

            if (result == null)
                return;

            int? driverId = (int)result;

            clsDriver driver = clsDriver.Find(driverId.Value);

            using (var frm = new frmShowPersonLicenseHistory(driver.PersonId))
            {
                frm.ShowDialog();
            }
        }
    }
}
