using System;

namespace DVLD_Shared
{
    public class ApplicationDTO
    {
        public int ApplicationId { get; set; }
        public int PersonId { get; set; }
        public int ApplicationTypeId { get; set; }
        public int ApplicationStatus { get; set; }
        public DateTime LastStatusDate { get; set; }
        public DateTime ApplicationDate { get; set; }
        public int CreatedByUserId { get; set; }
        public decimal PaidFees { get; set; }
    }
}
