using DVLD.Login;
using DVLD.People;
using DVLD_Business;
using DVLD_Business.Users;
using System;
using System.Data;
using System.Windows.Forms;
using DVLD.GlobalClasses;
using DVLD_Shared;

namespace DVLD.Users
{
    public partial class frmListUsers : Form
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

        DataTable _dtUsers { get; set; } = null;
        DataView _viewUsers { get; set; } = null;

        private FilterItem _selectedFilter = null;
        private FilterStatusItem _selectedStatus = null;

        public frmListUsers()
        {
            InitializeComponent();
        }

        private void _LoadUsersList()
        {
            _dtUsers = clsUser.GetAllUsers();
            lblNumberOfRecords.Text = _dtUsers.Rows.Count.ToString();
            _viewUsers = _dtUsers.DefaultView;
            dgvUsersList.DataSource = _viewUsers;
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

        private void _RefreshUsersList()
        {
            _LoadUsersList();
            _ApplyCurrentFilter();
        }

        private void _ConfigControls()
        {
            if (dgvUsersList.Columns.Count == 0)
                return;

            dgvUsersList.Columns[0].HeaderText = "User ID";
            dgvUsersList.Columns[0].Width = 120;

            dgvUsersList.Columns[1].HeaderText = "Person ID";
            dgvUsersList.Columns[1].Width = 120;

            dgvUsersList.Columns[2].HeaderText = "Full Name";
            dgvUsersList.Columns[2].Width = 500;

            dgvUsersList.Columns[3].HeaderText = "UserName";
            dgvUsersList.Columns[3].Width = 140;

            dgvUsersList.Columns[4].HeaderText = "Is Active";
            dgvUsersList.Columns[4].Width = 100;
            dgvUsersList.Columns[4].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;

            dgvUsersList.ColumnHeadersDefaultCellStyle.Alignment =
                DataGridViewContentAlignment.MiddleCenter;

            int[] centerColumns = { 0, 1, 4 };

            foreach (int index in centerColumns)
            {
                dgvUsersList.Columns[index].DefaultCellStyle.Alignment =
                    DataGridViewContentAlignment.MiddleCenter;
            }

            dgvUsersList.Columns[2].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dgvUsersList.Columns[3].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
        }

        private void _FillFilterBy()
        {
            cbFilterBy.Items.Add(new FilterItem("None", "None", enFilterType.None));
            cbFilterBy.Items.Add(new FilterItem("UserID", "User ID", enFilterType.Number));
            cbFilterBy.Items.Add(new FilterItem("UserName", "UserName", enFilterType.Text));
            cbFilterBy.Items.Add(new FilterItem("PersonID", "Person ID", enFilterType.Number));
            cbFilterBy.Items.Add(new FilterItem("FullName", "Full Name", enFilterType.Text));
            cbFilterBy.Items.Add(new FilterItem("IsActive", "Is Active", enFilterType.Status));
            
            cbFilterBy.DisplayMember = "LabelName";
            cbFilterBy.ValueMember = "ColumnName";
            cbFilterBy.SelectedIndex = 0;
        }

        private void _FillFilterStatus()
        {
            cbUserStatus.Items.Add(new FilterStatusItem("All", enStatus.All));
            cbUserStatus.Items.Add(new FilterStatusItem("Active", enStatus.Active));
            cbUserStatus.Items.Add(new FilterStatusItem("InActive", enStatus.Inactive));

            cbUserStatus.SelectedIndex = 0;
            cbUserStatus.Visible = false;
        }

        private void frmListUsers_Load(object sender, EventArgs e)
        {
            _LoadUsersList();
            _FillFilterBy();
            _FillFilterStatus();
            _ConfigControls();
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
                cbUserStatus.Visible = false;
                return;
            }

            if(_selectedFilter.FilterType == enFilterType.Status)
            {
                _ShowStatusUser(true);
                cbUserStatus.SelectedIndex = 0;
                return;
            }

            _ShowStatusUser(false);
            txtFilter.Clear();
            txtFilter.Select();
        }

        private void _ApplyStatusFilter()
        {
            if (_selectedStatus.Status == enStatus.All)
            {
                _ResetList();
                return;
            }

            _viewUsers.RowFilter = $"[isActive] = {(int)_selectedStatus.Status}";
            lblNumberOfRecords.Text = _viewUsers.Count.ToString();
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

                _viewUsers.RowFilter = $"[{_selectedFilter.ColumnName}] = {result}";
            }
            else if (_selectedFilter.FilterType == enFilterType.Text)
            {
                string searchValue = txtFilter.Text.Trim().Replace("'", "''");
                _viewUsers.RowFilter = $"[{_selectedFilter.ColumnName}] LIKE '{searchValue}%'";
            }

            lblNumberOfRecords.Text = _viewUsers.Count.ToString();
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

        private void cbFilterStatus_SelectedIndexChanged(object sender, EventArgs e)
        {
            _selectedStatus = cbUserStatus.SelectedItem as FilterStatusItem;

            if (_selectedStatus == null)
                return;

            _ApplyStatusFilter();
        }

        private void _ShowStatusUser(bool show)
        {
            cbUserStatus.Visible = show;
            txtFilter.Visible = !show;
        }

        private void _ResetList()
        {
            _viewUsers.RowFilter = null;
            lblNumberOfRecords.Text = _viewUsers.Count.ToString();
        }

        private int? GetSelectedUserId()
        {
            object cellValue = dgvUsersList.CurrentRow?.Cells[0].Value;

            if (cellValue == null)
                return null;

            return Convert.ToInt32(cellValue);
        }

        private void UsersManagment_OnUserSaved(int userId)
        {
            _RefreshUsersList();
        }

        private void AddNewUser()
        {
            using (frmAddEditUser frm = new frmAddEditUser())
            {
                frm.OnUserSaved += UsersManagment_OnUserSaved;
                frm.ShowDialog();
            }
        }

        private void EditUser()
        {
            int? userId = GetSelectedUserId();

            if (userId == null) 
                return;

            using (frmAddEditUser frm = new frmAddEditUser(userId.Value))
            {
                frm.OnUserSaved += UsersManagment_OnUserSaved;
                frm.ShowDialog();
            }
        }

        private void ShowMessageError(enDeleteResult deleteResult)
        {
            switch (deleteResult)
            {
                case enDeleteResult.HasRelatedData:

                    MessageBox.Show(
                        "User was not deleted because it has data linked to it.",
                        "Delete Failed",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Error
                    );

                    break;

                case enDeleteResult.NotFound:

                    MessageBox.Show(
                        "User was not found.",
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

        private void DeleteUser()
        {
            int? userId = GetSelectedUserId();

            if(userId == null) 
                return;

            if(userId.Value == clsGlobal.CurrentUser.UserId)
            {
                MessageBox.Show(
                    "You cannot delete the currently logged-in user.",
                    "Action Denied",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error
                );

                return;
            }

            DialogResult confirmDelete = MessageBox.Show(
                $"Are you sure you want to delete person [{userId.Value}]",
                "Confirm Delete",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Warning,
                MessageBoxDefaultButton.Button2
            );

            if (confirmDelete == DialogResult.No)
                return;

            enDeleteResult deleteResult = clsUser.Delete(userId.Value);

            if(deleteResult != enDeleteResult.Success)
            {
                ShowMessageError(deleteResult);
                return;
            }

            MessageBox.Show(
               "User Deleted Successfully.",
               "Successful",
               MessageBoxButtons.OK,
               MessageBoxIcon.Information
           );

            _RefreshUsersList();
        }

        private void _ChangePassword()
        {
            int? userId = GetSelectedUserId();

            if (userId == null)
                return;

            using (frmChangePassword frm = new frmChangePassword(userId.Value))
            {
                frm.ShowDialog();
            }
        }

        private void _ShowUserDetails()
        {
            int? userId = GetSelectedUserId();

            if (userId == null)
                return;

            using (frmUserInfo frm = new frmUserInfo(userId.Value))
            {
                frm.OnPersonUpdated += UsersManagment_OnUserSaved;
                frm.ShowDialog();
            }
        }

        private void tsmAddNewUser_Click(object sender, EventArgs e)
        {
            AddNewUser();
        }

        private void btnAddNewUser_Click(object sender, EventArgs e)
        {
            AddNewUser();
        }

        private void tsmEditUser_Click(object sender, EventArgs e)
        {
            EditUser();
        }

        private void tsmDeleteUser_Click(object sender, EventArgs e)
        {
            DeleteUser();
        }

        private void tsmShowUserDetails_Click(object sender, EventArgs e)
        {
            _ShowUserDetails();
        }

        private void tsmChangePassword_Click(object sender, EventArgs e)
        {
            _ChangePassword();
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
