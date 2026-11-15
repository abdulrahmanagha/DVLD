using DVLD.GlobalClasses;
using DVLD.Licenses;
using DVLD.People.Controls;
using DVLD_Business.Drivers;
using DVLD_Business.Licenses;
using DVLD_Business.LocalDrivingLicenseApplications;
using DVLD_Business.Users;
using System;
using System.Data;
using System.Linq;
using System.Windows.Forms;
using DVLD_Shared;
using DVLD.Applications.InternationalLicense;

namespace DVLD.Drivers
{
    public partial class frmListDrivers : Form
    {
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


        DataTable _dtDrivers { get; set; } = null;
        DataView _viewDrivers { get; set; } = null;

        private FilterItem _selectedFilter = null;
        private FilterStatusItem _selectedStatus = null;


        public frmListDrivers()
        {
            InitializeComponent();
        }

        private void _LoadDriversList()
        {
            _dtDrivers = clsDriver.GetAllDriversWithDetails();
            lblNumberOfRecords.Text = _dtDrivers.Rows.Count.ToString();
            _viewDrivers = _dtDrivers.DefaultView;
            dgvDriversList.DataSource = _viewDrivers;
        }

        private void _ApplyCurrentFilter()
        {
            if (_selectedFilter == null || _selectedFilter.FilterType == enFilterType.None)
                return;


            _ApplyFilter();
        }

        private void _RefreshDriversList(int value)
        {
            _LoadDriversList();
            _ApplyCurrentFilter();
        }

        private void _ConfigControls()
        {
            if (dgvDriversList.Columns.Count == 0)
                return;

            if (dgvDriversList.Columns["HasInternationalLicense"] != null)
            {
                dgvDriversList.Columns["HasInternationalLicense"].Visible = false;
            }

            dgvDriversList.Columns[0].HeaderText = "Driver ID";
            dgvDriversList.Columns[0].Width = 120;

            dgvDriversList.Columns[1].HeaderText = "Person ID";
            dgvDriversList.Columns[1].Width = 120;

            dgvDriversList.Columns[2].HeaderText = "Full Name";
            dgvDriversList.Columns[2].Width = 500;

            dgvDriversList.Columns[3].HeaderText = "Created Date";
            dgvDriversList.Columns[3].Width = 200;

            dgvDriversList.Columns[4].HeaderText = "Active Licenses";
            dgvDriversList.Columns[4].Width = 100;
            dgvDriversList.Columns[4].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;

            dgvDriversList.ColumnHeadersDefaultCellStyle.Alignment =
                DataGridViewContentAlignment.MiddleCenter;

            int[] centerColumns = { 0, 1, 3, 4 };

            foreach (int index in centerColumns)
            {
                dgvDriversList.Columns[index].DefaultCellStyle.Alignment =
                    DataGridViewContentAlignment.MiddleCenter;
            }
        }

        private void _FillFilterBy()
        {
            cbFilterBy.Items.Add(new FilterItem("None", "None", enFilterType.None));
            cbFilterBy.Items.Add(new FilterItem("DriverId", "Driver ID", enFilterType.Number));
            cbFilterBy.Items.Add(new FilterItem("PersonID", "Person ID", enFilterType.Number));
            cbFilterBy.Items.Add(new FilterItem("FullName", "Full Name", enFilterType.Text));
            cbFilterBy.Items.Add(new FilterItem("ActiveLicense", "Active Licenses", enFilterType.Number));

            cbFilterBy.DisplayMember = "LabelName";
            cbFilterBy.ValueMember = "ColumnName";
            cbFilterBy.SelectedIndex = 0;
        }

        private void frmListDrivers_Load(object sender, EventArgs e)
        {
            _LoadDriversList();
            _FillFilterBy();
            _ConfigControls();
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

                _viewDrivers.RowFilter = $"[{_selectedFilter.ColumnName}] = {result}";
            }
            else if (_selectedFilter.FilterType == enFilterType.Text)
            {
                string searchValue = txtFilter.Text.Trim().Replace("'", "''");
                _viewDrivers.RowFilter = $"[{_selectedFilter.ColumnName}] LIKE '{searchValue}%'";
            }

            lblNumberOfRecords.Text = _viewDrivers.Count.ToString();
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

        private void txtFilter_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = (
                _selectedFilter.FilterType == enFilterType.Number &&
                !(Char.IsDigit(e.KeyChar) || Char.IsControl(e.KeyChar))
            );
        }

        private void _ResetList()
        {
            _viewDrivers.RowFilter = null;
            lblNumberOfRecords.Text = _viewDrivers.Count.ToString();
        }

        private void btnClose_Click_1(object sender, EventArgs e)
        {
            Close();
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
                return;
            }

            txtFilter.Visible = true;
            txtFilter.Clear();
            txtFilter.Select();
        }

        private void tsmShowPersonInfo_Click(object sender, EventArgs e)
        {
            int? personId = GetSelectedPersonId();

            if (personId == null)
                return;

            using (var frm = new frmShowPersonInfo(personId.Value))
            {
                frm.OnPersonUpdated += _RefreshDriversList;
                frm.ShowDialog();
            }
        }

        private void tsmIssueInternationalLicense_Click(object sender, EventArgs e)
        {
            int? personId = GetSelectedPersonId();

            if (personId == null)
                return;

            int licenseId = clsLicense.GetActiveLicenseIDByPersonID(personId.Value, 3);

            using (var frm = new frmNewInternationalLicenseApplication(licenseId))
            {
                frm.OnInternationalLicenseApplicationSaved += _RefreshDriversList;
                frm.ShowDialog();
            }
        }

        private int? GetSelectedPersonId()
        {
            object cellValue = dgvDriversList.CurrentRow?.Cells["PersonID"].Value;

            if (cellValue == null)
                return null;

            return Convert.ToInt32(cellValue);
        }

        private void showToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int? personId = GetSelectedPersonId();

            if (personId == null)
                return;

            using (var frm = new frmShowPersonLicenseHistory(personId.Value))
            {
                frm.OnPersonUpdated += _RefreshDriversList;
                frm.ShowDialog();
            }
        }

        private void cmServiceDrivers_Opening(object sender, System.ComponentModel.CancelEventArgs e)
        {
            tsmIssueInternationalLicense.Enabled = !Convert.ToBoolean(dgvDriversList.CurrentRow.Cells["HasInternationalLicense"].Value);
        }
    }
}
