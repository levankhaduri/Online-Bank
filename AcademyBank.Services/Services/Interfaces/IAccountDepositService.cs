using AcademyBank.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace AcademyBank.BLL.Services.Interfaces
{
    public interface IAccountDepositService : IDisposable
    {
        Task<AccountDeposit> Create(AccountDeposit accountDeposit);
        Task<IEnumerable<AccountDeposit>> Get();
        Task<IEnumerable<AccountDeposit>> GetPendingDeposits();
        Task<AccountDeposit> GetById(int id);
        Task<AccountDeposit> ApproveDeposit(int id);
        Task<AccountDeposit> RejectDeposit(int id);
        Task<AccountDeposit> Update(AccountDeposit accountDeposit);
        Task Delete(AccountDeposit accountDeposit);
		Task<List<AccountDeposit>> GetAccountDepositsByUserId(int id);
        Task<IEnumerable<Account>> AddDepositPercents();
    }
}
