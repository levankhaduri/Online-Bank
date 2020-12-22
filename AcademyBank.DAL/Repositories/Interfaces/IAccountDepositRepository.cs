using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace AcademyBank.DAL.Repositories.Interfaces
{
    public interface IAccountDepositRepository<T>
    {
        Task<T> ApproveDeposit(int id);
        Task<T> RejectDeposit(int id);
        Task<IEnumerable<T>> GetPendingDeposits();
		Task<List<T>> GetAccountDepositsByUserId(int id);
	}
}
