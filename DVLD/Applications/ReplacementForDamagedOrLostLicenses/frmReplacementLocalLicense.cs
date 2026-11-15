using DVLD.GlobalClasses;
using DVLD.Licenses;
using DVLD_Business.Licenses;
using DVLD_Business.Manage_Types;
using DVLD_Business.Users;
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

namespace DVLD.Applications.ReplacementForDamagedOrLostLicenses
{
    public partial class frmReplacementLocalLicense : Form
    {
        public event Action<int> OnReplacedLicense;
        protected virtual void ReplacedLicense(int value)
        {
            OnReplacedLicense?.Invoke(value);
        }

        clsLicense _license = null;
        clsLicense _newLicense = null;
        clsApplicationType _damagedApplicationType = null;
        clsApplicationType _lostApplicationType = null;
        clsApplicationType _applicationType = null;
        string DAMAGED_TITLE = "Replacement For Damaged License";
        string LOST_TITLE = "Replacement For Lost License";

        public frmReplacementLocalLicense()
        {
            InitializeComponent();
            ctrlDriverLicenseInfoWithFilter1.OnLicenseSelected += AfterLicenseSelected;
            ctrlDriverLicenseInfoWithFilter1.OnNotFoundLicense += CtrlDriverLicenseInfoWithFilter1_OnNotFoundLicense; ;
            ctrlDriverLicenseInfoWithFilter1.FilterEnable = true;
            lblShowLicenseInfo.Enabled = false;
            lblShowLicensesHistory.Enabled = false;
            btnReplace.Enabled = false;
            gbReplacementFor.Enabled = false;
            txtNotes.Enabled = false;
            rbDamagedLicense.Tag = enApplicationType.ReplaceDamagedDrivingLicense;
            rbLostLicense.Tag = enApplicationType.ReplaceLostDrivingLicense;
        }

        private void CtrlDriverLicenseInfoWithFilter1_OnNotFoundLicense()
        {
            _license = null;
            lblShowLicensesHistory.Enabled = false;
            btnReplace.Enabled = false;
            gbReplacementFor.Enabled = false;
            txtNotes.Enabled = false;

            _ClearInitialData();
        }

        private void AfterLicenseSelected(int value)
        {
            _license = ctrlDriverLicenseInfoWithFilter1.SelectedLicense;

            if (_license == null)
                return;

            ctrlDriverLicenseInfoWithFilter1.FilterFocus();
            btnReplace.Enabled = false;
            gbReplacementFor.Enabled = false;
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

            if (_license.IsLicenseExpired())
            {
                MessageBox.Show(
                    $"Selected license with ID = {_license.LicenseId} is expired",
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
            gbReplacementFor.Enabled = true;
            btnReplace.Enabled = true;
            rbDamagedLicense.Checked = true;
            txtNotes.Enabled = true;
            _applicationType = _damagedApplicationType;
            ctrlDriverLicenseInfoWithFilter1.FilterFocus();
            btnReplace.Select();
        }

        private void _DisplayInitialData()
        {
            lblOldLicenseId.Text = _license.LicenseId.ToString();
            txtNotes.Text = _license.Notes?.ToString();
            lblApplicationFees.Text = _damagedApplicationType.ApplicationTypeFees.ToString();
        }

        private void _ClearInitialData()
        {
            lblOldLicenseId.Text = "N\\A";
            lblApplicationFees.Text = "N\\A";
            txtNotes.Clear();
        }

        private void _PerpareData()
        {
            lblApplicationDate.Text = DateTime.Now.ToString("dd/MMM/yyyy", System.Globalization.CultureInfo.InvariantCulture);
            lblCreatedByUser.Text = clsGlobal.CurrentUser.UserName;
        }

        private void frmReplacementLocalLicense_Load(object sender, EventArgs e)
        {
            _damagedApplicationType = clsApplicationType.Find(enApplicationType.ReplaceDamagedDrivingLicense);
            _lostApplicationType = clsApplicationType.Find(enApplicationType.ReplaceLostDrivingLicense);
            _applicationType = _damagedApplicationType;
            _PerpareData();
        }
        
        private void ChangeModeToLost()
        {
            this.Text = LOST_TITLE;
            _applicationType = _lostApplicationType;
            lblApplicationFees.Text = _lostApplicationType.ApplicationTypeFees.ToString();
        }

        private void ChangeModeToDamaged()
        {
            this.Text = DAMAGED_TITLE;
            _applicationType = _damagedApplicationType;
            lblApplicationFees.Text = _damagedApplicationType.ApplicationTypeFees.ToString();
        }

        private void ChangeReplacementMode(object sender, EventArgs e)
        {
            RadioButton rb = sender as RadioButton;

            if (!rb.Checked)
                return;

            if((enApplicationType)rb.Tag == enApplicationType.ReplaceLostDrivingLicense)
            {
                ChangeModeToLost();
            }
            else  
            {
                ChangeModeToDamaged();
            }
        }

        private void _PerformReplacedSuccessfully()
        {
            MessageBox.Show("Replaced License Successfully.",
                "Done",
                MessageBoxButtons.OK,
                MessageBoxIcon.Information
            );
        }

        private bool _PerformReplace()
        {
            string notes = (string.IsNullOrEmpty(txtNotes.Text)) ? null : txtNotes.Text;
            _newLicense = _license.ReplaceLicense(notes, clsGlobal.CurrentUser.UserId, _applicationType);
            
            bool isRenewed = (_newLicense != null);

            if (isRenewed)
            {
                _PerformReplacedSuccessfully();
                lblReplacedLicenseId.Text = _newLicense.LicenseId.ToString();
                lblReplaceApplicationId.Text = _newLicense.ApplicationId.ToString();
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

        private void btnReplace_Click(object sender, EventArgs e)
        {
            DialogResult answerUser = MessageBox.Show(
               "Are you sure you want to replace the license?",
               "Confirm",
               MessageBoxButtons.YesNo,
               MessageBoxIcon.Warning,
               MessageBoxDefaultButton.Button2
           );

            if (answerUser != DialogResult.Yes)
            {
                return;
            }

            bool isSaved = _PerformReplace();

            if (isSaved)
            {
                ctrlDriverLicenseInfoWithFilter1.Reload();
                btnReplace.Enabled = false;
                ctrlDriverLicenseInfoWithFilter1.FilterEnable = false;
                gbReplacementFor.Enabled = false;
                txtNotes.Enabled = false;
                lblShowLicenseInfo.Enabled = true;
                ReplacedLicense(_newLicense.LicenseId);
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
                frm.OnPersonUpdated += Frm_OnPersonUpdated; ;
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
