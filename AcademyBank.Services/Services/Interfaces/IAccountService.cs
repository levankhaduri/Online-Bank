using AcademyBank.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace AcademyBank.BLL.Services.Interfaces
{
    public interface IAccountService
    {
        Task<Account> Create(Account account);
        Task<IEnumerable<Account>> Get();
        Task<Account> GetById(int Id);
        Task<IEnumerable<Account>> Get(int Id);
        Task<Account> GetByUserId(int userId);
        Task<Account> Update(Account account);
        Task Delete(Account account);
    }
}
