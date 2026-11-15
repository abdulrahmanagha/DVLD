using System;
using System.Data;
using System.Windows.Forms;
using DVLD.People.Controls;
using DVLD_Business;
using DVLD.GlobalClasses;
using DVLD_Shared;

namespace DVLD.People
{
    public partial class frmListPeople : Form
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

        DataTable _dtPeople { get; set; } = null;
        DataView _viewPeople { get; set; } = null;
        private FilterItem _selectedFilter = null;

        public frmListPeople()
        {
            InitializeComponent();
        }

        private void _LoadPeopleList()
        {
            _dtPeople = clsPerson.GetAllPeople();
            lblNumberOfRecords.Text = _dtPeople.Rows.Count.ToString();
            _viewPeople = _dtPeople.DefaultView;
            dgvPeopleList.DataSource = _viewPeople;
        }

        private void _ApplyCurrentFilter()
        {
            if (_selectedFilter == null || _selectedFilter.FilterType == enFilterType.None)
                return;

            _ApplyFilter();
        }

        private void _RefreshPeopleList()
        {
            _LoadPeopleList();
            _ApplyCurrentFilter();
        }

        private void frmPeopleManagement_Load(object sender, EventArgs e)
        {
            _LoadPeopleList();
            _FillFilterBy();
            _ConfigControls();
        }

        private void _ConfigControls()
        {
            if (dgvPeopleList.Columns.Count == 0)
                return;

            dgvPeopleList.Columns[0].HeaderText = "Person ID";
            dgvPeopleList.Columns[0].Width = 100;

            dgvPeopleList.Columns[1].HeaderText = "National No";
            dgvPeopleList.Columns[1].Width = 120;

            dgvPeopleList.Columns[2].HeaderText = "First Name";
            dgvPeopleList.Columns[2].Width = 120;

            dgvPeopleList.Columns[3].HeaderText = "Second Name";
            dgvPeopleList.Columns[3].Width = 140;

            dgvPeopleList.Columns[4].HeaderText = "Third Name";
            dgvPeopleList.Columns[4].Width = 120;

            dgvPeopleList.Columns[5].HeaderText = "Last Name";
            dgvPeopleList.Columns[5].Width = 120;

            dgvPeopleList.Columns[6].HeaderText = "Gender";
            dgvPeopleList.Columns[6].Width = 80;

            dgvPeopleList.Columns[7].HeaderText = "Date Of Birth";
            dgvPeopleList.Columns[7].Width = 100;

            dgvPeopleList.Columns[8].HeaderText = "Nationality";
            dgvPeopleList.Columns[8].Width = 100;

            dgvPeopleList.Columns[9].HeaderText = "Phone";
            dgvPeopleList.Columns[9].Width = 100;

            dgvPeopleList.Columns[10].HeaderText = "Email";
            dgvPeopleList.Columns[10].AutoSizeMode =
                DataGridViewAutoSizeColumnMode.Fill;

            dgvPeopleList.ColumnHeadersDefaultCellStyle.Alignment =
                DataGridViewContentAlignment.MiddleCenter;

            int[] centerColumns = { 0, 1, 6, 7, 9 };

            foreach (int index in centerColumns)
            {
                dgvPeopleList.Columns[index].DefaultCellStyle.Alignment =
                    DataGridViewContentAlignment.MiddleCenter;

                dgvPeopleList.Columns[index].HeaderCell.Style.Alignment =
                    DataGridViewContentAlignment.MiddleCenter;
            }
        }

        private void txtFilter_TextChanged(object sender, EventArgs e)
        {
            if(string.IsNullOrWhiteSpace(txtFilter.Text))
            {
                _viewPeople.RowFilter = null;
                lblNumberOfRecords.Text = _viewPeople.Count.ToString();
                return;
            }

            _ApplyFilter();
        }

        private void _FillFilterBy()
        {
            cbFilterBy.Items.Add(new FilterItem("None", "None", enFilterType.None));
            cbFilterBy.Items.Add(new FilterItem("PersonID", "Person ID", enFilterType.Number));
            cbFilterBy.Items.Add(new FilterItem("NationalNo", "National No", enFilterType.Text));
            cbFilterBy.Items.Add(new FilterItem("FirstName", "First Name", enFilterType.Text));
            cbFilterBy.Items.Add(new FilterItem("SecondName", "Second Name", enFilterType.Text));
            cbFilterBy.Items.Add(new FilterItem("ThirdName", "Third Name", enFilterType.Text));
            cbFilterBy.Items.Add(new FilterItem("LastName", "Last Name", enFilterType.Text));
            cbFilterBy.Items.Add(new FilterItem("CountryName", "Nationality", enFilterType.Text));
            cbFilterBy.Items.Add(new FilterItem("Gender", "Gender", enFilterType.Text));
            cbFilterBy.Items.Add(new FilterItem("Phone", "Phone", enFilterType.Text));
            cbFilterBy.Items.Add(new FilterItem("Email", "Email", enFilterType.Text));

            cbFilterBy.DisplayMember = "LabelName";
            cbFilterBy.ValueMember = "ColumnName";
            cbFilterBy.SelectedIndex = 0;
        }
        
        private void _ApplyFilter()
        {
            if (_selectedFilter.FilterType == enFilterType.Number)
            {
                if(!int.TryParse(txtFilter.Text.Trim(), out int result))
                {
                    txtFilter.Clear();
                    return;
                }

                _viewPeople.RowFilter = $"[{_selectedFilter.ColumnName}] = {result}";
            }
            else if(_selectedFilter.FilterType == enFilterType.Text)
            {
                string searchValue = txtFilter.Text.Trim().Replace("'", "''");
                _viewPeople.RowFilter = $"[{_selectedFilter.ColumnName}] LIKE '{searchValue}%'";
            }

            lblNumberOfRecords.Text = _viewPeople.Count.ToString();
        }

        private void cbFilterBy_SelectedIndexChanged(object sender, EventArgs e)
        {
            _selectedFilter = cbFilterBy.SelectedItem as FilterItem;

            if (_selectedFilter == null)
                return;

            if (_selectedFilter.FilterType == enFilterType.None)
            {
                txtFilter.Visible = false;
                _viewPeople.RowFilter = null;
                lblNumberOfRecords.Text = _viewPeople.Count.ToString();
                return;
            }

            txtFilter.Visible = true;
            txtFilter.Clear();
            txtFilter.Select();
        }

        private void txtFilter_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (
                _selectedFilter?.FilterType == enFilterType.Number &&
                !Char.IsDigit(e.KeyChar) &&
                !Char.IsControl(e.KeyChar) // to avoid (ENTER, ESC, BKSP) Inputs
            )
            {
                e.Handled = true;
            }
        }
        
        private void btnClose_Click(object sender, EventArgs e)
        {
            Close();
        }

        private int? GetSelectedPersonId()
        {
            object cellValue = dgvPeopleList.CurrentRow?.Cells[0].Value;

            if (cellValue == null)
                return null;

            return Convert.ToInt32(cellValue);
        }

        private void PeopleManagment_OnPersonSaved(object sender, int personId)
        {
            _RefreshPeopleList();
        }

        private void AddNewPerson()
        {
            using (frmAddEditPerson frm = new frmAddEditPerson())
            {
                frm.OnPersonSaved += PeopleManagment_OnPersonSaved;
                frm.ShowDialog();
            }
        }

        private void ShowMessageError(enDeleteResult deleteResult)
        {
            switch (deleteResult)
            {
                case enDeleteResult.HasRelatedData:

                    MessageBox.Show(
                        "Person was not deleted because it has data linked to it.",
                        "Delete Failed",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Error
                    );

                    break;

                case enDeleteResult.NotFound:

                    MessageBox.Show(
                        "Person was not found.",
                        "Delete Failed",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Error
                    );

                    break;

                case enDeleteResult.Error:

                    MessageBox.Show(
                        "An unexpected error occurred while deleting the person.",
                        "Error",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Error
                    );

                    break;
            }
        }

        private void DeletePerson()
        {
            int? personId = GetSelectedPersonId();

            if (personId == null)
                return;

            DialogResult confirmDelete = MessageBox.Show(
                $"Are you sure you want to delete delete person [{personId.Value}]",
                "Confirm Delete",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Warning,
                MessageBoxDefaultButton.Button2
            );

            if (confirmDelete == DialogResult.No)
                return;

            enDeleteResult deleteResult = clsPerson.Delete(personId.Value);

            if (deleteResult != enDeleteResult.Success)
            {
                ShowMessageError(deleteResult);
                return;
            }

            MessageBox.Show(
                "Person Deleted Successfully.",
                "Successful",
                MessageBoxButtons.OK,
                MessageBoxIcon.Information
            );

            _RefreshPeopleList();
        }

        private void EditPerson()
        {
            int? personId = GetSelectedPersonId();

            if (personId == null)
                return;

            using (frmAddEditPerson frm = new frmAddEditPerson(personId.Value))
            {
                frm.OnPersonSaved += PeopleManagment_OnPersonSaved;
                frm.ShowDialog();
            }
        }

        private void tsmShowDetailsPerson_Click(object sender, EventArgs e)
        {
            int? personId = GetSelectedPersonId();

            if (personId == null)
                return;

            using (frmShowPersonInfo frm = new frmShowPersonInfo(personId.Value))
            {
                frm.ShowDialog();
            }
        }

        private void btnAddNewPerson_Click(object sender, EventArgs e)
        {
            AddNewPerson();
        }

        private void tsmAddNewPerson_Click(object sender, EventArgs e)
        {
            AddNewPerson();
        }

        private void tsmDeletePerson_Click(object sender, EventArgs e)
        {
            DeletePerson();
        }

        private void tsmEditPerson_Click(object sender, EventArgs e)
        {
            EditPerson();
        }

        private void tsmSendEmail_Click(object sender, EventArgs e)
        {
            MessageBox.Show(
                "This Featuer Not Implemented Yet!",
                "Not Ready!",
                MessageBoxButtons.OK,
                MessageBoxIcon.Warning
            );
        }

        private void tsmPhoneCall_Click(object sender, EventArgs e)
        {
            MessageBox.Show(
                "This Featuer Not Implemented Yet!",
                "Not Ready!",
                MessageBoxButtons.OK,
                MessageBoxIcon.Warning
            );
        }

    }
}
