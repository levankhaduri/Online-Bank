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
    public class AccountServiceTests
    {
        [TestMethod]
        public void Create()
        {
            //Arrange
            var account = new Account() { Id = 1 };
            var iRepository = new MockIRepository<Account>()
                .MockCreate(account);
            var accountRepository = new MockAccountRepository();
            var cardRepository = new MockCardRepository();
            var accountService = new AccountService(
                accountRepository.Object,
                new EncryptionService(),
                iRepository.Object,
                cardRepository.Object);

            //Act
            var createdAccount = accountService.Create(account);

            //Assert
            Assert.IsNotNull(createdAccount);
        }
        [TestMethod]
        public void Get()
        {
            //Arrange
            var accounts = new List<Account>() { new Account() { Id = 1 } };
            var iRepository = new MockIRepository<Account>()
                .MockGet(accounts);
            var accountRepository = new MockAccountRepository();
            var cardRepository = new MockCardRepository();
            var accountService = new AccountService(
                accountRepository.Object,
                new EncryptionService(),
                iRepository.Object,
                cardRepository.Object);
            //Act
            var gotAccounts = accountService.Get();
            //Assert
            Assert.IsNotNull(gotAccounts);
        }
        [TestMethod]
        public void GetById()
        {
            //Arrange
            var account = new Account() { Id = 1 };
            var iRepository = new MockIRepository<Account>()
                .MockGetById(account);
            var accountRepository = new MockAccountRepository();
            var cardRepository = new MockCardRepository();
            var accountService = new AccountService(
                accountRepository.Object,
                new EncryptionService(),
                iRepository.Object,
                cardRepository.Object);

            //Act
            var accountById = accountService.GetById(1);

            //Assert
            Assert.IsNotNull(accountById);
        }
        [TestMethod]
        public void Update()
        {
            //Arrange
            var account = new Account() { Id = 1 };
            var iRepository = new MockIRepository<Account>()
                .MockUpdate(account);
            var accountRepository = new MockAccountRepository();
            var cardRepository = new MockCardRepository();
            var accountService = new AccountService(
                accountRepository.Object,
                new EncryptionService(),
                iRepository.Object,
                cardRepository.Object);

            //Act
            var updatedAccount = accountService.Update(account);

            //Assert
            Assert.IsNotNull(updatedAccount);
        }
        [TestMethod]
        public void GetByUserId()
        {
            //Arrange
            var account = new Account() { Id = 1 };
            var iRepository = new MockIRepository<Account>();
            var accountRepository = new MockAccountRepository()
                .MockGetByUserId(account);
            var cardRepository = new MockCardRepository();
            var accountService = new AccountService(
                accountRepository.Object,
                new EncryptionService(),
                iRepository.Object,
                cardRepository.Object);

            //Act
            var accountByUserId = accountService.GetByUserId(1);

            //Assert
            Assert.IsNotNull(accountByUserId);
        }

    }
}
