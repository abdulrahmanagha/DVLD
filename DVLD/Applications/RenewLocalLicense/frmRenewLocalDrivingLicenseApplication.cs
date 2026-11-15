using DVLD.Applications.InternationalLicense;
using DVLD.GlobalClasses;
using DVLD_Business.InternationalLicenses;
using DVLD_Business.Licenses;
using DVLD_Business.Manage_Types;
using DVLD_Shared;
using System;
using System.Windows.Forms;

namespace DVLD.Licenses.RenewLicense
{
    public partial class frmRenewLocalDrivingLicenseApplication : Form
    {
        public event Action<int> OnRenewedLicense;

        protected virtual void RenewedLicense(int value)
        {
            OnRenewedLicense?.Invoke(value);
        }

        clsLicense _license = null;
        clsLicense _newLicense = null;
        clsApplicationType _applicationType = null;

        public frmRenewLocalDrivingLicenseApplication()
        {
            InitializeComponent();
            ctrlDriverLicenseInfoWithFilter1.OnLicenseSelected += AfterLicenseSelected;
            ctrlDriverLicenseInfoWithFilter1.OnNotFoundLicense += CtrlDriverLicenseInfoWithFilter1_OnNotFoundLicense; ;
            ctrlDriverLicenseInfoWithFilter1.FilterEnable = true;
            lblShowLicenseInfo.Enabled = false;
            lblShowLicensesHistory.Enabled = false;
            btnRenew.Enabled = false;
            txtNotes.Enabled = false;
        }

        private void _DisplayInitialData()
        {
            DateTime expirationDate = DateTime.Now.AddYears(
                _license.LicenseClassInfo.DefaultValidityLength
            );
            lblExpirationDate.Text = expirationDate.ToString("dd/MMM/yyyy", System.Globalization.CultureInfo.InvariantCulture);
            lblLicenseFees.Text = _license.LicenseClassInfo.ClassFees.ToString();
            lblTotalFees.Text = (_license.LicenseClassInfo.ClassFees + _applicationType.ApplicationTypeFees).ToString();
            lblOldLicenseId.Text = _license.LicenseId.ToString();
            txtNotes.Text = _license.Notes?.ToString();
        }

        private void _ClearInitialData()
        {
            lblExpirationDate.Text = "N\\A";
            lblTotalFees.Text = "N\\A";
            lblLicenseFees.Text = "N\\A";
            lblOldLicenseId.Text = "N\\A";
            txtNotes.Clear();
        }

        private void CtrlDriverLicenseInfoWithFilter1_OnNotFoundLicense()
        {
            _license = null;
            lblShowLicensesHistory.Enabled = false;
            btnRenew.Enabled = false;
            txtNotes.Enabled = false;
            _ClearInitialData();
        }

        private void AfterLicenseSelected(int value)
        {
            _license = ctrlDriverLicenseInfoWithFilter1.SelectedLicense;

            if (_license == null)
                return;

            ctrlDriverLicenseInfoWithFilter1.FilterFocus();
            btnRenew.Enabled = false;
            txtNotes.Enabled = false;
            lblShowLicensesHistory.Enabled = (_license != null);

            _ClearInitialData();

            if (!_license.IsActive)
            {
                MessageBox.Show(
                    $"Selected license with ID = {_license.LicenseId} is not active. You can only renew active licenses.",
                    "Not Allowed",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error
                );
                return;
            }

            if (!_license.IsLicenseExpired())
            {
                MessageBox.Show(
                    $"Selected license with ID = {_license.LicenseId} is not expired yet."
                    + $" It will expire on {_license.ExpirationDate.ToShortDateString()}.",
                    "Not Allowed",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error
                );
                return;
            }

            if(_license.IsDetained)
            {
                MessageBox.Show(
                    $"Selected license with ID = {_license.LicenseId} is detained. You must release it first before renewing.",
                    "Not Allowed",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error
                );
                return;
            }

            _DisplayInitialData();
            btnRenew.Enabled = true;
            txtNotes.Enabled = true;
            btnRenew.Select();
        }

        private void _PerpareData()
        {
            lblApplicationDate.Text = DateTime.Now.ToString("dd/MMM/yyyy", System.Globalization.CultureInfo.InvariantCulture);
            lblIssueDate.Text = DateTime.Now.ToString("dd/MMM/yyyy", System.Globalization.CultureInfo.InvariantCulture);
            lblApplicationFees.Text = _applicationType.ApplicationTypeFees.ToString();
            lblCreatedByUser.Text = clsGlobal.CurrentUser.UserName;
        }

        private void frmRenewLicense_Load(object sender, EventArgs e)
        {
            _applicationType = clsApplicationType.Find(enApplicationType.RenewDrivingLicense);
            _PerpareData();
        }

        private void _PerformRenewSuccessfully()
        {
            MessageBox.Show("Renewed License Successfully.",
                "Done",
                MessageBoxButtons.OK,
                MessageBoxIcon.Information
            );
        }

        private bool _PerformRenew()
        {
            string notes = (string.IsNullOrEmpty(txtNotes.Text)) ? null : txtNotes.Text;
            _newLicense = _license.RenewLicense(notes, clsGlobal.CurrentUser.UserId, _applicationType);

            bool isRenewed = (_newLicense != null);

            if (isRenewed)
            {
                _PerformRenewSuccessfully();
                lblRenewedLicenseId.Text = _newLicense.LicenseId.ToString();
                lblRenewApplicationId.Text = _newLicense.ApplicationId.ToString();
            }
            else
            {
                MessageBox.Show(
                    "Faild to Renew License",
                    "Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error
                );
            }

            return isRenewed;
        }

        private void btnRenew_Click(object sender, EventArgs e)
        {
            DialogResult answerUser = MessageBox.Show(
                "Are you sure you want to renew the license?",
                "Confirm",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Warning,
                MessageBoxDefaultButton.Button2
            );

            if (answerUser != DialogResult.Yes)
            {
                return;
            }

            bool isSaved = _PerformRenew();

            if (isSaved)
            {
                ctrlDriverLicenseInfoWithFilter1.Reload();
                btnRenew.Enabled = false;
                ctrlDriverLicenseInfoWithFilter1.FilterEnable = false;
                txtNotes.Enabled = false;
                lblShowLicenseInfo.Enabled = true;
                RenewedLicense(_newLicense.LicenseId);
            }
        }

        private void lblShowLicensesHistory_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            if (_license == null)
                return;

            using (var frm = new frmShowPersonLicenseHistory(
                _license.DriverInfo.PersonId
            ))
            {
                frm.OnPersonUpdated += Frm_OnPersonUpdated;
                frm.ShowDialog();
            }
        }

        private void Frm_OnPersonUpdated(int obj)
        {
            ctrlDriverLicenseInfoWithFilter1.Reload();
        }

        private void lblShowLicenseInfo_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {

            using (var frm = new frmShowLicenseInfo(_newLicense.LicenseId))
            {
                frm.ShowDialog();
            }
        }
    }
}
