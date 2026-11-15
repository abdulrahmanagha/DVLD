using System;

namespace DVLD_Shared
{
    public class DriverDTO
    {
        public int DriverId { get; set; }
        public int PersonId { get; set; }
        public int CreatedByUserId { get; set; }
        public DateTime CreatedDate { get; set; }
    }
}
