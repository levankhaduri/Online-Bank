using AcademyBank.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace AcademyBank.DAL.Repositories.Interfaces
{
    public interface ILoginReportRepository<T>
    {
        Task<LoginReport> Create(LoginReport loginReport);

        Task<LoginReport> Update(LoginReport loginReport);
        Task<IEnumerable<LoginReport>> GetReports();
        Task<LoginReport> GetReportById(int id);

    }
}
