using AcademyBank.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace AcademyBank.BLL.Services.Interfaces
{
    public interface IUserInfoService
    {
        Task<UserInfo> Create(UserInfo member);
        Task<IEnumerable<UserInfo>> Get();
        Task<UserInfo> GetById(int id);
        Task<UserInfo> Update(UserInfo member);
        Task Delete(UserInfo member);
    }
}
