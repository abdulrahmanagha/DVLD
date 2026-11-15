using DVLD.GlobalClasses;
using DVLD.Login;
using DVLD_Business.Drivers;
using DVLD_Business.LicenseClasses;
using DVLD_Business.Licenses;
using DVLD_Business.LocalDrivingLicenseApplications;
using System;
using System.Windows.Forms;
using DVLD_Shared;

namespace DVLD.Licenses
{
    public partial class frmIssueDriverLicenseForTheFirstTime : Form
    {
        public event Action<int> OnLicenseIssued;

        protected virtual void LicenseIssued(int issuedId)
        {
            OnLicenseIssued?.Invoke(issuedId);
        }

        private clsLocalDrivingLicenseApplication _localApplication = null;
        private int? _localApplicationId = null;
        private int? _licenseId = null;

        public frmIssueDriverLicenseForTheFirstTime(int? localApplicationId)
        {
            InitializeComponent();
            _localApplicationId = localApplicationId;
        }

        private void frmIssueDriverLicenseForTheFirstTime_Load(object sender, EventArgs e)
        {
            _localApplication = clsLocalDrivingLicenseApplication.FindLocalDrivingLicenseApplication(
                _localApplicationId.Value
            );

            if( _localApplication == null )
            {
                MessageBox.Show(
                    $"Not Found Local Application With ID = {_localApplicationId.Value}",
                    "Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information
                );

                Close();
                return;
            }

            if(!_localApplication.IsPassedAllTests())
            {
                MessageBox.Show(
                    $"Person Should Pass All Tests First.",
                    "Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information
                );

                Close();
                return;
            }

            int? licenseId = _localApplication.GetIssuedLicenseId();

            if (licenseId != null)
            {
                MessageBox.Show(
                    $"License has already been issued for this application With ID = {licenseId.Value}",
                    "Succeeded",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information
                );

                Close();
                return;
            }

            ctrlDrivingLicenseApplicationInfo1.LoadApplication(_localApplication);
        }

        private bool _PerformIssue()
        {
            string notes = string.IsNullOrWhiteSpace(txtNotes.Text) ? null : txtNotes.Text;

            _licenseId = _localApplication.IssueLicenseForTheFirstTime(notes, clsGlobal.CurrentUser.UserId);

            bool isSaved = (_licenseId != null);

            if (isSaved)
            {
                MessageBox.Show(
                    $"Licese Issued Successfully With Licesne ID = {_licenseId.Value}",
                    "Succeeded",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information
                );
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

            return isSaved;
        }

        private void btnIssue_Click(object sender, EventArgs e)
        { 
            bool isSaved = _PerformIssue();

            if (isSaved)
            {
                _localApplication.CompleteApplication();
                LicenseIssued(_licenseId.Value);
            }

            Close();
        }
    }
}
