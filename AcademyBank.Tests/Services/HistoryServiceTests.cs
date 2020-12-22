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
    public class HistoryServiceTests
    {
        [TestMethod]
        public void Create()
        {
            //Arrange
            var history = new TransactionsHistory() { Id = 1 };
            var historyRepository = new MockIRepository<TransactionsHistory>()
                .MockCreate(history);
            var historyService = new HistoryService(
                historyRepository.Object,
                null,
                new MockIRepository<Account>().Object);

            //Act
            var createdHistory = historyService.Create(history);

            //Assert
            Assert.IsNotNull(createdHistory);
        }
        [TestMethod]
        public void Get()
        {
            //Arrange
            var histories = new List<TransactionsHistory>() { new TransactionsHistory() { Id = 1 } };
            var historyRepository = new MockIRepository<TransactionsHistory>()
                .MockGet(histories);
            var historyService = new HistoryService(
                historyRepository.Object,
                null,
                new MockIRepository<Account>().Object);

            //Act
            var getHistories = historyService.Get();

            //Assert
            Assert.IsNotNull(getHistories);
        }
        [TestMethod]
        public void GetById()
        {
            //Arrange
            var history = new TransactionsHistory() { Id = 1 };
            var historyRepository = new MockIRepository<TransactionsHistory>()
                .MockGetById(history);
            var historyService = new HistoryService(
                historyRepository.Object,
                null,
                new MockIRepository<Account>().Object);

            //Act
            var historyById = historyService.GetById(1);

            //Assert
            Assert.IsNotNull(historyById);
        }
        [TestMethod]
        public void Update()
        {
            //Arrange
            var history = new TransactionsHistory() { Id = 1 };
            var historyRepository = new MockIRepository<TransactionsHistory>()
                .MockUpdate(history);
            var historyService = new HistoryService(
                historyRepository.Object,
                null,
                new MockIRepository<Account>().Object);

            //Act
            var updatedHistory = historyService.Update(history);

            //Assert
            Assert.IsNotNull(updatedHistory);
        }
    }
}
