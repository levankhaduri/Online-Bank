using AcademyBank.BLL.Services.Implementations;
using AcademyBank.Models;
using AcademyBank.Tests.Mocks.Repositories;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Text;

namespace AcademyBank.Tests.Services
{
    [TestClass]
    public class CardServiceTests
    {
        [TestMethod]
        public void Create()
        {
            //Arrange
            var card = new Card() { Id = 1 };
            var iRepositoryCard = new MockIRepository<Card>()
                .MockCreate(card);
            var iRepositoryAccount = new MockIRepository<Account>();
            var cardRepository = new MockCardRepository();
            var accountRepository = new MockAccountRepository();
            var cardService = new CardService(
                cardRepository.Object, 
                accountRepository.Object,
                new EncryptionService(),
                iRepositoryAccount.Object,
                iRepositoryCard.Object);

            //Act
            var createdCard = cardService.Create(card);

            //Assert
            Assert.IsNotNull(createdCard);
        }
        [TestMethod]
        public void Get()
        {
            //Arrange
            var cards = new List<Card>() { new Card() { Id = 1 } };
            var iRepositoryCard = new MockIRepository<Card>()
                .MockGet(cards);
            var iRepositoryAccount = new MockIRepository<Account>();
            var cardRepository = new MockCardRepository();
            var accountRepository = new MockAccountRepository();
            var cardService = new CardService(
                cardRepository.Object,
                accountRepository.Object,
                new EncryptionService(),
                iRepositoryAccount.Object,
                iRepositoryCard.Object);

            //Act
            var getCards = cardService.Get();

            //Assert
            Assert.IsNotNull(getCards);
        }
        [TestMethod]
        public void GetById()
        {
            //Arrange
            var card = new Card() { Id = 1 };
            var iRepositoryCard = new MockIRepository<Card>()
                .MockGetById(card);
            var iRepositoryAccount = new MockIRepository<Account>();
            var cardRepository = new MockCardRepository();
            var accountRepository = new MockAccountRepository();
            var cardService = new CardService(
                cardRepository.Object,
                accountRepository.Object,
                new EncryptionService(),
                iRepositoryAccount.Object,
                iRepositoryCard.Object);

            //Act
            var cardById = cardService.GetById(1);

            //Assert
            Assert.IsNotNull(cardById);
        }
        [TestMethod]
        public void Update()
        {
            //Arrange
            var card = new Card() { Id = 1 };
            var iRepositoryCard = new MockIRepository<Card>()
                .MockUpdate(card);
            var iRepositoryAccount = new MockIRepository<Account>();
            var cardRepository = new MockCardRepository();
            var accountRepository = new MockAccountRepository();
            var cardService = new CardService(
                cardRepository.Object,
                accountRepository.Object,
                new EncryptionService(),
                iRepositoryAccount.Object,
                iRepositoryCard.Object);

            //Act
            var createdCard = cardService.Update(card);

            //Assert
            Assert.IsNotNull(createdCard);
        }
        [TestMethod]
        public void GetByNumber()
        {
            //Arrange
            var card = new Card() { CardNumber = "1" };
            var iRepositoryCard = new MockIRepository<Card>();
            var iRepositoryAccount = new MockIRepository<Account>();
            var cardRepository = new MockCardRepository()
                .MockGetByNumber(card);
            var accountRepository = new MockAccountRepository();
            var cardService = new CardService(
                cardRepository.Object,
                accountRepository.Object,
                new EncryptionService(),
                iRepositoryAccount.Object,
                iRepositoryCard.Object);

            //Act
            var cardByNumber = cardService.GetByNumber(card.CardNumber);

            //Assert
            Assert.IsNotNull(cardByNumber);
        }
        [TestMethod]
        public void GetCardsByUserId()
        {
            //Arrange
            var cards = new List<Card>() { new Card() { Id = 1 } };
            var iRepositoryCard = new MockIRepository<Card>();
            var iRepositoryAccount = new MockIRepository<Account>();
            var cardRepository = new MockCardRepository()
                .MockGetCardsByUserId(cards);
            var accountRepository = new MockAccountRepository();
            var cardService = new CardService(
                cardRepository.Object,
                accountRepository.Object,
                new EncryptionService(),
                iRepositoryAccount.Object,
                iRepositoryCard.Object);

            //Act
            var cardsByUserId = cardService.GetCardsByUserId(1);

            //Assert
            Assert.IsNotNull(cardsByUserId);
        }
    }
}
