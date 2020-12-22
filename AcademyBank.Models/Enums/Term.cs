using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace AcademyBank.Models.Enums
{
    public enum Term
    {
        [Description("6 Months")] Six = 6,
        [Description("9 Months")] Nine = 9,
        [Description("12 Months")] Twelve = 12,
        [Description("18 Months")] Yearhalf = 18,
        [Description("24 Months")] Twoyear = 24,
    }
}
