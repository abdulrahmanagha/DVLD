using DVLD.GlobalClasses;
using DVLD.Licenses;
using DVLD_Business.Applications;
using DVLD_Business.DetainedLicenses;
using DVLD_Business.Licenses;
using DVLD_Business.Manage_Types;
using DVLD_Shared;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DVLD.Applications.DetainAndReleaseLicenses
{
    public partial class frmReleaseDetainedLicenseApplication : Form
    {
        public event Action<int> OnReleasedLicense;

        protected virtual void ReleasedLicense(int value)
        {
            OnReleasedLicense?.Invoke(value);
        }

        private clsLicense _license = null;
        private clsDetainedLicense _detainedLicense = null;
        private clsApplicationType _applicationType = null;
        private int? _applicationId = null;

        public frmReleaseDetainedLicenseApplication(int licenseId)
        {
            InitializeComponent();
            ctrlDriverLicenseInfoWithFilter1.OnLicenseSelected += AfterLicenseSelected;
            ctrlDriverLicenseInfoWithFilter1.OnNotFoundLicense += CtrlDriverLicenseInfoWithFilter1_OnNotFoundLicense;

            lblShowLicenseInfo.Enabled = false;
            lblShowLicensesHistory.Enabled = false;
            btnRelease.Enabled = false;
            ctrlDriverLicenseInfoWithFilter1.LoadLicenseInfo(licenseId);
        }

        public frmReleaseDetainedLicenseApplication()
        {
            InitializeComponent();
            ctrlDriverLicenseInfoWithFilter1.OnLicenseSelected += AfterLicenseSelected;
            ctrlDriverLicenseInfoWithFilter1.OnNotFoundLicense += CtrlDriverLicenseInfoWithFilter1_OnNotFoundLicense; ;
            ctrlDriverLicenseInfoWithFilter1.FilterEnable = true;
            lblShowLicenseInfo.Enabled = false;
            lblShowLicensesHistory.Enabled = false;
            btnRelease.Enabled = false;
        }

        private void _DisplayInitialData()
        {
            lblDetainId.Text = _detainedLicense.DetainId.ToString();
            lblDetainDate.Text = _detainedLicense.DetainDate.ToString("dd/MMM/yyyy", System.Globalization.CultureInfo.InvariantCulture);
            lblDetainFees.Text = _detainedLicense.FineFees.ToString();
            lblLicenseId.Text = _license.LicenseId.ToString();
            lblApplicationFees.Text = _applicationType?.ApplicationTypeFees.ToString();
            lblTotalFees.Text = (_applicationType?.ApplicationTypeFees + _detainedLicense.FineFees).ToString();
        }

        private void _ClearInitialData()
        {
            lblDetainId.Text = "N\\A";
            lblDetainDate.Text = "N\\A";
            lblApplicationFees.Text = "N\\A";
            lblDetainFees.Text = "N\\A";
            lblTotalFees.Text = "N\\A";
            lblLicenseId.Text = "N\\A";
            lblLicenseId.Text = "N\\A";
        }

        private void CtrlDriverLicenseInfoWithFilter1_OnNotFoundLicense()
        {
            _license = null;
            _detainedLicense = null;
            lblShowLicensesHistory.Enabled = false;
            btnRelease.Enabled = false;
            _ClearInitialData();
        }

        private void AfterLicenseSelected(int value)
        {
            _license = ctrlDriverLicenseInfoWithFilter1.SelectedLicense;

            if (_license == null)
                return;

            ctrlDriverLicenseInfoWithFilter1.FilterFocus();
            btnRelease.Enabled = false;
            lblShowLicensesHistory.Enabled = (_license != null);

            _ClearInitialData();

            if (!_license.IsActive)
            {
                MessageBox.Show(
                    $"Selected License is not active. You cannot release an inactive license.",
                    "Not Allowed",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error
                );

                return;
            }

            bool isLicenseDetaind = _license.IsDetained;

            if (!isLicenseDetaind)
            {
                MessageBox.Show(
                    $"Selected license is not detained, choose another one.",
                    "Not Allowed",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error
                );

                return;
            }

            _detainedLicense = clsDetainedLicense.FindByLicneseId(_license.LicenseId);

            if (_detainedLicense == null)
            {
                MessageBox.Show(
                    $" An error occurred; license with ID = {_license.LicenseId} was not found, or it may not be currently reserved",
                    "Not Allowed",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error
                );

                return;
            }

            _DisplayInitialData();
            btnRelease.Enabled = true;
            btnRelease.Select();
        }

        private void _PerpareData()
        {
            lblCreatedByUser.Text = clsGlobal.CurrentUser.UserName;

            if(_license != null)
            {
                lblApplicationFees.Text = _applicationType.ApplicationTypeFees.ToString();
                lblTotalFees.Text = (_applicationType.ApplicationTypeFees + _detainedLicense.FineFees).ToString();
            }
        }

        private void frmReleaseDetainedLicenseApplication_Load(object sender, EventArgs e)
        {
            _applicationType = clsApplicationType.Find(enApplicationType.ReleaseDetainedDrivingLicsense);

            if (_applicationType == null)
                return;

            _PerpareData();
        }

        private void _PerformReleasedSuccessfully()
        {
            MessageBox.Show("Released License Successfully.",
                "Done",
                MessageBoxButtons.OK,
                MessageBoxIcon.Information
            );
        }

        private bool _PerformRelease()
        {
            bool isReleased = _license.ReleaseLicense(
                _detainedLicense.DetainId,
                ref _applicationId,
                clsGlobal.CurrentUser.UserId,
                _applicationType
            );

            if (isReleased)
            {
                _PerformReleasedSuccessfully();
                lblApplicationId.Text = _applicationId.Value.ToString();
            }
            else
            {
                MessageBox.Show(
                    "Faild to Release License",
                    "Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error
                );
            }

            return isReleased;
        }

        private void btnRelease_Click(object sender, EventArgs e)
        {
            DialogResult answerUser = MessageBox.Show(
                "Are you sure you want to release the license?",
                "Confirm",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Warning,
                MessageBoxDefaultButton.Button2
            );

            if (answerUser != DialogResult.Yes)
            {
                return;
            }

            bool isReleased = _PerformRelease();

            if (isReleased)
            {
                ctrlDriverLicenseInfoWithFilter1.Reload();
                btnRelease.Enabled = false;
                ctrlDriverLicenseInfoWithFilter1.FilterEnable = false;
                lblShowLicenseInfo.Enabled = true;
                ReleasedLicense(_detainedLicense.DetainId);
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
            using (var frm = new frmShowLicenseInfo(_license.LicenseId))
            {
                frm.ShowDialog();
            }
        }

    }
}
