using AcademyBank.DAL.Repositories.Interfaces;
using AcademyBank.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AcademyBank.DAL.Repositories.Implementations
{
    public class AccountRepository : IRepository<Account>, IAccountRepository<Account>
    {
        private readonly BankDbContext _context;

        public AccountRepository(BankDbContext context)
        {
            _context = context;
        }

        public async Task<Account> Create(Account model)
        {
            _context.Accounts.Add(model);
            await _context.SaveChangesAsync();
            return model;
        }

        public async Task Delete(Account model)
        {
            _context.Accounts.Remove(model);
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<Account>> Get()
        {
            return await _context
              .Accounts
              .Include(x => x.AccountLoans)
              .Include(x => x.AccountDeposits)
              .Include(x => x.Cards)
              .AsNoTracking()
              .ToListAsync();
        }

        public async Task<Account> GetById(int id)
        {
            return await _context
                   .Accounts
                   .Include(x=>x.AccountLoans)
                   .Include(x=>x.AccountDeposits)
                   .Include(x=>x.Cards)
                   .AsNoTracking()
                   .SingleOrDefaultAsync(x => x.Id == id);
        }

        public async Task<Account> GetByUserId(int userId)
        {
            return await _context
                   .Accounts
                   .Include(x => x.AccountLoans)
                   .Include(x => x.AccountDeposits)
                   .Include(x => x.Cards)
                   .Include(x => x.Transactions)
                   .Include(x => x.MoneyTransfers)
                   .AsNoTracking()  
                   .SingleOrDefaultAsync(x => x.UserId == userId);
        }

        public async Task<Account> Update(Account model)
        {
            _context.Accounts.Update(model);
            await _context.SaveChangesAsync();
            return model;
        }
    }
}
