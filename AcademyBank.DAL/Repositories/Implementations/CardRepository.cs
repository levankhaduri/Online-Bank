using AcademyBank.DAL.Repositories.Interfaces;
using AcademyBank.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AcademyBank.DAL.Repositories.Implementations
{
    public class CardRepository : IRepository<Card>,ICardRepository<Card>
    {
        private readonly BankDbContext _context;
        private readonly IRepository<Account> _accountRep;
        private readonly IAccountRepository<Account> _accountRepository;

        public CardRepository(BankDbContext context, IAccountRepository<Account> accountRepository, IRepository<Account> accountRep)
        {
            _context = context;
            _accountRepository = accountRepository;
            _accountRep = accountRep;
        }

        public async Task<Card> Create(Card card)
        {
            _context.Cards.Add(card);
            await _context.SaveChangesAsync();
            return card;
        }

        public async Task Delete(Card  card)
        {
            _context.Cards.Remove(card);
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<Card>> Get()
        {
            return await _context
                .Cards
                .AsNoTracking()
                .ToListAsync();
        }

        public async Task<Card> GetById(int id)
        {
            return await _context
                .Cards
                .AsNoTracking()
                .SingleOrDefaultAsync(x => x.Id == id);
        }

        public async Task<Card> GetByNumber(string cardNumber)
        {
            return await _context
                .Cards
                .AsNoTracking()
                .SingleOrDefaultAsync(x => x.CardNumber == cardNumber);
        }

        public async Task<List<Card>> GetCardsByUserId(int id)
        {
            return await _context
                .Cards
                .Include(c => c.Account)
                .Where(x => x.Account.User.Id == id)
                .AsNoTracking()
                .ToListAsync();
        }

        public async Task Transfer(Account transferFrom, Account transferTo, decimal amount)
        {
            await _accountRep.Update(transferFrom);
            await _accountRep.Update(transferTo);    

            return;
        }

        public async Task<Card> Update(Card card)
        {
            _context.Cards.Update(card);
            await _context.SaveChangesAsync();
            return card;
        }

        public async Task UtilityTransfer(Account transferFrom, decimal amount)
        {
            await _accountRep.Update(transferFrom);

            return;
        }
    }
}