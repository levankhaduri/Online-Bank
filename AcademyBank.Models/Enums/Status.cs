using System;
using System.Collections.Generic;
using System.Text;

namespace AcademyBank.Models.Enums
{
    public enum Status
    {
        IsBlocked = 0,
        IsActive = 1,
        Pending = 2,
        Declined = 3,
        Accepted = 4,
        Rejected = 5,
        Finished = 6
    }
}