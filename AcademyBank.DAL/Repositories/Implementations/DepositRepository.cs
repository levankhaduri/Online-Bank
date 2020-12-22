using AcademyBank.DAL.Repositories.Interfaces;
using AcademyBank.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace AcademyBank.DAL.Repositories.Implementations
{
    public class DepositRepository : IRepository<Deposit>
    {
        private readonly BankDbContext _context;
        public DepositRepository(BankDbContext context)
        {
            _context = context;
        }
        public async Task<Deposit> Create(Deposit deposit)
        {
            _context.Deposits.Add(deposit);
            await _context.SaveChangesAsync();
            return deposit;
        }

        public async Task Delete(Deposit deposit)
        {
            _context.Remove(deposit);
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<Deposit>> Get()
        {
            return await _context
                .Deposits
                .AsNoTracking()
                .ToListAsync();
        }

        public async Task<Deposit> GetById(int id)
        {
            return await _context
                .Deposits
                .AsNoTracking()
                .SingleOrDefaultAsync(x => x.Id == id);
        }

        public async Task<Deposit> Update(Deposit deposit)
        {
            _context.Deposits.Update(deposit);
            await _context.SaveChangesAsync();
            return deposit;
        }
    }
}
