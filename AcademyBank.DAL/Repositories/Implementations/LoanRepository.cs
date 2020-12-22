using AcademyBank.DAL.Repositories.Interfaces;
using AcademyBank.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace AcademyBank.DAL.Repositories.Implementations
{
    public class LoanRepository : IRepository<Loan>
    {
        private readonly BankDbContext _context;
        public LoanRepository(BankDbContext context)
        {
            _context = context;
        }

        public async Task<Loan> Create(Loan loan)
        {
            _context.Loans.Add(loan);
            await _context.SaveChangesAsync();
            return loan;
        }

        public async Task Delete(Loan loan)
        {
            _context.Loans.Remove(loan);
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<Loan>> Get()
        {  var loans= await _context.Loans.AsNoTracking().ToListAsync();
            return loans;
        }

        public async Task<Loan> GetById(int id)
        {
            return await _context.Loans.AsNoTracking()
                .SingleOrDefaultAsync(x => x.Id == id);
        }

        public async Task<Loan> Update(Loan loan)
        {
             _context.Loans.Update(loan);
            await _context.SaveChangesAsync();
            return loan;
        }
    }
}
