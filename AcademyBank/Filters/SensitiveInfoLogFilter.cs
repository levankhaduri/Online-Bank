using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AcademyBank.Web.Filters
{
    public class SensitiveInfoLogFilter:Attribute, IFilterMetadata
    {
        public SensitiveInfoLogFilter()
        {

        }
    }
}
