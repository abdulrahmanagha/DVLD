using DVLD_Business.Users;
using System;
using System.Drawing;
using System.Windows.Forms;

namespace DVLD.Users.Controls
{
    public partial class ctrlUserCard : UserControl
    {
        public event Action<int> OnUserSelected;
        public event Action<int> OnPersonUpdated;
        
        protected virtual void UserSelected(int userId)
        {
            OnUserSelected?.Invoke(userId);
        }

        protected virtual void PersonUpdated(int personId)
        {
            OnPersonUpdated?.Invoke(personId);
        }

        private clsUser _user = null;
        private int? _userId = null;
        
        public int? UserId
        {
            get {  return _userId; }
        }

        public clsUser SelectedUserInfo
        {
            get { return _user; }
        }

        public ctrlUserCard()
        {
            InitializeComponent();
            ctrlPersonCard1.OnPersonUpdated += ctrlUserCard_OnPersonUpdated;
        }

        private void _ResetDefaultValues()
        {
            _user = null;
            _userId = null;

            ctrlPersonCard1.Clear();
            gbLoginInfo.Text = "Login Information";
            lblUserId.Text = "N/A";
            lblUserName.Text = "N/A";
            lblActive.Text = "N/A";
        }

        private void _FillUserInfo()
        {
            _userId = _user.UserId;

            lblUserId.Text = _userId.Value.ToString();
            lblUserName.Text = _user.UserName;
            lblActive.Text = (_user.IsActive) ? "Yes" : "No";
            lblActive.ForeColor = (_user.IsActive) ? Color.Green : Color.Red;

            ctrlPersonCard1.LoadPerson(_user.PersonId.Value);
            UserSelected(_user.PersonId.Value);
        }

        private void _LoadUser(int userId)
        {
            _user = clsUser.Find(userId);

            if (_user == null)
            {
                _ResetDefaultValues();

                MessageBox.Show(
                    $"Not Found User With User ID [{userId}] .",
                    "Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error
                );

                return;
            }

            _FillUserInfo();
        }

        public void LoadUser(clsUser user)
        {
            _user = user;

            if(_user != null) 
                _FillUserInfo();
            else
                _ResetDefaultValues();
        }

        public void LoadUser(int userId)
        {
            _LoadUser(userId);
        }
    
        private void ctrlUserCard_OnPersonUpdated(int personId)
        {
            PersonUpdated(personId);
        }
    }
}
