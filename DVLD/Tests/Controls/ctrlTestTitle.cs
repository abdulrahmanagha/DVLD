using DVLD.Properties;
using DVLD_Business.ManageTestTypes;
using System;
using System.Windows.Forms;
using DVLD_Shared;

namespace DVLD.Tests
{
    public partial class ctrlTestTitle : UserControl
    {
        private enTestType _testTypeId = 0;

        public ctrlTestTitle()
        {
            InitializeComponent();
        }

        public void _PrepareVisionTest()
        {
            lblTitle.Text = "Vision Test";
            pbImageType.Image = Resources.Vision_512;
        }

        public void _PrepareWrittenTest()
        {
            lblTitle.Text = "Written Test";
            pbImageType.Image = Resources.Written_Test_512;
        }

        public void _PrepareStreetTest()
        {
            lblTitle.Text = "Street Test";
            pbImageType.Image = Resources.Schedule_Test_512;
        }

        public void _PerpareTestType()
        {

            switch (_testTypeId)
            {
                case enTestType.VisionTest:
                    _PrepareVisionTest();
                    break;

                case enTestType.WrittenTest:
                    _PrepareWrittenTest();
                    break;

                case enTestType.StreetTest:
                    _PrepareStreetTest();
                    break;

                default:
                    _PrepareVisionTest();
                    break;
            }
        }

        public void LoadTypeWithTitle(string title, enTestType testTypeId)
        {
            _testTypeId = testTypeId;
            _PerpareTestType();
            lblTitle.Text = title;
        }

        public void LoadType(enTestType testTypeId)
        {
            _testTypeId = testTypeId;
            _PerpareTestType();
        }

        public string Title
        {
            set
            {
                lblTitle.Text = value;
            }
            get
            {
                return lblTitle.Text;
            }
        }
    }
}
