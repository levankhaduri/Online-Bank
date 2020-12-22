using AcademyBank.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace AcademyBank.BLL.Services.Interfaces
{
    public interface IDepositService 
    {
        Task<Deposit> Create(Deposit card);
        Task<IEnumerable<Deposit>> Get();
        Task<Deposit> GetById(int id);
        Task<Deposit> Update(Deposit card);
        Task Delete(Deposit card);
        Task<IEnumerable<Deposit>> GetFilteredDeposits(string? searchCurrency, decimal? searchAmount);
    }
}
