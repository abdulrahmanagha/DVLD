using DVLD_Business;
using System;
using System.Windows.Forms;

namespace DVLD.People.Controls
{
    public partial class ctrlPersonCardWithFilter : UserControl
    {
        public event Action<int> OnPersonSelected;
        public event Action OnPersonNotFound;
        public event Action<int> OnPersonUpdated;

        protected virtual void PersonSelected(int personId)
        {
            OnPersonSelected?.Invoke(personId);
        }

        protected virtual void PersonUpdated(int personId)
        {
            OnPersonUpdated?.Invoke(personId);
        }

        protected virtual void PersonNotFound()
        {
            OnPersonNotFound?.Invoke();
        }

        private class FilterItem
        {
            public string ColumnName { get; }
            public enFilterType FilterType { get; }

            public FilterItem(string columnName, enFilterType filterType)
            {
                ColumnName = columnName;
                FilterType = filterType;
            }

            public override string ToString()
            {
                return ColumnName;
            }
        }

        private enum enFilterType { Number, Text }

        private enFilterType _filterType = enFilterType.Number;
        private enFilterType _lastFilterType = enFilterType.Number;
        private string _lastSearchValue = string.Empty;

        public ctrlPersonCardWithFilter()
        {
            InitializeComponent();
            _InitializeFilter();

            this.ctrlPersonCard1.OnPersonUpdated += PersonCardWithFilter_OnPersonUpdated;
        }

        private bool _ShowAddPerson = true;
        private bool _FilterEnable = true;

        public bool ShowAddPerson
        {
            get
            {
                return _ShowAddPerson;
            }

            set
            {
                _ShowAddPerson = value;
                btnAddNewPerson.Visible = value;
            }
        }

        public bool FilterEnable
        {
            get
            {
                return _FilterEnable;
            }

            set
            {
                _FilterEnable = value;
                gbFilterPerson.Enabled = value;
            }
        }

        public int? PersonId
        {
            get
            {
                return ctrlPersonCard1.PersonId;
            }
        }

        public clsPerson SelectedPersonInfo
        {
            get
            {
                return ctrlPersonCard1.SelectedPersonInfo;
            }
        }

        public ctrlPersonCard GetPersonCard
        {
            get
            {
                return ctrlPersonCard1;
            }
        }

        public void LoadPerson(clsPerson person)
        {
            if (person == null)
            {
                Clear();
                return;
            }

            ctrlPersonCard1.LoadPerson(person);
            cbFilterBy.SelectedIndex = 0;
            txtFilter.Text = person.PersonId.ToString();
        }

        private void _InitializeFilter()
        {
            cbFilterBy.Items.Add(new FilterItem("Person ID", enFilterType.Number));
            cbFilterBy.Items.Add(new FilterItem("National No", enFilterType.Text));
            cbFilterBy.SelectedIndex = 1;
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
                _lastSearchValue == searchValue &&
                _lastFilterType == _filterType
            )
            {
                return;
            }

            if (_filterType == enFilterType.Number)
            {
                if (!int.TryParse(txtFilter.Text, out int personId))
                {
                    MessageBox.Show(
                        "Person ID must contain numbers only.",
                        "Invalid Input",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Warning
                    );

                    return;
                }

                ctrlPersonCard1.LoadPerson(personId);
            }
            else
            {
                ctrlPersonCard1.LoadPerson(searchValue);
            }

            _lastSearchValue = searchValue;
            _lastFilterType = _filterType;

            if (ctrlPersonCard1.SelectedPersonInfo == null)
            {
                PersonNotFound();
                return;
            }

            if (FilterEnable)
                PersonSelected(ctrlPersonCard1.PersonId.Value);
        }

        private void cbFilterBy_SelectedIndexChanged(object sender, EventArgs e)
        {
            FilterItem item = cbFilterBy.SelectedItem as FilterItem;

            if (item == null)
                return;

            _filterType = item.FilterType;
            txtFilter.Clear();
            txtFilter.Select();
        }

        private void txtFilter_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = (
                _filterType == enFilterType.Number &&
                !(Char.IsDigit(e.KeyChar) || Char.IsControl(e.KeyChar))
            );
        }

        private void txtFilter_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.SuppressKeyPress = true;
                _ApplyFilter();
            }
        }

        private void btnFindPerson_Click(object sender, EventArgs e)
        {
            _ApplyFilter();
        }

        private void btnAddNewPerson_Click(object sender, EventArgs e)
        {
            using (frmAddEditPerson frm = new frmAddEditPerson())
            {
                frm.OnPersonSaved += PersonCardWithFilter_OnPersonSaved;
                frm.ShowDialog();
            }
        }

        private void _FillPersonInfo(int personId)
        {
            cbFilterBy.SelectedIndex = 0;
            txtFilter.Text = personId.ToString();
            _ApplyFilter();
        }

        private void PersonCardWithFilter_OnPersonSaved(object sender, int personId)
        {
            _FillPersonInfo(personId);
        }

        private void PersonCardWithFilter_OnPersonUpdated(int personId)
        {
            PersonUpdated(personId);
        }

        private void txtFilter_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.IsInputKey = true; // يمنع الفورم من تحويل الضغطة لزر Save
            }
        }

        public void Clear()
        {
            ctrlPersonCard1.Clear();
            txtFilter.Clear();
        }

        public void FilterFocus()
        {
            txtFilter.Select();
        }
    }
}
