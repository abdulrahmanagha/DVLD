using DVLD.GlobalClasses;
using DVLD.Licenses;
using DVLD_Business;
using DVLD_Business.Applications;
using DVLD_Business.InternationalLicenses;
using DVLD_Business.Licenses;
using DVLD_Business.LocalDrivingLicenseApplications;
using DVLD_Business.Manage_Types;
using DVLD_Shared;
using System;
using System.ComponentModel;
using System.Windows.Forms;

namespace DVLD.Applications.InternationalLicense
{
    public partial class frmNewInternationalLicenseApplication : Form
    {
        public event Action<int> OnInternationalLicenseApplicationSaved;

        protected virtual void InternationalLicenseApplicationSaved(int value)
        {
            OnInternationalLicenseApplicationSaved?.Invoke(value);
        }

        private clsLicense _license = null;
        private clsInternationalLicense _internationalLicense = null;
        private int? _licenseId = null;
        public frmNewInternationalLicenseApplication(int licenseId)
        {
            InitializeComponent();
            ctrlDriverLicenseInfoWithFilter1.OnLicenseSelected += AfterLicenseSelected;
            ctrlDriverLicenseInfoWithFilter1.OnNotFoundLicense += CtrlDriverLicenseInfoWithFilter1_OnNotFoundLicense;
            ctrlDriverLicenseInfoWithFilter1.FilterEnable = true;
            lblShowLicenseInfo.Enabled = false;
            lblShowLicensesHistory.Enabled = false;
            btnIssue.Enabled = false;
            _licenseId = licenseId;
        }

        public frmNewInternationalLicenseApplication()
        {
            InitializeComponent();
            ctrlDriverLicenseInfoWithFilter1.FilterEnable = true;
            ctrlDriverLicenseInfoWithFilter1.OnLicenseSelected += AfterLicenseSelected;
            ctrlDriverLicenseInfoWithFilter1.OnNotFoundLicense += CtrlDriverLicenseInfoWithFilter1_OnNotFoundLicense;
            lblShowLicenseInfo.Enabled = false;
            lblShowLicensesHistory.Enabled = false;
            btnIssue.Enabled = false;
        }

        private void CtrlDriverLicenseInfoWithFilter1_OnNotFoundLicense()
        {
            lblShowLicensesHistory.Enabled = false;
            btnIssue.Enabled = false;
            _ClearInitialData();
        }

        private void _PerpareInternationalLicense()
        {
            _internationalLicense = new clsInternationalLicense();
            _internationalLicense.IsActive = true;
            _internationalLicense.ApplicationStatus = enApplicationStatus.New;
            _internationalLicense.LastStatusDate = DateTime.Now;
            _internationalLicense.ApplicationDate = DateTime.Now;
            _internationalLicense.ExpirationDate = DateTime.Now.AddYears(10);
            _internationalLicense.IssueDate = DateTime.Now;
            _internationalLicense.ApplicationTypeId = (int)enApplicationType.NewInternationalLicense;
            _internationalLicense.PaidFees = clsApplicationType.Find(enApplicationType.NewInternationalLicense).ApplicationTypeFees;
            _internationalLicense.CreatedByUserId = clsGlobal.CurrentUser.UserId;
        }

        private void _PerpareData()
        {
            lblApplicationDate.Text = _internationalLicense.ApplicationDate.ToString("dd/MMM/yyyy", System.Globalization.CultureInfo.InvariantCulture);
            lblApplicationDate.Text = _internationalLicense.IssueDate.ToString("dd/MMM/yyyy", System.Globalization.CultureInfo.InvariantCulture);
            lblCreatedByUser1.Text = clsGlobal.CurrentUser.UserName;
            lblFees.Text = _internationalLicense.PaidFees.ToString();
            lblIssueDate.Text = _internationalLicense.IssueDate.ToString("dd/MMM/yyyy", System.Globalization.CultureInfo.InvariantCulture);

            if(_license != null)
            {
                lblExpirationDate.Text = _internationalLicense?.ExpirationDate.ToString("dd/MMM/yyyy", System.Globalization.CultureInfo.InvariantCulture);
            }
        }

        private void frmNewInternationalLicenseApplication_Load(object sender, EventArgs e)
        {
            _PerpareInternationalLicense();
            _PerpareData();
            ctrlDriverLicenseInfoWithFilter1.FilterFocus();
        }

        private void _DisplayInitialData()
        {
            lblExpirationDate.Text = _internationalLicense?.ExpirationDate.ToString("dd/MMM/yyyy", System.Globalization.CultureInfo.InvariantCulture);
            lblLocalLicenseId.Text = _license.LicenseId.ToString();
        }

        private void _ClearInitialData()
        {
            lblExpirationDate.Text = "N\\A";
            lblLocalLicenseId.Text = "N\\A";
        }

        private void AfterLicenseSelected(int value)
        {
            _license = ctrlDriverLicenseInfoWithFilter1.SelectedLicense;
            btnIssue.Enabled = false;
            lblShowLicensesHistory.Enabled = (_license != null);
            ctrlDriverLicenseInfoWithFilter1.FilterFocus();

            _ClearInitialData();

            if (_license == null)
            {
                MessageBox.Show(
                    $"Not Found license with ID = {value}.",
                    "Not Allowed",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error
                );
        
                return;
            }

            if (_license.LicenseClassId != 3)
            {
                MessageBox.Show(
                    "Selected License should be Class 3, select another one.", 
                    "Not allowed", 
                    MessageBoxButtons.OK, 
                    MessageBoxIcon.Error
                );

                return;
            }

            if (!_license.IsActive)
            {
                MessageBox.Show(
                    $"Selected license with ID = {_license.LicenseId} is not active. Please choose an active license.",
                    "Not Allowed",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error
                );

                return;
            }

            if (_license.ExpirationDate < DateTime.Today)
            {
                MessageBox.Show(
                    $"Selected license with ID = {_license.LicenseId} has expired on {_license.ExpirationDate.ToShortDateString()}. You must renew it first.",
                    "Not Allowed",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error
                );

                return;
            }

            if (clsInternationalLicense.HasDriverInternationalLicense(
                _license.DriverId
            ))
            {
                MessageBox.Show(
                    $"Person already has an active international license.",
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
            btnIssue.Enabled = true;
            btnIssue.Select();
        }

        private void _PerformAddNew()
        {
            MessageBox.Show("International Issued Successfully.",
                "Done",
                MessageBoxButtons.OK,
                MessageBoxIcon.Information
            );           
        }

        private void _SetDataToSave()
        {
            _internationalLicense.PersonId = _license.DriverInfo.PersonId;
            _internationalLicense.DriverId = _license.DriverInfo.DriverId;
            _internationalLicense.IssuedUsingLocalLicenseId = _license.LicenseId;
        }

        private bool _PerformIssue()
        {
            _SetDataToSave();

            bool isSaved = _internationalLicense.Save();

            if(isSaved)
            {
                _PerformAddNew();
                lblApplicationId.Text = _internationalLicense.ApplicationId.ToString();
                lblInternationalLicenseId.Text = _internationalLicense.InternationalLicenseId.ToString();
            }
            else
            {
                MessageBox.Show(
                    "Faild to Issue International License", 
                    "Error", 
                    MessageBoxButtons.OK, 
                    MessageBoxIcon.Error
                );
            }

            return isSaved;
        }

        private void btnIssue_Click(object sender, EventArgs e)
        {
            DialogResult answerUser = MessageBox.Show(
                "Are you sure you want to issue the license?",
                "Confirm",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Warning,
                MessageBoxDefaultButton.Button2
            );

            if (answerUser != DialogResult.Yes)
            {
                return;
            }

            bool isSaved = _PerformIssue();
            
            if(isSaved)
            {
                btnIssue.Enabled = false;
                lblShowLicenseInfo.Enabled = true;
                ctrlDriverLicenseInfoWithFilter1.FilterEnable = false;
                InternationalLicenseApplicationSaved(_internationalLicense.InternationalLicenseId);
            }
        }

        private void lblShowLicensesHistory_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        { 
            using(var frm = new frmShowPersonLicenseHistory(
                ctrlDriverLicenseInfoWithFilter1.SelectedLicense.DriverInfo.PersonId
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
            using (var frm = new frmShowInternationalLicenseInfo(_internationalLicense))
            {
                frm.ShowDialog();
            }
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void frmNewInternationalLicenseApplication_Shown(object sender, EventArgs e)
        {
            if (_licenseId != null)
            {
                ctrlDriverLicenseInfoWithFilter1.LoadLicenseInfo(_licenseId.Value);
                ctrlDriverLicenseInfoWithFilter1.FilterEnable = false;
            }
        }
    }
}
