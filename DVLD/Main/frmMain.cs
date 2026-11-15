using DVLD.Applications.DetainAndReleaseLicenses;
using DVLD.Applications.InternationalLicense;
using DVLD.Applications.ReplacementForDamagedOrLostLicenses;
using DVLD.Drivers;
using DVLD.GlobalClasses;
using DVLD.Licenses.DetainLicense;
using DVLD.Licenses.RenewLicense;
using DVLD.LocalDrivingLicenseApplications;
using DVLD.Login;
using DVLD.Manage_Types;
using DVLD.ManageTestTypes;
using DVLD.People;
using DVLD.Users;
using DVLD_Business.Users;
using System;
using System.Drawing;
using System.Windows.Forms;

namespace DVLD
{
    public partial class frmMain : Form
    {
        public event Action<object> OnSignOut;

        protected virtual void SignOut(object sender)
        {
            OnSignOut?.Invoke(sender);
        }

        public frmMain()
        {
            InitializeComponent();
        }

        private void tsmPeople_Click(object sender, EventArgs e)
        {
            using (Form frm = new frmListPeople())
            {
                frm.ShowDialog();
            }
        }

        private void tsmUsers_Click(object sender, EventArgs e)
        {
            using (frmListUsers frm = new frmListUsers())
            {
                frm.ShowDialog();
            }
        }

        private void _Signout()
        {
            clsGlobal.CurrentUser = null;
            SignOut(this);
            Close();
        }

        private void tsmCurrentUserInfo_Click(object sender, EventArgs e)
        {
            using (frmUserInfo frm = new frmUserInfo(clsGlobal.CurrentUser.UserId))
            {
                frm.OnPersonUpdated += PersonUpdated;
                frm.ShowDialog();
            }
        }

        private void tsmChangePassword_Click(object sender, EventArgs e)
        {
            using (frmChangePassword frm = new frmChangePassword(clsGlobal.CurrentUser.UserId))
            {
                frm.OnUserUpdated += UserUpdated;
                frm.ShowDialog();
            }
        }

        private void PersonUpdated(int value)
        {
        }

        private void UserUpdated(int value)
        {
        }

        private void _RefreshLoggedInUserInfo()
        {
        }

        private void tsmSignOut_Click(object sender, EventArgs e)
        {
            _Signout();
        }

        private void tsmManageApplicationTypes_Click(object sender, EventArgs e)
        {
            using (var frm = new frmListApplicationTypes())
            {
                frm.ShowDialog();
            }
        }

        private void tsmManageTestTypes_Click(object sender, EventArgs e)
        {
            using (var frm = new frmListTestTypes())
            {
                frm.ShowDialog();
            }
        }

        private void tsmLocalLicense_Click(object sender, EventArgs e)
        {
            using (var frm = new frmAddEditLocalDrivingLicenseApplication())
            {
                frm.ShowDialog();
            }
        }

        private void tsmLocalDrivingLicenseApplication_Click(object sender, EventArgs e)
        {
            using (var frm = new frmListLocalDrivingLicenseApplication())
            {
                frm.ShowDialog();
            }
        }

        private void tsmDrivers_Click(object sender, EventArgs e)
        {
            using (var frm = new frmListDrivers())
            {
                frm.ShowDialog();
            }
        }

        private void tsmRetakeTest_Click(object sender, EventArgs e)
        {
            using (var frm = new frmListLocalDrivingLicenseApplication())
            {
                frm.ShowDialog();
            }
        }

        private void tsmInternationalLicense_Click(object sender, EventArgs e)
        {
            using (var frm = new frmNewInternationalLicenseApplication())
            {
                frm.ShowDialog();
            }
        }

        private void tsmInternationalLicenseApplications_Click(object sender, EventArgs e)
        {
            using (var frm = new frmListInternationalLicenses())
            {
                frm.ShowDialog();
            }
        }

        private void tsmRenewDrivingLicense_Click(object sender, EventArgs e)
        {
            using (var frm = new frmRenewLocalDrivingLicenseApplication())
            {
                frm.ShowDialog();
            }
        }

        private void tsmReplacementForLostOrDamagedLicense_Click(object sender, EventArgs e)
        {
            using (var frm = new frmReplacementLocalLicense())
            {
                frm.ShowDialog();
            }
        }

        private void tsmReleaseDeatinedLicense_Click(object sender, EventArgs e)
        {
            using (var frm = new frmReleaseDetainedLicenseApplication())
            {
                frm.ShowDialog();
            }
        }

        private void tsmDetainLicense_Click(object sender, EventArgs e)
        {
            using (var frm = new frmDetainLicense())
            {
                frm.ShowDialog();
            }
        }

        private void tsmManageDetainedLicenses_Click(object sender, EventArgs e)
        {
            using (var frm = new frmListDetainedLicenses())
            {
                frm.ShowDialog();
            }
        }

        private void tsmReleaseDetainedDrivingLicense_Click(object sender, EventArgs e)
        {
            using (var frm = new frmReleaseDetainedLicenseApplication())
            {
                frm.ShowDialog();
            }
        }
    }
}
