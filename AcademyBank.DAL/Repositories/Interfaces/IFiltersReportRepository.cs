using AcademyBank.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace AcademyBank.DAL.Repositories.Interfaces
{
    public interface IFiltersReportRepository<T>
    {
        Task<FiltersReport> Create(FiltersReport filtersReport);
        Task<IEnumerable<FiltersReport>> GetReports();

        Task<FiltersReport> Update(FiltersReport filtersReport);
        Task<FiltersReport> GetReportById(int id);
    }
}
