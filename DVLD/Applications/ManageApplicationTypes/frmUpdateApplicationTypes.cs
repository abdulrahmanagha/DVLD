using DVLD_Business.Manage_Types;
using DVLD_Shared;
using System;
using System.ComponentModel;
using System.Linq;
using System.Windows.Forms;

namespace DVLD.Manage_Types
{
    public partial class frmUpdateApplicationTypes : Form
    {
        public event Action<int> OnApplicationTypeUpdated;

        protected virtual void ApplicationTypeUpdated(int applicationId)
        {
            OnApplicationTypeUpdated?.Invoke(applicationId);
        }

        private enApplicationType _applicationTypeId;
        private clsApplicationType _applicationType = null;

        public frmUpdateApplicationTypes(enApplicationType applicationTypeId)
        {
            InitializeComponent();
            _applicationTypeId = applicationTypeId;

            this.AutoValidate = AutoValidate.EnableAllowFocusChange;
        }

        private void LoadData()
        {
            _applicationType = clsApplicationType.Find(_applicationTypeId);

            if( _applicationType == null )
            {
                MessageBox.Show(
                   "This form will be closed, because No Application Type with ID = " + (int)_applicationTypeId,
                   "Error",
                   MessageBoxButtons.OK,
                   MessageBoxIcon.Error
               );

                this.Close();
                return;
            }

            lblApplicationId.Text = ((int)_applicationType.ApplicationTypeId).ToString();
            txtApplicationTitle.Text = _applicationType.ApplicationTypeTitle;
            txtApplicationFees.Text = _applicationType.ApplicationTypeFees.ToString(
                System.Globalization.CultureInfo.InvariantCulture
            );

            txtApplicationTitle.Select();
        }
    
        private bool _PerformSave()
        {
            _applicationType.ApplicationTypeTitle = txtApplicationTitle.Text.Trim();
            _applicationType.ApplicationTypeFees = decimal.Parse(txtApplicationFees.Text.Trim(), 
                System.Globalization.CultureInfo.InvariantCulture);

            bool isValidUpdated = _applicationType.Save();
            if (isValidUpdated)
            {
                MessageBox.Show(
                     "Update Application Type Successfully",
                     "Done",
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

            return isValidUpdated;
        }

        private bool _IsFormDirty()
        {
            bool isTitleChanged = txtApplicationTitle.Text.Trim() != _applicationType.ApplicationTypeTitle;

            decimal currentFees = 0;
            bool isFeesValid = decimal.TryParse(txtApplicationFees.Text.Trim(),
                   System.Globalization.NumberStyles.Number,
                   System.Globalization.CultureInfo.InvariantCulture,
                   out currentFees);

            bool isFeesChanged = isFeesValid && (currentFees != _applicationType.ApplicationTypeFees);

            return (isTitleChanged || isFeesChanged);
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if(!ValidateChildren())
            {
                MessageBox.Show(
                    "Please complete all required fields before saving",
                    "Invalid", MessageBoxButtons.OK,
                    MessageBoxIcon.Error
                );
                return;
            }

            if(!_IsFormDirty())
            {
                MessageBox.Show(
                    "No changes were made.",
                    "Information",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information
                );

                return;
            }

            bool isValid = _PerformSave();

            if(isValid)
            {
                ApplicationTypeUpdated((int)_applicationType.ApplicationTypeId);
            }
        }

        private void frmUpdateApplicationTypes_Load(object sender, EventArgs e)
        {
            LoadData();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void txtApplicationFees_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (char.IsDigit(e.KeyChar) || char.IsControl(e.KeyChar))
                return;

            if (
                e.KeyChar == '.' && 
                (!txtApplicationFees.Text.Contains('.') || 
                txtApplicationFees.SelectedText.Contains('.'))
            )
            {
                return;
            }

            e.Handled = true;
        }

        private void txtApplicationTitle_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                txtApplicationFees.Select();
        }

        private void txtApplicationTitle_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.IsInputKey = true;
            }
        }

        private void frmUpdateApplicationTypes_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (_applicationType == null)
                return;

            if (_IsFormDirty())
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

        private void txtApplicationTitle_Validating(object sender, CancelEventArgs e)
        {
            if(string.IsNullOrWhiteSpace(txtApplicationTitle.Text))
            {
                e.Cancel = true;
                errorProvider1.SetError(txtApplicationTitle, "Title cannot be empty!");
                return;
            }

            errorProvider1.SetError(txtApplicationTitle, null);
        }

        private void txtApplicationFees_Validating(object sender, CancelEventArgs e)
        {
            string textFees = txtApplicationFees.Text.Trim();

            if (string.IsNullOrWhiteSpace(textFees))
            {
                e.Cancel = true;
                errorProvider1.SetError(txtApplicationFees, "Fees cannot be empty!");
                return;
            }

            if (!decimal.TryParse(textFees, System.Globalization.NumberStyles.Number,
                                  System.Globalization.CultureInfo.InvariantCulture, out decimal fees))
            {
                e.Cancel = true;
                errorProvider1.SetError(txtApplicationFees, "Invalid number format!");
                return;
            }

            if (fees < 0)
            {
                e.Cancel = true;
                errorProvider1.SetError(txtApplicationFees, "Fees cannot be negative!");
                return;
            }

            errorProvider1.SetError(txtApplicationFees, null);
        }

        private void txtApplicationFees_Enter(object sender, EventArgs e)
        {
            if (txtApplicationFees.Text.Trim() == "0")
            {
                txtApplicationFees.SelectAll();
            }
        }

        private void txtApplicationFees_Leave(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtApplicationFees.Text))
            {
                txtApplicationFees.Text = "0";
            }
        }

    }
}
