using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace AcademyBank.Models.Enums
{
    public enum SecurityQuestions
    {
        [Description("What was the last name of your first grade teacher?")] FirstQuestion = 1,
        [Description("In what city/town was your mother born?")] SecondQuestion = 2,
        [Description("What street did you live on when you where 8 years old?")] ThirdQuestion = 3,
        [Description("What was the last name of your year 4 teacher?")] FourthQuestion = 4,
        [Description("What was your grandfather's occupation?")] FivethQuestion = 5,
        [Description("What was your grandmother's occupation?")] SixthQuestion = 6
    }
}