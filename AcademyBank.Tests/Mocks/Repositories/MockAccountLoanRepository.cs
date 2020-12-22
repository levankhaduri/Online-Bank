using AcademyBank.DAL.Repositories.Implementations;
using AcademyBank.DAL.Repositories.Interfaces;
using AcademyBank.Models;
using Moq;
using System;
using System.Collections.Generic;
using System.Text;

namespace AcademyBank.Tests.Mocks.Repositories
{
    public class MockAccountLoanRepository : Mock<IAccountLoanRepository<AccountLoan>>
    {
        public MockAccountLoanRepository MockGetPendingLoans(IEnumerable<AccountLoan> result)
        {
            Setup(x => x.GetPendingLoans()).ReturnsAsync(result);
            return this;
        }
        public MockAccountLoanRepository MockGetAccountWithLoan(AccountLoan result)
        {
            Setup(x => x.GetAccountWithLoan(It.IsAny<int>()))
                .ReturnsAsync(result);
            return this;
        }
        public MockAccountLoanRepository MockAccountLoanList(List<AccountLoan> result)
        {
            Setup(x => x.AccountLoanList()).ReturnsAsync(result);
            return this;
        }
        public MockAccountLoanRepository MockApproveLoan(AccountLoan result)
        {
            Setup(x => x.ApproveLoan(It.IsAny<int>()))
                .ReturnsAsync(result);
            return this;
        }
        public MockAccountLoanRepository MockRejectLoan(AccountLoan result)
        {
            Setup(x => x.RejectLoan(It.IsAny<int>()))
                .ReturnsAsync(result);
            return this;
        }
        public MockAccountLoanRepository MockRequestedLoanDetails(AccountLoan result)
        {
            Setup(x => x.RequestedLoanDetails(It.IsAny<int>()))
                .ReturnsAsync(result);
            return this;
        }
        public MockAccountLoanRepository MockGetAccountLoansByUserId(List<AccountLoan> result)
        {
            Setup(x => x.GetAccountLoansByUserId(It.IsAny<int>()))
                .ReturnsAsync(result);
            return this;
        }
    }
}
