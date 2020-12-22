using AcademyBank.BLL.Services.Interfaces;
using AcademyBank.DAL;
using AcademyBank.DAL.Repositories.Interfaces;
using AcademyBank.Models;
using AcademyBank.Models.Enums;
using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace AcademyBank.BLL.Services.Implementations
{
    public class AccountLoanService : IAccountLoanService, IDisposable
    {
        private readonly IRepository<AccountLoan> _repository;
        private readonly IAccountLoanRepository<AccountLoan> _accRepo;
        private readonly BankDbContext _context;
        private readonly IRepository<Account> _accountRepository;
        private readonly ILoanService _loanService;
        public AccountLoanService(IRepository<AccountLoan> repository, IAccountLoanRepository<AccountLoan> accRepo,
            BankDbContext context, IRepository<Account> accountRepository, ILoanService loanService)
        {
            _repository = repository;
            _accRepo = accRepo;
            _context = context;
            _accountRepository = accountRepository;
            _loanService = loanService;
        }
        public async Task<AccountLoan> Create(AccountLoan accountLoan)
        {
            var createdAccountLoan = await _repository.Create(accountLoan);
            return createdAccountLoan;
        }

        public async Task Delete(AccountLoan accountLoan)
        {
            await _repository.Delete(accountLoan);
        }

        public async Task<IEnumerable<AccountLoan>> Get()
        {
            var accountLoans = await _repository.Get();
            return accountLoans;
        }

        public async Task<IEnumerable<AccountLoan>> GetPendingLoans()
        {
            var result = await _accRepo.GetPendingLoans();
            return result;
        }

        public async Task<AccountLoan> GetById(int id)
        {
            var accountLoan = await _repository.GetById(id);
            return accountLoan;
        }

        public async Task<AccountLoan> Update(AccountLoan accountLoan)
        {
            var updatedAccountLoan = await _repository.Update(accountLoan);
            return updatedAccountLoan;
        }

        public async Task<AccountLoan> GetAccountWithLoan(int? id)
        {
            var accountLoan = await _accRepo.GetAccountWithLoan(id);
            return accountLoan;
        }

        public Task<List<AccountLoan>> AccountLoanList()
        {
            var bankDbContext = _accRepo.AccountLoanList();

            return bankDbContext;
        }

        public async Task<AccountLoan> ApproveLoan(int id)
        {
            var loan = await _accRepo.ApproveLoan(id);

            return loan;
        }

        public async Task<AccountLoan> RejectLoan(int id)
        {
            var loan = await _accRepo.RejectLoan(id);

            return loan;
        }

        public async Task<AccountLoan> RequestedLoanDetails(int id)
        {
            var requestedLoan = await _accRepo.RequestedLoanDetails(id);

            return requestedLoan;
        }

        public async Task<List<AccountLoan>> GetAccountLoansByUserId(int id)
        {
            var result = await _accRepo.GetAccountLoansByUserId(id);
            return result;
        }

        public void Dispose()
        {
        }

        public async Task<IEnumerable<Account>> EvaluateAmount()
        {
            var accounts = await _accountRepository.Get();
            var bankAccount = new Account();
            foreach (var acc in accounts)
            {
                if (acc.AccountName == "BankBalance")
                {
                    bankAccount = acc;
                }
            }
            foreach (var acc in accounts )
            {
                if (acc.AccountLoans != null && acc != bankAccount)
                {
                    foreach (var accountLoan in acc.AccountLoans)
                    {
                        var termstr = accountLoan.Term;
                        var resultString = Regex.Match(termstr, @"\d+").Value;
                        var term = Convert.ToInt32(resultString);
                        var startdate = accountLoan.AcceptanceTime;
                        var countdays = Convert.ToInt32((DateTime.Now - startdate).TotalDays);
                        if (countdays == term + 1)
                        {
                            accountLoan.Status = Status.Finished.ToString();
                            accountLoan.Sum = 0;
                        }
                        if (accountLoan.Status == Status.IsActive.ToString())
                        {
                            accountLoan.Loan = await _loanService.GetById(accountLoan.LoanId);
                           
                            var percent = accountLoan.Loan.Percentage;
                            var loanPercent = (accountLoan.Sum * percent) / 100;
                            var moneyToBringMonthly = (accountLoan.Sum + loanPercent) / term;
                            bankAccount.Balance += loanPercent;

                            accountLoan.Account.Balance -= moneyToBringMonthly;
                            await _repository.Update(accountLoan);
                            await _accountRepository.Update(bankAccount);
                        }
                    }
                }
                await _context.SaveChangesAsync();
            }      
            return accounts;
        }
    }
}
