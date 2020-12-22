using AcademyBank.DAL.Repositories.Interfaces;
using Moq;
using System;
using System.Collections.Generic;
using System.Text;

namespace AcademyBank.Tests.Mocks.Repositories
{
    public class MockIRepository<T> : Mock<IRepository<T>>
    {
        public MockIRepository<T> MockCreate(T result)
        {
            Setup(x => x.Create(It.IsAny<T>()))
                .ReturnsAsync(result);
            return this;
        }
        public MockIRepository<T> MockGet(IEnumerable<T> result)
        {
            Setup(x => x.Get()).ReturnsAsync(result);
            return this;
        }
        public MockIRepository<T> MockGetById(T result)
        {
            Setup(x => x.GetById(1)).ReturnsAsync(result);
            return this;
        }
        public MockIRepository<T> MockUpdate(T result)
        {
            Setup(x => x.Update(It.IsAny<T>()))
                .ReturnsAsync(result);
            return this;
        }
        
    }
}
