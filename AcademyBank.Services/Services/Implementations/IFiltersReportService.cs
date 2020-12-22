using AcademyBank.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace AcademyBank.BLL.Services.Interfaces
{
    public interface IFiltersReportService
    {
        Task<FiltersReport> Create(FiltersReport filtersReport);

        Task<FiltersReport> Update(FiltersReport filtersReport);
        Task<FiltersReport> GetReport(int id);
    }
}
