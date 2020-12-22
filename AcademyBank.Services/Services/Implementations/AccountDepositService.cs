using AcademyBank.BLL.Helpers.Implementations;
using AcademyBank.BLL.Services.Interfaces;
using AcademyBank.DAL;
using AcademyBank.DAL.Repositories.Interfaces;
using AcademyBank.Models;
using AcademyBank.Models.Enums;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace AcademyBank.BLL.Services.Implementations
{
    public class AccountDepositService : IAccountDepositService, IDisposable
    {
        private readonly IAccountDepositRepository<AccountDeposit> _accountDeposit;
        private readonly IRepository<AccountDeposit> _rep;
        private readonly IRepository<Account> _accountRepository;
        private readonly UserManager<User> _userManager;
        private readonly IUserService _userService;
        private readonly BankDbContext _context;
        private readonly IDepositService _depositService;
        public AccountDepositService(IAccountDepositRepository<AccountDeposit> repository, IRepository<AccountDeposit> rep, UserManager<User> userManager,
            IUserService userService, IRepository<Account> accountRepository, 
            BankDbContext context, IDepositService depositService)
        {
            _accountDeposit = repository;
            _rep = rep;
            _userManager = userManager;
            _userService = userService;
            _accountRepository = accountRepository;
            _context = context;
            _depositService = depositService;
        }
        public async Task<AccountDeposit> Create(AccountDeposit accountDeposit)
        {
            var result = await _rep.Create(accountDeposit);
            return result;
        }

        public async Task Delete(AccountDeposit accountDeposit)
        {
            await _rep.Delete(accountDeposit);
        }

        public async Task<IEnumerable<AccountDeposit>> Get()
        {
            var result = await _rep.Get();
            return result;
        }

        public async Task<IEnumerable<AccountDeposit>> GetPendingDeposits()
        {
            var result = await _accountDeposit.GetPendingDeposits();
            return result;
        }

        public async Task<AccountDeposit> GetById(int id)
        {
            var result = await _rep.GetById(id);
            return result;
        }

        public async Task<AccountDeposit> Update(AccountDeposit accountDeposit)
        {
            var result = await _rep.Update(accountDeposit);
            return result;
        }

        public async Task<AccountDeposit> ApproveDeposit(int id)
        {
            var deposit = await _accountDeposit.ApproveDeposit(id);

            return deposit;
        }

        public async Task<AccountDeposit> RejectDeposit(int id)
        {
            var deposit = await _accountDeposit.RejectDeposit(id);

            return deposit;
        }

        public async Task<List<AccountDeposit>> GetAccountDepositsByUserId(int id)
        {
            var result = await _accountDeposit.GetAccountDepositsByUserId(id);
            return result;
        }

        public void Dispose()
        {
        }

        public async Task<IEnumerable<Account>> AddDepositPercents()
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
            foreach (var acc in accounts)
            {
                if (acc.AccountDeposits != null && acc != bankAccount)
                {
                    foreach (var accountDeposit in acc.AccountDeposits)
                    {
                        var termstr = accountDeposit.Term;
                        var resultString = Regex.Match(termstr, @"\d+").Value;
                        var term = Convert.ToInt32(resultString);
                        var startdate = accountDeposit.AcceptanceTime;
                        var countdays = Convert.ToInt32((DateTime.Now - startdate).TotalDays);
                        if(countdays == term + 1)
                        {
                            accountDeposit.Status = Status.Finished.ToString();
                            acc.Balance += accountDeposit.DepositAmount;
                            accountDeposit.DepositAmount = 0;
                        }
                        if (accountDeposit.Status == Status.IsActive.ToString())
                        {
                            accountDeposit.Deposit = await _depositService.GetById(accountDeposit.DepositId);
                            var percent = accountDeposit.Deposit.Annual;
                            var depositPercent = ((accountDeposit.DepositAmount * percent) / 100);
                            bankAccount.Balance -= depositPercent;
                            accountDeposit.DepositAmount += depositPercent;
                            await _accountRepository.Update(bankAccount);
                            await _rep.Update(accountDeposit);
                        }
                    }
                }
                await _context.SaveChangesAsync();
            }
            return accounts;
        }
    }
}
