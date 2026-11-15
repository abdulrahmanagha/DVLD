using DVLD.Properties;
using DVLD_Business;
using DVLD_Business.Licenses;
using System.IO;
using System;
using System.Drawing;
using System.Windows.Forms;

namespace DVLD.Licenses.Controls
{
    public partial class ctrlLicenseInfo : UserControl
    {
        public event Action<int> OnLoadLicenseInfo;
        public event Action OnNotFoundLicense;
        protected virtual void LoadLicenseInfo(int value)
        {
            OnLoadLicenseInfo?.Invoke(value);
        }

        protected virtual void NotFoundLicense()
        {
            OnNotFoundLicense?.Invoke();
        }

        private Image _imageMan = Resources.Person_Man;
        private Image _imageWoman = Resources.Person_Woman;
        private clsLicense _license = null;
        private int? _licenseId = null;

        public int LicenseId
        {
            get
            {
                return _licenseId.Value;
            }
        }

        public clsLicense License
        {
            get { return _license; }
        }

        public ctrlLicenseInfo()
        {
            InitializeComponent();
        }

        public void _ResetDefaultValues()
        {
            lblClass.Text = "N\\A";
            lblName.Text = "N\\A";
            lblLicenseId.Text = "N\\A";
            lblNationalNo.Text = "N\\A";
            lblGender.Text = "N\\A";
            lblIssueDate.Text = "N\\A";
            lblIssueReason.Text = "N\\A";
            lblNotes.Text = "N\\A";
            lblIsActive.Text = "N\\A";
            lblDateOfBirth.Text = "N\\A";
            lblDriverId.Text = "N\\A";
            lblExpirationDate.Text = "N\\A";
            lblIsDetained.Text = "N\\A";
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

        private bool _PerpareLicenseInfo()
        {
            _license = clsLicense.Find(_licenseId.Value);

            if (_license == null)
            {
                MessageBox.Show(
                    $"Can't Find License Application ID [{_licenseId.Value}]",
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
            clsPerson person = _license.DriverInfo.PersonInfo;

            lblLicenseId.Text = _license.LicenseId.ToString();
            lblIsActive.Text = (_license.IsActive == true) ? "Yes" : "No";
            lblIsDetained.Text = "N\\A";
            lblClass.Text = _license.LicenseClassInfo.ClassName;
            lblName.Text = person.GetName();
            lblNationalNo.Text = person.NationalNo;
            lblGender.Text = (person.Gender == 0) ? "Male" : "Female";
            lblDateOfBirth.Text = person.DateOfBirth.ToString("dd/MMM/yyyy", System.Globalization.CultureInfo.InvariantCulture);
            lblIssueDate.Text = _license.IssueDate.ToString("dd/MMM/yyyy", System.Globalization.CultureInfo.InvariantCulture);
            lblIssueReason.Text = _license.IssueReasonString();
            lblNotes.Text = (_license.Notes == null) ? "N\\A" : _license.Notes;
            lblDriverId.Text = _license.DriverId.ToString();
            lblExpirationDate.Text = _license.ExpirationDate.ToString("dd/MMM/yyyy", System.Globalization.CultureInfo.InvariantCulture);
            lblIsDetained.Text = _license.IsDetained ? "Yes" : "No";
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
                (_license.DriverInfo.PersonInfo.Gender == 0) ?  _imageMan : _imageWoman)
            );
        }

        public void LoadLicesnesInfo(int licenseId)
        {
            _licenseId = licenseId;
            bool validPerpare = _PerpareLicenseInfo();

            if (!validPerpare)
            {
                _ResetDefaultValues();
                _license = null;
                NotFoundLicense();
                return;
            }

            _LoadInfo();
            LoadLicenseInfo(licenseId);
        }

        public void Clear()
        {
            _ResetDefaultValues();
        }
    }
}
