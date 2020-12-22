using AcademyBank.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace AcademyBank.DAL.Repositories.Interfaces
{
    public interface IAccountLoanRepository<T>
    {
        Task<List<T>> AccountLoanList();
        Task<T> GetAccountWithLoan(int? id);
        Task<IEnumerable<T>> GetPendingLoans();
        Task<T> RejectLoan(int id);
        Task<T> RequestedLoanDetails(int id);
        Task<T> ApproveLoan(int id);
        Task<List<AccountLoan>> GetAccountLoansByUserId(int id);
	}
}
