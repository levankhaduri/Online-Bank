using AcademyBank.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace AcademyBank.BLL.Services.Interfaces
{
    public interface IUserService
    {
        Task<IdentityResult> Create(User member);
        Task<IEnumerable<User>> Get();
        Task<User> GetById(int id);
        Task<User> GetByEmail(string email);
        Task<User> GetByName(string name);
        Task<IdentityResult> Update(User member);
        Task Delete(User member);
        Task<IEnumerable<User>> GetActiveUsers();
        Task<IEnumerable<User>> GetBlockedUsers();
        Task BlockUserById(int id);
        Task UnBlockUserById(int id);
        Task UploadPhoto(IFormFile image, string userName);
    }
}
