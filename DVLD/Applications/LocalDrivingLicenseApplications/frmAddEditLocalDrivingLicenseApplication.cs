using DVLD.Login;
using DVLD_Business;
using DVLD_Business.Applications;
using DVLD_Business.LicenseClasses;
using DVLD_Business.LocalDrivingLicenseApplications;
using DVLD_Business.Manage_Types;
using DVLD_Business.Users;
using System;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using DVLD_Shared;
using DVLD.GlobalClasses;
using DVLD_Business.Licenses;

namespace DVLD.LocalDrivingLicenseApplications
{
    public partial class frmAddEditLocalDrivingLicenseApplication : Form
    {
        public event Action<int> OnLocalDrivingLicenseApplicationSaved;
        protected virtual void LocalDrivingLicenseApplicationSaved(int value)
        {
            OnLocalDrivingLicenseApplicationSaved?.Invoke(value);
        }

        private enum enMode { Update, AddNew };

        private const string UPDATE_TITLE = "Update Local Driving License Application";
        private const string ADD_NEW_TITLE = "New Local Driving License Application";
        private clsLocalDrivingLicenseApplication _localApplication = null;
        private int? _localApplicationId = 0;
        private enMode _mode;

        public frmAddEditLocalDrivingLicenseApplication()
        {
            InitializeComponent();
            _HideTabHeaders();
            _mode = enMode.AddNew;
            this.AutoValidate = AutoValidate.EnableAllowFocusChange;
        }

        public frmAddEditLocalDrivingLicenseApplication(int localApplicationId)
        {
            InitializeComponent();
            _HideTabHeaders();
            _localApplicationId = localApplicationId;
            _mode = enMode.Update;
            this.AutoValidate = AutoValidate.EnableAllowFocusChange;
        }

        private void _HideTabHeaders()
        {
            tabCtrl.Appearance = TabAppearance.FlatButtons;
            tabCtrl.ItemSize = new Size(0, 1);
            tabCtrl.SizeMode = TabSizeMode.Fixed;
        }

        private void _PrepareUpdateMode()
        {
            _localApplication = clsLocalDrivingLicenseApplication.FindLocalDrivingLicenseApplication(_localApplicationId.Value);

            if (_localApplication == null)
            {
                MessageBox.Show(
                    "This form will be closed, because No Application with ID = " + _localApplicationId.Value,
                    "Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error
                );

                this.Close();
                return;
            }

            lblTitleForm.Text = UPDATE_TITLE;
            this.Text = UPDATE_TITLE;
            lblDLApplicationId.Text = _localApplication.ApplicationId.ToString();
            lblApplicationDate.Text = _localApplication.ApplicationDate.ToShortDateString();
            lblApplicationFees.Text = _localApplication.PaidFees.ToString();
            cbLicenseClasses.SelectedValue = _localApplication.LicenseClassId;
            lblCreatedBy.Text = _localApplication.CreatedByUserInfo.UserName;

            if (_localApplication.PersonInfo != null)
                ctrlPersonCardWithFilter1.LoadPerson(_localApplication.PersonInfo);

            ctrlPersonCardWithFilter1.FilterEnable = false;
        }

        private void _PrepareAddNewMode()
        {
            _localApplication = new clsLocalDrivingLicenseApplication();

            _localApplication.ApplicationTypeInfo = clsApplicationType.Find(enApplicationType.NewDrivingLicense);
            _localApplication.ApplicationTypeId = (int)_localApplication.ApplicationTypeInfo.ApplicationTypeId;
            _localApplication.ApplicationStatus = enApplicationStatus.New;
            _localApplication.ApplicationDate = DateTime.Today;
            _localApplication.LastStatusDate = DateTime.Now;
            _localApplication.PaidFees = _localApplication.ApplicationTypeInfo.ApplicationTypeFees;
            _localApplication.CreatedByUserId = clsGlobal.CurrentUser.UserId; 

            _FillDataToAddNew();
            ctrlPersonCardWithFilter1.Clear();

            lblTitleForm.Text = ADD_NEW_TITLE;
            this.Text = ADD_NEW_TITLE;
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

        private void _FillDataToAddNew()
        {
            lblDLApplicationId.Text = "N\\A";
            lblApplicationDate.Text = _localApplication.ApplicationDate.ToShortDateString();
            lblApplicationFees.Text = _localApplication.PaidFees.ToString();
            lblCreatedBy.Text = clsGlobal.CurrentUser.UserName;
        }

        private void _FillLicenseClasses()
        {
            DataTable dt = clsLicenseClass.GetAllLicenseClasses();

            cbLicenseClasses.DataSource = dt;
            cbLicenseClasses.DisplayMember = "ClassName";
            cbLicenseClasses.ValueMember = "LicenseClassID";

            cbLicenseClasses.DropDownStyle = ComboBoxStyle.DropDown;
            cbLicenseClasses.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
            cbLicenseClasses.AutoCompleteSource = AutoCompleteSource.ListItems;
            cbLicenseClasses.SelectedValue = 3;
        }

        private void _SetDefaultValuesToControls()
        {
            tabCtrl.SelectedIndex = 0;
            btnSave.Enabled = false;
        }

        private void frmAddNewLocalDrivingLicenseApplication_Load(object sender, EventArgs e)
        {
            _FillLicenseClasses();
            _PerpareMode();
            _SetDefaultValuesToControls();
        }

        private void _SetDataToSave()
        { 
            _localApplication.LicenseClassId = (int)cbLicenseClasses.SelectedValue;
        }

        private void _ConvertModeToUpdate()
        {
            _mode = enMode.Update;
            lblTitleForm.Text = UPDATE_TITLE;
            this.Text = UPDATE_TITLE;
            ctrlPersonCardWithFilter1.FilterEnable = false;
            lblDLApplicationId.Text = _localApplication.LocalDrivingLicenseApplicationId.ToString();
        }

        private void _PerformAddNew()
        {
            MessageBox.Show("Data Saved Successfully",
                    "Done",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information
            );

            _ConvertModeToUpdate();
        }

        private void _PerformUpdate()
        {
            MessageBox.Show(
                "Update Application Successfully",
                "Done",
                MessageBoxButtons.OK,
                MessageBoxIcon.Information
            );
        }

        private bool _PerformSave()
        {  
            _SetDataToSave();

            bool saved = _localApplication.Save();
            
            if (saved)
            {
                if (_mode == enMode.Update)
                {
                    _PerformUpdate();
                }
                else
                {
                    _PerformAddNew();
                }
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

            return saved;
        }

        private bool _IsFormDirty()
        {
            bool isLicenseClassChanged = _localApplication.LicenseClassId != (int)cbLicenseClasses.SelectedValue;
            return isLicenseClassChanged;
        }

        private bool _CheckViolateConstraints()
        {
            if (clsLicense.IsLicenseExistByPersonID(
                 _localApplication.PersonId,
                 (int)cbLicenseClasses.SelectedValue
             ))
            {
                MessageBox.Show(
                    "Person already have a license with the same applied driving class, Choose diffrent driving class",
                    "Not allowed",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error
                );

                return false;
            }


            bool isClassChanged = (_mode == enMode.Update && _localApplication.LicenseClassId != (int)cbLicenseClasses.SelectedValue);
            if (_mode == enMode.AddNew || isClassChanged)
            {
                int? activeApplicationId = clsApplication.GetActiveApplicationIDForLicenseClass(
                    _localApplication.PersonId,
                    enApplicationType.NewDrivingLicense,
                    (int)cbLicenseClasses.SelectedValue
                );

                if (activeApplicationId != null)
                {
                    MessageBox.Show(
                        "Choose another License Class, The selected Person Already have an active application "
                        + $"for the selected class with id = {activeApplicationId.Value}",
                        "Invalid", MessageBoxButtons.OK,
                        MessageBoxIcon.Error
                    );

                    return false ;
                }
            }

            int minimumAllowedAge = clsLicenseClass.GetMinimumAllowedAge((int)cbLicenseClasses.SelectedValue);

            if(minimumAllowedAge > _localApplication.PersonInfo.Age)
            {
                MessageBox.Show(
                    "Person is not allowed for this Driving License Class, "
                    + $"it requires a {minimumAllowedAge} years old and above",
                    "Invalid", MessageBoxButtons.OK,
                    MessageBoxIcon.Error
                );

                return false;
            }

            return true;
        }


        private void btnSave_Click(object sender, EventArgs e)
        {
            if (_mode == enMode.Update && !_IsFormDirty())
            {
                MessageBox.Show(
                    "No changes were made.",
                    "Information",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information
                );

                return;
            }

            if (!this.ValidateChildren())
            {
                MessageBox.Show(
                    "Please complete all required fields before saving",
                    "Invalid", MessageBoxButtons.OK,
                    MessageBoxIcon.Error
                );
                return;
            }

            if (!_CheckViolateConstraints())
                return;

            bool isSave = _PerformSave();

            if( isSave )
            {
                LocalDrivingLicenseApplicationSaved(
                    _localApplication.LocalDrivingLicenseApplicationId
                );
            }
        }

        private void tabCtrl_Selecting(object sender, TabControlCancelEventArgs e)
        {
            if (e.TabPage != tpApplicationInfo)
                return;

            if (ctrlPersonCardWithFilter1.SelectedPersonInfo == null)
            {
                MessageBox.Show(
                     "Please select a person to link to the Local Driving License Application.",
                     "Select A Person",
                     MessageBoxButtons.OK,
                     MessageBoxIcon.Error
                 );

                e.Cancel = true;
                return;
            }

            _localApplication.PersonInfo = ctrlPersonCardWithFilter1.SelectedPersonInfo;
            _localApplication.PersonId = ctrlPersonCardWithFilter1.PersonId.Value;
        }

        private void cbLicenseClasses_Validating(object sender, CancelEventArgs e)
        {
            if (cbLicenseClasses.SelectedIndex == -1)
            {
                errorProvider1.SetError(cbLicenseClasses, "Please select a license class from the list.");
                e.Cancel = true;
                return;
            }

            errorProvider1.SetError(cbLicenseClasses, null);
        }

        private void btnNext_Click(object sender, EventArgs e)
        {
            tabCtrl.SelectedTab = tpApplicationInfo;
        }

        private void btnBack_Click(object sender, EventArgs e)
        {
            tabCtrl.SelectedTab = tpPersonalInfo;
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void tabCtrl_SelectedIndexChanged(object sender, EventArgs e)
        {
            btnSave.Enabled = (tabCtrl.SelectedIndex == 1);
        }

        private void frmAddEditLocalDrivingLicenseApplication_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (_localApplication == null)
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

        private void frmAddEditLocalDrivingLicenseApplication_Activated(object sender, EventArgs e)
        {
            ctrlPersonCardWithFilter1.FilterFocus();
        }

        private void ctrlPersonCardWithFilter1_OnPersonSelected(int obj)
        {
            _localApplication.PersonInfo = ctrlPersonCardWithFilter1.SelectedPersonInfo;
            _localApplication.PersonId = ctrlPersonCardWithFilter1.PersonId.Value;
        }

        private void ctrlPersonCardWithFilter1_OnPersonUpdated(int obj)
        {
            _localApplication.PersonInfo = ctrlPersonCardWithFilter1.SelectedPersonInfo;
        }
    }
}
