using DVLD_DataAccess.ManageTestTypes;
using System;
using System.Data;
using DVLD_Shared;

namespace DVLD_Business.ManageTestTypes
{
    public class clsTestType
    {
        public enTestType TestTypeId { get; set; } = enTestType.VisionTest;
        public string TestTypeTitle { get; set; }
        public string TestTypeDescription { get; set; }
        public decimal TestTypeFees { get; set; }

        private clsTestType(enTestType testTypeId, string testTypeTitle,
            string testTypeDescription, decimal testTypeFees)
        {
            TestTypeId = testTypeId;
            TestTypeTitle = testTypeTitle;
            TestTypeDescription = testTypeDescription;
            TestTypeFees = testTypeFees;
        }

        public static clsTestType Find(enTestType testTypeId)
        {
            string testTypeTitle = null;
            string testTypeDescription = null;
            decimal testTypeFees = 0;

            if(
                clsTestTypesData.GetTestTypeById(
                    (int)testTypeId, 
                    ref testTypeTitle, 
                    ref testTypeDescription, 
                    ref testTypeFees    
                )
            )
            {
                return new clsTestType(
                    testTypeId, testTypeTitle,
                    testTypeDescription, testTypeFees
                );
            }
            else
                return null;
        }

        public static DataTable GetAllTestTypes()
        {
            return clsTestTypesData.GetAllTestTypes();
        }

        public bool Update()
        {
            return clsTestTypesData.UpdateTestType(
                (int)TestTypeId,
                TestTypeTitle,
                TestTypeDescription,
                TestTypeFees
            );
        }

        public string GetTestTypeName()
        {
            switch (TestTypeId)
            {
                case enTestType.VisionTest:
                    return "Vision";

                case enTestType.WrittenTest:
                    return "Written";

                case enTestType.StreetTest:
                    return "Street";

                default:
                    return "Vision";
            }
        }

    }
}
