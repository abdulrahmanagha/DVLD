using DVLD_Business.Users;
using System;
using System.Windows.Forms;

namespace DVLD.Users
{
    public partial class frmUserInfo : Form
    {
        public event Action<int> OnPersonUpdated;
        protected virtual void PersonUpdated(int personId)
        {
            OnPersonUpdated?.Invoke(personId);
        }

        public frmUserInfo(int userId)
        {
            InitializeComponent();
            ctrlUserCard1.OnPersonUpdated += frmUserInfo_OnPersonUpdated;
            ctrlUserCard1.LoadUser(userId);
        }

        public frmUserInfo(clsUser user)
        {
            InitializeComponent();
            ctrlUserCard1.OnPersonUpdated += frmUserInfo_OnPersonUpdated;
            ctrlUserCard1.LoadUser(user);
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            Close();
        }
    
        private void frmUserInfo_OnPersonUpdated(int personId)
        {
            PersonUpdated(personId);
        }
    }
}
