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
    public class DepositServiceTests
    {
        [TestMethod]
        public void Create()
        {
            //Arrange
            var deposit = new Deposit() { Id = 1 };
            var depositRepository = new MockIRepository<Deposit>()
                .MockCreate(deposit);
            var depositService = new DepositService(depositRepository.Object);

            //Act
            var createdDeposit = depositService.Create(deposit);

            //Assert
            Assert.IsNotNull(createdDeposit);
        }
        [TestMethod]
        public void Get()
        {
            //Arrange
            var deposits = new List<Deposit>() { new Deposit() { Id = 1 } };
            var depositRepository = new MockIRepository<Deposit>()
                .MockGet(deposits);
            var depositService = new DepositService(depositRepository.Object);

            //Act
            var getDeposits = depositService.Get();

            //Assert
            Assert.IsNotNull(getDeposits);
        }
        [TestMethod]
        public void GetById()
        {
            //Arrange
            var deposit = new Deposit() { Id = 1 };
            var depositRepository = new MockIRepository<Deposit>()
                .MockGetById(deposit);
            var depositService = new DepositService(depositRepository.Object);

            //Act
            var depositById = depositService.GetById(1);

            //Assert
            Assert.IsNotNull(depositById);
        }
        [TestMethod]
        public void Update()
        {
            //Arrange
            var deposit = new Deposit() { Id = 1 };
            var depositRepository = new MockIRepository<Deposit>()
                .MockUpdate(deposit);
            var depositService = new DepositService(depositRepository.Object);

            //Act
            var updatedDeposit = depositService.Update(deposit);

            //Assert
            Assert.IsNotNull(updatedDeposit);
        }

        [TestMethod]
        public void GetFilteredDeposits()
        {
            //Arrange
            var deposits = new List<Deposit>() { new Deposit() { Id = 1 } };
            var depositRepository = new MockIRepository<Deposit>()
                .MockGet(deposits);
            var depositService = new DepositService(depositRepository.Object);

            //Act
            var getFilteredDeposits = depositService
                .GetFilteredDeposits(default(string), default(decimal));

            //Assert
            Assert.IsNotNull(getFilteredDeposits);
        }
    }
}
