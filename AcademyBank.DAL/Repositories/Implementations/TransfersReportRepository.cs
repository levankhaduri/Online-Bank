using AcademyBank.DAL.Repositories.Interfaces;
using AcademyBank.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace AcademyBank.DAL.Repositories.Implementations
{
    public class TransfersReportRepository : ITransfersReportRepository<TransfersReport>
    {
        private readonly BankDbContext _context;
        public TransfersReportRepository(BankDbContext context)
        {
            _context = context;
        }
        public async Task<TransfersReport> Create(TransfersReport transfersReport)
        {
            _context.TransfersReports.Add(transfersReport);
            await _context.SaveChangesAsync();
            return transfersReport;
        }

        public async Task<TransfersReport> Update(TransfersReport transfersReport)
        {
            _context.TransfersReports.Update(transfersReport);
            await _context.SaveChangesAsync();
            return transfersReport;
        }
        public async Task<IEnumerable<TransfersReport>> GetReports()
        {
            var reports = await _context.TransfersReports.Include(a => a.User).AsNoTracking().ToListAsync();
            return reports;
        }

        public async Task<TransfersReport> GetReportById(int id)
        {
            var report = await _context.TransfersReports
                .AsNoTracking().Include(a => a.User)
                .SingleOrDefaultAsync(x => id == x.UserId);
            return report;
        }
    }
}