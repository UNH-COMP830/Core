using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameSlam.Core.Enums
{
    public enum Category
    {
        Windows = 1,
        Linux = 2,
        OSX = 3
    }

    public enum ApprovalStatus
    {
        WaitingForApprovial = 1,
        Approved = 2,
        Declined = 3
    }
}
