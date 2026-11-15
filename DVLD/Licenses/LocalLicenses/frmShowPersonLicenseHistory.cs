using DVLD_Business;
using DVLD_Business.Drivers;
using System;
using System.Windows.Forms;

namespace DVLD.Licenses
{
    public partial class frmShowPersonLicenseHistory : Form
    {
        public event Action<int> OnPersonUpdated;

        protected virtual void PersonUpdated(int value)
        {
            OnPersonUpdated?.Invoke(value);
        }

        private int? _personId = null;
        private clsDriver _driver = null;

        public frmShowPersonLicenseHistory(int personId)
        {
            InitializeComponent();
            _personId = personId;
            ctrlPersonCardWithFilter1.OnPersonSelected += AfterPersonSelected;
            ctrlPersonCardWithFilter1.OnPersonUpdated += AfterPersonUpdated;
            ctrlPersonCardWithFilter1.OnPersonNotFound += AfterPersonNotFound;
        }

        public frmShowPersonLicenseHistory()
        {
            InitializeComponent();
            _personId = null;
            ctrlPersonCardWithFilter1.OnPersonSelected += AfterPersonSelected;
            ctrlPersonCardWithFilter1.OnPersonUpdated += AfterPersonUpdated;
            ctrlPersonCardWithFilter1.OnPersonNotFound += AfterPersonNotFound;
        }

        private void _LoadPerson(clsPerson person)
        {
            if (person != null)
            {
                ctrlPersonCardWithFilter1.LoadPerson(person);
            }            
        }

        private void _LoadDriverLicenses()
        {
            ctrlDriverLicenses1.LoadInfoByDriverId(_driver.DriverId);
        }

        private void _LoadInfo()
        {
            _driver = clsDriver.FindByPersonId(_personId.Value);

            if (_driver == null)
            {
                DialogResult = DialogResult.Cancel;
                MessageBox.Show(
                    "This person is not registered as a driver.",
                    "Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error
                );

                return;
            }

            _LoadPerson(_driver.PersonInfo);
            _LoadDriverLicenses();
        }

        private void frmLicenseHistory_Load(object sender, EventArgs e)
        {
            if(_personId == null)
            {
                ctrlDriverLicenses1.ClearLists();
                ctrlPersonCardWithFilter1.FilterFocus();
                return;
            }

            ctrlPersonCardWithFilter1.FilterEnable = false;
            _LoadInfo();
        }

        private void AfterPersonSelected(int value)
        {
            _personId = value;
            _LoadInfo();
        }

        private void AfterPersonUpdated(int value)
        {
            if(_driver != null)
            {
                _driver.PersonInfo = ctrlPersonCardWithFilter1.SelectedPersonInfo;
            }
            
            PersonUpdated(value);
        }

        private void AfterPersonNotFound()
        {
            ctrlDriverLicenses1.ClearLists();
        }
    }
}
