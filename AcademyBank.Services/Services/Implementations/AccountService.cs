using AcademyBank.BLL.Services.Interfaces;
using AcademyBank.DAL.Repositories.Interfaces;
using AcademyBank.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AcademyBank.BLL.Services.Implementations
{
    public class AccountService : IAccountService
    {
        private readonly IAccountRepository<Account> _account;
        private readonly IRepository<Account> _rep;
        private readonly IEncryptionService _encryptionService;
        private readonly ICardRepository<Card> _cardRepository;

        public AccountService(IAccountRepository<Account> repository, IEncryptionService encryptionService, IRepository<Account> rep, ICardRepository<Card> cardRepository)
        {
            _rep = rep;
            _account = repository;
            _encryptionService = encryptionService;
            _cardRepository = cardRepository;
        }
        public async Task<Account> Create(Account account)
        {
            account.Balance = 0;
            Random random = new Random();
            string Template = "ACDM";
            string Numbers = random.Next(0, 999999999).ToString("D9");
            string AccountNumebr = Template + Numbers;
            account.AccountNumber = AccountNumebr;

            var result = await _rep.Create(account);
            return result;
        }
        public async Task Delete(Account account)
        {
            await _rep.Delete(account);
        }

        public async Task<IEnumerable<Account>> Get()
        {
            var allAccounts = await _rep.Get();
            return allAccounts;
        }

        public async Task<IEnumerable<Account>> Get(int Id)
        {
            var allAccounts = await _rep.Get();

            var result = allAccounts.Where(x => x.UserId == Id);
            return result;
        }

        public async Task<Account> GetById(int Id)
        {
            var result = await _rep.GetById(Id);

            if (result != null)
            {
                foreach (var card in result.Cards)
                {
                    card.CardNumber = _encryptionService.Decrypt(card.CardNumber);
                }
            }

            return result;
        }
        public async Task<Account> GetByUserId(int userId)
        {
            var result = await _account.GetByUserId(userId);

            if(result != null)
            {
                foreach (var card in result.Cards)
                {
                    card.CardNumber = _encryptionService.Decrypt(card.CardNumber);
                }
            }


            return result;
        }
        public async Task<Account> Update(Account account)
        {
            var result = await _rep.Update(account);

            var cards = result.Cards;
            foreach (var card in cards)
            {
                card.CardNumber = _encryptionService.Encrypt(card.CardNumber);
            }
            return result;
        }
    }
}


