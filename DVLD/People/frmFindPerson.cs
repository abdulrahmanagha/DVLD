using System;
using System.Windows.Forms;

namespace DVLD.People
{
    public partial class frmFindPerson : Form
    {
        public event Action<object, int> OnPersonSelected;

        protected virtual void PersonSelected(int personId)
        {
            OnPersonSelected?.Invoke(this, personId);
        }

        public frmFindPerson()
        {
            InitializeComponent();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void frmFindPerson_FormClosed(object sender, FormClosedEventArgs e)
        {
            PersonSelected(ctrlPersonCardWithFilter1.PersonId.Value);
        }
    }
}
