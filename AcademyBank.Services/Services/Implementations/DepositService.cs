using AcademyBank.BLL.Services.Interfaces;
using AcademyBank.DAL.Repositories.Implementations;
using AcademyBank.DAL.Repositories.Interfaces;
using AcademyBank.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AcademyBank.BLL.Services.Implementations
{
    public class DepositService : IDepositService
    {
        private readonly IRepository<Deposit> _deposit;
        public DepositService(IRepository<Deposit> deposit)
        {
            _deposit = deposit;
        }
        public async Task<Deposit> Create(Deposit deposit)
        {
            var result = await _deposit.Create(deposit);
            return result;
        }

        public async Task Delete(Deposit deposit)
        {
            await _deposit.Delete(deposit);
        }

        public void Dispose()
        {
        }

        public async Task<IEnumerable<Deposit>> Get()
        {
            var result = await _deposit.Get();
            return result;
        }

        public async Task<Deposit> GetById(int id)
        {
            var result = await _deposit.GetById(id);
            return result;
        }

        public async Task<IEnumerable<Deposit>> GetFilteredDeposits(string? searchCurrency, decimal? searchAmount)
        {
            var deposits = await Get();
            if (searchAmount != null && searchCurrency == null)
            {
                deposits = deposits.Where(s => s.MaxAMount > searchAmount
                                       && s.MinAmount < searchAmount);
            }
            else if (searchAmount == null && searchCurrency != null)
            {
                deposits = deposits.Where(s => s.Currency == searchCurrency);
            }
            else if (searchCurrency != null && searchAmount != null)
            {
                deposits = deposits.Where(s => s.MaxAMount > searchAmount
                   && s.MinAmount < searchAmount && s.Currency == searchCurrency);
            }
            return deposits;
        }

        public async Task<Deposit> Update(Deposit deposit)
        {
            var result = await _deposit.Update(deposit);
            return result;
        }
    }
}
