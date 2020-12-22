using AcademyBank.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace AcademyBank.BLL.Services.Interfaces
{
    public interface ILoginReportService
    {
        Task<LoginReport> Create(User user);

        Task<LoginReport> Update(LoginReport loginReport);
        Task<LoginReport> GetReport(int id);
        Task<LoginReport> CreateOrUpdate(User user);
    }
}
