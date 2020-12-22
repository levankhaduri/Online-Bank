using AcademyBank.BLL.Services.Interfaces;
using AcademyBank.DAL.Repositories.Interfaces;
using AcademyBank.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace AcademyBank.BLL.Services.Implementations
{
    public class LoanService : ILoanService
    {
        private readonly IRepository<Loan> _Loans;
        public LoanService(IRepository<Loan> repository)
        {
            _Loans = repository;
        }
        public async Task<Loan> Create(Loan loan)
        {
            var result = await _Loans.Create(loan);
            return result;
        }

        public async Task Delete(Loan loan)
        {
            await _Loans.Delete(loan);
        }

        public async Task<IEnumerable<Loan>> Get()
        {
            var result = await _Loans.Get();
            return result;
        }

        public async Task<Loan> GetById(int id)
        {
            var result = await _Loans.GetById(id);
            return result;
        }

        public async Task<Loan> Update(Loan loan)
        {
            var result = await _Loans.Update(loan);
            return result;
        }
    }
}
