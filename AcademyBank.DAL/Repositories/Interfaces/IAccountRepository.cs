using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace AcademyBank.DAL.Repositories.Interfaces
{
    public interface IAccountRepository<T>
    {
        Task<T> GetByUserId(int userId);
    }
}
