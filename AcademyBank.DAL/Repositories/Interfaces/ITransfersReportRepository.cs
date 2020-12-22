using AcademyBank.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace AcademyBank.DAL.Repositories.Interfaces
{
    public interface ITransfersReportRepository<T>
    {
        Task<TransfersReport> Create(TransfersReport transfersReport);
        Task<IEnumerable<TransfersReport>> GetReports();

        Task<TransfersReport> Update(TransfersReport transfersReport);
        Task<TransfersReport> GetReportById(int id);
    }
}
