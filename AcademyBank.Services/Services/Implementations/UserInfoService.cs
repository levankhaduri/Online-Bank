using AcademyBank.BLL.Services.Interfaces;
using AcademyBank.DAL.Repositories.Interfaces;
using AcademyBank.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace AcademyBank.BLL.Services.Implementations
{
    public class UserInfoService : IUserInfoService
    {
        private readonly IRepository<UserInfo> _userInfos;
        public UserInfoService(IRepository<UserInfo> repository)
        {
            _userInfos = repository;
        }
        public async Task<UserInfo> Create(UserInfo userInfo)
        {
            var result = await _userInfos.Create(userInfo);
            return result;
        }

        public async Task Delete(UserInfo userInfo)
        {
            await _userInfos.Delete(userInfo);
        }

        public async Task<IEnumerable<UserInfo>> Get()
        {
            var result = await _userInfos.Get();
            return result;
        }

        public async Task<UserInfo> GetById(int id)
        {
            var result = await _userInfos.GetById(id);
            return result;
        }

        public async Task<UserInfo> Update(UserInfo userInfo)
        {
            var result = await _userInfos.Update(userInfo);
            return result;
        }
    }
}
