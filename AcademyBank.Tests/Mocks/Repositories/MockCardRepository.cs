using AcademyBank.DAL.Repositories.Implementations;
using AcademyBank.DAL.Repositories.Interfaces;
using AcademyBank.Models;
using Moq;
using System;
using System.Collections.Generic;
using System.Text;

namespace AcademyBank.Tests.Mocks.Repositories
{
    public class MockCardRepository : Mock<ICardRepository<Card>>
    {
        public MockCardRepository MockGetByNumber(Card result)
        {
            Setup(x => x.GetByNumber(It.IsAny<string>()))
                .ReturnsAsync(result);
            return this;
        }
        public MockCardRepository MockGetCardsByUserId(List<Card> result)
        {
            Setup(x => x.GetCardsByUserId(It.IsAny<int>()))
                .ReturnsAsync(result);
            return this;
        }
    }
}
