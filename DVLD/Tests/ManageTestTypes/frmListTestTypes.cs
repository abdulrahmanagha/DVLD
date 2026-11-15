using DVLD_Business.Manage_Types;
using DVLD_Business.ManageTestTypes;
using System;
using System.Data;
using System.Windows.Forms;
using DVLD_Shared;

namespace DVLD.ManageTestTypes
{
    public partial class frmListTestTypes : Form
    {
        DataTable _dtTestTypes { get; set; } = null;

        public frmListTestTypes()
        {
            InitializeComponent();
        }

        private void _RefreshTestTypeList()
        {
            _dtTestTypes = clsTestType.GetAllTestTypes();
            dgvTestTypesList.DataSource = _dtTestTypes.DefaultView;
            lblNumberOfRecords.Text = _dtTestTypes.Rows.Count.ToString();
        }

        private void _ConfigControls()
        {
            if (dgvTestTypesList.Columns.Count == 0)
                return;

            dgvTestTypesList.Columns[0].HeaderText = "ID";
            dgvTestTypesList.Columns[0].Width = 120;

            dgvTestTypesList.Columns[1].HeaderText = "Title";
            dgvTestTypesList.Columns[1].Width = 200;

            dgvTestTypesList.Columns[2].HeaderText = "Description";
            dgvTestTypesList.Columns[2].Width = 500;

            dgvTestTypesList.Columns[3].HeaderText = "Fees";
            dgvTestTypesList.Columns[3].Width = 100;
            dgvTestTypesList.Columns[3].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;

            dgvTestTypesList.ColumnHeadersDefaultCellStyle.Alignment =
                DataGridViewContentAlignment.MiddleCenter;

            int[] centerColumns = { 0, 3 };

            foreach (int index in centerColumns)
            {
                dgvTestTypesList.Columns[index].DefaultCellStyle.Alignment =
                    DataGridViewContentAlignment.MiddleCenter;
            }
        }

        private void frmListTestTypes_Load(object sender, EventArgs e)
        {
            _RefreshTestTypeList();
            _ConfigControls();
        }

        private int? GetSelectedTestTypeId()
        {
            object cellValue = dgvTestTypesList.CurrentRow?.Cells[0].Value;

            if (cellValue == null)
                return null;

            return Convert.ToInt32(cellValue);
        }

        private void tsmEditTestType_Click(object sender, EventArgs e)
        {
            int? testTypeId = GetSelectedTestTypeId();

            if(testTypeId == null) 
                return;

            using (frmUpdateTestTypes frm = new frmUpdateTestTypes((enTestType)testTypeId.Value))
            {
                frm.OnTestTypeUpdated += TestTypeUpdated;
                frm.ShowDialog();
            }
        }

        private void TestTypeUpdated(int testTypeId)
        {
            _RefreshTestTypeList();
        }
    }
}
