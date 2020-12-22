
using AcademyBank.BLL.Services.Interfaces;
using AcademyBank.DAL.Repositories.Interfaces;
using AcademyBank.Models;
using System.Threading.Tasks;

namespace AcademyBank.BLL.Services.Implementations
{
    public class TransfersReportService : ITransfersReportService
    {
        private readonly ITransfersReportRepository<TransfersReport> _transfersReport;
        public TransfersReportService(ITransfersReportRepository<TransfersReport> transfersReport)
        {
            _transfersReport = transfersReport;
        }

        public async Task<TransfersReport> Create(TransfersReport transfersReport)
        {
            var result = await _transfersReport.Create(transfersReport);
            return result;
        }

        public async Task<TransfersReport> GetReport(int id)
        {
            var report = await _transfersReport.GetReportById(id);
            return report;
        }

        public async Task<TransfersReport> Update(TransfersReport transfersReport)
        {
            var result = await _transfersReport.Update(transfersReport);
            return result;
        }
    }
}
