using DVLD_Business.Manage_Types;
using DVLD_Business.ManageTestTypes;
using System;
using System.ComponentModel;
using System.Linq;
using System.Windows.Forms;
using DVLD_Shared;

namespace DVLD.ManageTestTypes
{
    public partial class frmUpdateTestTypes : Form
    {
        public event Action<int> OnTestTypeUpdated;

        protected virtual void TestTypeUpdated(int testTypeId)
        {
            OnTestTypeUpdated?.Invoke(testTypeId);
        }

        private enTestType _testTypeId = enTestType.VisionTest;
        private clsTestType _testType = null;

        public frmUpdateTestTypes(enTestType testTypeId)
        {
            InitializeComponent();
            _testTypeId = testTypeId;

            this.AutoValidate = AutoValidate.EnableAllowFocusChange;
        }

        private void LoadData()
        {
            _testType = clsTestType.Find((enTestType)_testTypeId);

            if (_testType == null)
            {
                MessageBox.Show(
                   "This form will be closed, because No Test Type with ID = " + (int)_testTypeId,
                   "Error",
                   MessageBoxButtons.OK,
                   MessageBoxIcon.Error
               );

                this.Close();
                return;
            }

            lblTestId.Text = ((int)_testType.TestTypeId).ToString();
            txtTestTitle.Text = _testType.TestTypeTitle;
            txtTestDescription.Text = _testType.TestTypeDescription;
            txtTestFees.Text = _testType.TestTypeFees.ToString(
                System.Globalization.CultureInfo.InvariantCulture
            );

            txtTestTitle.Select();
        }

        private bool _PerformSave()
        {
            _testType.TestTypeTitle = txtTestTitle.Text.Trim();
            _testType.TestTypeDescription = txtTestDescription.Text.Trim();
            _testType.TestTypeFees = decimal.Parse(txtTestFees.Text.Trim(),
                System.Globalization.CultureInfo.InvariantCulture);

            bool isValidUpdated = _testType.Update();
            if (isValidUpdated)
            {
                MessageBox.Show(
                     "Update Test Type Successfully",
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
            bool isTestTypeTitleChanged = txtTestTitle.Text.Trim() != _testType.TestTypeTitle;
            bool isTestTypeDescriptionChanged = txtTestDescription.Text.Trim() != _testType.TestTypeDescription;

            decimal currentFees = 0;
            bool isFeesValid = decimal.TryParse(txtTestFees.Text.Trim(),
                   System.Globalization.NumberStyles.Number,
                   System.Globalization.CultureInfo.InvariantCulture,
                   out currentFees);

            bool isFeesChanged = isFeesValid && (currentFees != _testType.TestTypeFees);

            return isTestTypeTitleChanged || isTestTypeDescriptionChanged || isFeesChanged;
        }

        private void btnSave_Click(object sender, EventArgs e)
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

            if (!_IsFormDirty())
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

            if (isValid)
            {
                TestTypeUpdated((int)_testType.TestTypeId);
            }
        }

        private void frmUpdateTestTypes_Load(object sender, EventArgs e)
        {
            LoadData();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void txtTestFees_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (char.IsDigit(e.KeyChar) || char.IsControl(e.KeyChar))
                return;

            if (
                e.KeyChar == '.' &&
                (!txtTestFees.Text.Contains('.') ||
                txtTestFees.SelectedText.Contains('.'))
            )
            {
                return;
            }

            e.Handled = true;
        }

        private void txtTestTitle_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                txtTestDescription.Select();
        }

        private void txtTestDescription_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                txtTestFees.Select();
        }

        private void txtTestTitle_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.IsInputKey = true;
            }
        }

        private void txtTestDescription_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.IsInputKey = true;
            }
        }

        private void frmUpdateTestTypes_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (_testType == null)
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

        private void txtTestTitle_Validating(object sender, CancelEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtTestTitle.Text))
            {
                e.Cancel = true;
                errorProvider1.SetError(txtTestTitle, "Title cannot be empty!");
                return;
            }

            errorProvider1.SetError(txtTestTitle, null);
        }

        private void txtTestDescription_Validating(object sender, CancelEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtTestDescription.Text))
            {
                e.Cancel = true;
                errorProvider1.SetError(txtTestDescription, "Description cannot be empty!");
                return;
            }

            errorProvider1.SetError(txtTestDescription, null);
        }

        private void txtTestFees_Validating(object sender, CancelEventArgs e)
        {
            string textFees = txtTestFees.Text.Trim();

            if (string.IsNullOrWhiteSpace(textFees))
            {
                e.Cancel = true;
                errorProvider1.SetError(txtTestFees, "Fees cannot be empty!");
                return;
            }

            if (!decimal.TryParse(textFees, System.Globalization.NumberStyles.Number,
                                  System.Globalization.CultureInfo.InvariantCulture, out decimal fees))
            {
                e.Cancel = true;
                errorProvider1.SetError(txtTestFees, "Invalid number format!");
                return;
            }

            if (fees < 0)
            {
                e.Cancel = true;
                errorProvider1.SetError(txtTestFees, "Fees cannot be negative!");
                return;
            }

            errorProvider1.SetError(txtTestFees, null);
        }

        private void txtTestFees_Enter(object sender, EventArgs e)
        {
            if (txtTestFees.Text.Trim() == "0")
            {
                txtTestFees.SelectAll();
            }
        }

        private void txtTestFees_Leave(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtTestFees.Text))
            {
                txtTestFees.Text = "0";
            }
        }
    }
}
