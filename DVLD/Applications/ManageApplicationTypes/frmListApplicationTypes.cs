using DVLD_Business.Manage_Types;
using DVLD_Business.Users;
using System;
using System.Data;
using System.Windows.Forms;
using DVLD_Shared;

namespace DVLD.Manage_Types
{
    public partial class frmListApplicationTypes : Form
    {
        DataTable _dtApplicationTypes { get; set; } = null;

        public frmListApplicationTypes()
        {
            InitializeComponent();
        }

        private void _RefreshApplicationTypesList()
        {
            _dtApplicationTypes = clsApplicationType.GetAllApplicationTypes();
            dgvApplicationTypesList.DataSource = _dtApplicationTypes.DefaultView;
            lblNumberOfRecords.Text = _dtApplicationTypes.Rows.Count.ToString();
        }

        private void _ConfigControls()
        {
            if (dgvApplicationTypesList.Columns.Count == 0)
                return;

            dgvApplicationTypesList.Columns[0].HeaderText = "ID";
            dgvApplicationTypesList.Columns[0].Width = 120;

            dgvApplicationTypesList.Columns[1].HeaderText = "Title";
            dgvApplicationTypesList.Columns[1].Width = 500;

            dgvApplicationTypesList.Columns[2].HeaderText = "Fees";
            dgvApplicationTypesList.Columns[2].Width = 100;
            dgvApplicationTypesList.Columns[2].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;

            dgvApplicationTypesList.ColumnHeadersDefaultCellStyle.Alignment =
                DataGridViewContentAlignment.MiddleCenter;

            int[] centerColumns = { 0, 2 };

            foreach (int index in centerColumns)
            {
                dgvApplicationTypesList.Columns[index].DefaultCellStyle.Alignment =
                    DataGridViewContentAlignment.MiddleCenter;
            }
        }

        private void frmListApplicationTypes_Load(object sender, EventArgs e)
        {
            _RefreshApplicationTypesList();
            _ConfigControls();
        }

        private int? GetSelectedApplicationTypeId()
        {
            object cellValue = dgvApplicationTypesList.CurrentRow?.Cells[0].Value;

            if (cellValue == null)
                return null;

            return Convert.ToInt32(cellValue);
        }

        private void tsmEditApplicationType_Click(object sender, EventArgs e)
        {
            int? applicationTypeId = GetSelectedApplicationTypeId();

            if (applicationTypeId == null)
                return;

            using (frmUpdateApplicationTypes frm = new frmUpdateApplicationTypes((enApplicationType)applicationTypeId.Value))
            {
                frm.OnApplicationTypeUpdated += ApplicationTypeUpdated;
                frm.ShowDialog();
            }
        }

        private void ApplicationTypeUpdated(int applicationTypeId)
        {
            _RefreshApplicationTypesList();
        }
    }
}
