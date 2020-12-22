using AcademyBank.BLL.Services.Implementations;
using AcademyBank.DAL;
using AcademyBank.DAL.Repositories.Interfaces;
using AcademyBank.Models;
using AcademyBank.Tests.Mocks.Repositories;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Text;

namespace AcademyBank.Tests.Services
{
    [TestClass]
    public class AccountDepositServiceTests
    {
        [TestMethod]
        public void Create()
        {
            //Arrange
            var accountDeposit = new AccountDeposit() { Id = 1 };
            var iRepositoryAccountDeposit = new MockIRepository<AccountDeposit>()
                .MockCreate(accountDeposit);
            var accountDepositRepository = new MockAccountDepositRepository();
            var bankDbContext = new BankDbContext();
            var iRepositoryAccount = new MockIRepository<Account>();
            var accountDepositService = new AccountDepositService(
                accountDepositRepository.Object,
                iRepositoryAccountDeposit.Object,
                null,
                null,
                iRepositoryAccount.Object,
                bankDbContext,
                null);
            //Act
            var createdDeposit = accountDepositService.Create(accountDeposit);
            //Assert
            Assert.IsNotNull(createdDeposit);
        }
        [TestMethod]
        public void Get()
        {
            //Arrange
            var deposits = new List<AccountDeposit>() { new AccountDeposit() { Id = 1 } };
            var iRepositoryAccountDeposit = new MockIRepository<AccountDeposit>()
                .MockGet(deposits);
            var accountDepositRepository = new MockAccountDepositRepository();
            var bankDbContext = new BankDbContext();
            var iRepositoryAccount = new MockIRepository<Account>();
            var accountDepositService = new AccountDepositService(
                accountDepositRepository.Object,
                iRepositoryAccountDeposit.Object,
                null,
                null,
                iRepositoryAccount.Object,
                bankDbContext,
                null);
            //Act
            var accountDeposits = accountDepositService.Get();
            //Assert
            Assert.IsNotNull(accountDeposits);
        }
        [TestMethod]
        public void GetById()
        {
            //Arrange
            var deposit = new AccountDeposit() { Id = 1 };
            var iRepositoryAccountDeposit = new MockIRepository<AccountDeposit>()
                .MockGetById(deposit);
            var accountDepositRepository = new MockAccountDepositRepository();
            var bankDbContext = new BankDbContext();
            var iRepositoryAccount = new MockIRepository<Account>();
            var accountDepositService = new AccountDepositService(
                accountDepositRepository.Object,
                iRepositoryAccountDeposit.Object,
                null,
                null,
                iRepositoryAccount.Object,
                bankDbContext,
                null);
            //Act
            var accountDeposit = accountDepositService.GetById(1);
            //Assert
            Assert.IsNotNull(accountDeposit);
        }
        [TestMethod]
        public void Update()
        {
            //Arrange
            var accountDeposit = new AccountDeposit() { Id = 1 };
            var iRepositoryAccountDeposit = new MockIRepository<AccountDeposit>()
                .MockUpdate(accountDeposit);
            var accountDepositRepository = new MockAccountDepositRepository();
            var bankDbContext = new BankDbContext();
            var iRepositoryAccount = new MockIRepository<Account>();
            var accountDepositService = new AccountDepositService(
                accountDepositRepository.Object,
                iRepositoryAccountDeposit.Object,
                null,
                null,
                iRepositoryAccount.Object,
                bankDbContext,
                null);
            //Act
            var updatedDeposit = accountDepositService.Update(accountDeposit);

            //Assert
            Assert.IsNotNull(updatedDeposit);
        }
        [TestMethod]
        public void GetPendingDeposits()
        {
            //Arrange
            var deposits = new List<AccountDeposit>() { new AccountDeposit() { Id = 1 } };
            var iRepositoryAccountDeposit = new MockIRepository<AccountDeposit>();
            var accountDepositRepo = new MockAccountDepositRepository()
                .MockGetPendingDeposits(deposits);
            var accountDepositRepository = new MockAccountDepositRepository();
            var bankDbContext = new BankDbContext();
            var iRepositoryAccount = new MockIRepository<Account>();
            var accountDepositService = new AccountDepositService(
                accountDepositRepository.Object,
                iRepositoryAccountDeposit.Object,
                null,
                null,
                iRepositoryAccount.Object,
                bankDbContext,
                null);
            //Act
            var pendingDeposits = accountDepositService.GetPendingDeposits();

            //Assert
            Assert.IsNotNull(pendingDeposits);
        }
        [TestMethod]
        public void ApproveDeposit()
        {
            //Arrange
            var accountDeposit = new AccountDeposit() { Id = 1 };
            var accountDepositRepository = new MockAccountDepositRepository()
                .MockApproveDeposit(accountDeposit);
            var iRepositoryAccountDeposit = new MockIRepository<AccountDeposit>();
            var bankDbContext = new BankDbContext();
            var iRepositoryAccount = new MockIRepository<Account>();
            var accountDepositService = new AccountDepositService(
                accountDepositRepository.Object,
                iRepositoryAccountDeposit.Object,
                null,
                null,
                iRepositoryAccount.Object,
                bankDbContext,
                null);
            //Act
            var approvedDeposit = accountDepositService.ApproveDeposit(1);

            //Assert
            Assert.IsNotNull(approvedDeposit);
        }
        [TestMethod]
        public void RejectDeposit()
        {
            //Arrange
            var accountDeposit = new AccountDeposit() { Id = 1 };
            var accountDepositRepository = new MockAccountDepositRepository()
                .MockRejectDeposit(accountDeposit);
            var iRepositoryAccountDeposit = new MockIRepository<AccountDeposit>();
            var bankDbContext = new BankDbContext();
            var iRepositoryAccount = new MockIRepository<Account>();
            var accountDepositService = new AccountDepositService(
                accountDepositRepository.Object,
                iRepositoryAccountDeposit.Object,
                null,
                null,
                iRepositoryAccount.Object,
                bankDbContext,
                null);
            //Act
            var rejectedDeposit = accountDepositService.RejectDeposit(1);

            //Assert
            Assert.IsNotNull(rejectedDeposit);
        }
        [TestMethod]
        public void GetAccountDepositsByUserId()
        {
            //Arrange
            var deposits = new List<AccountDeposit>() { new AccountDeposit() { Id = 1 } };
            var accountDepositRepository = new MockAccountDepositRepository()
                .MockGetAccountDepositsByUserId(deposits);
            var iRepositoryAccountDeposit = new MockIRepository<AccountDeposit>();
            var bankDbContext = new BankDbContext();
            var iRepositoryAccount = new MockIRepository<Account>();
            var accountDepositService = new AccountDepositService(
                accountDepositRepository.Object,
                iRepositoryAccountDeposit.Object,
                null,
                null,
                iRepositoryAccount.Object,
                bankDbContext,
                null);
            //Act
            var accoutDeposits = accountDepositService.GetAccountDepositsByUserId(1);

            //Assert
            Assert.IsNotNull(accoutDeposits);
        }
    }
}
