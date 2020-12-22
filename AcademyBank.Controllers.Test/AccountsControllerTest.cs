using AcademyBank.BLL.Services.Interfaces;
using AcademyBank.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using Xunit;

namespace AcademyBank.Controllers.Test
{
    public class AccountsControllerTest
    {
        private readonly Mock<IAccountService> _accMock = new Mock<IAccountService>();
        private readonly Mock<UserManager<User>> _userMock = new Mock<UserManager<User>>();
        private readonly Mock<ILoanService> _loanMock = new Mock<ILoanService>();
        private readonly Mock<IDepositService> _depositMock = new Mock<IDepositService>();
        private readonly Mock<IAccountDepositService> _accDepoMock = new Mock<IAccountDepositService>();
        private readonly Mock<IAccountLoanService> _AccLoanMock = new Mock<IAccountLoanService>();


        //[Fact]
        public async Task Index_ReturnsAViewResult()
        {
            // Arrange
            var claim = new DefaultHttpContext
            {
                User = new ClaimsPrincipal(new ClaimsIdentity(new Claim[]
                                            {
                                                new Claim(ClaimTypes.Name, "foo")
                                            }))
            };

            var user = GetUser();
            _userMock.Setup(repo => repo.GetUserAsync(claim.User))
                        .ReturnsAsync(user);

            var controller = new AccountsController(_accMock.Object, UserManagerMockHelper().Object, _loanMock.Object, _depositMock.Object,_accDepoMock.Object,_AccLoanMock.Object);

            // Act
            var result = await controller.Index();

            // Assert
            Assert.IsType<System.Web.Mvc.ViewResult>(result);
        }

        [Fact]
        public async Task Details_ReturnsANotFoundPage_WithAccountNull()
        {
            // Arrange
            var testId = 123;
            _accMock.Setup(repo => repo.GetById(testId))
               .ReturnsAsync((Account)null);


            var controller = new AccountsController(_accMock.Object, UserManagerMockHelper().Object, _loanMock.Object, _depositMock.Object, _accDepoMock.Object, _AccLoanMock.Object);

            // Act
            var result = await controller.Details(testId);

            // Assert
            var notFoundObjectResult = Assert.IsType<NotFoundResult>(result);
            Assert.Equal(404, notFoundObjectResult.StatusCode);
        }

        [Fact]
        public async Task Details_ReturnsANotFoundPage_WithValidAccountId()
        {
            // Arrange
            var testId = 123;
            _accMock.Setup(repo => repo.Get(testId))
               .ReturnsAsync(GetAccount());

            _depositMock.Setup(repo => repo.GetById(testId))
             .ReturnsAsync((Deposit)null);

            _loanMock.Setup(repo => repo.GetById(testId))
             .ReturnsAsync((Loan)null);


            var controller = new AccountsController(_accMock.Object, UserManagerMockHelper().Object, _loanMock.Object, _depositMock.Object, _accDepoMock.Object, _AccLoanMock.Object);

            // Act
            var result = await controller.Details(testId);

            //Assert
            Assert.IsType<NotFoundResult>(result);

        }


        [Fact]
        public async Task Delete_ReturnsANotFoundPage_WithInValidId()
        {
            // Arrange
            var testId = 123;
            _accMock.Setup(repo => repo.Get(testId))
               .ReturnsAsync((IEnumerable<Account>)null);
            var controller = new AccountsController(_accMock.Object, UserManagerMockHelper().Object, _loanMock.Object, _depositMock.Object, _accDepoMock.Object, _AccLoanMock.Object);

            // Act
            var result = await controller.Details(testId);

            //Assert
            Assert.IsType<NotFoundResult>(result);
        }

        [Fact]
        public async Task Delete_ReturnsViewPage_WithValidId()
        {
            // Arrange
            var testId = 123;
            _accMock.Setup(repo => repo.Get(testId))
               .ReturnsAsync(GetAccount());
            var controller = new AccountsController(_accMock.Object, UserManagerMockHelper().Object, _loanMock.Object, _depositMock.Object, _accDepoMock.Object, _AccLoanMock.Object);

            // Act
            var result = await controller.Details(testId);

            //Assert
            Assert.IsType<NotFoundResult>(result);

        }


        //[Fact]
        public async Task DeleteConfirmed_ReturnsredirectedViewPage()
        {
            // Arrange
            var testId = 123;
            var account = _accMock.Setup(repo => repo.Get(testId))
               .ReturnsAsync(GetAccount());

            var controller = new AccountsController(_accMock.Object, UserManagerMockHelper().Object, _loanMock.Object, _depositMock.Object, _accDepoMock.Object, _AccLoanMock.Object);

            // Act
            var result = await controller.DeleteConfirmed(testId);

            //Assert
            Assert.IsType<System.Web.Mvc.RedirectResult>(result);

        }

        private Mock<UserManager<User>> UserManagerMockHelper()
        {
            var mockstore = Mock.Of<IUserStore<User>>();
            return new Mock<UserManager<User>>(mockstore, null, null, null, null, null, null, null, null);
        }

        private User GetUser()
        {
            var user = new User()
            {
                Id = 1,
                UserInfoId = 1,
                Email = "Example@gmail.com",
                LockoutEnd = DateTime.Now,
                NormalizedEmail = "Emxample@gmail.com",
                NormalizedUserName = "Test",
                PasswordHash = "123123ASDASD123123",
                PhoneNumber = "123123",
                PhoneNumberConfirmed = true,
                SecurityStamp = "Test",
                UserInfo = new UserInfo()
                {
                    Id = 1,
                    FirstName = "Fortest",
                    LastName = "ForTest",
                    Sex = "Male",
                    PassportId = "12312312312",
                    City = "Tbilisi",
                    Address = "Politkovskaia 10"
                },
                UserName = "TEST"
            };
            return user;
        }

        private List<Account> GetAccount()
        {
            var account = new List<Account>();
            account.Add(new Account()
            {
                Id = 1,
                UserId = 1,
                Balance = 100,
                AccountName = "blabla",
                AccountNumber = "123123"
            });

            return account;
        }
    }
}
