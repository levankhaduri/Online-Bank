using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace AcademyBank.Models.Enums
{
    public enum LoanPurposes
    {
        [Description("Consolidate debt")]
        ConsolidateDebt = 1,
        [Description("Alternative to a payday loan")]
        Alternative = 2,
        [Description("Finance home remodeling")]
        Finance = 3,
        [Description("Money for moving expenses")]
        MovingExpenses = 4,
        [Description("Unplanned emergency expenses")]
        UnplannedExpenses = 5,
        [Description("Make a large purchase")]
        LargePurchase = 6

    }
}
