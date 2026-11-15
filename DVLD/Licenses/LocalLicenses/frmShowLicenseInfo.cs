using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DVLD.Licenses
{
    public partial class frmShowLicenseInfo : Form
    {
        private int? _licenseId = null;

        public frmShowLicenseInfo(int licenseId)
        {
            InitializeComponent();
            _licenseId = licenseId;
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void frmShowLicenseInfo_Load(object sender, EventArgs e)
        {
            ctrlLicenseInfo1.LoadLicesnesInfo(_licenseId.Value);
        }
    }
}
