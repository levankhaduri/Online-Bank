using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace AcademyBank.Models.Enums
{
    public enum Currency
    {
        [Description("USD")] USD = 1,
        [Description("GEL")] GEL = 2,
        [Description("RUB")] RUB = 3,
        [Description("EUR")] EUR = 4,
    }
}
