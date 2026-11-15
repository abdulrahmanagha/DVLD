using System;
namespace DVLD_Shared
{
    public class LicenseClassDTO
    {
        public int LicenseClassId { get; set; }
        public string ClassName { get; set; }
        public string ClassDescription { get; set; }
        public int MinimumAllowedAge { get; set; }
        public int DefaultValidityLength { get; set; }
        public decimal ClassFees { get; set; }
    }
}
