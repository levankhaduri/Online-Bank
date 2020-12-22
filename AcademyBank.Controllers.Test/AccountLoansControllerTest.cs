using AcademyBank.BLL.Helpers.Implementations;
using AcademyBank.BLL.Services.Interfaces;
using AcademyBank.Models;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.Extensions.Logging;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using System.Security.Principal;

namespace AcademyBank.Controllers.Test
{
    public class AccountLoansControllerTest
    {
        private readonly Mock<IAccountLoanService> _accLoanMock = new Mock<IAccountLoanService>();
        private readonly Mock<IAccountService> _accMock = new Mock<IAccountService>();
        private readonly Mock<ILoanService> _loanMock = new Mock<ILoanService>();
        private readonly Mock<IMapper> _mapperMock = new Mock<IMapper>();
        private readonly Mock<IUserService> _userMock = new Mock<IUserService>();
        public System.Security.Principal.IPrincipal User { get; set; }

        [Fact]
        public async Task Index_ReturnsAViewResult_WithAListOfLoans()
        {
            // Arrange
            _loanMock.Setup(repo => repo.Get())
                .ReturnsAsync(GetAccountLoans());

            //_mapperMock.Setup(repo => repo.Map<IEnumerable<Loan>(null,null,null))
            //   .ReturnsAsync((IEnumerable<Loan>)null);
            var controller = new AccountLoansController(_accLoanMock.Object, _accMock.Object, _loanMock.Object,
                                                        _mapperMock.Object, UserManagerMockHelper().Object, _userMock.Object);

            var user = new ClaimsPrincipal(new ClaimsIdentity(new Claim[]
                                            {
                                                new Claim(ClaimTypes.Name, "example name"),
                                                new Claim(ClaimTypes.NameIdentifier, "1"),
                                                new Claim("custom-claim", "example claim value"),
                                            }, "mock"));

            controller.ControllerContext = new ControllerContext()
            {
                HttpContext = new DefaultHttpContext() { User = user }
            };



            //Act
            var result = await controller.Index(null, null, null);

            // Assert
            var viewResult = Assert.IsType<ViewResult>(result);
        }

        [Fact]
        public async Task Details_ReturnsHttpNotFound_ForNullId()
        {
            // Arrange
            var controller = new AccountLoansController(_accLoanMock.Object, _accMock.Object, _loanMock.Object,
                                                         _mapperMock.Object, UserManagerMockHelper().Object, _userMock.Object);

            var user = new ClaimsPrincipal(new ClaimsIdentity(new Claim[]
                                            {
                                                new Claim(ClaimTypes.Name, "example name"),
                                                new Claim(ClaimTypes.NameIdentifier, "1"),
                                                new Claim("custom-claim", "example claim value"),
                                            }, "mock"));

            controller.ControllerContext = new ControllerContext()
            {
                HttpContext = new DefaultHttpContext() { User = user }
            };

            // Act
            var result = await controller.Details(null);

            // Assert
            var notFoundObjectResult = Assert.IsType<NotFoundResult>(result);
            Assert.Equal(404, notFoundObjectResult.StatusCode);
        }

        [Fact]
        public async Task Details_ReturnsHttpNotFound_ForNotNullInvalidId()
        {
            // Arrange
            int testId = 123;
            _accLoanMock.Setup(repo => repo.GetById(testId))
                .ReturnsAsync((AccountLoan)null);
            var controller = new AccountLoansController(_accLoanMock.Object, _accMock.Object, _loanMock.Object,
                                                         _mapperMock.Object, UserManagerMockHelper().Object, _userMock.Object);
            var user = new ClaimsPrincipal(new ClaimsIdentity(new Claim[]
                                            {
                                                new Claim(ClaimTypes.Name, "example name"),
                                                new Claim(ClaimTypes.NameIdentifier, "1"),
                                                new Claim("custom-claim", "example claim value"),
                                            }, "mock"));

            controller.ControllerContext = new ControllerContext()
            {
                HttpContext = new DefaultHttpContext() { User = user }
            };

            // Act
            var result = await controller.Details(testId);

            // Assert
            var notFoundObjectResult = Assert.IsType<NotFoundResult>(result);
            Assert.Equal(404, notFoundObjectResult.StatusCode);
        }

        //[Fact]
        public async Task Create_ReturnsViewResultType()
        {
            // Arrange
            var _userManagerMock = new Mock<UserManager<User>>();


            //_userManagerMock.Setup(repo => repo.GetUserId(User.Identity.Name);
            //    .Returns("123");

            var controller = new AccountLoansController(_accLoanMock.Object, _accMock.Object, _loanMock.Object,
                                                         _mapperMock.Object, UserManagerMockHelper().Object, _userMock.Object);

            var httpContext = new DefaultHttpContext();
            var tempData = new TempDataDictionary(httpContext, Mock.Of<ITempDataProvider>());
            tempData["LoanId"] = null;
            controller.TempData = tempData;

            // Act
            var result = await controller.Create(123);

            // Assert
            Assert.IsType<ViewResult>(result);
        }

        [Fact]
        public async Task Create_ReturnsViewResultType_ForInvalidModel()
        {
            // Arrange
            var controller = new AccountLoansController(_accLoanMock.Object, _accMock.Object, _loanMock.Object,
                                                         _mapperMock.Object, UserManagerMockHelper().Object, _userMock.Object);

            var httpContext = new DefaultHttpContext();
            var tempData = new TempDataDictionary(httpContext, Mock.Of<ITempDataProvider>());
            tempData["LoanId"] = null;
            controller.TempData = tempData;

            controller.ModelState.AddModelError("error", "some error");

            // Act
            var result = await controller.Create(accountLoanViewModel: null);

            // Assert
            Assert.IsType<ViewResult>(result);
        }

        //[Fact]
        public async Task Create_ReturnsARedirectToIndexAction_ForValidModel()
        {
            // Arrange
            var controller = new AccountLoansController(_accLoanMock.Object, _accMock.Object, _loanMock.Object,
                                                         _mapperMock.Object, UserManagerMockHelper().Object, _userMock.Object);

            var httpContext = new DefaultHttpContext();
            var tempData = new TempDataDictionary(httpContext, Mock.Of<ITempDataProvider>());
            tempData["LoanId"] = null;
            controller.TempData = tempData;

            // Act
            var AccloanModel = new AccountLoanViewModel
            {
                Employment = "test",
                Sum = 1000,
                Currency = "GEL",
                Term = "test",
                OfficePhoneNumber = "+995 598 545 555"
            };

            var mapperResult = _mapperMock.Setup(m => m.Map<AccountLoanViewModel>(It.IsAny<AccountLoanViewModel>())).Returns(AccloanModel);



            var result = await controller.Create(accountLoanViewModel: AccloanModel);

            // Assert
            var redirectToActionResult = Assert.IsType<RedirectToActionResult>(result);
            Assert.Equal("AccountLoans", redirectToActionResult.ControllerName);
            Assert.Equal("Index", redirectToActionResult.ActionName);
        }

        private Mock<UserManager<User>> UserManagerMockHelper()
        {
            var mockstore = Mock.Of<IUserStore<User>>();
            return new Mock<UserManager<User>>(mockstore, null, null, null, null, null, null, null, null);
        }

        private List<Loan> GetAccountLoans()
        {
            var accountLoans = new List<Loan>();
            accountLoans.Add(new Loan()
            {

            });

            return accountLoans;
        }
    }
}
