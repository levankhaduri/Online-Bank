using AcademyBank.DAL.Repositories.Interfaces;
using AcademyBank.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace AcademyBank.DAL.Repositories.Implementations
{
    public class UserInfoRepository : IRepository<UserInfo>
    {
        private readonly BankDbContext _context;
        public UserInfoRepository(BankDbContext context)
        {
            _context = context;
        }

        public async Task<UserInfo> Create(UserInfo model)
        {
            _context.UserInfos.Add(model);
            await _context.SaveChangesAsync();
            return model;
        }

        public async Task Delete(UserInfo model)
        {
            _context.UserInfos.Remove(model);
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<UserInfo>> Get()
        {
            return await _context.UserInfos
                .AsNoTracking()
                .ToListAsync();
        }

        public async Task<UserInfo> GetById(int id)
        {
            return await _context.UserInfos
                .AsNoTracking()
                .SingleOrDefaultAsync(x => x.Id == id);
        }

        public async Task<UserInfo> Update(UserInfo model)
        {
            _context.UserInfos.Update(model);
            await _context.SaveChangesAsync();
            return model;
        }
    }
}
