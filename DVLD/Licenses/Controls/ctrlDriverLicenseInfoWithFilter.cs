using DVLD.GlobalClasses;
using DVLD.People.Controls;
using DVLD_Business.Licenses;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DVLD.Licenses.Controls
{
    public partial class ctrlDriverLicenseInfoWithFilter : UserControl
    {
        public event Action<int> OnLicenseSelected;
        public event Action OnNotFoundLicense;

        protected virtual void LicenseSelected(int value)
        {
            OnLicenseSelected?.Invoke(value);
        }


        protected virtual void NotFoundLicense()
        {
            OnNotFoundLicense?.Invoke();
        }

        private string _lastSearchValue = string.Empty;
        private bool _filterEnable = false;
        private bool _isReload = false;

        public ctrlDriverLicenseInfoWithFilter()
        {
            InitializeComponent();
            ctrlLicenseInfo1.OnLoadLicenseInfo += AfterLoadLicenseInfo;
            ctrlLicenseInfo1.OnNotFoundLicense += CtrlLicenseInfo1_OnNotFoundLicense; ;
        }

        private void CtrlLicenseInfo1_OnNotFoundLicense()
        {
            NotFoundLicense();
        }

        public bool FilterEnable
        {
            set
            {
                _filterEnable = value;
                gbFilter.Enabled = _filterEnable;
            }

            get { return _filterEnable; }
        }

        public int LicenseId
        {
            get
            {
                return ctrlLicenseInfo1.LicenseId;
            }
        }

        public clsLicense SelectedLicense
        {
            get
            {
                return ctrlLicenseInfo1.License;
            }
        }

        public void FilterFocus()
        {
            txtFilter.Select();
        }

        private void _ApplyFilter()
        {
            if (string.IsNullOrWhiteSpace(txtFilter.Text))
            {
                MessageBox.Show(
                    "Please enter a value to search.",
                    "Search",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning
                );
                return;
            }

            string searchValue = txtFilter.Text.Trim();

            if (
                _lastSearchValue == searchValue
            )
            {
                return;
            }

            _lastSearchValue = searchValue;

            if (!int.TryParse(searchValue, out int value))
            {
                MessageBox.Show(
                    "Please enter a valid number",
                    "Search",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning
                );
                return;
            }

            ctrlLicenseInfo1.LoadLicesnesInfo(value);
        }

        private void btnFindLicense_Click(object sender, EventArgs e)
        {
            _ApplyFilter();
        }

        private void txtFilter_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                _ApplyFilter();
        }

        private void txtFilter_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
        {
            e.IsInputKey = true;
        }

        private void txtFilter_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = (
                !(Char.IsDigit(e.KeyChar) || Char.IsControl(e.KeyChar))
            );
        }

        private void AfterLoadLicenseInfo(int value)
        {
            if (_isReload)
                return;

            LicenseSelected(value);
        }

        public void LoadLicenseInfo(int licenseId)
        {
            txtFilter.Text = licenseId.ToString();
            _lastSearchValue = null;
            _ApplyFilter();
        }

        public void Clear()
        {
            ctrlLicenseInfo1.Clear();
            txtFilter.Clear();
        }

        public void Reload()
        {
            _isReload = true;
            string searchValue = txtFilter.Text.Trim();

            if (!int.TryParse(searchValue, out int value))
            {
                return;
            }

            ctrlLicenseInfo1.LoadLicesnesInfo(value);
            _isReload = false;
        }

    }
}
