using AcademyBank.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace AcademyBank.BLL.Services.Interfaces
{
    public interface ITransfersReportService
    {
        Task<TransfersReport> Create(TransfersReport countersReport);
        Task<TransfersReport> Update(TransfersReport countersReport);
        Task<TransfersReport> GetReport(int id);
    }
}
