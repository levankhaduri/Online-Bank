using AcademyBank.BLL.Services.Interfaces;
using AcademyBank.DAL.Repositories.Interfaces;
using AcademyBank.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace AcademyBank.BLL.Services.Implementations
{
   public class HistoryService : IHistoryService
    {
        private readonly IRepository<Account> _rep;
        private readonly IAccountRepository<Account> _account;
        private readonly IRepository<TransactionsHistory> _transactionsHistories;
        public HistoryService(IRepository<TransactionsHistory> transactionHistories, IAccountRepository<Account> account, IRepository<Account> rep)
        {
            _rep = rep;
            _account = account;
            _transactionsHistories = transactionHistories;
        }
        public async Task<TransactionsHistory> Create(TransactionsHistory history)
        {
            var result = await _transactionsHistories.Create(history);
            return result;
        }

        public  async Task Delete(TransactionsHistory history)
        {
            await _transactionsHistories.Delete(history);
        }

        public async Task<IEnumerable<TransactionsHistory>> Get()
        {
            var result = await _transactionsHistories.Get();
            return result;
        }

        public async Task<TransactionsHistory> GetById(int id)
        {
            var result = await _transactionsHistories.GetById(id);
            return result;
        }

        public async Task<TransactionsHistory> Update(TransactionsHistory history)
        {
            var result = await _transactionsHistories.Update(history);
            return result;
        }
        public async Task<List<TransactionsHistory>> GetByUserId(int id)
        {
            var account = await _rep.GetById(id);

            var AllTransactions = new List<TransactionsHistory>();

            foreach (var transactions in account.Transactions)
            {
                AllTransactions.Add(transactions);
            }

            return AllTransactions;
        }
    }
}
