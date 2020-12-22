using AcademyBank.DAL.Repositories.Interfaces;
using AcademyBank.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace AcademyBank.DAL.Repositories.Implementations
{
    public class LoginReportRepository : ILoginReportRepository<LoginReport>
    {

        private readonly BankDbContext _context;
        public LoginReportRepository(BankDbContext context)
        {
            _context = context;
        }
        public async Task<LoginReport> Create(LoginReport loginReport)
        {
            _context.LoginReports.Add(loginReport);
            await _context.SaveChangesAsync();
            return loginReport;

        }

        public async Task<LoginReport> GetReportById(int id)
        {
            var report = await _context.LoginReports
                .AsNoTracking().Include(a => a.User)
                .SingleOrDefaultAsync(x => id == x.UserId);
            return report;
        }

        public async Task<IEnumerable<LoginReport>> GetReports()
        {
            var reports = await _context.LoginReports.Include(a => a.User).AsNoTracking().ToListAsync();
            return reports;
        }

        public async Task<LoginReport> Update(LoginReport loginReport)
        {
            _context.LoginReports.Update(loginReport);
            await _context.SaveChangesAsync();
            return loginReport;
        }
    }
}
