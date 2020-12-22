using AcademyBank.DAL.Repositories.Interfaces;
using AcademyBank.Models;
using AcademyBank.Models.Enums;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AcademyBank.DAL.Repositories.Implementations
{
    public class AccountLoanRepository : IRepository<AccountLoan>,IAccountLoanRepository<AccountLoan>
    {
        private readonly BankDbContext _context;
        public AccountLoanRepository(BankDbContext context)
        {
            _context = context;
        }

        public async Task<AccountLoan> Create(AccountLoan accountLoan)
        {
            _context.AccountLoans.Add(accountLoan);
            await _context.SaveChangesAsync();
            return accountLoan;           
        }

        public async Task Delete(AccountLoan accountLoan)
        {
            _context.AccountLoans.Remove(accountLoan);
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<AccountLoan>> Get()
        {
            var result = await _context.AccountLoans
                               .Include(x => x.Account)
                               .Include(x=>x.Account.User)
                               .Include(x=>x.Account.User.UserInfo)
                               .Include(x => x.Loan)
                               .Include(x => x.AccrueAccount)
                               .Include(x => x.AccrueAccount.User)
                               .Include(x => x.AccrueAccount.User.UserInfo)
                               .AsNoTracking()
                               .ToListAsync();
            return result;
        }

        public async Task<IEnumerable<AccountLoan>> GetPendingLoans()
        {
            var accountLoans = await _context.AccountLoans.Where(x => x.Status == Status.Pending.ToString())
                                .Include(x => x.Account)
                               .Include(x => x.Account.User)
                               .Include(x => x.Account.User.UserInfo)
                               .Include(x => x.Loan)
                               .Include(x => x.AccrueAccount)
                               .Include(x => x.AccrueAccount.User)
                               .Include(x => x.AccrueAccount.User.UserInfo)
                               .AsNoTracking()
                               .ToListAsync();                             
            return accountLoans;
        }

        public async Task<AccountLoan> GetById(int id)
        {
            return await _context.AccountLoans
                .Include(x => x.Account)
                .Include(x => x.Loan)
                .Include(x => x.AccrueAccount)
                .Include(x=>x.Account.User)
                .Include(x=>x.Account.User.UserInfo)
                .Include(x=>x.AccrueAccount.User)
                .Include(x=>x.AccrueAccount.User.UserInfo)
                .AsNoTracking()
                .SingleOrDefaultAsync(x => x.Id == id);
        }

        public async Task<AccountLoan> Update(AccountLoan accountLoan)
        {
            _context.AccountLoans.Update(accountLoan);
            await _context.SaveChangesAsync();
            return accountLoan;
        }
        public async Task<AccountLoan> GetAccountWithLoan(int? id)
        {
            var accountLoan = await _context.AccountLoans
                .Include(a => a.Account)
                .Include(a => a.Loan)
                .FirstOrDefaultAsync(m => m.Id == id);
            return accountLoan;
        }

        public async Task<List<AccountLoan>> AccountLoanList()
        {
            var bankDbContext = await _context.AccountLoans.Include(a => a.Account).Include(a => a.Loan).ToListAsync();
            
            return bankDbContext;
        }

        public async Task<AccountLoan> RejectLoan(int id)
        {
            var loan = await GetById(id);
            loan.Status = Status.Rejected.ToString();
            await Update(loan);
            await _context.SaveChangesAsync();

            return loan;
        }

        public async Task<AccountLoan> ApproveLoan(int id)
        {
            var loan = await GetById(id);
            loan.Status = Status.IsActive.ToString();
            loan.AcceptanceTime = DateTime.Now;
            await Update(loan);
            await _context.SaveChangesAsync();

            return loan;
        }

        public async Task<AccountLoan> RequestedLoanDetails(int id)
        {
            var requestedLoan = await GetById(id);

            return requestedLoan;
        }

        public async Task<List<AccountLoan>> GetAccountLoansByUserId(int id)
        {
            var result = await _context.AccountLoans
                              .Include(x => x.Account)
                              .Include(x => x.Account.User)
                              .Include(x => x.Account.User.UserInfo)
                              .Include(x => x.Loan)
                              .Include(x => x.AccrueAccount)
                              .Include(x => x.AccrueAccount.User)
                              .Include(x => x.AccrueAccount.User.UserInfo)
                              .Where(x => x.Account.User.Id == id)
                              .AsNoTracking()
                              .ToListAsync();
            return result;
        }
    }
}
