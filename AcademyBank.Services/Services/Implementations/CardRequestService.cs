using AcademyBank.BLL.Services.Interfaces;
using AcademyBank.DAL.Repositories.Interfaces;
using AcademyBank.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace AcademyBank.BLL.Services.Implementations
{
    public class CardRequestService : ICardRequestService
    {
        private readonly ICardRequestRepository<Card> _cardRequest;
        private readonly IEncryptionService _encryptionService;

        public CardRequestService(ICardRequestRepository<Card> cardRequest, IEncryptionService encryptionService)
        {
            _cardRequest = cardRequest;
            _encryptionService = encryptionService;
        }

        public async Task<Card> GetById(int id)
        {
            return await _cardRequest.GetById(id);
        }

        public async Task<IEnumerable<Card>> GetPendingCards()
        {
            return await _cardRequest.GetPendingCards(); 
        }

        public async Task<IEnumerable<Card>> GetRejectedCards()
        {
            return await _cardRequest.GetRejectedCards();
        }
        public async Task<IEnumerable<Card>> GetActiveCards()
        {
            return await _cardRequest.GetActiveCards();
        }
        public async Task<IEnumerable<Card>> GetBlockedCards()
        {
            return await _cardRequest.GetBlockedCards();
        }
        public async Task<Card> ApproveCard(int id, Card card)
        {
            card.CardNumber = _encryptionService.Encrypt(card.CardNumber);
            return await _cardRequest.ApproveCard(id, card);
        }

        public async Task<Card> Delete(Card card)
        {
            await _cardRequest.Delete(card);
            return card;
        }

        public async Task<Card> Reject(int id)
        {
            var card = await _cardRequest.Reject(id);
            return card;
        }

        public async Task<IEnumerable<Card>> GetExpiredCards()
        {
            return await _cardRequest.GetExpiredCards();
        }

        public async Task<Card> BlockCardById(int id)
        {
            return await _cardRequest.BlockCardById(id);
        }

        public async Task<Card> UnblockCardById(int id)
        {
            return await _cardRequest.UnblockCardById(id);
        }
    }
}
