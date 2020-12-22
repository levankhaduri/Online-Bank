using AcademyBank.DAL.Repositories.Interfaces;
using AcademyBank.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace AcademyBank.DAL.Repositories.Implementations
{
    public class CountersReportRepository : ICountersReportRepository<CountersReport>
    {
        private readonly BankDbContext _context;
        public CountersReportRepository(BankDbContext context)
        {
            _context = context;
        }

        public async Task<CountersReport> Create(CountersReport countersReport)
        {

            _context.CountersReports.Add(countersReport);
            await _context.SaveChangesAsync();
            return countersReport;
        }

        public async Task<CountersReport> Update(CountersReport countersReport)
        {
            _context.CountersReports.Update(countersReport);
            await _context.SaveChangesAsync();
            return countersReport;
        }
        public async Task<IEnumerable<CountersReport>> GetReports()
        {
            var reports = await _context.CountersReports.Include(a => a.User).AsNoTracking().ToListAsync();
            return reports;
        }

        public async Task<CountersReport> GetReportById(int id)
        {
            var report = await _context.CountersReports
               .AsNoTracking().Include(a => a.User)
               .SingleOrDefaultAsync(x => id == x.UserId);
            return report;
        }
    }
}

