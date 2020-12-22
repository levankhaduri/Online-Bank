using AcademyBank.DAL.Repositories.Interfaces;
using AcademyBank.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace AcademyBank.DAL.Repositories.Implementations
{
	public class HistoryRepository : IRepository<TransactionsHistory>
	{
		private readonly BankDbContext _context;
		public HistoryRepository(BankDbContext context)
		{
			_context = context;
		}
		public async Task<TransactionsHistory> Create(TransactionsHistory model)
		{
			_context.TransactionsHistories.Add(model);
			await _context.SaveChangesAsync();
			return model;
		}

		public async Task Delete(TransactionsHistory model)
		{
			_context.TransactionsHistories.Remove(model);
			await _context.SaveChangesAsync();
		}

		public async Task<IEnumerable<TransactionsHistory>> Get()
		{
			return await _context.TransactionsHistories
				.Include(x => x.Account)
				.Include(y => y.RecipientAccount)
				.Include(x => x.Account.User)
				.Include(x => x.Account.User.UserInfo)
				.Include(y => y.RecipientAccount.User)
				.Include(y => y.RecipientAccount.User.UserInfo)
				 .AsNoTracking()
				 .ToListAsync();
		}

		public async Task<TransactionsHistory> GetById(int id)
		{
			return await _context.TransactionsHistories
				.Include(x => x.Account)
				.Include(y => y.RecipientAccount)
				.Include(x => x.Account.User)
				.Include(x => x.Account.User.UserInfo)
				.Include(y => y.RecipientAccount.User)
				.Include(y => y.RecipientAccount.User.UserInfo)
			   .AsNoTracking()
			   .SingleOrDefaultAsync(x => x.Id == id);
		}

		public async Task<TransactionsHistory> Update(TransactionsHistory model)
		{
			_context.TransactionsHistories.Update(model);
			await _context.SaveChangesAsync();
			return model;
		}
	}
}
