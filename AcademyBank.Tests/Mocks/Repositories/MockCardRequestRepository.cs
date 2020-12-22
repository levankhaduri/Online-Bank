using AcademyBank.DAL.Repositories.Implementations;
using AcademyBank.DAL.Repositories.Interfaces;
using AcademyBank.Models;
using Moq;
using System;
using System.Collections.Generic;
using System.Text;

namespace AcademyBank.Tests.Mocks.Repositories
{
    public class MockCardRequestRepository : Mock<ICardRequestRepository<Card>>
    {
        public MockCardRequestRepository MockGetById(Card result)
        {
            Setup(x => x.GetById(1)).ReturnsAsync(result);
            return this;
        }
        public MockCardRequestRepository MockGetPendingCards(List<Card> result)
        {
            Setup(x => x.GetPendingCards())
                .ReturnsAsync(result);
            return this;
        }
        public MockCardRequestRepository MockGetRejectedCards(List<Card> result)
        {
            Setup(x => x.GetRejectedCards()).ReturnsAsync(result);
            return this;
        }
        public MockCardRequestRepository MockGetActiveCards(List<Card> result)
        {
            Setup(x => x.GetActiveCards()).ReturnsAsync(result);
            return this;
        }
        public MockCardRequestRepository MockApproveCard(Card result)
        {
            Setup(x => x.ApproveCard(It.IsAny<int>(), It.IsAny<Card>()))
                .ReturnsAsync(result);
            return this;
        }
        public MockCardRequestRepository MockReject(Card result)
        {
            Setup(x => x.Reject(It.IsAny<int>()))
                .ReturnsAsync(result);
            return this;
        }
    }
}
