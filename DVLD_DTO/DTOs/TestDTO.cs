using System;

namespace DVLD_Shared
{
    public class TestDTO
    {
        public int TestId { get; set; }
        public int TestAppointmentId { get; set; }
        public bool TestResult { get; set; }
        public string Notes { get; set; }
        public int CreatedByUserId { get; set; }

    }
}
