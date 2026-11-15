using DVLD.Properties;
using DVLD_Business;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using System.IO;
using DVLD.Login;
using DVLD.GlobalClasses;

namespace DVLD.People
{
    public partial class frmAddEditPerson : Form
    {
        public event Action<object, int> OnPersonSaved;
        protected virtual void PersonSaved(int personId)
        {
            OnPersonSaved?.Invoke(this, personId);
        }

        private enum enValidationState { NotValidated, Valid, Invalid }
        private enum enMode { Update, AddNew };

        private enValidationState _nationalNoState = enValidationState.NotValidated;
        private enValidationState _emailState = enValidationState.NotValidated;
        private const string UPDATE_TITLE = "Update Person";
        private const string ADD_NEW_TITLE = "Add New Person";
        private Image _imageMan = Resources.Person_Man;
        private Image _imageWoman = Resources.Person_Woman;
        private bool _ignoreStateRadioButton;
        private clsPerson _person;
        private int _personId;
        private enMode _mode;
        private string _ImagePathLocation = null;

        public frmAddEditPerson()
        {
            InitializeComponent();
            _mode = enMode.AddNew;

            this.AutoValidate = AutoValidate.EnableAllowFocusChange;
        }

        public frmAddEditPerson(int personId)
        {
            InitializeComponent();
            _personId = personId;
            _mode = enMode.Update;
        }

        private void _SetDefaultImage()
        {
            Image img = (_person.Gender == 0) ? _imageMan : _imageWoman;
            _LoadImage(img);
        }

        private void _ChangeDefaultImage()
        {
            Image img = (rbMale.Checked) ? _imageMan : _imageWoman;
            _LoadImage(img);
        }

        private void _UpdateImageStates()
        {
            bool pathIsFound = !string.IsNullOrWhiteSpace(_person.ImagePath) && File.Exists(_person.ImagePath);

            if (pathIsFound)
            {
                _LoadImage(_person.ImagePath);
            }
            else
            {
                _SetDefaultImage();
            }

            lblRemoveImage.Visible = pathIsFound;
        }

        private void _PrepareUpdateMode()
        {
            _person = clsPerson.Find(_personId);

            if(_person == null)
            {
                MessageBox.Show(
                    "This form will be closed, because NO person with Id = " + _personId,
                    "Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error
                );

                this.Close();
                return;
            }

            lblTitleForm.Text = UPDATE_TITLE;
            lblPersonId.Text = _person.PersonId.ToString();
            txtNationalNo.Text = _person.NationalNo;
            txtFirstName.Text = _person.FirstName;
            txtSecondName.Text = _person.SecondName;
            txtThirdName.Text = _person.ThirdName;
            txtLastName.Text = _person.LastName;
            txtEmail.Text = _person.Email;
            txtPhone.Text = _person.Phone;
            txtAddress.Text = _person.Address;
            pDateOfBirth.Value = _person.DateOfBirth;
            cbCountries.SelectedValue = _person.NationalityCountryId;
            rbMale.Checked = (_person.Gender == 0);
            rbFemale.Checked = (_person.Gender != 0);
            _UpdateImageStates();
        }

        private void _PrepareAddNewMode()
        {
            _person = new clsPerson();
            lblTitleForm.Text = ADD_NEW_TITLE;
            lblRemoveImage.Visible = false;
        }

        private void _PerpareMode()
        {
            if (_mode == enMode.AddNew)
            {
                _PrepareAddNewMode();
            }
            else
            {
                _PrepareUpdateMode();
            }
        }
        
        private void _FillCountriesInComboBox()
        {
            DataTable db = clsCountry.GetAllCountries();
           
            cbCountries.DataSource = db;
            cbCountries.DisplayMember = "CountryName";
            cbCountries.ValueMember = "CountryID";

            cbCountries.DropDownStyle = ComboBoxStyle.DropDown;
            cbCountries.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
            cbCountries.AutoCompleteSource = AutoCompleteSource.ListItems;
            cbCountries.SelectedIndex = cbCountries.FindString("Jordan");
        }

        private void _PerperFileDialog()
        {
            openFileDialog1.FileName = null;
            openFileDialog1.Filter = "Image Files|*.jpg;*.jpeg;*.png;*.bmp;*.gif";

            openFileDialog1.Title = "Select Person Image";
            openFileDialog1.CheckFileExists = true;
            openFileDialog1.CheckPathExists = true;
            openFileDialog1.Multiselect = false;
            openFileDialog1.RestoreDirectory = true;
        }

        private void _PerperDateTimePicker()
        {
            DateTime today = DateTime.Today;

            pDateOfBirth.MaxDate = today.AddYears(-18);
            pDateOfBirth.MinDate = today.AddYears(-200);
            pDateOfBirth.Value = pDateOfBirth.MaxDate;
        }

        private void _PerperGenderControls()
        {
            rbMale.Checked = true;
            _LoadImage(_imageMan);
        }

        private void _SetTagsToControls()
        {
            txtFirstName.Tag = "First Name";
            txtSecondName.Tag = "Second Name";
            txtLastName.Tag = "Last Name";
            txtPhone.Tag = "Phone";
            txtAddress.Tag = "Address";
            txtNationalNo.Tag = "National No";
        }

        private void _ResetDefaultValues()
        {
            pbPersonImage.Image = null;
            _PerperFileDialog();
            _PerperGenderControls();
            _PerperDateTimePicker();
            _FillCountriesInComboBox();
            _SetTagsToControls();
        }

        private void frmAddEditPerson_Load(object sender, EventArgs e)
        {
            _ignoreStateRadioButton = true;
            _ResetDefaultValues();
            _PerpareMode();
            _ignoreStateRadioButton = false;
        }

        private void _SetDataToSave()
        {
            _person.NationalNo = txtNationalNo.Text.Trim();
            _person.FirstName = txtFirstName.Text.Trim();
            _person.SecondName = txtSecondName.Text.Trim();
            _person.ThirdName = (string.IsNullOrWhiteSpace(txtThirdName.Text)) ? null : txtThirdName.Text.Trim();
            _person.LastName = txtLastName.Text.Trim();
            _person.Email = (string.IsNullOrWhiteSpace(txtEmail.Text)) ? null : txtEmail.Text.ToLower().Trim();
            _person.Phone = txtPhone.Text.Trim();
            _person.Address = txtAddress.Text.Trim();
            _person.DateOfBirth = pDateOfBirth.Value;
            _person.NationalityCountryId = Convert.ToInt16(cbCountries.SelectedValue);
            _person.Gender = (rbMale.Checked) ? Convert.ToByte(0) : Convert.ToByte(1);
            _person.ImagePath = _ImagePathLocation;
        }

        private void _PerformUpdate()
        {
            MessageBox.Show(
                "Update Person Successfully", 
                "Done", 
                MessageBoxButtons.OK, 
                MessageBoxIcon.Information
            );
        }

        private void _ConvertModeToUpdate()
        {
            _mode = enMode.Update;
            _personId = _person.PersonId;
            lblTitleForm.Text = UPDATE_TITLE;
            lblPersonId.Text = _person.PersonId.ToString();
        }

        private void _PerformAddNew()
        {
            MessageBox.Show("Add New Person Successfully", 
                "Done", 
                MessageBoxButtons.OK, 
                MessageBoxIcon.Information
            );

            _ConvertModeToUpdate();
        }

        private bool _PerformSave()
        {
            _SetDataToSave();

            if (_person.Save())
            {
                _ImagePathLocation = _person.ImagePath;

                if (_mode == enMode.Update)
                {
                    _PerformUpdate();
                }
                else
                {
                    _PerformAddNew();
                }

                return true;
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

            return false;
        }

        private bool _ValidateRequiredTextBox(TextBox txt)
        {
            if (string.IsNullOrWhiteSpace(txt.Text))
            {
                string fieldName = txt.Tag?.ToString() ?? "This field";
                errorProvider1.SetError(txt, $"{fieldName} is required.");
                return false;
            }

            errorProvider1.SetError(txt, "");
            return true;
        }

        private bool _ValidateBeforeSave()
        {
            if(!ValidateChildren())
            {
                return false;
            }

            bool isValid = true;

            TextBox[] requiredFields = new TextBox[]
            {
                txtNationalNo,
                txtFirstName,
                txtSecondName,
                txtLastName,
                txtPhone,
                txtAddress
            };

            foreach (TextBox txt in requiredFields)
            {
                if (!_ValidateRequiredTextBox(txt))
                {
                    isValid = false;
                }
            }

            if (!isValid)
                return false;

            if(_nationalNoState != enValidationState.Valid)
                _PerformValidateNationalNo();

            if(_emailState != enValidationState.Valid)
                _PerformValidateEmail();

            return (_nationalNoState == enValidationState.Valid &&
                _emailState == enValidationState.Valid);
        }

        private bool _IsFormDirty()
        {
            if (_mode == enMode.AddNew)
            {
                bool hasText = !string.IsNullOrWhiteSpace(txtFirstName.Text) ||
                               !string.IsNullOrWhiteSpace(txtSecondName.Text) ||
                               !string.IsNullOrWhiteSpace(txtThirdName.Text) ||
                               !string.IsNullOrWhiteSpace(txtLastName.Text) ||
                               !string.IsNullOrWhiteSpace(txtNationalNo.Text) ||
                               !string.IsNullOrWhiteSpace(txtPhone.Text) ||
                               !string.IsNullOrWhiteSpace(txtEmail.Text) ||
                               !string.IsNullOrWhiteSpace(txtAddress.Text);

                bool hasImage = !string.IsNullOrEmpty(_ImagePathLocation);

                return hasText || hasImage;
            }

            bool isEmailChange;

            if (string.IsNullOrWhiteSpace(txtEmail.Text))
                isEmailChange = !string.IsNullOrEmpty(_person.Email);
            else
                isEmailChange = txtEmail.Text.ToLower().Trim() != _person.Email;

            bool isThirdNameChange;

            if (string.IsNullOrWhiteSpace(txtThirdName.Text))
                isThirdNameChange = !string.IsNullOrEmpty(_person.ThirdName);
            else
                isThirdNameChange = txtThirdName.Text.Trim() != _person.ThirdName;

            bool isFirstNameChange = txtFirstName.Text.Trim() != _person.FirstName;
            bool isSecondNameChange = txtSecondName.Text.Trim() != _person.SecondName;
            bool isLastNameChange = txtLastName.Text.Trim() != _person.LastName;
            bool isNationalNoChange = txtNationalNo.Text.Trim() != _person.NationalNo;
            bool isPhoneChange = txtPhone.Text.Trim() != _person.Phone;
            bool isAddressChange = txtAddress.Text.Trim() != _person.Address;
            bool isGenderChange = (rbMale.Checked ? 0 : 1) != _person.Gender;
            bool isCountryChange = Convert.ToInt32(cbCountries.SelectedValue) != _person.NationalityCountryId;
            bool isDOBChange = pDateOfBirth.Value.Date != _person.DateOfBirth.Date;
            bool isImagePathChange = _ImagePathLocation != _person.ImagePath;

            return isFirstNameChange || isSecondNameChange || isThirdNameChange ||
                isLastNameChange || isNationalNoChange || isEmailChange || isPhoneChange ||
                isAddressChange || isGenderChange || isCountryChange || isDOBChange || isImagePathChange;
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if ((_mode == enMode.Update) && !_IsFormDirty())
            {
                MessageBox.Show(
                    "No changes were made.",
                    "Information",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information
                );

                return;
            }

            if(!_ValidateBeforeSave())
            {
                MessageBox.Show(
                    "Please complete all required fields before saving",
                    "Invalid", MessageBoxButtons.OK,
                    MessageBoxIcon.Error
                );

                return;
            }

            bool isSave = _PerformSave();

            if (isSave)
            {
                clsGlobal.SyncIfCurrentPerson(_person.PersonId); 
                PersonSaved(_person.PersonId);
            }
        }

        private void _LoadImage(string path)
        {
            bool isLoaded = clsGlobal.LoadImage(path, pbPersonImage);
            
            if(!isLoaded)
            {
                MessageBox.Show(
                    "Failed to load the image. Please check the file path or format.",
                    "Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error
                );

                return;
            }

            _ImagePathLocation = path;
        }

        private void _LoadImage(Image image)
        {
            clsGlobal.LoadImage(image, pbPersonImage);
            _ImagePathLocation = null;
        }

        private void _RemoveImage()
        {
            clsGlobal.DisposeCurrentImage(pbPersonImage);
            lblRemoveImage.Visible = false;
        }

        private void lblSetImage_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            if (openFileDialog1.ShowDialog(this) == DialogResult.OK)
            {
                _LoadImage(openFileDialog1.FileName);
                lblRemoveImage.Visible = true;
            }
        }

        private void lblRemoveImage_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            _RemoveImage();
            _ChangeDefaultImage();
        }

        private void ChooseGender_CheckedChanged(object sender , EventArgs e)
        {
            RadioButton rb = sender as RadioButton;

            if (rb.Checked && !_ignoreStateRadioButton)
            {
                if(_ImagePathLocation == null)
                {
                    _ChangeDefaultImage();
                }
            }
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }

        private bool _ValidateEmail()
        {
            if (string.IsNullOrWhiteSpace(txtEmail.Text))
            {
                errorProvider1.SetError(txtEmail, "");
                return true;
            }

            string email = txtEmail.Text;
            List<string> errors = new List<string>();

            if (!clsValidation.IsValidUserNameStart(email))
                errors.Add("The email must start with a letter or number.");

            if (!clsValidation.IsSingleAtSymbol(email))
                errors.Add("The email must contain exactly one '@' symbol.");

            if (!clsValidation.IsKnownDomainName(email))
                errors.Add("The domain name must be recognized (e.g., gmail, yahoo).");

            if (!clsValidation.IsValidTLD(email))
                errors.Add("The email must end with a valid extension like '.com' or '.net'.");

            if (errors.Any())
            {
                errorProvider1.SetError(txtEmail, string.Join("\n", errors));
                return false;
            }

            errorProvider1.SetError(txtEmail, "");
            return true;
        }

        private void txtEmail_Validating(object sender, CancelEventArgs e)
        {
            if(_emailState == enValidationState.NotValidated)
            {
                _PerformValidateEmail();
            }
        }

        private bool _ValidateNationalNo()
        {
            string nationalNo = txtNationalNo.Text;

            if (string.IsNullOrWhiteSpace(nationalNo))
            {
                errorProvider1.SetError(txtNationalNo, "National number is required");
                return false;
            }

            bool isFoundPerson = clsPerson.IsExistPerson(nationalNo);

            if (isFoundPerson)
            {
                if (_mode == enMode.AddNew ||
                    nationalNo != _person.NationalNo)
                {
                    errorProvider1.SetError(
                        txtNationalNo,
                        "National number is used for another person."
                    );

                    return false;
                }
            }

            errorProvider1.SetError(txtNationalNo, "");
            return true;
        }

        private void txtNationalNo_Validating(object sender, CancelEventArgs e)
        {
            if (_nationalNoState == enValidationState.NotValidated)
            {
                _PerformValidateNationalNo();
            }
        }

        private void txtNationalNo_TextChanged(object sender, EventArgs e)
        {
            _nationalNoState = enValidationState.NotValidated;
        }

        private void txtEmail_TextChanged(object sender, EventArgs e)
        {
            _emailState = enValidationState.NotValidated;
        }

        private void frmAddEditPerson_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (_person == null)
                return;

            if (_mode == enMode.Update && _IsFormDirty())
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

        private void _PerformValidateNationalNo()
        {
            bool isValid = _ValidateNationalNo();

            _nationalNoState = (isValid) ?
                enValidationState.Valid :
                enValidationState.Invalid;
        }

        private void _PerformValidateEmail()
        {
            bool isValid = _ValidateEmail();

            _emailState = (isValid) ?
                enValidationState.Valid :
                enValidationState.Invalid;
        }

        private void PropertiesRequired(object sender)
        {
            if (!(sender is TextBox txt))
                return;

            if (string.IsNullOrWhiteSpace(txt.Text))
            {
                string fieldName = txt.Tag?.ToString();

                if (!string.IsNullOrWhiteSpace(fieldName))
                {
                    errorProvider1.SetError(txt, $"{fieldName} is required");
                }

                return;
            }

            errorProvider1.SetError(txt, "");
        }

        private void _PerformValidateFirstName()
        {
            PropertiesRequired(txtFirstName);
        }

        private void _PerformValidateSecondName()
        {
            PropertiesRequired(txtSecondName);
        }

        private void _PerformValidateLastName()
        {
            PropertiesRequired(txtLastName);
        }

        private void _PerformValidatePhone()
        {
            PropertiesRequired(txtPhone);
        }

        private void _PerformValidateAddress()
        {
            PropertiesRequired(txtAddress);
        }

        private void txtFirstName_Validating(object sender, CancelEventArgs e)
        {
            _PerformValidateFirstName();
        }

        private void txtSecondName_Validating(object sender, CancelEventArgs e)
        {
            _PerformValidateSecondName();
        }

        private void txtLastName_Validating(object sender, CancelEventArgs e)
        {
            _PerformValidateLastName();
        }

        private void txtPhone_Validating(object sender, CancelEventArgs e)
        {
            _PerformValidatePhone();
        }

        private void txtAddress_Validating(object sender, CancelEventArgs e)
        {
            _PerformValidateAddress();
        }

        private void cbCountries_Validating(object sender, CancelEventArgs e)
        {
            if(cbCountries.SelectedIndex == -1)
            {
                errorProvider1.SetError(cbCountries, "Please select a country from the list.");
                e.Cancel = true;
                return;
            }

            errorProvider1.SetError(cbCountries, null);
        }
    }
}
