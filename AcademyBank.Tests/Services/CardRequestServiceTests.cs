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
    public class CardRequestServiceTests
    {
        [TestMethod]
        public void GetById()
        {
            //Arrange
            var card = new Card() { Id = 1 };
            var cardRequestRepository = new MockCardRequestRepository()
                .MockGetById(card);
            var cardRequestService = new CardRequestService(cardRequestRepository.Object, null);

            //Act
            var cardById = cardRequestService.GetById(1);

            //Assert
            Assert.IsNotNull(cardById);
        }
        [TestMethod]
        public void GetPendingCards()
        {
            //Arrange
            var cards = new List<Card>() { new Card() { Id = 1 } };
            var cardRequestRepository = new MockCardRequestRepository()
                .MockGetPendingCards(cards);
            var cardRequestService = new CardRequestService(cardRequestRepository.Object, null);

            //Act
            var pendingCards = cardRequestService.GetPendingCards();

            //Assert
            Assert.IsNotNull(pendingCards);
        }
        [TestMethod]
        public void GetRejectedCards()
        {
            //Arrange
            var cards = new List<Card>() { new Card() { Id = 1 } };
            var cardRequestRepository = new MockCardRequestRepository()
                .MockGetRejectedCards(cards);
            var cardRequestService = new CardRequestService(cardRequestRepository.Object, null);

            //Act
            var rejectedCards = cardRequestService.GetRejectedCards();

            //Assert
            Assert.IsNotNull(rejectedCards);
        }
        [TestMethod]
        public void GetActiveCards()
        {
            //Arrange
            var cards = new List<Card>() { new Card() { Id = 1 } };
            var cardRequestRepository = new MockCardRequestRepository()
                .MockGetActiveCards(cards);
            var cardRequestService = new CardRequestService(cardRequestRepository.Object, null);

            //Act
            var activeCards = cardRequestService.GetActiveCards();

            //Assert
            Assert.IsNotNull(activeCards);
        }
        [TestMethod]
        public void ApproveCard()
        {
            //Arrange
            var card = new Card() { Id = 1 };
            var cardRequestRepository = new MockCardRequestRepository()
                .MockApproveCard(card);
            var cardRequestService = new CardRequestService(cardRequestRepository.Object, null);

            //Act
            var approvedCard = cardRequestService.ApproveCard(1, card);

            //Assert
            Assert.IsNotNull(approvedCard);
        }
        [TestMethod]
        public void Reject()
        {
            //Arrange
            var card = new Card() { Id = 1 };
            var cardRequestRepository = new MockCardRequestRepository()
                .MockReject(card);
            var cardRequestService = new CardRequestService(cardRequestRepository.Object, null);

            //Act
            var rejectedCard = cardRequestService.Reject(1);

            //Assert
            Assert.IsNotNull(rejectedCard);
        }
    }
}
