using DVLD.Applications.InternationalLicense;
using DVLD_Business;
using DVLD_Business.Drivers;
using DVLD_Business.InternationalLicenses;
using DVLD_Business.Licenses;
using DVLD_Business.LocalDrivingLicenseApplications;
using System;
using System.Data;
using System.Windows.Forms;

namespace DVLD.Licenses.Controls
{
    public partial class ctrlDriverLicenses : UserControl
    {
        private int _driverId = -1;
        DataTable _dtLocalLicenses = null;
        DataTable _dtInternationalLicenses = null;
        DataView _viewLocalLicenses = null;
        DataView _viewInternationalLicenses = null;

        public ctrlDriverLicenses()
        {
            InitializeComponent();
        }

        private void _LoadLocalLicensesList()
        {
            _dtLocalLicenses = clsLicense.GetAllLicenseDriver(_driverId);
            lblLocalNumberOfRecords.Text = _dtLocalLicenses.Rows.Count.ToString();
            _viewLocalLicenses = _dtLocalLicenses.DefaultView;
            dgvLocalLicenses.DataSource = _viewLocalLicenses;
        }

        private void _LoadInternationalLicensesList()
        {
            _dtInternationalLicenses = clsInternationalLicense.GetAllInternationalLicenseDriver(_driverId);
            lblInternationalNumberOfRecords.Text = _dtInternationalLicenses.Rows.Count.ToString();
            _viewInternationalLicenses = _dtInternationalLicenses.DefaultView;
            dgvInternationalList.DataSource = _viewInternationalLicenses;
        }

        private void _ConfigControlsForLocalList()
        {
            if (dgvLocalLicenses.Columns.Count == 0)
                return;

            dgvLocalLicenses.Columns[0].HeaderText = "Lic. ID";
            dgvLocalLicenses.Columns[0].Width = 100;

            dgvLocalLicenses.Columns[1].HeaderText = "App. ID";
            dgvLocalLicenses.Columns[1].Width = 100;

            dgvLocalLicenses.Columns[2].HeaderText = "Class Name";
            dgvLocalLicenses.Columns[2].Width = 350;

            dgvLocalLicenses.Columns[3].HeaderText = "Issue Date";
            dgvLocalLicenses.Columns[3].Width = 200;

            dgvLocalLicenses.Columns[4].HeaderText = "Expiration Date";
            dgvLocalLicenses.Columns[4].Width = 200;

            dgvLocalLicenses.Columns[5].HeaderText = "Is Active";
            dgvLocalLicenses.Columns[5].Width = 70;
            dgvLocalLicenses.Columns[5].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;

            dgvLocalLicenses.ColumnHeadersDefaultCellStyle.Alignment =
              DataGridViewContentAlignment.MiddleCenter;


            int[] centerColumns = { 0, 1, 3, 4, 5 };

            foreach (int index in centerColumns)
            {
                dgvLocalLicenses.Columns[index].DefaultCellStyle.Alignment =
                    DataGridViewContentAlignment.MiddleCenter;
            }
        }

        private void _ConfigControlsForInternationalList()
        {
            if (dgvInternationalList.Columns.Count == 0)
                return;

            dgvInternationalList.Columns[0].HeaderText = "Int. License ID";
            dgvInternationalList.Columns[0].Width = 150;

            dgvInternationalList.Columns[1].HeaderText = "Application ID";
            dgvInternationalList.Columns[1].Width = 150;

            dgvInternationalList.Columns[2].HeaderText = "Driver ID";
            dgvInternationalList.Columns[2].Width = 150;

            dgvInternationalList.Columns[3].HeaderText = "L. License ID";
            dgvInternationalList.Columns[3].Width = 150;

            dgvInternationalList.Columns[4].HeaderText = "Issue Date";
            dgvInternationalList.Columns[4].Width = 200;

            dgvInternationalList.Columns[5].HeaderText = "Expiration Date";
            dgvInternationalList.Columns[5].Width = 200;

            dgvInternationalList.Columns[6].HeaderText = "Is Active";
            dgvInternationalList.Columns[6].Width = 140;
            dgvInternationalList.Columns[6].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;

            dgvInternationalList.ColumnHeadersDefaultCellStyle.Alignment =
                DataGridViewContentAlignment.MiddleCenter;

            int[] centerColumns = { 0, 1, 2, 3, 4, 5 , 6};

            foreach (int index in centerColumns)
            {
                dgvInternationalList.Columns[index].DefaultCellStyle.Alignment =
                    DataGridViewContentAlignment.MiddleCenter;
            }
        }

        private void _LoadDriverLicenses()
        {
            _LoadLocalLicensesList();
            _LoadInternationalLicensesList();
            _ConfigControlsForLocalList();
            _ConfigControlsForInternationalList();
        }

        public void LoadInfoByDriverId(int driverId)
        {
            if(!clsDriver.IsDriverExist(driverId))
            {
                MessageBox.Show(
                    $"There is no driver with Id = {driverId}",
                    "No Allowed",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error
                );

                return;
            }

            _driverId = driverId;
            _LoadDriverLicenses();
        }

        public void LoadInfoByPersonId(int personId)
        {
            clsDriver driver = clsDriver.FindByPersonId(personId);

            if(driver == null)
            {
                MessageBox.Show(
                    $"There is no driver linked with person Id = {personId}",
                    "No Allowed",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error
                );

                return;
            }
             
            _driverId = driver.DriverId;
            _LoadDriverLicenses();
        }

        public void ClearLists()
        {
            _dtLocalLicenses?.Clear();
            _dtInternationalLicenses?.Clear();
            lblInternationalNumberOfRecords.Text = "0";
            lblLocalNumberOfRecords.Text = "0";
        }

        private int? GetSelectedLocalLicenseId()
        {
            object cellValue = dgvLocalLicenses.CurrentRow?.Cells[0].Value;

            if (cellValue == null)
                return null;

            return Convert.ToInt32(cellValue);
        }

        private int? GetSelectedInternationalLicenseId()
        {
            object cellValue = dgvInternationalList.CurrentRow?.Cells[0].Value;

            if (cellValue == null)
                return null;

            return Convert.ToInt32(cellValue);
        }

        private void _PerfromShowLocalLicense()
        {
            int? licenseId = GetSelectedLocalLicenseId();

            if (licenseId == null)
                return;

            using (var frm = new frmShowLicenseInfo(licenseId.Value))
            {
                frm.ShowDialog();
            }
        }

        private void _PerfromShowInternationalLicense()
        {
            int? licenseId = GetSelectedInternationalLicenseId();

            if (licenseId == null)
                return;

            using (var frm = new frmShowInternationalLicenseInfo(licenseId.Value))
            {
                frm.ShowDialog();
            }
        }

        private void tsmShowLicenseInfo_Click(object sender, System.EventArgs e)
        {
            Control sourceControl = cmServiceLocalLicenses.SourceControl;

            if (sourceControl == dgvLocalLicenses)
            {
                _PerfromShowLocalLicense();
            }
            else if (sourceControl == dgvInternationalList)
            {
                _PerfromShowInternationalLicense();
            }
        }
    }
}
