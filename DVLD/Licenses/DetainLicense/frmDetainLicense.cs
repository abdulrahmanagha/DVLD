using DVLD.GlobalClasses;
using DVLD_Business.Licenses;
using DVLD_Shared;
using System;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Windows.Forms;

namespace DVLD.Licenses.DetainLicense
{
    public partial class frmDetainLicense : Form
    {
        public event Action<int> OnDetainedLicense;

        protected virtual void DetainedLicense(int value)
        {
            OnDetainedLicense?.Invoke(value);
        }

        clsLicense _license = null;
        private int? _detainedLicenseId = null;
        
        public frmDetainLicense()
        {
            InitializeComponent();
            ctrlDriverLicenseInfoWithFilter1.OnLicenseSelected += AfterLicenseSelected;
            ctrlDriverLicenseInfoWithFilter1.OnNotFoundLicense += CtrlDriverLicenseInfoWithFilter1_OnNotFoundLicense; ;
            ctrlDriverLicenseInfoWithFilter1.FilterEnable = true;
            lblShowLicenseInfo.Enabled = false;
            lblShowLicensesHistory.Enabled = false;
            btnDetain.Enabled = false;
            txtFees.Enabled = false;

            this.AutoValidate = AutoValidate.EnableAllowFocusChange;
        }

        private void _DisplayInitialData()
        {
            lblLicenseId.Text = _license.LicenseId.ToString();
        }

        private void _ClearInitialData()
        {
            lblLicenseId.Text = "N\\A";
            txtFees.Clear();
            errorProvider1.SetError(txtFees, null);
        }

        private void CtrlDriverLicenseInfoWithFilter1_OnNotFoundLicense()
        {
            _license = null;
            _detainedLicenseId = null;
            lblShowLicensesHistory.Enabled = false;
            btnDetain.Enabled = false;
            txtFees.Enabled = false;
            _ClearInitialData();
        }

        private void AfterLicenseSelected(int value)
        {
            _license = ctrlDriverLicenseInfoWithFilter1.SelectedLicense;

            if (_license == null)
                return;

            ctrlDriverLicenseInfoWithFilter1.FilterFocus();
            btnDetain.Enabled = false;
            lblShowLicensesHistory.Enabled = (_license != null);

            _ClearInitialData();

            if(!_license.IsActive)
            {
                MessageBox.Show(
                    $"Selected License is not active. You cannot detain an inactive license.",
                    "Not Allowed",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error
                );

                return;
            }

            bool isLicenseDetaind = _license.IsDetained;

            if (isLicenseDetaind)
            {
                MessageBox.Show(
                    $"Selected license is already detained, choose another one.",
                    "Not Allowed",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error
                );

                return;
            }

            _DisplayInitialData();
            btnDetain.Enabled = true;
            txtFees.Enabled = true;
            txtFees.Select();
        }

        private void _PerpareData()
        {
            lblDetainDate.Text = DateTime.Now.ToString("dd/MMM/yyyy", System.Globalization.CultureInfo.InvariantCulture);
            lblCreatedByUser.Text = clsGlobal.CurrentUser.UserName;
        }

        private void frmDetainLicense_Load(object sender, EventArgs e)
        {
            _PerpareData();
        }

        private void _PerformDetainedSuccessfully()
        {
            MessageBox.Show("Detained License Successfully.",
                "Done",
                MessageBoxButtons.OK,
                MessageBoxIcon.Information
            );
        }

        private bool _PerformDetain()
        {
            decimal fees = decimal.Parse(txtFees.Text.Trim(),
                System.Globalization.CultureInfo.InvariantCulture);

            _detainedLicenseId = _license.DetainLicense(fees, clsGlobal.CurrentUser.UserId);
            bool isDetained = (_detainedLicenseId != null);

            if (isDetained)
            {
                _PerformDetainedSuccessfully();
                lblDetainId.Text = _detainedLicenseId.Value.ToString();
            }
            else
            {
                MessageBox.Show(
                    "Faild to Detain License",
                    "Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error
                );
            }

            return isDetained;
        }

        private void btnDetain_Click(object sender, EventArgs e)
        {
            if (!ValidateChildren())
            {
                MessageBox.Show(
                    "Please complete all required fields before saving",
                    "Invalid", MessageBoxButtons.OK,
                    MessageBoxIcon.Error
                );
                return;
            }

            DialogResult answerUser = MessageBox.Show(
                "Are you sure you want to detain the license?",
                "Confirm",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Warning,
                MessageBoxDefaultButton.Button2
            );

            if (answerUser != DialogResult.Yes)
            {
                return;
            }

            bool isDetained = _PerformDetain();

            if (isDetained)
            {
                ctrlDriverLicenseInfoWithFilter1.Reload();
                btnDetain.Enabled = false;
                ctrlDriverLicenseInfoWithFilter1.FilterEnable = false;
                txtFees.Enabled = false;
                lblShowLicenseInfo.Enabled = true;
                DetainedLicense(_detainedLicenseId.Value);
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

        private void txtFees_Validating(object sender, CancelEventArgs e)
        {
            string textFees = txtFees.Text.Trim();

            if (string.IsNullOrWhiteSpace(textFees))
            {
                e.Cancel = true;
                errorProvider1.SetError(txtFees, "Fees cannot be empty!");
                return;
            }

            if (!decimal.TryParse(textFees, System.Globalization.NumberStyles.Number,
                                  System.Globalization.CultureInfo.InvariantCulture, out decimal fees))
            {
                e.Cancel = true;
                errorProvider1.SetError(txtFees, "Invalid number format!");
                return;
            }

            if (fees < 0)
            {
                e.Cancel = true;
                errorProvider1.SetError(txtFees, "Fees cannot be negative!");
                return;
            }

            errorProvider1.SetError(txtFees, null);
        }

        private void txtFees_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (char.IsDigit(e.KeyChar) || char.IsControl(e.KeyChar))
                return;

            if (
                e.KeyChar == '.' &&
                (!txtFees.Text.Contains('.') ||
                txtFees.SelectedText.Contains('.'))
            )
            {
                return;
            }

            e.Handled = true;
        }
    }
}
