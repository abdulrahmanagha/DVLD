using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DVLD.LocalDrivingLicenseApplications
{
    public partial class frmLocalDrivingLicenseApplicationInfo : Form
    {

        public frmLocalDrivingLicenseApplicationInfo(int localApplicationId, bool ShowLicenseInfo = false)
        {
            InitializeComponent();

            
            ctrlDrivingLicenseApplicationInfo1.ShowLicenseInfo = ShowLicenseInfo;
            ctrlDrivingLicenseApplicationInfo1.LoadApplication(localApplicationId);
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void frmLocalDrivingLicenseApplicationInfo_Load(object sender, EventArgs e)
        {

        }
    }
}
