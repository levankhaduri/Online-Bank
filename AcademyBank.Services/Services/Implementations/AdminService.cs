using AcademyBank.BLL.Helpers;
using AcademyBank.BLL.Helpers.Implementations;
using AcademyBank.BLL.Services.Interfaces;
using AcademyBank.Models;
using AcademyBank.Models.Enums;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AcademyBank.BLL.Services.Implementations
{
	public class AdminService : IAdminService
	{
		private readonly IDepositService _depositService;
		private readonly ILoanService _loanService;
		private readonly IAccountDepositService _accountDepositService;
		private readonly IAccountLoanService _accountLoanService;
		private readonly ICardService _cardService;
		private readonly IUserService _userService;
		private readonly IHistoryService _historyService;
		private readonly UserManager<User> _userManager;
		private readonly IUserInfoService _userInfoService;
		private readonly IAccountService _accountService;
		private readonly ICardRequestService _cardRequestService;

		public AdminService(IDepositService depositService,
			ILoanService loanService,
			IAccountDepositService accountDepositService,
			IAccountLoanService accountLoanService,
			ICardService cardService,
			IUserService userService,
			IHistoryService historyService,
			UserManager<User> userManager,
			IUserInfoService userInfoService,
			IAccountService accountService,
			ICardRequestService cardRequestService)
		{
			_depositService = depositService;
			_loanService = loanService;
			_accountDepositService = accountDepositService;
			_accountLoanService = accountLoanService;
			_cardService = cardService;
			_userService = userService;
			_historyService = historyService;
			_userManager = userManager;
			_userInfoService = userInfoService;
			_cardRequestService = cardRequestService;
			_accountService = accountService;
		}

		public async Task<AdminStatistics> IndexStatisticsData()
		{
			var accountDepositsList = (await _accountDepositService.Get()).ToList();
			var requestedDeposits = accountDepositsList.Count(x => x.Status == Status.Pending.ToString());
			var acceptedDeposits = accountDepositsList.Count(x => x.Status == Status.IsActive.ToString());
			var rejectedDeposits = accountDepositsList.Count(x => x.Status == Status.Rejected.ToString());

			var accountLoansList = (await _accountLoanService.Get()).ToList();
			var requestedLoans = accountLoansList.Count(x => x.Status == Status.Pending.ToString());
			var acceptedLoans = accountLoansList.Count(x => x.Status == Status.IsActive.ToString());
			var rejectedLoans = accountLoansList.Count(x => x.Status == Status.Rejected.ToString());

			var accountCardsList = (await _cardService.Get()).ToList();
			var requestedCards = accountCardsList.Count(x => x.Status == CardStatus.Pending.ToString());
			var acceptedCards = accountCardsList.Count(x => x.Status == CardStatus.Active.ToString());
			var rejectedCards = accountCardsList.Count(x => x.Status == CardStatus.Rejected.ToString());

			var users = (await _userService.Get()).ToList();
			var activeUsersCount = users.Count(x => x.IsBanned == false);
			var blockedUsersCount = users.Count(x => x.IsBanned == true);

			var adminStatistics = new AdminStatistics
			{
				RequestedDeposits = requestedDeposits,
				AcceptedDeposits = acceptedDeposits,
				RejectedDeposits = rejectedDeposits,

				RequestedLoans = requestedLoans,
				AcceptedLoans = acceptedLoans,
				RejectedLoans = rejectedLoans,

				RequestedCards = requestedCards,
				AcceptedCards = acceptedCards,
				RejectedCards = rejectedCards,

				ActiveUsersCount = activeUsersCount,
				BlockedUsersCount = blockedUsersCount
			};

			return adminStatistics;
		}

		public async Task<IEnumerable<Loan>> GetLoansList(string orderBy)
		{
			var LoansList = await _loanService.Get();

			if (orderBy != null)
			{
				var isColumnValid = SortingDictionaries.LoanSortings.TryGetValue(orderBy, out var sortFunc);

				if (isColumnValid)
				{
					LoansList = orderBy.Contains("Desc")
						? LoansList.OrderByDescending(sortFunc)
						: LoansList.OrderBy(sortFunc);
				}
			}

			return LoansList;
		}
		public async Task CreateLoan(Loan loan)
		{
			await _loanService.Create(loan);
		}
		public async Task<Loan> GetLoanById(int id)
		{
			return await _loanService.GetById(id);
		}
		public async Task<Loan> DeleteLoan(Loan loan)
		{
			await _loanService.Delete(loan);
			return loan;
		}
		public async Task UpdateLoan(Loan loan)
		{
			await _loanService.Update(loan);
		}

		public async Task<IEnumerable<Deposit>> GetDepositsList(string orderBy)
		{
			var depositsList = await _depositService.Get();

			if (orderBy != null)
			{
				var isColumnValid = SortingDictionaries.DepositSortings.TryGetValue(orderBy, out var sortFunc);

				if (isColumnValid)
				{
					depositsList = orderBy.Contains("Desc")
						? depositsList.OrderByDescending(sortFunc)
						: depositsList.OrderBy(sortFunc);
				}
			}

			return depositsList;
		}

		public async Task CreateDeposit(Deposit deposit)
		{
			await _depositService.Create(deposit);
		}

		public async Task UpdateDeposit(Deposit deposit)
		{
			await _depositService.Update(deposit);
		}

		public async Task<User> GetCurrentAuthorizedUser(HttpContext getContext)
		{
			var currentAuthorisedUser = await _userManager.GetUserAsync(getContext.User);
			return currentAuthorisedUser;
		}

		public async Task<IEnumerable<AccountLoan>> GetPendingLoansList(string orderBy)
		{
			var loanRequests = await _accountLoanService.GetPendingLoans();

			if (orderBy != null)
			{
				var isColumnValid = SortingDictionaries.PendingLoanSortings.TryGetValue(orderBy, out var sortFunc);

				if (isColumnValid)
				{
					loanRequests = orderBy.Contains("Desc")
						? loanRequests.OrderByDescending(sortFunc)
						: loanRequests.OrderBy(sortFunc);
				}
			}

			return loanRequests;
		}

		public async Task RejectLoanById(int? id)
		{
			await _accountLoanService.RejectLoan(id.GetValueOrDefault());
		}

		public async Task ApproveLoanById(int? id)
		{
			await _accountLoanService.ApproveLoan(id.GetValueOrDefault());
		}
		public async Task<AccountLoan> GetRequestedLoanDetailsById(int? id)
		{
			var requestedLoan = await _accountLoanService.RequestedLoanDetails(id.GetValueOrDefault());
			return requestedLoan;
		}

		public async Task<IEnumerable<AccountDeposit>> GetPendingDepositsList(string orderBy)
		{
			var depositrequests = await _accountDepositService.GetPendingDeposits();
			if (orderBy != null)
			{
				var isColumnValid = SortingDictionaries.PendingDepositSortings.TryGetValue(orderBy, out var sortFunc);
				if (isColumnValid)
				{
					depositrequests = orderBy.Contains("Desc")
						? depositrequests.OrderByDescending(sortFunc)
						: depositrequests.OrderBy(sortFunc);
				}
			}
			return depositrequests;
		}

		public async Task RejectDepositById(int? id)
		{
			await _accountDepositService.RejectDeposit(id.GetValueOrDefault());
		}

		public async Task ApproveDepositById(int? id)
		{
			await _accountDepositService.ApproveDeposit(id.GetValueOrDefault());
		}

		public async Task<IEnumerable<User>> GetActiveUsersList(string orderBy)
		{
			var activeUsers = await _userService.GetActiveUsers();

			if (orderBy != null)
			{
				var isColumnValid = SortingDictionaries.UsersSortings.TryGetValue(orderBy, out var sortFunc);

				if (isColumnValid)
				{
					activeUsers = orderBy.Contains("Desc")
						? activeUsers.OrderByDescending(sortFunc)
						: activeUsers.OrderBy(sortFunc);
				}
			}

			return activeUsers;
		}

		public async Task<User> GetUserById(int? id)
		{
			var user = await _userService.GetById(id.GetValueOrDefault());
			var userInfo = (await _userInfoService.Get()).SingleOrDefault(x => x.UserId == user.Id);
			user.UserInfo = userInfo;

			return user;
		}

		public async Task BlockUserById(int? id)
		{
			await _userService.BlockUserById(id.GetValueOrDefault());
		}

		public async Task UnBlockUserById(int? id)
		{
			await _userService.UnBlockUserById(id.GetValueOrDefault());
		}

		public async Task<IEnumerable<TransactionsHistory>> GetTransactionsHistoriesList(string orderBy)
		{
			var transactionHistory = await _historyService.Get();

			if (orderBy != null)
			{
				var isColumnValid = SortingDictionaries.TransactionsHistorySortings.TryGetValue(orderBy, out var sortFunc);

				if (isColumnValid)
				{
					transactionHistory = orderBy.Contains("Desc")
						? transactionHistory.OrderByDescending(sortFunc)
						: transactionHistory.OrderBy(sortFunc);
				}
			}

			return transactionHistory;
		}

		public async Task<TransactionsHistory> GetTransactionsHistoryById(int? id)
		{
			var getHistory = await _historyService.GetById(id.GetValueOrDefault());
			return getHistory;
		}

		public async Task<IEnumerable<User>> GetBlockedUsersList(string orderBy)
		{
			var blockedUsers = await _userService.GetBlockedUsers();

			if (orderBy != null)
			{
				var isColumnValid = SortingDictionaries.UsersSortings.TryGetValue(orderBy, out var sortFunc);
				if (isColumnValid)
				{
					blockedUsers = orderBy.Contains("Desc")
						? blockedUsers.OrderByDescending(sortFunc)
						: blockedUsers.OrderBy(sortFunc);
				}
			}

			return blockedUsers;
		}

		public async Task<Deposit> GetDepositById(int? id)
		{
			var result = await _depositService.GetById(id.GetValueOrDefault());
			return result;
		}

		public async Task DeleteDeposit(Deposit deposit)
		{
			await _depositService.Delete(deposit);
		}
		public async Task<IEnumerable<Card>> GetPendingCards(string orderBy, string cardStatus)
		{

			IEnumerable<Card> cardsList = await _cardRequestService.GetPendingCards();

			if (cardStatus != null) {
				if (cardStatus == CardStatus.Active.ToString())
				{
					cardsList = await _cardRequestService.GetActiveCards();
				}
				else if (cardStatus == CardStatus.Blocked.ToString())
				{
					cardsList = await _cardRequestService.GetBlockedCards();
				}
				else if (cardStatus == CardStatus.Expired.ToString())
				{
					cardsList = await _cardRequestService.GetExpiredCards();
				}
				else if (cardStatus == CardStatus.Rejected.ToString()) {
					cardsList = await _cardRequestService.GetRejectedCards();
				}
			}


			if (orderBy != null)
			{
				var isColumnValid = SortingDictionaries.PendingCardSortings.TryGetValue(orderBy, out var sortFunc);

				if (isColumnValid)
				{
					cardsList = orderBy.Contains("Desc")
						? cardsList.OrderByDescending(sortFunc)
						: cardsList.OrderBy(sortFunc);
				}
			}

			return cardsList;
		}

		public async Task<IEnumerable<Card>> GetBlockedCards()
		{
			var blockedCards = await _cardRequestService.GetBlockedCards();
			return blockedCards;
		}
		public async Task<IEnumerable<Card>> GetRejectedCards()
		{
			var rejectedCards = await _cardRequestService.GetRejectedCards();
			return rejectedCards;
		}
		public async Task<IEnumerable<Card>> GetActiveCards()
		{
			var activeCards = await _cardRequestService.GetActiveCards();
			return activeCards;
		}
		public async Task<Card> RejectCardById(int id)
		{
			var card = await _cardRequestService.Reject(id);

			return card;
		}
		public async Task<Card> GetCardById(int id)
		{
			var accountCard = await _cardService.GetById(id);
			return accountCard;
		}

		public async Task<Card> ApproveCard(int id, Card card)
		{
			var newCard = await _cardRequestService.ApproveCard(id, card);
			return newCard;
		}

		public async Task<int> RequestedDepositsCounter()
		{
			var accountDepositsList = (await _accountDepositService.Get()).ToList();
			var requestedDeposits = accountDepositsList.Count(x => x.Status == Status.Pending.ToString());
			return requestedDeposits;
		}

		public async Task<int> RequestedLoansCounter()
		{
			var accountLoansList = (await _accountLoanService.Get()).ToList();
			var requestedLoans = accountLoansList.Count(x => x.Status == Status.Pending.ToString());
			return requestedLoans;
		}

		public async Task<int> RequestedCardssCounter()
		{
			var accountCardsList = (await _cardService.Get()).ToList();
			var requestedCards = accountCardsList.Count(x => x.Status == Status.Pending.ToString());
			return requestedCards;
		}

		public Task<List<AccountLoan>> GetAccountLoansByUserId(int id)
		{
			var result = _accountLoanService.GetAccountLoansByUserId(id);
			return result;
		}

		public Task<List<Card>> GetAccountCardsByUserId(int id)
		{
			var result = _cardService.GetCardsByUserId(id);
			return result;
		}

		public Task<List<AccountDeposit>> GetAccountDepositsByUserId(int id)
		{
			var result = _accountDepositService.GetAccountDepositsByUserId(id);
			return result;
		}

		public List<string> GetEnumCities() {
			var cities =typeof(Cities).GetAllEnumNames();
			return cities;
		}

		public List<string> GetEnumCardStatuses()
		{
			var cities = typeof(CardStatus).GetAllEnumNames();
			return cities;
		}

		public async Task<Card> BlockCardById(int id)
		{
			var blockCard = await _cardRequestService.BlockCardById(id);
			return blockCard;
		}
		public async Task<Card> UnblockCardById(int id)
		{
			var unblockCard = await _cardRequestService.UnblockCardById(id);
			return unblockCard;
		}
	}
}
