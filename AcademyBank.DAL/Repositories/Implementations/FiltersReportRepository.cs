using AcademyBank.DAL.Repositories.Interfaces;
using AcademyBank.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace AcademyBank.DAL.Repositories.Implementations
{
    public class FiltersReportRepository : IFiltersReportRepository<FiltersReport>
    {
        private readonly BankDbContext _context;
        public FiltersReportRepository(BankDbContext context)
        {
            _context = context;
        }
        public async Task<FiltersReport> Create(FiltersReport filtersReport)
        {
            _context.FiltersReports.Add(filtersReport);
            await _context.SaveChangesAsync();
            return filtersReport;
        }

        public async Task<FiltersReport> Update(FiltersReport filtersReport)
        {
            _context.FiltersReports.Update(filtersReport);
            await _context.SaveChangesAsync();
            return filtersReport;
        }
        public async Task<IEnumerable<FiltersReport>> GetReports()
        {
            var reports = await _context.FiltersReports.Include(a => a.User).AsNoTracking().ToListAsync();
            return reports;
        }

        public async Task<FiltersReport> GetReportById(int id)
        {
            var report = await _context.FiltersReports
               .AsNoTracking().Include(a => a.User)
               .SingleOrDefaultAsync(x => id == x.UserId);
            return report;
        }
    }
}
