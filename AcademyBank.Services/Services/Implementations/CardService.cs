using AcademyBank.BLL.Services.Interfaces;
using AcademyBank.DAL.Repositories.Interfaces;
using AcademyBank.Models;
using Microsoft.AspNetCore.Identity;
using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using AcademyBank.DAL;

namespace AcademyBank.BLL.Services.Implementations
{
    public class CardService : ICardService
    {
        private readonly ICardRepository<Card> _card;
        private readonly IRepository<Card> _cardRep;
        private readonly IAccountRepository<Account> _account;
        private readonly IRepository<Account> _accRep;
        private readonly IEncryptionService _encryptionService;

        public CardService(
            ICardRepository<Card> card,
            IAccountRepository<Account> account,
            IEncryptionService encryptionService,
            IRepository<Account> accRep,
            IRepository<Card> cardRep)
        {
            _card = card;
            _cardRep = cardRep;
            _accRep = accRep;
            _account = account;
            _encryptionService = encryptionService;
        }

        public async Task<Card> Create(Card card)
        {
            card.CardNumber = _encryptionService.Encrypt(card.CardNumber);
            var result = await _cardRep.Create(card);
            return result;
        }

        public async Task Delete(Card card)
        {
            await _cardRep.Delete(card);
        }

        public async Task<IEnumerable<Card>> Get()
        {
            var result = await _cardRep.Get();
            foreach (Card card in result)
            {
                card.CardNumber = _encryptionService.Decrypt(card.CardNumber);
            }
            return result;
        }

        public async Task<IEnumerable<Card>> GetUserCards(int id)
        {
            var accounts = (await _accRep.Get()).Where(x => x.UserId == id);
            List<Card> cards = new List<Card>();
            foreach (Account account in accounts)
            {
                var accCards = (await _cardRep.Get()).Where(x => x.AccountId == account.Id);
                foreach (Card card in accCards)
                {
                    card.CardNumber = _encryptionService.Decrypt(card.CardNumber);
                    cards.Add(card);
                }
            }
            return cards;
        }

        public async Task<Card> GetById(int id)
        {
            var result = await _cardRep.GetById(id);
            result.CardNumber = _encryptionService.Decrypt(result.CardNumber);
            return result;
        }

        public async Task<Card> GetByNumber(string cardNumber)
        {
            var result = await _card.GetByNumber(_encryptionService.Encrypt(cardNumber));
            result.CardNumber = _encryptionService.Decrypt(result.CardNumber);
            return result;
        }

        public async Task Transfer(Account transferFrom, Account transferTo, decimal amount)
        {
            transferFrom.Balance -= amount;
            transferTo.Balance += amount;

            EncryptCards(transferFrom, transferTo);

            await _card.Transfer(transferFrom, transferTo, amount);

            return;
        }

        public async Task UtilityTransfer(Account transferFrom, decimal amount)
        {
            transferFrom.Balance -= amount;

            EncryptCards(transferFrom);

            await _card.UtilityTransfer(transferFrom, amount);

            return;
        }

        public async Task<Card> Update(Card card)
        {
            card.CardNumber = _encryptionService.Encrypt(card.CardNumber);
            var result = await _cardRep.Update(card);
            return result;
        }

        public void EncryptCards(Account account, Account secondAccount=null)
        {
            foreach (var card in account.Cards)
            {
                card.CardNumber = _encryptionService.Encrypt(card.CardNumber);
            }

            if (secondAccount==null)
            {
                return;
            }

            foreach (var card in secondAccount.Cards)
            {
                card.CardNumber = _encryptionService.Encrypt(card.CardNumber);
            }
        }

        public async Task<List<Card>> GetCardsByUserId(int id)
        {
            var result = await _card.GetCardsByUserId(id);

            for (int i = 0; i < result.Count; i++ )
            {
                var decripted =  _encryptionService.Decrypt(result[i].CardNumber);
                result[i].CardNumber = decripted;
            }
            return result;
        }
    }
}