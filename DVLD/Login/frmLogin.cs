using DVLD.Properties;
using System;
using System.Drawing;
using System.Windows.Forms;
using DVLD_Business.Users;
using DVLD.GlobalClasses;

namespace DVLD.Login
{
    public partial class frmLogin : Form
    {
        private Image _imgEyeOpen = Resources.show_password_32;
        private Image _imgEyeClosed = Resources.hide_password_32;

        public frmLogin()
        {
            InitializeComponent();
            chkRememberMe.Checked = false;
        }

        private void frmLogin_OnSignOut(object sender)
        {
            _LoadData();
            this.Show();
            txtUserName.Select();
        }

        private void FrmMain_FormClosed(object sender, FormClosedEventArgs e)
        {
            if (clsGlobal.CurrentUser != null)
            {
                this.Close();
            }
        }

        private void _Login(clsUser user)
        {
            clsGlobal.CurrentUser = user;

            frmMain frm = new frmMain();
            frm.OnSignOut += frmLogin_OnSignOut;
            frm.FormClosed += FrmMain_FormClosed;

            this.Hide();
            frm.Show();
        }

        private void _CheckToLogin()
        {
            string userName = txtUserName.Text.Trim();
            string password = txtPassword.Text.Trim();
            
            clsUser user = clsUser.Find(userName, password);
            
            if(user == null)
            {
                MessageBox.Show(
                    "Invalid Username/Password",
                    "Wrong Credentials",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error
                );

                return;
            }

            if (user.IsActive == false)
            {
                MessageBox.Show(
                    "Can't login because this user is Inactive!, Contact Admin.",
                    "Inactive Account",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error
                );

                return;
            }

            if (chkRememberMe.Checked)
            {
                bool isSaved = clsGlobal.RememberUsernameAndPassword(
                    userName, password
                );

                if(!isSaved)
                {
                    MessageBox.Show(
                        "An error occurred while saving your login credentials.",
                        "Save Error",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Error
                    );

                    return;
                }
            }
            else
            {
                clsGlobal.RememberUsernameAndPassword(null, null);
            }

            _Login(user);
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            _CheckToLogin();
        }

        private void pbPassword_MouseDown(object sender, MouseEventArgs e)
        {
            txtPassword.UseSystemPasswordChar = false;
            pbPassword.Image = _imgEyeOpen;
        }

        private void pbPassword_MouseUp(object sender, MouseEventArgs e)
        {
            txtPassword.UseSystemPasswordChar = true;
            pbPassword.Image = _imgEyeClosed;
        }

        private void txtUserName_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
        {
            e.IsInputKey = true;
        }

        private void txtUserName_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Tab)
            {
                txtPassword.Select();
            }
        }

        private void _LoadData()
        {
            string username = null;
            string password = null;

            bool isStored = clsGlobal.GetStoredCredential(
                ref username, 
                ref password
            );

            if (isStored)
            {
                txtUserName.Text = username;
                txtPassword.Text = password;
                btnLogin.Select();
            }
            else
            {
                txtUserName.Clear();
                txtPassword.Clear();
                txtUserName.Select();
            }

            chkRememberMe.Checked = isStored;
        }

        private void frmLogin_Load(object sender, EventArgs e)
        {
            _LoadData();
        }
    }
}
