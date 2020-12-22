using AcademyBank.BLL.Services.Interfaces;
using AcademyBank.DAL.Repositories.Interfaces;
using AcademyBank.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AcademyBank.BLL.Services.Implementations
{
    public class LoginReportService : ILoginReportService
    {
        private readonly ILoginReportRepository<LoginReport> _loginReport;
        public LoginReportService(ILoginReportRepository<LoginReport> loginReport)
        {
            _loginReport = loginReport;
        }

        public async Task<LoginReport> Create(User user)
        {

            var loginreport = new LoginReport
            {
                UserId = user.Id,
                CounterLogsIn = 1,
                FirstLogin = DateTime.Now,
                LastLogin = DateTime.Now,
                AvgPerday = 1,
                PerMonth = 1,
                User = user
            };
            var result = await _loginReport.Create(loginreport);
            return result;
        }

        public async Task<LoginReport> CreateOrUpdate(User user)
        {
            var report = (await _loginReport.GetReports()).ToList().SingleOrDefault(x => x.UserId == user.Id);
            if (report != null)
            {
                report.CounterLogsIn++;
                report.LastLogin = DateTime.Now;
                report.PerMonth++;
                report.AvgPerday = (decimal)(report.CounterLogsIn / (report.LastLogin - report.FirstLogin).TotalDays);
                var result = await Update(report);
                return result;
            }
            else
            {   
                var result = await Create(user);
                return result;
            }
        }

        public async Task<LoginReport> GetReport(int id)
        {
            var report = await _loginReport.GetReportById(id);
            return report;
        }

        public async Task<LoginReport> Update(LoginReport loginReport)
        {
            var result = await _loginReport.Update(loginReport);
            return result;
        }
    }
}
