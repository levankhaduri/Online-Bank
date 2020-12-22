using System;
using System.Collections.Generic;
using System.Text;

namespace AcademyBank.Models.Enums
{
    public enum CardStatus
    {
        Blocked = 0,
        Active = 1,
        Pending = 2,
        Expired = 3,
        Rejected = 4
    }
}