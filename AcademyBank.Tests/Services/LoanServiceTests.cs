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
    public class LoanServiceTests
    {
        [TestMethod]
        public void Create()
        {
            //Arrange
            var loan = new Loan() { Id = 1 };
            var loanRepository = new MockIRepository<Loan>()
                .MockCreate(loan);
            var loanService = new LoanService(loanRepository.Object);

            //Act
            var createdLoan = loanService.Create(loan);

            //Assert
            Assert.IsNotNull(createdLoan);
        }
        [TestMethod]
        public void Get()
        {
            //Arrange
            var loans = new List<Loan>() { new Loan() { Id = 1 } };
            var loanRepository = new MockIRepository<Loan>()
                .MockGet(loans);
            var loanService = new LoanService(loanRepository.Object);

            //Act
            var getLoans = loanService.Get();

            //Assert
            Assert.IsNotNull(getLoans);
        }
        [TestMethod]
        public void GetById()
        {
            //Arrange
            var loan = new Loan() { Id = 1 };
            var loanRepository = new MockIRepository<Loan>()
                .MockGetById(loan);
            var loanService = new LoanService(loanRepository.Object);

            //Act
            var loansById = loanService.GetById(1);

            //Assert
            Assert.IsNotNull(loansById);
        }
        [TestMethod]
        public void Update()
        {
            //Arrange
            var loan = new Loan() { Id = 1 };
            var loanRepository = new MockIRepository<Loan>()
                .MockUpdate(loan);
            var loanService = new LoanService(loanRepository.Object);

            //Act
            var updatedLoan = loanService.Update(loan);

            //Assert
            Assert.IsNotNull(updatedLoan);
        }
    }
}
