using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DVLD.GlobalClasses
{
    public enum enFilterType
    {
        None,
        Number,
        Text,
        Status
    }

    public enum enStatus
    {
        All = -1,
        Inactive = 0,
        Active = 1
    };
}
