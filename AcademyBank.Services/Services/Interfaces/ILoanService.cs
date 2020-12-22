using AcademyBank.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace AcademyBank.BLL.Services.Interfaces
{
    public interface ILoanService
    {
        Task<Loan> Create(Loan loan);
        Task<IEnumerable<Loan>> Get();
        Task<Loan> GetById(int id);
        Task<Loan> Update(Loan loan);
        Task Delete(Loan loan);
    }
}
