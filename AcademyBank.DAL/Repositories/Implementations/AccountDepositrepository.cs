using AcademyBank.DAL.Repositories.Interfaces;
using AcademyBank.Models;
using AcademyBank.Models.Enums;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AcademyBank.DAL.Repositories.Implementations
{
   public class AccountDepositrepository : IRepository<AccountDeposit>, IAccountDepositRepository<AccountDeposit>
    {
        private readonly BankDbContext _context;
        public AccountDepositrepository(BankDbContext context)
        {
            _context = context;
        }
        public async Task<AccountDeposit> Create(AccountDeposit accountDeposit)
        {
            _context.UserDeposits.Add(accountDeposit);
            await _context.SaveChangesAsync();
            return accountDeposit;
        }

        public async Task Delete(AccountDeposit accountDeposit)
        {
            _context.UserDeposits.Remove(accountDeposit);
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<AccountDeposit>> Get()
        {
            var result = await _context.UserDeposits
                        .Include(x => x.Account)
                        .Include(x => x.Deposit)
                        .AsNoTracking()
                        .ToListAsync();
            return result;
        }
        public async Task<IEnumerable<AccountDeposit>> GetPendingDeposits()
        {
            var accountDeposits = await _context.UserDeposits.Where(x=>x.Status== Status.Pending.ToString())
                               .Include(x => x.Account)
                               .Include(x=>x.Account.User.UserInfo)
                               .Include(x => x.Deposit)
                               .AsNoTracking()
                               .ToListAsync();

            return accountDeposits;
        }

        public async Task<AccountDeposit> GetById(int id)
        {
            return await _context.UserDeposits
                .Include(x => x.Account)
                .Include(x => x.Deposit)
                .AsNoTracking()
                .SingleOrDefaultAsync(x => x.Id == id);
        }

        public async Task<AccountDeposit> Update(AccountDeposit accountDeposit)
        {
            _context.UserDeposits.Update(accountDeposit);
            await _context.SaveChangesAsync();
            return accountDeposit;
        }

        public async Task<AccountDeposit> ApproveDeposit(int id)
        {
            var deposit = await GetById(id);
            deposit.Status = Status.IsActive.ToString();
            deposit.AcceptanceTime = DateTime.Now;
            await Update(deposit);
            await _context.SaveChangesAsync();

            return deposit;
        }

        public async Task<AccountDeposit> RejectDeposit(int id)
        {
            var deposit = await GetById(id);
            deposit.Status = Status.Rejected.ToString();
            await Update(deposit);
            await _context.SaveChangesAsync();

            return deposit;
        }

        public async Task<List<AccountDeposit>> GetAccountDepositsByUserId(int id)
        {
            return await _context.UserDeposits
                .Include(x => x.Account)
                .Include(x => x.Deposit)
                .Where(x => x.Account.User.Id == id)
                .AsNoTracking()
                .ToListAsync();
        }
    }
}
