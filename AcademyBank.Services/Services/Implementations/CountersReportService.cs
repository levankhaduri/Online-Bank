using AcademyBank.BLL.Services.Interfaces;
using AcademyBank.DAL.Repositories.Interfaces;
using AcademyBank.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace AcademyBank.BLL.Services.Implementations
{
    public class CountersReportService : ICountersReportService
    {
        private readonly ICountersReportRepository<CountersReport> _countersReport;
        public CountersReportService(ICountersReportRepository<CountersReport> countersReport)
        {
            _countersReport = countersReport;
        }

        public async Task<CountersReport> Create(CountersReport countersReport)
        {
            var result = await _countersReport.Create(countersReport);
            return result;
        }

        public async Task<CountersReport> GetReport(int id)
        {
            var report = await _countersReport.GetReportById(id);
            return report;
        }

        public async Task<CountersReport> Update(CountersReport countersReport)
        {
            var result = await _countersReport.Update(countersReport);
            return result;
        }
    }
}
