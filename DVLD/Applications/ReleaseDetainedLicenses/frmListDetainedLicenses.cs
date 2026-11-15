using DVLD.GlobalClasses;
using DVLD.Licenses;
using DVLD.Licenses.DetainLicense;
using DVLD.People.Controls;
using DVLD_Business.DetainedLicenses;
using DVLD_Business.LocalDrivingLicenseApplications;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DVLD.Applications.DetainAndReleaseLicenses
{
    public partial class frmListDetainedLicenses : Form
    {
        private enum enStatus { All = -1, Detained = 0, Released = 1 };

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

        DataTable _dtDetainLicense { get; set; } = null;
        DataView _viewDetainLicense { get; set; } = null;

        private FilterItem _selectedFilter = null;
        private FilterStatusItem _selectedStatus = null;

        public frmListDetainedLicenses()
        {
            InitializeComponent();
        }

        private void _LoadDetainedLicensesList()
        {
            _dtDetainLicense = clsDetainedLicense.GetAllDetainedLicenses();
            lblNumberOfRecords.Text = _dtDetainLicense.Rows.Count.ToString();
            _viewDetainLicense = _dtDetainLicense.DefaultView;
            dgvDetainedLicenseList.DataSource = _viewDetainLicense;
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

        private void _RefreshDetainedLicensesList()
        {
            _LoadDetainedLicensesList();
            _ApplyCurrentFilter();
        }

        private void _ConfigControls()
        {
            if (dgvDetainedLicenseList.Columns.Count == 0)
                return;

            dgvDetainedLicenseList.Columns["ReleaseDate"].DefaultCellStyle.NullValue = "N/A";
            dgvDetainedLicenseList.Columns["ReleaseApplicationID"].DefaultCellStyle.NullValue = "N/A";

            dgvDetainedLicenseList.Columns["PersonID"].Visible = false;

            dgvDetainedLicenseList.Columns[0].HeaderText = "D. ID";
            dgvDetainedLicenseList.Columns[0].Width = 100;

            dgvDetainedLicenseList.Columns[1].HeaderText = "L. ID";
            dgvDetainedLicenseList.Columns[1].Width = 100;

            dgvDetainedLicenseList.Columns[2].HeaderText = "Detained Date";
            dgvDetainedLicenseList.Columns[2].Width = 170;

            dgvDetainedLicenseList.Columns[3].HeaderText = "Is Released";
            dgvDetainedLicenseList.Columns[3].Width = 100;

            dgvDetainedLicenseList.Columns[4].HeaderText = "Fine Fees";
            dgvDetainedLicenseList.Columns[4].Width = 130;

            dgvDetainedLicenseList.Columns[5].HeaderText = "Released Date";
            dgvDetainedLicenseList.Columns[5].Width = 170;

            dgvDetainedLicenseList.Columns[6].HeaderText = "National No";
            dgvDetainedLicenseList.Columns[6].Width = 150;

            dgvDetainedLicenseList.Columns[7].HeaderText = "Full Name";
            dgvDetainedLicenseList.Columns[7].Width = 400;

            dgvDetainedLicenseList.Columns[8].HeaderText = "Release App. ID";
            dgvDetainedLicenseList.Columns[8].Width = 100;
            dgvDetainedLicenseList.Columns[8].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;

            dgvDetainedLicenseList.ColumnHeadersDefaultCellStyle.Alignment =
                DataGridViewContentAlignment.MiddleCenter;

            int[] centerColumns = { 0, 1, 2, 3, 5, 8 };

            foreach (int index in centerColumns)
            {
                dgvDetainedLicenseList.Columns[index].DefaultCellStyle.Alignment =
                    DataGridViewContentAlignment.MiddleCenter;
            }

        }

        private void _FillFilterBy()
        {
            cbFilterBy.Items.Add(new FilterItem("None", "None", enFilterType.None));
            cbFilterBy.Items.Add(new FilterItem("DetainID", "Detain ID", enFilterType.Number));
            cbFilterBy.Items.Add(new FilterItem("NationalNo", "National No", enFilterType.Text));
            cbFilterBy.Items.Add(new FilterItem("FullName", "Full Name", enFilterType.Text));
            cbFilterBy.Items.Add(new FilterItem("ReleaseApplicationID", "Release Application ID", enFilterType.Number));
            cbFilterBy.Items.Add(new FilterItem("IsReleased", "Is Released", enFilterType.Status));

            cbFilterBy.DisplayMember = "LabelName";
            cbFilterBy.ValueMember = "ColumnName";
            cbFilterBy.SelectedIndex = 0;
        }

        private void _FillFilterStatus()
        {
            cbApplicationStatus.Items.Add(new FilterStatusItem("All", enStatus.All));
            cbApplicationStatus.Items.Add(new FilterStatusItem("Detained", enStatus.Detained));
            cbApplicationStatus.Items.Add(new FilterStatusItem("Released", enStatus.Released));

            cbApplicationStatus.SelectedIndex = 0;
            cbApplicationStatus.Visible = false;
        }

        private void frmListDetained_Load(object sender, EventArgs e)
        {
            _LoadDetainedLicensesList();
            _FillFilterBy();
            _FillFilterStatus();
            _ConfigControls();
        }

        private void _ResetList()
        {
            _viewDetainLicense.RowFilter = null;
            lblNumberOfRecords.Text = _viewDetainLicense.Count.ToString();
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

            _viewDetainLicense.RowFilter = $"[IsReleased] = {(int)_selectedStatus.Status}";
            lblNumberOfRecords.Text = _viewDetainLicense.Count.ToString();
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

                _viewDetainLicense.RowFilter = $"[{_selectedFilter.ColumnName}] = {result}";
            }
            else if (_selectedFilter.FilterType == enFilterType.Text)
            {
                string searchValue = txtFilter.Text.Trim().Replace("'", "''");
                _viewDetainLicense.RowFilter = $"[{_selectedFilter.ColumnName}] LIKE '{searchValue}%'";
            }

            lblNumberOfRecords.Text = _viewDetainLicense.Count.ToString();
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

        private void cbApplicationStatus_SelectedIndexChanged(object sender, EventArgs e)
        {
            _selectedStatus = cbApplicationStatus.SelectedItem as FilterStatusItem;

            if (_selectedStatus == null)
                return;

            _ApplyStatusFilter();
        }

        private int? GetSelectedDetainId()
        {
            object cellValue = dgvDetainedLicenseList.CurrentRow?.Cells[0].Value;

            if (cellValue == null)
                return null;

            return Convert.ToInt32(cellValue);
        }

        private void Frm_OnPersonUpdated(int obj)
        {
            _RefreshDetainedLicensesList();
        }

        private void Frm_OnReleasedLicense(int obj)
        {
            _RefreshDetainedLicensesList();
        }

        private void Frm_OnDetainedLicense(int obj)
        {
            _RefreshDetainedLicensesList();
        }

        private void tsmShowPersonDetails_Click(object sender, EventArgs e)
        {
            object cellValue = dgvDetainedLicenseList.CurrentRow?.Cells["PersonID"].Value;

            if (cellValue == null)
                return;

            int? personId = Convert.ToInt32(cellValue);

            if(personId == null) 
                return;

            using (var frm = new frmShowPersonInfo(personId.Value))
            {
                frm.OnPersonUpdated += Frm_OnPersonUpdated;
                frm.ShowDialog();
            }
        }

        private void tsmShowLicense_Click(object sender, EventArgs e)
        {
            object cellValue = dgvDetainedLicenseList.CurrentRow?.Cells["LicenseID"].Value;

            if (cellValue == null)
                return;

            int? licenseId = Convert.ToInt32(cellValue);

            if(licenseId == null)
                return;

            using (var frm = new frmShowLicenseInfo(licenseId.Value))
            {
                frm.ShowDialog();
            }
        }

        private void tsmShowPersonLicenseHistory_Click(object sender, EventArgs e)
        {
            object cellValue = dgvDetainedLicenseList.CurrentRow?.Cells["PersonID"].Value;

            if (cellValue == null)
                return;

            int? personId = Convert.ToInt32(cellValue);

            if (personId == null)
                return;


            using (var frm = new frmShowPersonLicenseHistory(personId.Value))
            {
                frm.ShowDialog();
            }
        }

        private void cmServiceDetainLincense_Opening(object sender, CancelEventArgs e)
        {
            tsmReleaseDetainedLicense.Enabled = !(Boolean)dgvDetainedLicenseList.CurrentRow?.Cells["IsReleased"].Value;
        }

        private void btnReleaseLicense_Click(object sender, EventArgs e)
        {
            using (var frm = new frmReleaseDetainedLicenseApplication())
            {
                frm.OnReleasedLicense += Frm_OnReleasedLicense;
                frm.ShowDialog();
            }
        }

        private void btnDetainLicense_Click(object sender, EventArgs e)
        {
            using (var frm = new frmDetainLicense())
            {
                frm.OnDetainedLicense += Frm_OnDetainedLicense;
                frm.ShowDialog();
            }
        }

        private void tsmReleaseDetainedLicense_Click(object sender, EventArgs e)
        {
            object cellValue = dgvDetainedLicenseList.CurrentRow?.Cells["LicenseID"].Value;

            if (cellValue == null)
                return;

            int? licenseId = Convert.ToInt32(cellValue);

            if (licenseId == null)
                return;

            using (var frm = new frmReleaseDetainedLicenseApplication(licenseId.Value))
            {
                frm.OnReleasedLicense += Frm_OnReleasedLicense;
                frm.ShowDialog();
            }
        }

    }
}
