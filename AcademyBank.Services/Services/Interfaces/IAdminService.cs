using AcademyBank.Models;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace AcademyBank.BLL.Services.Interfaces
{
	public interface IAdminService
	{
		Task<AdminStatistics> IndexStatisticsData();
		Task<IEnumerable<Loan>> GetLoansList(string orderBy);
		Task CreateLoan(Loan loan);
		Task<Loan> GetLoanById(int id);
		Task<Loan> DeleteLoan(Loan loan);
		Task UpdateLoan(Loan loan);
		Task<IEnumerable<Deposit>> GetDepositsList(string orderBy);
		Task CreateDeposit(Deposit deposit);
		Task UpdateDeposit(Deposit deposit);
		Task<User> GetCurrentAuthorizedUser(HttpContext getContext);
		Task<IEnumerable<AccountLoan>> GetPendingLoansList(string orderBy);
		Task RejectLoanById(int? id);
		Task<AccountLoan> GetRequestedLoanDetailsById(int? id);
		Task ApproveLoanById(int? id);
		Task<IEnumerable<AccountDeposit>> GetPendingDepositsList(string orderBy);
		Task RejectDepositById(int? id);
		Task ApproveDepositById(int? id);
		Task<IEnumerable<User>> GetActiveUsersList(string orderBy);
		Task<User> GetUserById(int? id);
		Task BlockUserById(int? id);
		Task UnBlockUserById(int? id);
		Task<IEnumerable<TransactionsHistory>> GetTransactionsHistoriesList(string orderBy);
		Task<TransactionsHistory> GetTransactionsHistoryById(int? id);
		Task<IEnumerable<User>> GetBlockedUsersList(string orderBy);
		Task<Deposit> GetDepositById(int? id);
		Task DeleteDeposit(Deposit deposit);

		Task<IEnumerable<Card>> GetPendingCards(string orderBy, string cardStatus);

		Task<IEnumerable<Card>> GetRejectedCards();
		Task<IEnumerable<Card>> GetActiveCards();
		Task<IEnumerable<Card>> GetBlockedCards();
		Task<Card> RejectCardById(int id);
		Task<Card> ApproveCard(int id, Card card);

		Task<Card> GetCardById(int id);
		Task<int> RequestedDepositsCounter();
		Task<int> RequestedLoansCounter();
		Task<int> RequestedCardssCounter();
		Task<List<AccountLoan>> GetAccountLoansByUserId(int id);
		Task<List<Card>> GetAccountCardsByUserId(int id);
		Task<List<AccountDeposit>> GetAccountDepositsByUserId(int id);
		List<string> GetEnumCities();
		List<string> GetEnumCardStatuses();
		Task<Card> BlockCardById(int id);
		Task<Card> UnblockCardById(int id);
	}
}
