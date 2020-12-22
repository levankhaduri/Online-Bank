using AcademyBank.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace AcademyBank.BLL.Services.Interfaces
{
    public interface IAccountLoanService : IDisposable
    {
        Task<AccountLoan> Create(AccountLoan accountLoan);
        Task<IEnumerable<AccountLoan>> Get();
        Task<IEnumerable<AccountLoan>> GetPendingLoans();
        Task<AccountLoan> GetById(int id);
        Task<AccountLoan> RequestedLoanDetails(int id);
        Task<List<AccountLoan>> AccountLoanList();
        Task<AccountLoan> GetAccountWithLoan(int? id);
        Task<AccountLoan> ApproveLoan(int id);
        Task<AccountLoan> RejectLoan(int id);
        Task<AccountLoan> Update(AccountLoan accountLoan);
        Task Delete(AccountLoan accountLoan);
		Task<List<AccountLoan>> GetAccountLoansByUserId(int id);
        Task<IEnumerable<Account>> EvaluateAmount();
    }
}
