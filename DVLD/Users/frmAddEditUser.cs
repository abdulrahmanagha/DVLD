using DVLD.GlobalClasses;
using DVLD.Properties;
using DVLD_Business;
using DVLD_Business.Users;
using System;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Windows.Forms;

namespace DVLD.Users
{
    public partial class frmAddEditUser : Form
    {
        public event Action<int> OnUserSaved;

        protected virtual void UserSaved(int userId)
        {
            OnUserSaved?.Invoke(userId);
        }

        private enum enMode { Update, AddNew };
        private enum enValidationState { NotValidated, Valid, Invalid }

        private const string UPDATE_TITLE = "Update User";
        private const string ADD_NEW_TITLE = "Add New User";
        private enMode _mode;
        private int? _userId = null;
        private clsUser _user = null;
        private enValidationState _userNameState = enValidationState.NotValidated;
        private enValidationState _passwordState = enValidationState.NotValidated;
        private enValidationState _confirmPasswordState = enValidationState.NotValidated;
        private Image _imgEyeOpen = Resources.show_password_32;
        private Image _imgEyeClosed = Resources.hide_password_32;

        public frmAddEditUser()
        {
            InitializeComponent();
            _HideTabHeaders();
            _mode = enMode.AddNew;
            ctrlPersonCardWithFilter1.OnPersonUpdated += ctrlPersonCard_OnPersonUpdated;
        }

        public frmAddEditUser(int userId)
        {
            InitializeComponent();
            _HideTabHeaders();
            _userId = userId;
            _mode = enMode.Update;
            ctrlPersonCardWithFilter1.OnPersonUpdated += ctrlPersonCard_OnPersonUpdated;
        }

        private void _HideTabHeaders()
        {
            tabCtrl.Appearance = TabAppearance.FlatButtons;
            tabCtrl.ItemSize = new Size(0, 1);
            tabCtrl.SizeMode = TabSizeMode.Fixed;
        }

        private void _PrepareUpdateMode()
        {
            _user = clsUser.Find(_userId.Value);

            if (_user == null)
            {
                MessageBox.Show(
                    "This form will be closed, because No User with ID = " + _userId.Value,
                    "Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error
                );

                this.Close();
                return;
            }

            lblTitleForm.Text = UPDATE_TITLE;
            lblUserId.Text = _user.UserId.ToString();
            txtUserName.Text = _user.UserName;
            chkIsActive.Checked = _user.IsActive;

            if (_user.PersonInfo != null)
                ctrlPersonCardWithFilter1.LoadPerson(_user.PersonInfo);

            ctrlPersonCardWithFilter1.FilterEnable = false;

            if(_user.UserId == clsGlobal.CurrentUser.UserId)
            {
                chkIsActive.Enabled = false;
            }
        }

        private void _PrepareAddNewMode()
        {
            _user = new clsUser();
            lblTitleForm.Text = ADD_NEW_TITLE;
            _userId = null;

            ctrlPersonCardWithFilter1.Clear();
        }

        private void _PerpareMode()
        {
            if (_mode == enMode.AddNew)
            {
                _PrepareAddNewMode();
            }
            else
            {
                _PrepareUpdateMode();
            }
        }

        private void _PerformUpdate()
        {
            MessageBox.Show (
                "Update User Successfully",
                "Done",
                MessageBoxButtons.OK,
                MessageBoxIcon.Information
            );
        }

        private void _ConvertModeToUpdate()
        {
            _mode = enMode.Update;
            ctrlPersonCardWithFilter1.FilterEnable = false;
            _userId = _user.UserId;
            lblTitleForm.Text = UPDATE_TITLE;
            lblUserId.Text = _user.UserId.ToString();
        }

        private void _PerformAddNew()
        {
            MessageBox.Show("Add New User Successfully",
                "Done",
                MessageBoxButtons.OK,
                MessageBoxIcon.Information
            );

            _ConvertModeToUpdate();
        }

        private void _SetDataToSave()
        {
            _user.UserName = txtUserName.Text.Trim();
            _user.IsActive = chkIsActive.Checked;

            if(!string.IsNullOrEmpty(txtPassword.Text))
            {
                _user.Password = txtPassword.Text.Trim();
            }
        }

        private bool _PerformSave()
        {
            _SetDataToSave();

            if (_user.Save())
            {
                if (_mode == enMode.Update)
                {
                    _PerformUpdate();
                }
                else
                {
                    _PerformAddNew();
                }

                txtPassword.Clear();
                txtConfirmPassword.Clear();
                return true;
            }
            else
            {
                MessageBox.Show(
                    "Error, can't save data",
                    "Invalid",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error
                );
            }

            return false;
        }

        private bool _ValidateBeforeSave()
        {
            if (string.IsNullOrWhiteSpace(txtUserName.Text))
            {
                _PerformValidateUserName();
                return false;
            }

            if (_userNameState != enValidationState.Valid)
            {
                _PerformValidateUserName();
                if (_userNameState == enValidationState.Invalid)
                    return false;
            }

            if (_IsPasswordOptionalAndUntouched())
            {
                return true; 
            }

            _PerformValidatePassword();
            _PerformValidateConfirmPassword();

            return (_passwordState == enValidationState.Valid &&
            _confirmPasswordState == enValidationState.Valid);
        }

        private bool _IsFormDirty()
        {
            bool isUserNameChanged = txtUserName.Text.Trim() != _user.UserName;
            bool isActiveChanged = chkIsActive.Checked != _user.IsActive;
            bool isPasswordChanged = !string.IsNullOrEmpty(txtPassword.Text);
            bool isConfirmPasswordChange = !string.IsNullOrEmpty(txtConfirmPassword.Text);
            
            return isUserNameChanged || isActiveChanged || isPasswordChanged || isConfirmPasswordChange;
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (_mode == enMode.Update && !_IsFormDirty())
            {
                MessageBox.Show(
                    "No changes were made.",
                    "Information",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information
                );

                return;
            }

            if (!_ValidateBeforeSave())
            {
                MessageBox.Show(
                    "Please complete all required fields before saving",
                    "Invalid", MessageBoxButtons.OK,
                    MessageBoxIcon.Error
                );

                return;
            }

            bool isSave = _PerformSave();

            if (isSave)
            {
                clsGlobal.SyncIfCurrentUser(_user.UserId);
                UserSaved(_user.UserId);
            }
        }

        private void _SetDefaultValuesToControls()
        {
            tabCtrl.SelectedIndex = 0;
            btnSave.Enabled = false;
            pbPassword.Tag = txtPassword;
            pbConfirmPassword.Tag = txtConfirmPassword;
        }

        private void frmAddEditUser_Load(object sender, EventArgs e)
        {
            _PerpareMode();
            _SetDefaultValuesToControls();
        }

        private void tabCtrl_SelectedIndexChanged(object sender, EventArgs e)
        {
            btnSave.Enabled = (tabCtrl.SelectedIndex == 1);
        }

        private void tabCtrl_Selecting(object sender, TabControlCancelEventArgs e)
        {
            if (e.TabPage != tpLoginInfo)
                return;

            if (_mode != enMode.AddNew)
                return;

            if(ctrlPersonCardWithFilter1.SelectedPersonInfo == null)
            {
                MessageBox.Show(
                     "Please select a person to link to the user.",
                     "Select A Person",
                     MessageBoxButtons.OK,
                     MessageBoxIcon.Error
                 );

                e.Cancel = true;
                return;
            }

            if (
                clsUser.IsUserExistForPersonId(ctrlPersonCardWithFilter1.PersonId.Value)
            )
            {
                MessageBox.Show(
                     "Selected person already has a user, choose another one.",
                     "Select Anoher One",
                     MessageBoxButtons.OK,
                     MessageBoxIcon.Error
                 );

                e.Cancel = true;
                return;
            }

            _user.PersonId = ctrlPersonCardWithFilter1.PersonId;
            _user.PersonInfo = ctrlPersonCardWithFilter1.SelectedPersonInfo;
        }

        private void btnNext_Click(object sender, EventArgs e)
        {
            tabCtrl.SelectedTab = tpLoginInfo;
        }

        private void btnBack_Click(object sender, EventArgs e)
        {
            tabCtrl.SelectedTab = tpPersonallnfo;
        }

        private bool _ValidateUserName()
        {
            string userName = txtUserName.Text;
            if (string.IsNullOrWhiteSpace(userName))
            {
                errorProvider1.SetError(txtUserName, "UserName is required");
                return false;
            }

            if (
                (
                    _user.UserName == null || 
                    !_user.UserName.Equals(userName.Trim(), StringComparison.OrdinalIgnoreCase)
                ) && clsUser.IsUserExist(userName.Trim())
            )
            {
                errorProvider1.SetError(txtUserName, "Username not available");
                return false;
            }

            errorProvider1.SetError(txtUserName, "");
            return true;
        }

        private bool _ValidatePassword()
        {
            string password = txtPassword.Text;

            if (string.IsNullOrWhiteSpace(password))
            {
                errorProvider1.SetError(txtPassword, "Password cannot be blank.");
                return false;
            }

            errorProvider1.SetError(txtPassword, "");
            return true;
        }

        private bool _ValidateConfirmPassword()
        {
            string password = txtPassword.Text;
            string confirmPassword = txtConfirmPassword.Text;

            if (string.IsNullOrWhiteSpace(confirmPassword))
            {
                errorProvider1.SetError(txtConfirmPassword, "Confirm Password cannot be blank.");
                return false;
            }

            if (!password.Equals(confirmPassword))
            {
                errorProvider1.SetError(txtConfirmPassword, "Password Confirmation does not match Password!");
                return false;
            }

            errorProvider1.SetError(txtConfirmPassword, "");
            return true;
        }

        private void _PerformValidateUserName()
        {
            bool isValid = _ValidateUserName();

            _userNameState = (isValid) ?
                enValidationState.Valid :
                enValidationState.Invalid;
        }

        private void _PerformValidatePassword()
        {
            bool isValid = _ValidatePassword();

            _passwordState = (isValid) ?
                enValidationState.Valid :
                enValidationState.Invalid;
        }

        private void _PerformValidateConfirmPassword()
        {
            bool isValid = _ValidateConfirmPassword();

            _confirmPasswordState = (isValid) ?
                enValidationState.Valid :
                enValidationState.Invalid;
        }

        private bool _IsPasswordOptionalAndUntouched()
        {
            bool isValid = _mode == enMode.Update &&
                    string.IsNullOrEmpty(txtPassword.Text) &&
                    string.IsNullOrEmpty(txtConfirmPassword.Text);

            if (isValid)
            {
                errorProvider1.SetError(txtPassword, "");
                errorProvider1.SetError(txtConfirmPassword, "");
                _passwordState = enValidationState.Valid;
                _confirmPasswordState = enValidationState.Valid;
            }

            return isValid;
        }

        private void txtUserName_Validating(object sender, CancelEventArgs e)
        {
            if (_userNameState == enValidationState.NotValidated)
            {
                _PerformValidateUserName();
            }
        }

        private void txtPassword_Validating(object sender, CancelEventArgs e)
        {
            if (_passwordState == enValidationState.NotValidated)
            {
                if (_IsPasswordOptionalAndUntouched())
                {
                    return;
                }

                _PerformValidatePassword();
            }
        }

        private void txtConfirmPassword_Validating(object sender, CancelEventArgs e)
        {
            if (_confirmPasswordState == enValidationState.NotValidated)
            {
                if (_IsPasswordOptionalAndUntouched())
                {
                    return;
                }

                _PerformValidateConfirmPassword();
            }
        }

        private void txtUserName_TextChanged(object sender, EventArgs e)
        {
            _userNameState = enValidationState.NotValidated;
        }

        private void txtPassword_TextChanged(object sender, EventArgs e)
        {
            _passwordState = enValidationState.NotValidated;
        }

        private void txtConfirmPassword_TextChanged(object sender, EventArgs e)
        {
            _confirmPasswordState = enValidationState.NotValidated;
        }

        private void ctrlPersonCard_OnPersonUpdated(int personId)
        {
            if(_mode == enMode.Update && _user != null)
            {
                UserSaved(_user.UserId);
            }
        }

        private void pbEye_MouseDown(object sender, MouseEventArgs e)
        {
            if (sender is PictureBox pb && pb.Tag is TextBox txt)
            {
                txt.UseSystemPasswordChar = false;
                pb.Image = _imgEyeOpen;
            }
        }

        private void pbEye_MouseUp(object sender, MouseEventArgs e)
        {
            if (sender is PictureBox pb && pb.Tag is TextBox txt)
            {
                txt.UseSystemPasswordChar = true;
                pb.Image = _imgEyeClosed;
            }
        }

        private void frmAddEditUser_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (_user == null)
                return;

            if (_mode == enMode.Update && _IsFormDirty())
            {
                var result = MessageBox.Show(
                    "You have unsaved changes. Are you sure you want to exit?",
                    "Confirm Exit",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Warning
                );

                if (result == DialogResult.No)
                    e.Cancel = true;
            }
        }
    }
}
