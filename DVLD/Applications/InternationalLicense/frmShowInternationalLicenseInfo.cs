using DVLD_Business.InternationalLicenses;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DVLD.Applications.InternationalLicense
{
    public partial class frmShowInternationalLicenseInfo : Form
    {
        private enum enLoadBy {LoadById, LoadByObj};

        clsInternationalLicense _internationalLicense = null;
        private int? _internationalId = null;
        private enLoadBy _loadBy = enLoadBy.LoadById;
        
        public frmShowInternationalLicenseInfo(int internationalId)
        {
            InitializeComponent();
            _internationalId = internationalId;
            _loadBy = enLoadBy.LoadById;
        }

        public frmShowInternationalLicenseInfo(clsInternationalLicense internationalLicense)
        {
            InitializeComponent();
            _internationalLicense = internationalLicense;
            _loadBy = enLoadBy.LoadByObj;
        }

        private void frmShowInternationalLicenseInfo_Load(object sender, EventArgs e)
        {
            if(_loadBy == enLoadBy.LoadById)
                ctrlDriverInternationalLicenseInfo1.LoadLicesnesInfo(_internationalId.Value);
            else
                ctrlDriverInternationalLicenseInfo1.LoadLicesnesInfo(_internationalLicense);
        }
    }
}
