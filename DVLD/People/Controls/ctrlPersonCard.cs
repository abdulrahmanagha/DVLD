using DVLD.Properties;
using DVLD_Business;
using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;

namespace DVLD.People.Controls
{
    public partial class ctrlPersonCard : UserControl
    {
        public event Action<int> OnPersonUpdated;

        protected virtual void PersonUpdated(int personId)
        {
            OnPersonUpdated?.Invoke(personId);
        }

        private enum enLoadBy { ePersonId, eNationalNo };

        private Image ImageMan = Resources.Person_Man;
        private Image ImageWoman = Resources.Person_Woman;
        clsPerson _person = null;
        private int? _personId = null;

        public ctrlPersonCard()
        {
            InitializeComponent();
            _ResetDefaultValues();
        }

        public int? PersonId
        {
            get
            {
                return _personId;
            }
        }

        public clsPerson SelectedPersonInfo
        {
            get
            {
                return _person;
            }
        }

        private void _ResetDefaultValues()
        {
            _person = null;
            _personId = null;

            lblEditPersonInfo.Enabled = false;
            gbPerosnInfo.Text = "Person Information";
            lblPersonId.Text = "N/A";
            lblNationalNo.Text = "N/A";
            lblName.Text = "N/A";
            lblGender.Text = "N/A";
            lblEmail.Text = "N/A";
            lblPhone.Text = "N/A";
            lblDateOfBirth.Text = "N/A";
            lblCountry.Text = "N/A";
            lblAddress.Text = "N/A";
            LoadImage(ImageMan);
        }

        private void _LoadPersonImage()
        {
            if (_person.ImagePath != null)
            {
                using (Image temp = Image.FromFile(_person.ImagePath))
                {
                    LoadImage(temp);
                }
            }
            else
                LoadImage(
                    (_person.Gender == 0) ? ImageMan : ImageWoman
                );
        }

        private void _FillPersonInfo()
        {
            _personId = _person.PersonId;

            lblEditPersonInfo.Enabled = true;
            lblPersonId.Text = _person.PersonId.ToString();
            lblName.Text = _person.GetName();
            lblNationalNo.Text = _person.NationalNo.ToString();
            lblEmail.Text = (_person.Email != null) ? _person.Email.ToString() : "N/A";
            lblAddress.Text = _person.Address.ToString();
            lblDateOfBirth.Text = _person.DateOfBirth.ToString();
            lblPhone.Text = _person.Phone.ToString();
            lblCountry.Text = _person.CountryName;
            lblGender.Text = (_person.Gender == 0) ? "Male" : "Female";

            _LoadPersonImage();
        }

        private void _LoadPerson(enLoadBy loadBy, int? personId, string nationalNo = null)
        {
            _person = null;

            if (loadBy == enLoadBy.ePersonId)
                _person = clsPerson.Find(personId.Value);
            else
                _person = clsPerson.Find(nationalNo);

            if (_person == null)
            {
                _ResetDefaultValues();

                MessageBox.Show(
                    $"Not Found Person.",
                    "Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error
                );

                return;
            }

            _FillPersonInfo();
        }

        public void LoadPerson(clsPerson person)
        {
            _person = person;
            _FillPersonInfo();
        }

        public void LoadPerson(int personId)
        {
            _LoadPerson(enLoadBy.ePersonId, personId);
        }

        public void LoadPerson(string nationalNo)
        {
            _LoadPerson(enLoadBy.eNationalNo, -1, nationalNo);
        }

        public void Clear()
        {
            _ResetDefaultValues();
        }

        private void lblEditPersonInfo_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            if (_person == null)
            {
                MessageBox.Show(
                    "The Person Not Found!",
                    "Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error
                );

                return;
            }

            using (frmAddEditPerson frm = new frmAddEditPerson(_person.PersonId))
            {
                frm.OnPersonSaved += EditPerson_OnPersonSaved;
                frm.ShowDialog();
            }
        }

        private void EditPerson_OnPersonSaved(object sender, int personId)
        {
            LoadPerson(personId);
            PersonUpdated(personId);
        }

        private void LoadImage(Image image)
        {
            DisposeCurrentImage();
            pbPersonImage.Image = new Bitmap(image);
        }

        private void DisposeCurrentImage()
        {
            if (pbPersonImage.Image != null)
            {
                pbPersonImage.Image.Dispose();
                pbPersonImage.Image = null;
            }
        }
    }
}
