using AcademyBank.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace AcademyBank.BLL.Services.Interfaces
{
    public interface IHistoryService
    {
        Task<TransactionsHistory> Create(TransactionsHistory history);
        Task<IEnumerable<TransactionsHistory>> Get();
        Task<TransactionsHistory> GetById(int id);
        Task<TransactionsHistory> Update(TransactionsHistory history);
        Task Delete(TransactionsHistory history);
        Task<List<TransactionsHistory>> GetByUserId(int userId);
    }
}
