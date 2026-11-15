using DVLD.Licenses;
using DVLD_Business.Applications;
using DVLD_Business.LocalDrivingLicenseApplications;
using DVLD_Business.Users;
using DVLD_DataAccess.Tests;
using System;
using System.Drawing;
using System.Windows.Forms;
using DVLD_Shared;

namespace DVLD.AppointmentTests.Controls
{
    public partial class ctrlDrivingLicenseApplicationInfo : UserControl
    {
        private clsLocalDrivingLicenseApplication _localApplication = null;
        private int? _localApplicationId = null;
        private int _passedTests = 0;

        public ctrlDrivingLicenseApplicationInfo()
        {
            InitializeComponent();
        }

        public clsLocalDrivingLicenseApplication GetLocalApplication
        {
            get
            {
                return _localApplication;
            }
        }

        public int GetApplicationId
        {
            get
            {
                return _localApplication.ApplicationId;
            }
        }

        public bool ShowLicenseInfo
        {
            set
            {
                lblShowLicenseInfo.Visible = value;
                pbShowLicenseInfo.Visible = value;
            }

            get
            {
                return lblShowLicenseInfo.Visible;
            }
        }


        public bool EnableLicenseInfo
        {
            set
            {
                lblShowLicenseInfo.Enabled = value;
            }

            get
            {
                return lblShowLicenseInfo.Enabled;
            }
        }

        private void _ResetDefaultValues()
        {
            _localApplication = null;
            _localApplicationId = null;

            gbDrivingLicneseApplication.Text = "Driving License Application Info";
            lblDLApplicationId.Text = "N\\A";
            lblLicenseClassName.Text = "N\\A";
            lblPassedTests.Text = "N\\A";
        }

        private void _ChageColorPassedTests()
        {
            switch(_passedTests)
            {
                case 0:
                    lblPassedTests.ForeColor = Color.Crimson;
                    break;

                case 1:
                    lblPassedTests.ForeColor = Color.DarkOrange;
                    break;

                case 2:
                    lblPassedTests.ForeColor = Color.SteelBlue;
                    break;

                case 3:
                    lblPassedTests.ForeColor = Color.ForestGreen;
                    break;

                default:
                    lblPassedTests.ForeColor = SystemColors.ControlText;
                    break;
            }
        }

        public int SetPassedTests
        {
            set
            {
                _passedTests = value;
                lblPassedTests.Text = _passedTests.ToString() + "/3";
            }

            get { return _passedTests; }
        }

        private void _FillApplicationInfo()
        {
            _localApplicationId = _localApplication.LocalDrivingLicenseApplicationId;
            lblDLApplicationId.Text = _localApplicationId.ToString();
            lblLicenseClassName.Text = _localApplication.LicenseClassInfo.ClassName;

            _passedTests = clsTest.GetPassedTestsCount(_localApplicationId.Value);
            lblPassedTests.Text = _passedTests + "/3";
        }

        private bool CheckValidApplication()
        {
            if (_localApplication == null)
            {
                _ResetDefaultValues();

                MessageBox.Show(
                    $"Not Found Local Driving License Application.",
                    "Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error
                );

                return false;
            }

            return true;
        }

        private void _LoadLocalApplication()
        {
            if (CheckValidApplication())
            {
                _FillApplicationInfo();
                ctrlApplicationInfo1.LoadApplication(_localApplication.ApplicationId);
            }

            this.EnableLicenseInfo = (_localApplication.ApplicationStatus == enApplicationStatus.Completed);
        }

        public void LoadApplication(int localApplicationId)
        {
            _localApplication = clsLocalDrivingLicenseApplication.FindLocalDrivingLicenseApplication(localApplicationId);
            _LoadLocalApplication();
        }

        public void LoadApplication(clsLocalDrivingLicenseApplication localApplication)
        {
            _localApplication = localApplication;
            _LoadLocalApplication();
        }

        private void lblPassedTests_TextChanged(object sender, EventArgs e)
        {
            _ChageColorPassedTests();
        }

        private void lblShowLicenseInfo_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            int? licenseId = _localApplication.GetActiveLicenseID();

            if (licenseId == null)
            {
                MessageBox.Show(
                    "Not found active licese for this application",
                    "Not Allowed",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error
                );

                return;
            }

            using (var frm = new frmShowLicenseInfo(licenseId.Value))
            {
                frm.ShowDialog();
            }
        }
    }
}
