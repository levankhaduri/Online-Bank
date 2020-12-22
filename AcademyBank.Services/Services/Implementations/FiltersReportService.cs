using AcademyBank.BLL.Services.Interfaces;
using AcademyBank.DAL.Repositories.Interfaces;
using AcademyBank.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace AcademyBank.BLL.Services.Implementations
{
    public class FiltersReportService : IFiltersReportService
    {
        private readonly IFiltersReportRepository<FiltersReport> _filtersReport;
        public FiltersReportService(IFiltersReportRepository<FiltersReport> filtersReport)
        {
            _filtersReport = filtersReport;
        }

        public async Task<FiltersReport> Create(FiltersReport filtersReport)
        {
            var result = await _filtersReport.Create(filtersReport);
            return result;
        }

        public async Task<FiltersReport> GetReport(int id)
        {
            var report = await _filtersReport.GetReportById(id);
            return report;
        }

        public async Task<FiltersReport> Update(FiltersReport filtersReport)
        {
            var result = await _filtersReport.Update(filtersReport);
            return result;
        }
    }
}