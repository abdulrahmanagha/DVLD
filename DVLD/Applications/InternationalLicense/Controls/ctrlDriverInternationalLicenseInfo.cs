using DVLD.Properties;
using DVLD_Business;
using DVLD_Business.InternationalLicenses;
using DVLD_Business.Licenses;
using System;
using System.IO;
using System.Data;
using System.Drawing;
using System.Windows.Forms;

namespace DVLD.Applications.InternationalLicense.Controls
{
    public partial class ctrlDriverInternationalLicenseInfo : UserControl
    {
        public event Action<int> OnLoadInternationalLicenseInfo;

        protected virtual void LoadInternationalLicenseInfo(int value)
        {
            OnLoadInternationalLicenseInfo?.Invoke(value);
        }

        private Image _imageMan = Resources.Person_Man;
        private Image _imageWoman = Resources.Person_Woman;
        private clsInternationalLicense _internationalLicense = null;
        private int? _internationalLicenseId = null;

        public ctrlDriverInternationalLicenseInfo()
        {
            InitializeComponent();
        }

        public int InternationalLicenseId
        {
            get
            {
                return _internationalLicenseId.Value;
            }
        }

        public clsInternationalLicense InternationalLicense
        {
            get { return _internationalLicense; }
        }

        public void _ResetDefaultValues()
        {
            lblName.Text = "N\\A";
            lblInternationalLicenseId.Text = "N\\A";
            lblLicenseId.Text = "N\\A";
            lblNationalNo.Text = "N\\A";
            lblGender.Text = "N\\A";
            lblIssueDate.Text = "N\\A";
            lblApplicationId.Text = "N\\A";
            lblIsActive.Text = "N\\A";
            lblDateOfBirth.Text = "N\\A";
            lblDriverId.Text = "N\\A";
            lblExpirationDate.Text = "N\\A";
            _LoadImage(_imageMan);
        }

        private void _LoadImage(string path)
        {
            _DisposeCurrentImage();

            using (Image temp = Image.FromFile(path))
            {
                pbPersonImage.Image = new Bitmap(temp);
            }
        }

        private void _LoadImage(Image image)
        {
            _DisposeCurrentImage();

            pbPersonImage.Image = new Bitmap(image);
        }

        private void _DisposeCurrentImage()
        {
            if (pbPersonImage.Image != null)
            {
                pbPersonImage.Image.Dispose();
                pbPersonImage.Image = null;
            }
        }

        private bool _PerpareInternationalLicenseInfo()
        {
            _internationalLicense = clsInternationalLicense.FindInternationalLicense(
                _internationalLicenseId.Value
            );

            if (_internationalLicense == null)
            {
                MessageBox.Show(
                    $"Can't Find International License Application ID [{_internationalLicenseId.Value}]",
                    "Not Found",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error
                );

                return false;
            }

            return true;
        }

        private void _LoadInfo()
        {
            clsPerson person = _internationalLicense.DriverInfo.PersonInfo;

            lblName.Text = person.GetName();
            lblInternationalLicenseId.Text = _internationalLicense.InternationalLicenseId.ToString();
            lblLicenseId.Text = _internationalLicense.IssuedUsingLocalLicenseId.ToString();
            lblNationalNo.Text = person.NationalNo;
            lblGender.Text = (person.Gender == 0) ? "Male" : "Female";
            lblIssueDate.Text = _internationalLicense.IssueDate.ToString("dd/MMM/yyyy", System.Globalization.CultureInfo.InvariantCulture);
            lblApplicationId.Text = _internationalLicense.ApplicationId.ToString();
            lblIsActive.Text = (_internationalLicense.IsActive == true) ? "Yes" : "No";
            lblDateOfBirth.Text = person.DateOfBirth.ToString("dd/MMM/yyyy", System.Globalization.CultureInfo.InvariantCulture);
            lblDriverId.Text = _internationalLicense.DriverId.ToString();
            lblExpirationDate.Text = _internationalLicense.ExpirationDate.ToString("dd/MMM/yyyy", System.Globalization.CultureInfo.InvariantCulture);

            if (person.ImagePath != null && File.Exists(person.ImagePath))
            {
                _LoadImage(person.ImagePath);
            }
            else
            {
                _LoadDefaultImage();
            }
        }

        private void _LoadDefaultImage()
        {
            _LoadImage((
                (_internationalLicense.DriverInfo.PersonInfo.Gender == 0) ? _imageMan : _imageWoman)
            );
        }

        public void LoadLicesnesInfo(int internationalLicenseId)
        {
            _internationalLicenseId = internationalLicenseId;
            bool validPerpare = _PerpareInternationalLicenseInfo();

            if (!validPerpare)
                return;

            _LoadInfo();
            LoadInternationalLicenseInfo(internationalLicenseId);
        }

        public void LoadLicesnesInfo(clsInternationalLicense internationalLicense)
        {
            if (internationalLicense == null)
                return;

            _internationalLicense = internationalLicense;
            _LoadInfo();
            LoadInternationalLicenseInfo(_internationalLicense.InternationalLicenseId);
        }

    }
}
