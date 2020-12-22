using AcademyBank.DAL.Repositories.Implementations;
using AcademyBank.DAL.Repositories.Interfaces;
using AcademyBank.Models;
using Moq;
using System;
using System.Collections.Generic;
using System.Text;

namespace AcademyBank.Tests.Mocks.Repositories
{
    public class MockAccountRepository : Mock<IAccountRepository<Account>>
    {
        public MockAccountRepository MockGetByUserId(Account result)
        {
            Setup(x => x.GetByUserId(It.IsAny<int>()))
                .ReturnsAsync(result);
            return this;
        }
    }
}
