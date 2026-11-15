using System;

namespace DVLD_Shared
{
    public enum enDeleteResult
    {
        Success = 0,
        NotFound = 1,
        HasRelatedData = 2,
        Error = 3
    }

    public enum enApplicationType
    {
        NewDrivingLicense = 1,
        RenewDrivingLicense = 2,
        ReplaceLostDrivingLicense = 3,
        ReplaceDamagedDrivingLicense = 4,
        ReleaseDetainedDrivingLicsense = 5,
        NewInternationalLicense = 6,
        RetakeTest = 7
    }

    public enum enApplicationStatus
    {
        New = 1,
        Cancelled = 2,
        Completed = 3
    }

    public enum enTestType
    {
        VisionTest = 1,
        WrittenTest = 2,
        StreetTest = 3
    }

    public enum enIssueReason
    {
        FirstTime = 1,
        Renew = 2,
        ReplacementForDamaged = 3,
        ReplacementForLost = 4
    }

}
