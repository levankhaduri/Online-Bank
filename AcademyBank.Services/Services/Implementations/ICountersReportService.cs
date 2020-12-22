using AcademyBank.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace AcademyBank.BLL.Services.Interfaces
{
    public interface ICountersReportService
    {
        Task<CountersReport> Create(CountersReport countersReport);

        Task<CountersReport> Update(CountersReport countersReport);
        Task<CountersReport> GetReport(int id);
    }
}
