using DVLD.GlobalClasses;
using DVLD.Properties;
using DVLD_Business.Users;
using System;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using DVLD_Shared;
using DVLD_Shared.Utility;

namespace DVLD.Users
{
    public partial class frmChangePassword : Form
    {
        public event Action<int> OnUserUpdated;

        protected virtual void UserUpdated(int userId)
        {
            OnUserUpdated?.Invoke(userId);
        }

        private clsUser _user = null;
        private int? _userId = null;
        private Image _imgEyeOpen = Resources.show_password_32;
        private Image _imgEyeClosed = Resources.hide_password_32;

        public frmChangePassword(int userId)
        {
            InitializeComponent();
            _userId = userId;

            this.AutoValidate = AutoValidate.EnableAllowFocusChange;
        }

        private void _LoadUser()
        {
            ctrlUserCard1.LoadUser(_userId.Value);
            _user = ctrlUserCard1.SelectedUserInfo;

            if (_user == null)
            {
                Close();
            }
        }

        private bool _ValidateCurrentPassword()
        {
            string currentPassword = txtCurrentPassword.Text;

            if (string.IsNullOrWhiteSpace(currentPassword))
            {
                errorProvider1.SetError(txtCurrentPassword, "Current Password cannot be blank.");
                return false;
            }

            if(!_user.VerifyPassword(currentPassword))
            {
                errorProvider1.SetError(txtCurrentPassword, "Current Password is incorrect!");
                return false;
            }

            errorProvider1.SetError(txtCurrentPassword, null);
            return true;
        }

        private bool _ValidateNewPassword()
        {
            string newPassword = txtNewPassword.Text;

            if (string.IsNullOrWhiteSpace(newPassword))
            {
                errorProvider1.SetError(txtNewPassword, "New Password cannot be blank.");
                return false;
            }

            if(_user.VerifyPassword(newPassword))
            {
                errorProvider1.SetError(txtNewPassword, "New Password cannot be the same as the current password. Please choose a different password.");
                return false;
            }

            errorProvider1.SetError(txtNewPassword, null);
            return true;
        }

        private bool _ValidateConfirmPassword()
        {
            string newPassword = txtNewPassword.Text;
            string confirmPassword = txtConfirmPassword.Text;

            if (string.IsNullOrWhiteSpace(confirmPassword))
            {
                errorProvider1.SetError(txtConfirmPassword, "Confirm Password cannot be blank.");
                return false;
            }

            if (!newPassword.Equals(confirmPassword))
            {
                errorProvider1.SetError(txtConfirmPassword, "Password Confirmation does not match Password!");
                return false;
            }

            errorProvider1.SetError(txtConfirmPassword, null);
            return true;
        }

        private void txtCurrentPassword_Validating(object sender, CancelEventArgs e)
        {
            bool isValid = _ValidateCurrentPassword();
            e.Cancel = !isValid;
        }

        private void txtNewPassword_Validating(object sender, CancelEventArgs e)
        {
            bool isValid = _ValidateNewPassword();
            e.Cancel = !isValid;
        }

        private void txtConfirmPassword_Validating(object sender, CancelEventArgs e)
        {
            bool isValid = _ValidateConfirmPassword();
            e.Cancel = !isValid;
        }

        private void _SetTagsToControls()
        {
            pbCurrentPassword.Tag = txtCurrentPassword;
            pbNewPassword.Tag = txtNewPassword;
            pbConfirmPassword.Tag = txtConfirmPassword;
        }

        private void frmChangePassword_Load(object sender, EventArgs e)
        {
            _LoadUser();
            _SetTagsToControls();
        }

        private bool _PerformSave()
        {
            bool isChanged = false;

            try
            {
                isChanged = _user.ChangePassword(
                    txtCurrentPassword.Text.Trim(), 
                    txtNewPassword.Text.Trim()
                );
            }
            catch (Exception ex)
            {
                clsEventLogger.LogEvent(ex, $"Form: frmChangePassword | UserID: {_user?.UserId}");

                MessageBox.Show(
                    "An unexpected error occurred while changing the password. Please check the Event Log.",
                    "System Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error
                );

                return false;
            }

            if (isChanged)
            {
                MessageBox.Show(
                    "Password Changed Successfully",
                    "Done",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information
                );
            }
            else
            {
                MessageBox.Show(
                    "Current password is incorrect or failed to save data.",
                    "Invalid",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error
                );
            }

            return isChanged;
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (!this.ValidateChildren())
            {
                MessageBox.Show(
                    "Please complete all required fields before saving",
                    "Invalid", MessageBoxButtons.OK,
                    MessageBoxIcon.Error
                );

                return;
            }

            string newPlainPassword = txtNewPassword.Text.Trim();
            bool isSave = _PerformSave();
            
            if (isSave)
            {
                clsGlobal.SyncCurrentUser(_user.UserId, newPlainPassword);
                UserUpdated(_user.UserId);
                _SetDefaultValues();
            }
        }

        private void _SetDefaultValues()
        {
            txtCurrentPassword.Clear();
            txtNewPassword.Clear();
            txtConfirmPassword.Clear();
            txtCurrentPassword.Select();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            Close();
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

    }
}
