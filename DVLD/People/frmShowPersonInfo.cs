using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DVLD.People.Controls
{
    public partial class frmShowPersonInfo : Form
    {
        public event Action<int> OnPersonUpdated;

        protected virtual void PersonUpdated(int personId)
        {
            OnPersonUpdated?.Invoke(personId);
        }

        public frmShowPersonInfo(int personId)
        {
            InitializeComponent();
            ctrlPersonCard1.OnPersonUpdated += frmShowPersonInfo_OnPersonUpdated;
            ctrlPersonCard1.LoadPerson(personId);
        }

        private void frmShowPersonInfo_OnPersonUpdated(int value)
        {
            PersonUpdated(value);
        }

        public frmShowPersonInfo(string nationalNo)
        {
            InitializeComponent();
            ctrlPersonCard1.LoadPerson(nationalNo);
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
