using AcademyBank.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace AcademyBank.DAL.Repositories.Interfaces
{
    public interface ICountersReportRepository<T>
    {
        Task<CountersReport> Create(CountersReport countersReport);
        Task<IEnumerable<CountersReport>> GetReports();

        Task<CountersReport> Update(CountersReport countersReport);
        Task<CountersReport> GetReportById(int id);
    }
}
