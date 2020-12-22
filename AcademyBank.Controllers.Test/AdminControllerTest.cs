using AcademyBank.BLL.Services.Interfaces;
using AcademyBank.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace AcademyBank.Controllers.Test
{
    public class AdminControllerTest
    {
        private readonly Mock<IAdminService> _adminMock = new Mock<IAdminService>();
        private readonly Mock<IUserService> _userMock = new Mock<IUserService>();
        private readonly Mock<ILoginReportService> _loginMock = new Mock<ILoginReportService>();
        private readonly Mock<IUserInfoService> _userInfoMock = new Mock<IUserInfoService>();
        private readonly Mock<UserManager<User>> _userManagerMock = new Mock<UserManager<User>>();
        private readonly Mock<ITransfersReportService> _transfersReportsMock = new Mock<ITransfersReportService>();
        private readonly Mock<ICountersReportService> _countersReportMock = new Mock<ICountersReportService>();
        private readonly Mock<IFiltersReportService> _filtersReportMock = new Mock<IFiltersReportService>();

        [Fact]
        public async Task Index_ReturnsAViewResult()
        {
            // Arrange
            _adminMock.Setup(repo => repo.IndexStatisticsData())
               .ReturnsAsync((AdminStatistics)null);

            var controller = new AdminController(_adminMock.Object, _userMock.Object,_loginMock.Object,
                _userInfoMock.Object,_userManagerMock.Object,
                _transfersReportsMock.Object,_countersReportMock.Object,_filtersReportMock.Object);

            //Act
            var result = await controller.Index();

            // Assert
            var viewResult = Assert.IsType<ViewResult>(result);

        }
        [Fact]
        public async Task LoansManagment_ReturnsAViewResult()
        {
            // Arrange
            _adminMock.Setup(repo => repo.GetLoansList(""))
               .ReturnsAsync(GetLoans());

            var controller = new AdminController(_adminMock.Object, _userMock.Object, _loginMock.Object,
                _userInfoMock.Object, _userManagerMock.Object,
                _transfersReportsMock.Object, _countersReportMock.Object, _filtersReportMock.Object);

            //Act
            var result = await controller.LoansManagment("", 1);

            // Assert
            var viewResult = Assert.IsType<ViewResult>(result);

        }

        [Fact]
        public async Task AddLoanGet_ReturnsAViewResult()
        {
            var controller = new AdminController(_adminMock.Object, _userMock.Object, _loginMock.Object,
                _userInfoMock.Object, _userManagerMock.Object,
                _transfersReportsMock.Object, _countersReportMock.Object, _filtersReportMock.Object);
            //Act
            var result = await controller.AddLoan();

            // Assert
            var viewResult = Assert.IsType<ViewResult>(result);
        }


        [Fact]
        public async Task AddLoanPost_ReturnsAViewResult_WithInvalidModelState()
        {
            //Arrange
            var controller = new AdminController(_adminMock.Object, _userMock.Object, _loginMock.Object,
               _userInfoMock.Object, _userManagerMock.Object,
               _transfersReportsMock.Object, _countersReportMock.Object, _filtersReportMock.Object);
            controller.ModelState.AddModelError("error", "some error");

            // Act
            var result = await controller.AddLoan(loan: null);

            // Assert
            Assert.IsType<ViewResult>(result);
        }

        //[Fact]
        public async Task AddLoanPost_ReturnsARedirectedViewResult_ValidModelState()
        {
            //Arrange

            var controller = new AdminController(_adminMock.Object, _userMock.Object, _loginMock.Object,
                _userInfoMock.Object, _userManagerMock.Object,
                _transfersReportsMock.Object, _countersReportMock.Object, _filtersReportMock.Object);
            var LoanModel = new Loan
            {
                Name = "abc",
                Percentage = 10,
                AccidentInsurance = 10,
                Purpose = "abc",
                Term = "abc",
                Currency = "abc",
                RepaymentSchedule = "abc",
                AdvancedPayment = "abc",
                InterestRate = 10,
                MinAmount = 10,
                MaxAmount = 100,
                Id = 1000,
                InsuranceLoan = 10,
                LoanInterestRate = 10,
                ServiceFee = 10,
                AccountLoans = null,
            };

            //Act
            var result = await controller.AddLoan(loan: LoanModel);

            //Assert
            var redirectToActionResult = Assert.IsType<RedirectToActionResult>(result);
            Assert.Equal("Admin", redirectToActionResult.ControllerName);
            Assert.Equal("LoansManagment", redirectToActionResult.ActionName);
        }

        [Fact]
        public async Task EditLoanGet_ReturnsViewResult()
        {
            //Arrange
            var testid = 12;
            var controller = new AdminController(_adminMock.Object, _userMock.Object, _loginMock.Object,
               _userInfoMock.Object, _userManagerMock.Object,
               _transfersReportsMock.Object, _countersReportMock.Object, _filtersReportMock.Object);
            _adminMock.Setup(repo => repo.GetLoanById(testid))
              .ReturnsAsync((Loan)null);


            //Act
            var result = await controller.EditLoan(testid);

            // Assert
            var viewResult = Assert.IsType<ViewResult>(result);

        }


        [Fact]
        public async Task DeleteLoan_ReturnsNotDoundViewResult_WithIdNull()
        {
            //Arrange
            var controller = new AdminController(_adminMock.Object, _userMock.Object, _loginMock.Object,
               _userInfoMock.Object, _userManagerMock.Object,
               _transfersReportsMock.Object, _countersReportMock.Object, _filtersReportMock.Object);


            //Act
            var result = await controller.DeleteLoan(null);

            //Assert
            var viewResult = Assert.IsType<NotFoundResult>(result);
            Assert.Equal(404, viewResult.StatusCode);
        }


        [Fact]
        public async Task DeleteLoan_ReturnsNotFoundViewResult_WithLoanNull()
        {
            //Arrange
            var testid = 12;
            var controller = new AdminController(_adminMock.Object, _userMock.Object, _loginMock.Object,
               _userInfoMock.Object, _userManagerMock.Object,
               _transfersReportsMock.Object, _countersReportMock.Object, _filtersReportMock.Object);

            _adminMock.Setup(repo => repo.GetLoanById(testid))
              .ReturnsAsync((Loan)null);

            //Act
            var result = await controller.DeleteLoan(testid);

            //Assert
            var viewResult = Assert.IsType<NotFoundResult>(result);
            Assert.Equal(404, viewResult.StatusCode);

        }

        //[Fact]
        public async Task DeleteLoan_ReturnsViewResult()
        {
            //Arrange
            var testid = 12;
            var controller = new AdminController(_adminMock.Object, _userMock.Object, _loginMock.Object,
               _userInfoMock.Object, _userManagerMock.Object,
               _transfersReportsMock.Object, _countersReportMock.Object, _filtersReportMock.Object);
            var LoanModel = new Loan
            {
                Name = "abc",
                Percentage = 10,
                AccidentInsurance = 10,
                Purpose = "abc",
                Term = "abc",
                Currency = "abc",
                RepaymentSchedule = "abc",
                AdvancedPayment = "abc",
                InterestRate = 10,
                MinAmount = 10,
                MaxAmount = 100,
                Id = 1000,
                InsuranceLoan = 10,
                LoanInterestRate = 10,
                ServiceFee = 10,
                AccountLoans = null,
            };

            _adminMock.Setup(repo => repo.GetLoanById(It.IsAny<int>()))
              .ReturnsAsync(LoanModel);

            //Act
            var result = await controller.DeleteLoan(testid);

            //Assert
            var viewResult = Assert.IsType<RedirectResult>(result);
        }

        [Fact]
        public async Task EditLoanPost_ReturnsNotFoundViewResult_WithIdNull()
        {
            //Arrange
            var controller = new AdminController(_adminMock.Object, _userMock.Object, _loginMock.Object,
               _userInfoMock.Object, _userManagerMock.Object,
               _transfersReportsMock.Object, _countersReportMock.Object, _filtersReportMock.Object);

            //Act
            var result = await controller.EditLoan(null, null); ;

            //Assert
            var viewResult = Assert.IsType<NotFoundResult>(result);
            Assert.Equal(404, viewResult.StatusCode);
        }

        [Fact]
        public async Task DepositManagment_ReturnsViewResult()
        {
            //Arrange
            _adminMock.Setup(repo => repo.GetDepositsList(""))
             .ReturnsAsync(GetDeposits());
            var controller = new AdminController(_adminMock.Object, _userMock.Object, _loginMock.Object,
               _userInfoMock.Object, _userManagerMock.Object,
               _transfersReportsMock.Object, _countersReportMock.Object, _filtersReportMock.Object);

            //Act
            var result = await controller.DepositManagment("", 1);

            //Assert
            Assert.IsType<ViewResult>(result);

        }
        [Fact]
        public async Task AddDeposit_ReturnsViewResult()
        {
            //Arrange
            var controller = new AdminController(_adminMock.Object, _userMock.Object, _loginMock.Object,
               _userInfoMock.Object, _userManagerMock.Object,
               _transfersReportsMock.Object, _countersReportMock.Object, _filtersReportMock.Object);

            //Act
            var result = await controller.AddDeposit();

            //Assert
            Assert.IsType<ViewResult>(result);
        }

        //[Fact]
        public async Task AddDepositPost_ReturnsViewResult_WithNullModel()
        {
            //Arrange
            var controller = new AdminController(_adminMock.Object, _userMock.Object, _loginMock.Object,
              _userInfoMock.Object, _userManagerMock.Object,
              _transfersReportsMock.Object, _countersReportMock.Object, _filtersReportMock.Object);

            //Act
            var deposit = new Deposit()
            {

            };
            var result = await controller.AddDeposit(deposit);

            //Assert
            Assert.IsType<ViewResult>(result);
        }

        [Fact]
        public async Task EditDepositPost_ReturnsViewResult()
        {
            //Arrange
            var testid = 12;
            var controller = new AdminController(_adminMock.Object, _userMock.Object, _loginMock.Object,
               _userInfoMock.Object, _userManagerMock.Object,
               _transfersReportsMock.Object, _countersReportMock.Object, _filtersReportMock.Object);
            _adminMock.Setup(repo => repo.GetDepositById(testid))
              .ReturnsAsync((Deposit)null);

            //Act
            var result = await controller.EditDeposit(testid);

            //Assert
            Assert.IsType<ViewResult>(result);
        }

        //[Fact]
        public async Task EditDepositPost_ReturnsRedirectViewResult()
        {
            //Arrange
            var testid = 12;
            var controller = new AdminController(_adminMock.Object, _userMock.Object, _loginMock.Object,
               _userInfoMock.Object, _userManagerMock.Object,
               _transfersReportsMock.Object, _countersReportMock.Object, _filtersReportMock.Object);
            _adminMock.Setup(repo => repo.GetDepositById(testid))
            .ReturnsAsync(GetDeposit());

            //Act
            var result = await controller.DeleteDeposit(testid);

            //Assert
            var redirectToActionResult = Assert.IsType<RedirectToActionResult>(result);
            Assert.Equal("Admin", redirectToActionResult.ControllerName);
            Assert.Equal("DepositManagment", redirectToActionResult.ActionName);
        }

        [Fact]
        public async Task LoanRequests_ReturnsViewResult()
        {
            //Arrange
            var controller = new AdminController(_adminMock.Object, _userMock.Object, _loginMock.Object,
               _userInfoMock.Object, _userManagerMock.Object,
               _transfersReportsMock.Object, _countersReportMock.Object, _filtersReportMock.Object);
            var requests = _adminMock.Setup(repo => repo.GetPendingLoansList(""))
            .ReturnsAsync(new List<AccountLoan>()
            {
                new AccountLoan
                {

                }
            });
            //Act
            var result = await controller.LoanRequests("", 1);

            //Assert
            Assert.IsType<ViewResult>(result);

        }

        [Fact]
        public async Task RequestedLoanDetails_ReturnsViewResult()
        {
            //Arrange
            var testid = 123;
            var controller = new AdminController(_adminMock.Object, _userMock.Object, _loginMock.Object,
              _userInfoMock.Object, _userManagerMock.Object,
              _transfersReportsMock.Object, _countersReportMock.Object, _filtersReportMock.Object);
            _adminMock.Setup(repo => repo.GetRequestedLoanDetailsById(testid))
           .ReturnsAsync((AccountLoan)null);

            //Act
            var result = await controller.RequestedLoanDetails(testid);

            //Assert
            Assert.IsType<ViewResult>(result);

        }

        [Fact]
        public async Task DepositRequests_ReturnsViewResult()
        {
            //Arrange
            var controller = new AdminController(_adminMock.Object, _userMock.Object, _loginMock.Object,
               _userInfoMock.Object, _userManagerMock.Object,
               _transfersReportsMock.Object, _countersReportMock.Object, _filtersReportMock.Object);
            _adminMock.Setup(repo => repo.GetPendingLoansList(""))
            .ReturnsAsync((List<AccountLoan>)null);
            //Act
            var result = await controller.DepositRequests("", 1);

            //Assert
            Assert.IsType<ViewResult>(result);

        }

        [Fact]
        public async Task ActiveUsers_ReturnsViewResult()
        {
            //Arrange
            var controller = new AdminController(_adminMock.Object, _userMock.Object, _loginMock.Object,
               _userInfoMock.Object, _userManagerMock.Object,
               _transfersReportsMock.Object, _countersReportMock.Object, _filtersReportMock.Object);
            _adminMock.Setup(repo => repo.GetActiveUsersList(""))
             .ReturnsAsync(new List<User>()
            {
                new User
                {

                }
            });
            //Act
            var result = await controller.ActiveUsers("", 1);

            //Assert
            Assert.IsType<ViewResult>(result);

        }



        [Fact]
        public async Task CardRequests_ReturnsViewResult()
        {
            //Arrange
            var controller = new AdminController(_adminMock.Object, _userMock.Object, _loginMock.Object,
               _userInfoMock.Object, _userManagerMock.Object,
               _transfersReportsMock.Object, _countersReportMock.Object, _filtersReportMock.Object);
            _adminMock.Setup(repo => repo.GetPendingCards("", ""))
            .ReturnsAsync(new List<Card>()
            {
                new Card
                {

                }
            });
            //Act
            var result = await controller.CardsManagement("", 1, "");

            //Assert
            Assert.IsType<ViewResult>(result);
        }

        [Fact]
        public async Task UserDetails_ReturnsViewResult()
        {
            //Arrange
            var controller = new AdminController(_adminMock.Object, _userMock.Object, _loginMock.Object,
               _userInfoMock.Object, _userManagerMock.Object,
               _transfersReportsMock.Object, _countersReportMock.Object, _filtersReportMock.Object);
            _adminMock.Setup(repo => repo.GetUserById(123))
            .ReturnsAsync((User)null);
            //Act
            var result = await controller.UserDetails(123);
            //Assert
            Assert.IsType<NotFoundResult>(result);
        }

        [Fact]
        public async Task BlockUser_ReturnsViewResult()
        {
            //Arrange
            var controller = new AdminController(_adminMock.Object, _userMock.Object, _loginMock.Object,
               _userInfoMock.Object, _userManagerMock.Object,
               _transfersReportsMock.Object, _countersReportMock.Object, _filtersReportMock.Object);
            _adminMock.Setup(repo => repo.GetUserById(123))
            .ReturnsAsync((User)null);
            //Act
            var result = await controller.BlockUser(123);
            //Assert
            Assert.IsType<NotFoundResult>(result);
        }

        [Fact]
        public async Task TransactionsHistory_ReturnsViewResult()
        {
            //Arrange
            var controller = new AdminController(_adminMock.Object, _userMock.Object, _loginMock.Object,
               _userInfoMock.Object, _userManagerMock.Object,
               _transfersReportsMock.Object, _countersReportMock.Object, _filtersReportMock.Object);
            _adminMock.Setup(repo => repo.GetTransactionsHistoriesList(""))
             .ReturnsAsync(new List<TransactionsHistory>()
            {
                new TransactionsHistory
                {

                }
            });
            //Act
            var result = await controller.TransactionsHistory("", 1);
            //Assert
            Assert.IsType<ViewResult>(result);
        }

        [Fact]
        public async Task BlockedUsers_ReturnsViewResult()
        {
            //Arrange
            var controller = new AdminController(_adminMock.Object, _userMock.Object, _loginMock.Object,
               _userInfoMock.Object, _userManagerMock.Object,
               _transfersReportsMock.Object, _countersReportMock.Object, _filtersReportMock.Object);
            _adminMock.Setup(repo => repo.GetBlockedUsersList(""))
            .ReturnsAsync(new List<User>()
            {
                new User
                {

                }
            });
            //Act
            var result = await controller.BlockedUsers("", 1);
            //Assert
            Assert.IsType<ViewResult>(result);
        }

        [Fact]
        public async Task TransactionHistoryDetails_ReturnsViewResult()
        {
            //Arrange
            var controller = new AdminController(_adminMock.Object, _userMock.Object, _loginMock.Object,
               _userInfoMock.Object, _userManagerMock.Object,
               _transfersReportsMock.Object, _countersReportMock.Object, _filtersReportMock.Object);
            _adminMock.Setup(repo => repo.GetTransactionsHistoryById(123))
            .ReturnsAsync((TransactionsHistory)null);
            //Act
            var result = await controller.TransactionHistoryDetails(123);
            //Assert
            Assert.IsType<ViewResult>(result);
        }

        private List<Deposit> GetDeposits()
        {
            var deposit = new List<Deposit>();
            deposit.Add(new Deposit()
            {

            });

            return deposit;
        }

        private Deposit GetDeposit()
        {
            var deposit = new Deposit()
            {
                Name = "Deposit",
                Annual = 10,
                Bonus = 10,
                InterestPaymentDate = default,
                MinAmount = 10,
                MaxAMount = 20,
                Replenishment = 5,
                InterestRate = 5,
            };
            return deposit;

        }


        private List<Loan> GetLoans()
        {
            var accountLoans = new List<Loan>();
            accountLoans.Add(new Loan()
            {

            });

            return accountLoans;
        }
    }
}
