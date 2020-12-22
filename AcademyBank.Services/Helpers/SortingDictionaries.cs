using AcademyBank.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace AcademyBank.BLL.Helpers
{
	public static class SortingDictionaries
	{
		public static Dictionary<string, Func<Loan, object>> LoanSortings =
			new Dictionary<string, Func<Loan, object>>()
			{
				{"Name", x => x.Name },
				{"NameDesc", x => x.Name },
				{"Percentage", x => x.Percentage },
				{"PercentageDesc", x => x.Percentage },
				{"AccidentInsurance", x => x.AccidentInsurance },
				{"AccidentInsuranceDesc", x => x.AccidentInsurance },
				{"InsuranceLoan", x => x.InsuranceLoan },
				{"InsuranceLoanDesc", x => x.InsuranceLoan },
				{"ServiceFee", x => x.ServiceFee },
				{"ServiceFeeDesc", x => x.ServiceFee }
			};

		public static Dictionary<string, Func<Deposit, object>> DepositSortings =
			new Dictionary<string, Func<Deposit, object>>()
			{
				{"Name", x => x.Name },
				{"NameDesc", x => x.Name },
				{"Description", x => x.Description },
				{"DescriptionDesc", x => x.Description },
				{"Benefits", x => x.Benefits },
				{"BenefitsDesc", x => x.Benefits },
				{"Annual", x => x.Annual },
				{"AnnualDesc", x => x.Annual },
				{"Bonus", x => x.Bonus },
				{"BonusDesc", x => x.Bonus },
				{"InterestPaymentDate", x => x.InterestPaymentDate },
				{"InterestPaymentDateDesc", x => x.InterestPaymentDate },
				{"MinAmount", x => x.MinAmount },
				{"MinAmountDesc", x => x.MinAmount },
				{"MaxAmount", x => x.MaxAMount },
				{"MaxAmountDesc", x => x.MaxAMount },
				{"Replenishment", x => x.Replenishment },
				{"ReplenishmentDesc", x => x.Replenishment }

			};

		public static Dictionary<string, Func<AccountLoan, object>> PendingLoanSortings =
			new Dictionary<string, Func<AccountLoan, object>>()
			{
				{"Firstname", x => x.Account.User.UserInfo.FirstName },
				{"FirstnameDesc", x => x.Account.User.UserInfo.FirstName },
				{"Lastname", x => x.Account.User.UserInfo.LastName },
				{"LastnameDesc", x => x.Account.User.UserInfo.LastName },
				{"Mail", x => x.Account.User.Email },
				{"MailDesc", x => x.Account.User.Email },
				{"Phone", x => x.Account.User.PhoneNumber },
				{"PhoneDesc", x => x.Account.User.PhoneNumber },
				{"LoanName", x => x.Loan.Name },
				{"LoanNameDesc", x => x.Loan.Name },
				{"LoanAmmount", x => x.Sum },
				{"LoanAmmountDesc", x => x.Sum },
				{"LoanCurrency", x => x.Currency },
				{"LoanCurrencyDesc", x => x.Currency }

			};

		public static Dictionary<string, Func<AccountDeposit, object>> PendingDepositSortings =
			new Dictionary<string, Func<AccountDeposit, object>>()
			{
				{"Firstname", x => x.Account.User.UserInfo.FirstName },
				{"FirstnameDesc", x => x.Account.User.UserInfo.FirstName },
				{"Lastname", x => x.Account.User.UserInfo.LastName },
				{"LastnameDesc", x => x.Account.User.UserInfo.LastName },
				{"Mail", x => x.Account.User.Email },
				{"MailDesc", x => x.Account.User.Email },
				{"Phone", x => x.Account.User.PhoneNumber },
				{"PhoneDesc", x => x.Account.User.PhoneNumber },
				{"DepositName", x => x.Deposit.Name },
				{"DepositNameDesc", x => x.Deposit.Name },
				{"DepositAmmount", x => x.DepositAmount },
				{"DepositAmmountDesc", x => x.DepositAmount }

			};

		public static Dictionary<string, Func<Card, object>> PendingCardSortings =
			new Dictionary<string, Func<Card, object>>()
			{
				{"Firstname", x => x.Account.User.UserInfo.FirstName },
				{"FirstnameDesc", x => x.Account.User.UserInfo.FirstName },
				{"Lastname", x => x.Account.User.UserInfo.LastName },
				{"LastnameDesc", x => x.Account.User.UserInfo.LastName },
				{"Mail", x => x.Account.User.Email },
				{"MailDesc", x => x.Account.User.Email },
				{"Phone", x => x.Account.User.PhoneNumber },
				{"PhoneDesc", x => x.Account.User.PhoneNumber },
				{"CardType", x => x.CardType},
				{"CardTypeDesc", x => x.CardType }

			};


		public static Dictionary<string, Func<User, object>> UsersSortings =
			new Dictionary<string, Func<User, object>>()
			{
				{"Firstname", x => x.UserInfo.FirstName },
				{"FirstnameDesc", x => x.UserInfo.FirstName },
				{"Lastname", x => x.UserInfo.LastName },
				{"LastnameDesc", x => x.UserInfo.LastName },
				{"Sex", x => x.UserInfo.Sex },
				{"SexDesc", x => x.UserInfo.Sex },
				{"Mail", x => x.Email },
				{"MailDesc", x => x.Email },
				{"Phone", x => x.PhoneNumber },
				{"PhoneDesc", x => x.PhoneNumber },
				{"PassportId", x => x.UserInfo.PassportId },
				{"PassportIdDesc", x => x.UserInfo.PassportId },
				{"City", x => x.UserInfo.City },
				{"CityDesc", x => x.UserInfo.City },
				{"Address", x => x.UserInfo.Address },
				{"AddressDesc", x => x.UserInfo.Address }

			};

		public static Dictionary<string, Func<TransactionsHistory, object>> TransactionsHistorySortings =
			new Dictionary<string, Func<TransactionsHistory, object>>()
			{
				{"SenderAccName", x => x.Account.AccountName },
				{"SenderAccNameDesc", x => x.Account.AccountName },
				{"SenderPassportId", x => x.Account.User.UserInfo.PassportId },
				{"SenderPassportIdDesc", x => x.Account.User.UserInfo.PassportId },
				{"RecipientAccName", x => x.RecipientAccount.AccountName},
				{"RecipientAccNameDesc", x => x.RecipientAccount.AccountName},
				{"RecipientPassportId", x => x.RecipientAccount.User.UserInfo.PassportId },
				{"RecipientPassportIdDesc", x => x.RecipientAccount.User.UserInfo.PassportId },
				{"Ammount", x => x.Amount },
				{"AmmountDesc", x => x.Amount },
				{"Currency", x => x.Currency },
				{"CurrencyDesc", x => x.Currency },
				{"Date", x => x.Date },
				{"DateDesc", x => x.Date }

			};

	}
}
