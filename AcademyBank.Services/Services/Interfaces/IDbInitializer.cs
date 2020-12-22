using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace AcademyBank.BLL.Services.Interfaces
{
    public interface IDbInitializer
    {
        Task Seed();
    }
}
