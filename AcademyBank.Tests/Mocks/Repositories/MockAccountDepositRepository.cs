using AcademyBank.DAL.Repositories.Implementations;
using AcademyBank.DAL.Repositories.Interfaces;
using AcademyBank.Models;
using Moq;
using System;
using System.Collections.Generic;
using System.Text;

namespace AcademyBank.Tests.Mocks.Repositories
{
    
    public class MockAccountDepositRepository : Mock<IAccountDepositRepository<AccountDeposit>>
    {
        public MockAccountDepositRepository MockGetPendingDeposits(IEnumerable<AccountDeposit> result)
        {
            Setup(x => x.GetPendingDeposits()).ReturnsAsync(result);
            return this;
        }
        public MockAccountDepositRepository MockApproveDeposit(AccountDeposit result)
        {
            Setup(x => x.ApproveDeposit(It.IsAny<int>()))
                .ReturnsAsync(result);
            return this;
        }
        public MockAccountDepositRepository MockRejectDeposit(AccountDeposit result)
        {
            Setup(x => x.RejectDeposit(It.IsAny<int>()))
                .ReturnsAsync(result);
            return this;
        }
        public MockAccountDepositRepository MockGetAccountDepositsByUserId(List<AccountDeposit> result)
        {
            Setup(x => x.GetAccountDepositsByUserId(It.IsAny<int>()))
                .ReturnsAsync(result);
            return this;
        }
    }
}
