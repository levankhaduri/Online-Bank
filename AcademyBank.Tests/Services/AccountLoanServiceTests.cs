using AcademyBank.BLL.Services.Implementations;
using AcademyBank.DAL;
using AcademyBank.Models;
using AcademyBank.Tests.Mocks.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Text;

namespace AcademyBank.Tests.Services
{
    [TestClass]
    public class AccountLoanServiceTests
    {
        [TestMethod]
        public void Create()
        {
            //Arrange
            var accountLoan = new AccountLoan() { Id = 1 };
            var iRepositoryAccountLoan = new MockIRepository<AccountLoan>()
                .MockCreate(accountLoan);
            var accountLoanRepository = new MockAccountLoanRepository();
            var bankDbContext = new BankDbContext();
            var iRepositoryAccount = new MockIRepository<Account>();
            var accountLoanService = new AccountLoanService(
                iRepositoryAccountLoan.Object,
                accountLoanRepository.Object,
                bankDbContext,
                iRepositoryAccount.Object,
                null);
            //Act
            var createdLoan = accountLoanService.Create(accountLoan);
            //Assert
            Assert.IsNotNull(createdLoan);
        }
        [TestMethod]
        public void Get()
        {
            //Arrange
            var loans = new List<AccountLoan>() { new AccountLoan() { Id = 1 } };
            var iRepositoryAccountLoan = new MockIRepository<AccountLoan>()
                .MockGet(loans);
            var accountLoanRepository = new MockAccountLoanRepository();
            var bankDbContext = new BankDbContext();
            var iRepositoryAccount = new MockIRepository<Account>();
            var accountLoanService = new AccountLoanService(
                iRepositoryAccountLoan.Object,
                accountLoanRepository.Object,
                bankDbContext,
                iRepositoryAccount.Object,
                null);
            //Act
            var accounLoans = accountLoanService.Get();
            //Assert
            Assert.IsNotNull(accounLoans);
        }
        [TestMethod]
        public void GetById()
        {
            //Arrange
            var accountLoan = new AccountLoan() { Id = 1 };
            var iRepositoryAccountLoan = new MockIRepository<AccountLoan>()
                .MockGetById(accountLoan);
            var accountLoanRepository = new MockAccountLoanRepository();
            var bankDbContext = new BankDbContext();
            var iRepositoryAccount = new MockIRepository<Account>();
            var accountLoanService = new AccountLoanService(
                iRepositoryAccountLoan.Object,
                accountLoanRepository.Object,
                bankDbContext,
                iRepositoryAccount.Object,
                null);
            //Act
            var loanById = accountLoanService.GetById(1);
            //Assert
            Assert.IsNotNull(loanById);
        }
        [TestMethod]
        public void Update()
        {
            //Arrange
            var accountLoan = new AccountLoan() { Id = 1 };
            var iRepositoryAccountLoan = new MockIRepository<AccountLoan>()
                .MockUpdate(accountLoan);
            var accountLoanRepository = new MockAccountLoanRepository();
            var bankDbContext = new BankDbContext();
            var iRepositoryAccount = new MockIRepository<Account>();
            var accountLoanService = new AccountLoanService(
                iRepositoryAccountLoan.Object,
                accountLoanRepository.Object,
                bankDbContext,
                iRepositoryAccount.Object,
                null);
            //Act
            var updatedLoan = accountLoanService.Update(accountLoan);
            //Assert
            Assert.IsNotNull(updatedLoan);
        }
        [TestMethod]
        public void GetPendingLoans()
        {
            //Arrange
            var loans = new List<AccountLoan>() { new AccountLoan() { Id = 1 } };
            var iRepositoryAccountLoan = new MockIRepository<AccountLoan>();
            var accountLoanRepository = new MockAccountLoanRepository()
                .MockGetPendingLoans(loans);
            var bankDbContext = new BankDbContext();
            var iRepositoryAccount = new MockIRepository<Account>();
            var accountLoanService = new AccountLoanService(
                iRepositoryAccountLoan.Object,
                accountLoanRepository.Object,
                bankDbContext,
                iRepositoryAccount.Object,
                null);
            //Act
            var pendingLoans = accountLoanService.GetPendingLoans();
            //Assert
            Assert.IsNotNull(pendingLoans);
        }
        [TestMethod]
        public void GetAccountWithLoan()
        {
            //Arrange
            var loan = new AccountLoan() { Id = 1 };
            var iRepositoryAccountLoan = new MockIRepository<AccountLoan>();
            var accountLoanRepository = new MockAccountLoanRepository()
                .MockGetAccountWithLoan(loan);
            var bankDbContext = new BankDbContext();
            var iRepositoryAccount = new MockIRepository<Account>();
            var accountLoanService = new AccountLoanService(
                iRepositoryAccountLoan.Object,
                accountLoanRepository.Object,
                bankDbContext,
                iRepositoryAccount.Object,
                null);
            //Act
            var accountWithLoan = accountLoanService.GetAccountWithLoan(1);
            //Assert
            Assert.IsNotNull(accountWithLoan);
        }
        [TestMethod]
        public void AccountLoanList()
        {
            //Arrange
            var loans = new List<AccountLoan>() { new AccountLoan() { Id = 1 } };
            var iRepositoryAccountLoan = new MockIRepository<AccountLoan>();
            var accountLoanRepository = new MockAccountLoanRepository()
                .MockAccountLoanList(loans);
            var bankDbContext = new BankDbContext();
            var iRepositoryAccount = new MockIRepository<Account>();
            var accountLoanService = new AccountLoanService(
                iRepositoryAccountLoan.Object,
                accountLoanRepository.Object,
                bankDbContext,
                iRepositoryAccount.Object,
                null);
            //Act
            var accountLoanList = accountLoanService.AccountLoanList();
            //Assert
            Assert.IsNotNull(accountLoanList);
        }
        [TestMethod]
        public void ApproveLoan()
        {
            //Arrange
            var loan = new AccountLoan() { Id = 1 };
            var iRepositoryAccountLoan = new MockIRepository<AccountLoan>();
            var accountLoanRepository = new MockAccountLoanRepository()
                .MockApproveLoan(loan);
            var bankDbContext = new BankDbContext();
            var iRepositoryAccount = new MockIRepository<Account>();
            var accountLoanService = new AccountLoanService(
                iRepositoryAccountLoan.Object,
                accountLoanRepository.Object,
                bankDbContext,
                iRepositoryAccount.Object,
                null);
            //Act
            var approvedLoan = accountLoanService.ApproveLoan(1);
            //Assert
            Assert.IsNotNull(approvedLoan);
        }
        [TestMethod]
        public void RejectLoan()
        {
            //Arrange
            var loan = new AccountLoan() { Id = 1 };
            var iRepositoryAccountLoan = new MockIRepository<AccountLoan>();
            var accountLoanRepository = new MockAccountLoanRepository()
                .MockRejectLoan(loan);
            var bankDbContext = new BankDbContext();
            var iRepositoryAccount = new MockIRepository<Account>();
            var accountLoanService = new AccountLoanService(
                iRepositoryAccountLoan.Object,
                accountLoanRepository.Object,
                bankDbContext,
                iRepositoryAccount.Object,
                null);
            //Act
            var rejectedLoan = accountLoanService.RejectLoan(1);
            //Assert
            Assert.IsNotNull(rejectedLoan);
        }
        [TestMethod]
        public void RequestedLoanDetails()
        {
            //Arrange
            var loan = new AccountLoan() { Id = 1 };
            var iRepositoryAccountLoan = new MockIRepository<AccountLoan>();
            var accountLoanRepository = new MockAccountLoanRepository()
                .MockRequestedLoanDetails(loan);
            var bankDbContext = new BankDbContext();
            var iRepositoryAccount = new MockIRepository<Account>();
            var accountLoanService = new AccountLoanService(
                iRepositoryAccountLoan.Object,
                accountLoanRepository.Object,
                bankDbContext,
                iRepositoryAccount.Object,
                null);
            //Act
            var loanDetails = accountLoanService.RequestedLoanDetails(1);
            //Assert
            Assert.IsNotNull(loanDetails);
        }
        [TestMethod]
        public void GetAccountLoansByUserId()
        {
            //Arrange
            var loans = new List<AccountLoan>() { new AccountLoan() { Id = 1 } };
            var iRepositoryAccountLoan = new MockIRepository<AccountLoan>();
            var accountLoanRepository = new MockAccountLoanRepository()
                .MockGetAccountLoansByUserId(loans);
            var bankDbContext = new BankDbContext();
            var iRepositoryAccount = new MockIRepository<Account>();
            var accountLoanService = new AccountLoanService(
                iRepositoryAccountLoan.Object,
                accountLoanRepository.Object,
                bankDbContext,
                iRepositoryAccount.Object,
                null);
            //Act
            var accountLoans = accountLoanService.GetAccountLoansByUserId(1);
            //Assert
            Assert.IsNotNull(accountLoans);
        }
    }
}
