using AcademyBank.DAL.Repositories.Interfaces;
using AcademyBank.Models;
using AcademyBank.Models.Enums;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AcademyBank.DAL.Repositories.Implementations
{
    public class CardRequestRepository : ICardRequestRepository<Card>
    {
        private readonly BankDbContext _context;

        public CardRequestRepository(BankDbContext context)
        {
            _context = context;
        }

        public async Task<Card> GetById(int id)
        {
            var card = await 
                _context
                .Cards
                .Include(x => x.Account)
                .AsNoTracking()
                .SingleOrDefaultAsync(x => id == x.Id);
            return card;
        }

        public async Task<IEnumerable<Card>> GetPendingCards()
        {
            var pendingCards = await _context
                                .Cards
                                .Where(x => x.Status == CardStatus.Pending.ToString())
                                .Include(a => a.Account)
                                .Include(u => u.Account.User.UserInfo)
                                .AsNoTracking()
                                .ToListAsync();
            return pendingCards;
        }
        public async Task<IEnumerable<Card>> GetRejectedCards()
        {
            var rejectedCards = await _context
                                .Cards
                                .Where(x => x.Status == CardStatus.Rejected.ToString())
                                .Include(a => a.Account)
                                .Include(u => u.Account.User.UserInfo)
                                .AsNoTracking()
                                .ToListAsync();
            return rejectedCards;
        }
        public async Task<IEnumerable<Card>> GetActiveCards()
        {
            var activeCards = await _context
                                .Cards
                                .Where(x => x.Status == CardStatus.Active.ToString())
                                .Include(a => a.Account)
                                .Include(u => u.Account.User.UserInfo)
                                .AsNoTracking()
                                .ToListAsync();
            return activeCards;
        }
        public async Task<IEnumerable<Card>> GetBlockedCards()
        {
            var activeCards = await _context
                                .Cards
                                .Where(x => x.Status == CardStatus.Blocked.ToString())
                                .Include(a => a.Account)
                                .Include(u => u.Account.User.UserInfo)
                                .AsNoTracking()
                                .ToListAsync();
            return activeCards;
        }

        public async Task<Card> ApproveCard(int id, Card card)
        {
            var getCard = await GetById(id);
            getCard.Status = CardStatus.Active.ToString();
            getCard.ExpireDate = card.ExpireDate;
            getCard.CCV = card.CCV;
            getCard.CardNumber = card.CardNumber;
            getCard.CreatedAt = DateTime.Now;
            await Update(getCard);
            await _context.SaveChangesAsync();

            return card;
        }

        public async Task<Card> Update(Card card)
        {
            _context.Cards.Update(card);
            await _context.SaveChangesAsync();
            return card;
        }

        public async Task<Card> Delete(Card card)
        {
            _context.Cards.Remove(card);
            await _context.SaveChangesAsync();

            return card;
        }

        public async Task<Card> Reject(int id)
        {
            var card = await GetById(id);
            card.Status = CardStatus.Rejected.ToString();
            await Update(card);
            await _context.SaveChangesAsync();

            return card;
        }

        public async Task<IEnumerable<Card>> GetExpiredCards()
        {
            var expiredCards = await _context
                                .Cards
                                .Where(x => x.Status == CardStatus.Expired.ToString())
                                .Include(a => a.Account)
                                .Include(u => u.Account.User.UserInfo)
                                .AsNoTracking()
                                .ToListAsync();
            return expiredCards;
        }

        public async Task<Card> BlockCardById(int id)
        {
            var findCard = await _context.Cards.Where(x => x.Id == id).SingleOrDefaultAsync();
            findCard.Status = CardStatus.Blocked.ToString();
            var result = await Update(findCard);
            return result;
        }

        public async Task<Card> UnblockCardById(int id)
        {
            var findCard = await _context.Cards.Where(x => x.Id == id).SingleOrDefaultAsync();
            findCard.Status = CardStatus.Active.ToString();
            var result = await Update(findCard);
            return result;
        }
    }
}
