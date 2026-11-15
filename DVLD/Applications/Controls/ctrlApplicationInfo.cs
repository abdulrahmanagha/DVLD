using DVLD.People.Controls;
using DVLD_Business;
using DVLD_Business.Applications;
using DVLD_Business.Users;
using System.Windows.Forms;

namespace DVLD.ManageApplicationTypes.Controls
{
    public partial class ctrlApplicationInfo : UserControl
    {
        private clsApplication _application = null;
        private int? _applicationId = null;

        public ctrlApplicationInfo()
        {
            InitializeComponent();
        }

        private void _ResetDefaultValues()
        {
            _application = null;
            _applicationId = null;

            gbApplicationInfo.Text = "Application Basic Info";
            lblApplicationId.Text = "N\\A";
            lblStatus.Text = "N\\A";
            lblFees.Text = "N\\A";
            lblType.Text = "N\\A";
            lblApplicant.Text ="N\\A";
            lblDate.Text = "N\\A";
            lblStatusDate.Text = "N\\A";
            lblCreatedByUser.Text = "N\\A";
        }

        private void _FillApplicationInfo()
        {
            _applicationId = _application.ApplicationId;

            lblApplicationId.Text = _applicationId.ToString();
            lblStatus.Text = _application.ApplicationStatus.ToString();
            lblFees.Text = _application.PaidFees.ToString();
            lblType.Text = _application.ApplicationTypeInfo.ApplicationTypeTitle;
            lblApplicant.Text = _application.PersonInfo.GetName();
            lblDate.Text = _application.ApplicationDate.ToShortDateString();
            lblStatusDate.Text = _application.LastStatusDate.ToShortDateString();
            lblCreatedByUser.Text = clsUser.Find(_application.CreatedByUserId).UserName;
        }

        private bool CheckValidApplication()
        {
            if (_application == null)
            {
                _ResetDefaultValues();

                MessageBox.Show(
                    $"Not Found Application.",
                    "Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error
                );

                return false;
            }

            return true;
        }

        private void _LoadApplication()
        {
            if(CheckValidApplication())
                _FillApplicationInfo();
        }

        public void LoadApplication(int applicationId)
        {
            _application = clsApplication.FindBaseApplication(applicationId);
            _LoadApplication();
        }

        public void LoadApplication(clsApplication application)
        {
            _application = application;
            _LoadApplication();
        }

        private void lblViewPersonInfo_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            using (var frm = new frmShowPersonInfo(_application.PersonId))
            {
                frm.OnPersonUpdated += ctrlApplicationInfo_PersonUpdated;
                frm.ShowDialog();
            }
        }

        private void ctrlApplicationInfo_PersonUpdated(int personId)
        {
            _application.PersonInfo = clsPerson.Find(personId);
            lblApplicant.Text = _application.PersonInfo.GetName();
        }
    }
}
