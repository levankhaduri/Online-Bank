using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AcademyBank.BLL.Services.Interfaces;
using AcademyBank.DAL;
using AcademyBank.Models;
using AcademyBank.Models.Enums;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using PagedList;

namespace AcademyBank.Controllers
{
    public class AdminController : Controller
    {
        private readonly IAdminService _adminService;
        private readonly IUserService _userService;
        private readonly IUserInfoService _userInfoService;
        private readonly ILoginReportService _loginReportService;
        private readonly ITransfersReportService _transfersReportService;
        private readonly ICountersReportService _countersReportService;
        private readonly IFiltersReportService _filtersReportService;
        private readonly UserManager<User> _userManager;


        public AdminController(
            IAdminService adminService,
            IUserService userService,
            ILoginReportService loginReportService,
            IUserInfoService userInfoService,
            UserManager<User> userManager,
            ITransfersReportService transfersReportService,
            ICountersReportService countersReportService,
            IFiltersReportService filtersReportService
            )
        {
            _adminService = adminService;
            _userService = userService;
            _loginReportService = loginReportService;
            _userInfoService = userInfoService;
            _userManager = userManager;
            _transfersReportService = transfersReportService;
            _countersReportService = countersReportService;
            _filtersReportService = filtersReportService;

        }
        public async Task<IActionResult> Index()
        {
            var adminStatistics = await _adminService.IndexStatisticsData();
            ViewBag.RequestedLoansCounterMenu = await _adminService.RequestedLoansCounter();
            ViewBag.RequestedDepositsCounterMenu = await _adminService.RequestedDepositsCounter();
            ViewBag.RequestedCardsCounterMenu = await _adminService.RequestedCardssCounter();

            return View(adminStatistics);
        }

        [HttpGet]
        public async Task<IActionResult> LoansManagment(string orderBy, int? page)
        {
            var loans = await _adminService.GetLoansList(orderBy);

            var getPage = page ?? 1;
            loans = loans.ToPagedList(getPage, 10);

            ViewData["loans"] = loans;

            ViewBag.RequestedLoansCounterMenu = await _adminService.RequestedLoansCounter();
            ViewBag.RequestedDepositsCounterMenu = await _adminService.RequestedDepositsCounter();
            ViewBag.RequestedCardsCounterMenu = await _adminService.RequestedCardssCounter();

            return View();
        }
        [HttpGet("{Controller}/LoansManagment/AddLoan")]
        public async Task<IActionResult> AddLoan()
        {
            ViewBag.RequestedLoansCounterMenu = await _adminService.RequestedLoansCounter();
            ViewBag.RequestedDepositsCounterMenu = await _adminService.RequestedDepositsCounter();
            ViewBag.RequestedCardsCounterMenu = await _adminService.RequestedCardssCounter();

            return View();
        }

        [HttpPost("{Controller}/LoansManagment/AddLoan")]
        public async Task<IActionResult> AddLoan(Loan loan)
        {
            if (ModelState.IsValid)
            {
                await _adminService.CreateLoan(loan);
                return Redirect(Url.Action("LoansManagment", "Admin"));
            }
            return View(loan);
        }

        [HttpGet("{Controller}/LoansManagment/EditLoan/{id}")]
        public async Task<IActionResult> EditLoan(int id)
        {
            var loan = await _adminService.GetLoanById(id);
            ViewBag.RequestedLoansCounterMenu = await _adminService.RequestedLoansCounter();
            ViewBag.RequestedDepositsCounterMenu = await _adminService.RequestedDepositsCounter();
            ViewBag.RequestedCardsCounterMenu = await _adminService.RequestedCardssCounter();

            return View(loan);
        }

        [HttpGet("{Controller}/LoansManagment/DeleteLoan/{id?}")]
        public async Task<IActionResult> DeleteLoan(int? id)
        {
            ViewBag.RequestedLoansCounterMenu = await _adminService.RequestedLoansCounter();
            ViewBag.RequestedDepositsCounterMenu = await _adminService.RequestedDepositsCounter();
            ViewBag.RequestedCardsCounterMenu = await _adminService.RequestedCardssCounter();

            if (id == null)
            {
                return NotFound();
            }
            var loan = await _adminService.GetLoanById(id.GetValueOrDefault());
            await _adminService.DeleteLoan(loan);

            if (loan == null)
            {
                return NotFound();
            }

            return Redirect(Url.Action("LoansManagment", "Admin"));
        }

        [HttpPost("{Controller}/LoansManagment/EditLoan/{id}")]
        public async Task<IActionResult> EditLoan(int? id, Loan loan)
        {
            if (id == null)
            {
                return NotFound();
            }

            await _adminService.UpdateLoan(loan);

            if (loan == null)
            {
                return NotFound();
            }

            return Redirect(Url.Action("LoansManagment", "Admin"));
        }
        public async Task<IActionResult> DepositManagment(string orderBy, int? page)
        {
            var deposits = await _adminService.GetDepositsList(orderBy);

            var getPage = page ?? 1;
            deposits = deposits.ToPagedList(getPage, 10);

            ViewBag.RequestedLoansCounterMenu = await _adminService.RequestedLoansCounter();
            ViewBag.RequestedDepositsCounterMenu = await _adminService.RequestedDepositsCounter();
            ViewBag.RequestedCardsCounterMenu = await _adminService.RequestedCardssCounter();
            return View(deposits);
        }

        [HttpGet("{Controller}/DepositManagment/AddDeposit")]
        public async Task<IActionResult> AddDeposit()
        {
            ViewBag.RequestedLoansCounterMenu = await _adminService.RequestedLoansCounter();
            ViewBag.RequestedDepositsCounterMenu = await _adminService.RequestedDepositsCounter();
            ViewBag.RequestedCardsCounterMenu = await _adminService.RequestedCardssCounter();
            return View();
        }

        [HttpPost("{Controller}/DepositManagment/AddDeposit")]
        public async Task<IActionResult> AddDeposit(Deposit deposit)
        {
            if (ModelState.IsValid)
            {
                await _adminService.CreateDeposit(deposit);
                return Redirect(Url.Action("DepositManagment", "Admin"));
            }
            return View(deposit);
        }



        [HttpGet("{Controller}/DepositManagment/EditDeposit/{id}")]
        public async Task<IActionResult> EditDeposit(int? id)
        {
            ViewBag.RequestedLoansCounterMenu = await _adminService.RequestedLoansCounter();
            ViewBag.RequestedDepositsCounterMenu = await _adminService.RequestedDepositsCounter();
            ViewBag.RequestedCardsCounterMenu = await _adminService.RequestedCardssCounter();

            var deposit = await _adminService.GetDepositById(id);

            return View(deposit);
        }

        [HttpPost("{Controller}/DepositManagment/EditDeposit/{id}")]
        public async Task<IActionResult> EditDeposit(Deposit deposit)
        {
            await _adminService.UpdateDeposit(deposit);
            return Redirect(Url.Action("DepositManagment", "Admin"));
        }

        [HttpGet("{Controller}/DepositManagment/DeleteDeposit/{id?}")]
        public async Task<IActionResult> DeleteDeposit(int id)
        {
            ViewBag.RequestedLoansCounterMenu = await _adminService.RequestedLoansCounter();
            ViewBag.RequestedDepositsCounterMenu = await _adminService.RequestedDepositsCounter();
            ViewBag.RequestedCardsCounterMenu = await _adminService.RequestedCardssCounter();

            var deposit = await _adminService.GetDepositById(id);
            await _adminService.DeleteDeposit(deposit);

            return Redirect(Url.Action("DepositManagment", "Admin"));
        }

        [HttpGet("{Controller}/Profile")]
        public async Task<IActionResult> Profile()
        {
            ViewBag.RequestedLoansCounterMenu = await _adminService.RequestedLoansCounter();
            ViewBag.RequestedDepositsCounterMenu = await _adminService.RequestedDepositsCounter();
            ViewBag.RequestedCardsCounterMenu = await _adminService.RequestedCardssCounter();

            //Current authorised user
            var currentAuthorisedUser = await _adminService.GetCurrentAuthorizedUser(HttpContext);

            var id = currentAuthorisedUser.Id;

            User user = await _adminService.GetUserById(id);

            if (user == null)
            {
                return NotFound();
            }
            return View(user);
        }
        [HttpGet("{Controller}/Profile/EditProfile")]
        public async Task<IActionResult> EditProfile()
        {
            ViewBag.RequestedLoansCounterMenu = await _adminService.RequestedLoansCounter();
            ViewBag.RequestedDepositsCounterMenu = await _adminService.RequestedDepositsCounter();
            ViewBag.RequestedCardsCounterMenu = await _adminService.RequestedCardssCounter();
            ViewBag.Cities = _adminService.GetEnumCities();

            var currentAuthorisedUser = await _adminService.GetCurrentAuthorizedUser(HttpContext);

            var id = currentAuthorisedUser.Id;

            User user = await _adminService.GetUserById(id);

            if (user == null)
            {
                return NotFound();
            }
            return View(user);
        }

        [HttpPost("{Controller}/Profile/EditProfile")]
        public async Task<IActionResult> EditProfile(User user)
        {
            var currentAuthorisedUser = await _adminService.GetCurrentAuthorizedUser(HttpContext);
            user.Id = currentAuthorisedUser.Id;
            await _userService.Update(user);

            return RedirectToAction("Profile");
        }

        [HttpGet("{Controller}/LoanRequests")]
        public async Task<IActionResult> LoanRequests(string orderBy, int? page)
        {
            var loanRequests = await _adminService.GetPendingLoansList(orderBy);

            var getPage = page ?? 1;
            loanRequests = loanRequests.ToPagedList(getPage, 10);
            ViewBag.RequestedLoansCounterMenu = await _adminService.RequestedLoansCounter();
            ViewBag.RequestedDepositsCounterMenu = await _adminService.RequestedDepositsCounter();
            ViewBag.RequestedCardsCounterMenu = await _adminService.RequestedCardssCounter();

            return View(loanRequests);
        }
        [HttpGet("{Controller}/LoanRequests/Details/{id}")]
        public async Task<IActionResult> RequestedLoanDetails(int id)
        {
            var requestedLoan = await _adminService.GetRequestedLoanDetailsById(id);

            ViewBag.RequestedLoansCounterMenu = await _adminService.RequestedLoansCounter();
            ViewBag.RequestedDepositsCounterMenu = await _adminService.RequestedDepositsCounter();
            ViewBag.RequestedCardsCounterMenu = await _adminService.RequestedCardssCounter();

            return View(requestedLoan);
        }

        [HttpGet("{Controller}/LoanRequests/RejectLoan/{id?}")]
        public async Task<IActionResult> RejectLoan(int id)
        {
            await _adminService.RejectLoanById(id);

            return RedirectToAction("LoanRequests");
        }

        [HttpGet("{Controller}/LoanRequests/ApproveLoan/{id}")]
        public async Task<IActionResult> ApproveLoan(int id)
        {
            await _adminService.ApproveLoanById(id);

            return RedirectToAction("LoanRequests");
        }

        [HttpGet("{Controller}/DepositRequests")]
        public async Task<IActionResult> DepositRequests(string orderBy, int? page)
        {
            var depositRequests = await _adminService.GetPendingDepositsList(orderBy);
            var getPage = page ?? 1;
            depositRequests = depositRequests.ToPagedList(getPage, 10);

            ViewBag.RequestedLoansCounterMenu = await _adminService.RequestedLoansCounter();
            ViewBag.RequestedDepositsCounterMenu = await _adminService.RequestedDepositsCounter();
            ViewBag.RequestedCardsCounterMenu = await _adminService.RequestedCardssCounter();

            return View(depositRequests);
        }

        [HttpGet("{Controller}/DepositRequest/RejectDeposit/{id?}")]
        public async Task<IActionResult> RejectDeposit(int id)
        {
            await _adminService.RejectDepositById(id);

            return RedirectToAction("DepositRequests");
        }

        [HttpGet("{Controller}/DepositRequest/ApproveDeposit/{id}")]
        public async Task<IActionResult> ApproveDeposit(int id)
        {
            await _adminService.ApproveDepositById(id);

            return RedirectToAction("DepositRequests");
        }

        [HttpGet("{Controller}/CardsManagement")]
        public async Task<IActionResult> CardsManagement(string orderBy, int? page, string cardStatus)
        {
            var pendingCards = await _adminService.GetPendingCards(orderBy, cardStatus);

            var getPage = page ?? 1;
            pendingCards = pendingCards.ToPagedList(getPage, 10);
            ViewBag.RequestedLoansCounterMenu = await _adminService.RequestedLoansCounter();
            ViewBag.RequestedDepositsCounterMenu = await _adminService.RequestedDepositsCounter();
            ViewBag.RequestedCardsCounterMenu = await _adminService.RequestedCardssCounter();

            ViewData["CardStatuses"] = _adminService.GetEnumCardStatuses();

            return View(pendingCards);
        }
        [HttpGet]
        public async Task<IActionResult> RejectedCards()
        {
            var rejectedCards = await _adminService.GetRejectedCards();
            return View(rejectedCards);
        }
        [HttpGet]
        public async Task<IActionResult> ActiveCards()
        {
            var activeCards = await _adminService.GetActiveCards();
            return View(activeCards);
        }
        [HttpGet]
        public async Task<IActionResult> BlockedCards()
        {
            var blockedCards = await _adminService.GetBlockedCards();
            return View(blockedCards);
        }

        [HttpGet("{Controller}/CardsManagement/CreateCard/{id}")]
        public async Task<IActionResult> CreateCard(Card card)
        {
            ViewBag.RequestedLoansCounterMenu = await _adminService.RequestedLoansCounter();
            ViewBag.RequestedDepositsCounterMenu = await _adminService.RequestedDepositsCounter();
            ViewBag.RequestedCardsCounterMenu = await _adminService.RequestedCardssCounter();
            return View(card);
        }
        [HttpPost("{Controller}/CardsManagement/CreateCard/{id}")]
        public async Task<IActionResult> CreateCard(int id, Card card)
        {
            var accountCard = await _adminService.ApproveCard(id, card);
            return RedirectToAction("CardsManagement");
        }
        [HttpGet("{Controller}/RejectCard/{id?}")]
        public async Task<IActionResult> RejectCard(int id)
        {
            var result = await _adminService.RejectCardById(id);
            return RedirectToAction("CardsManagement");
        }
        [HttpGet("{Controller}/BlockCard/{id?}")]
        public async Task<IActionResult> BlockCard(int id)
        {
            var result = await _adminService.BlockCardById(id);
            return Redirect(Url.Action("CardsManagement", "Admin", new { page = 1, cardStatus = CardStatus.Active.ToString() }));
        }
        [HttpGet("{Controller}/UnblockCard/{id?}")]
        public async Task<IActionResult> UnblockCard(int id)
        {
            var result = await _adminService.UnblockCardById(id);
            return Redirect(Url.Action("CardsManagement", "Admin", new { page = 1, cardStatus = CardStatus.Blocked.ToString() }));
        }

        [HttpGet("{Controller}/ActiveUsers")]
        public async Task<IActionResult> ActiveUsers(string orderBy, int? page)
        {
            var activeUsers = await _adminService.GetActiveUsersList(orderBy);

            var getPage = page ?? 1;
            activeUsers = activeUsers.ToPagedList(getPage, 10);
            ViewBag.RequestedLoansCounterMenu = await _adminService.RequestedLoansCounter();
            ViewBag.RequestedDepositsCounterMenu = await _adminService.RequestedDepositsCounter();
            ViewBag.RequestedCardsCounterMenu = await _adminService.RequestedCardssCounter();

            return View(activeUsers);
        }

        [HttpGet("{Controller}/ActiveUsers/Details/{id}")]
        public async Task<IActionResult> ActiveUsersDetails(int id)
        {
            ViewBag.RequestedLoansCounterMenu = await _adminService.RequestedLoansCounter();
            ViewBag.RequestedDepositsCounterMenu = await _adminService.RequestedDepositsCounter();
            ViewBag.RequestedCardsCounterMenu = await _adminService.RequestedCardssCounter();

            var user = await _adminService.GetUserById(id);
            var getAllCardsByUserId = await _adminService.GetAccountCardsByUserId(id);
            ViewBag.CustomUserCardsCount = getAllCardsByUserId.Count();
            ViewData["CustomUserCards"] = getAllCardsByUserId;

            var getAllAccountsByUserId = user.Accounts.ToList();
            ViewBag.CustomUserAccountsCount = getAllAccountsByUserId.Count();

            var getAllLoansForUserByUserId = await _adminService.GetAccountLoansByUserId(id);
            ViewBag.CustomUserLoansCount = getAllLoansForUserByUserId.Count();
            ViewData["CustomUserLoans"] = getAllLoansForUserByUserId;

            var getAllDepositsForUserByUserId = await _adminService.GetAccountDepositsByUserId(id);
            ViewBag.CustomUserDepositsCount = getAllDepositsForUserByUserId.Count();
            ViewData["CustomUserDeposits"] = getAllDepositsForUserByUserId;

            if (user == null)
            {
                return NotFound();
            }
            return View(user);
        }
        [HttpGet("{Controller}/BlockedUsers/Details/{id}")]
        public async Task<IActionResult> BlockedUsersDetails(int id)
        {
            ViewBag.RequestedLoansCounterMenu = await _adminService.RequestedLoansCounter();
            ViewBag.RequestedDepositsCounterMenu = await _adminService.RequestedDepositsCounter();
            ViewBag.RequestedCardsCounterMenu = await _adminService.RequestedCardssCounter();

            var user = await _adminService.GetUserById(id);

            var getAllCardsByUserId = await _adminService.GetAccountCardsByUserId(id);
            ViewBag.CustomUserCardsCount = getAllCardsByUserId.Count();
            ViewData["CustomUserCards"] = getAllCardsByUserId;

            var getAllAccountsByUserId = user.Accounts.ToList();
            ViewBag.CustomUserAccountsCount = getAllAccountsByUserId.Count();

            var getAllLoansForUserByUserId = await _adminService.GetAccountLoansByUserId(id);
            ViewBag.CustomUserLoansCount = getAllLoansForUserByUserId.Count();
            ViewData["CustomUserLoans"] = getAllLoansForUserByUserId;

            var getAllDepositsForUserByUserId = await _adminService.GetAccountDepositsByUserId(id);
            ViewBag.CustomUserDepositsCount = getAllDepositsForUserByUserId.Count();
            ViewData["CustomUserDeposits"] = getAllDepositsForUserByUserId;

            if (user == null)
            {
                return NotFound();
            }
            return View(user);
        }

        [HttpGet("{Controller}/ActiveUsers/UserDetails/{id}")]
        public async Task<IActionResult> UserDetails(int id)
        {
            ViewBag.RequestedLoansCounterMenu = await _adminService.RequestedLoansCounter();
            ViewBag.RequestedDepositsCounterMenu = await _adminService.RequestedDepositsCounter();
            ViewBag.RequestedCardsCounterMenu = await _adminService.RequestedCardssCounter();

            User user = await _adminService.GetUserById(id);

            if (user == null)
            {
                return NotFound();
            }
            return View(user);
        }

        [HttpGet("{Controller}/BlockUser/{id}")]
        public async Task<IActionResult> BlockUser(int id)
        {
            ViewBag.RequestedLoansCounterMenu = await _adminService.RequestedLoansCounter();
            ViewBag.RequestedDepositsCounterMenu = await _adminService.RequestedDepositsCounter();
            ViewBag.RequestedCardsCounterMenu = await _adminService.RequestedCardssCounter();

            var user = await _adminService.GetUserById(id);
            if (user == null)
            {
                return NotFound();
            }
            await _adminService.BlockUserById(id);
            return RedirectToAction(nameof(BlockedUsers));
        }

        [HttpGet("{Controller}/UnBlockUser/{id}")]
        public async Task<IActionResult> UnBlockUser(int id)
        {
            User user = await _adminService.GetUserById(id);
            if (user == null)
            {
                return NotFound();
            }
            await _adminService.UnBlockUserById(id);
            return RedirectToAction(nameof(ActiveUsers));
        }

        [HttpGet("{Controller}/TransactionsHistory")]
        public async Task<IActionResult> TransactionsHistory(string orderBy, int? page)
        {
            ViewBag.RequestedLoansCounterMenu = await _adminService.RequestedLoansCounter();
            ViewBag.RequestedDepositsCounterMenu = await _adminService.RequestedDepositsCounter();
            ViewBag.RequestedCardsCounterMenu = await _adminService.RequestedCardssCounter();

            var transactionHistory = await _adminService.GetTransactionsHistoriesList(orderBy);

            var getPage = page ?? 1;
            transactionHistory = transactionHistory.ToPagedList(getPage, 10);

            return View(transactionHistory);
        }

        [HttpGet("{Controller}/BlockedUsers")]
        public async Task<IActionResult> BlockedUsers(string orderBy, int? page)
        {
            ViewBag.RequestedLoansCounterMenu = await _adminService.RequestedLoansCounter();
            ViewBag.RequestedDepositsCounterMenu = await _adminService.RequestedDepositsCounter();
            ViewBag.RequestedCardsCounterMenu = await _adminService.RequestedCardssCounter();

            var blockedUsers = await _adminService.GetBlockedUsersList(orderBy);

            var getPage = page ?? 1;
            blockedUsers = blockedUsers.ToPagedList(getPage, 10);

            return View(blockedUsers);
        }

        [HttpGet("{Controller}/TransactionsHistory/Details/{id}")]
        public async Task<IActionResult> TransactionHistoryDetails(int? id)
        {
            ViewBag.RequestedLoansCounterMenu = await _adminService.RequestedLoansCounter();
            ViewBag.RequestedDepositsCounterMenu = await _adminService.RequestedDepositsCounter();
            ViewBag.RequestedCardsCounterMenu = await _adminService.RequestedCardssCounter();
            var getHistory = await _adminService.GetTransactionsHistoryById(id);

            return View(getHistory);
        }


        [HttpGet]
        public async Task<ActionResult> Reports()
        {
            return View();
        }

        [HttpGet]
        public async Task<FileResult> LoginReport()
        {
            var usersInfos = await _userInfoService.Get();
            StringBuilder info = new StringBuilder();
            info.AppendLine($"User Id,Email,User Logs in,Last Login,AVG Login perday,Login permonth");

            foreach (var item in usersInfos)
            {
                var userId = item.UserId;
                var loginReports = await _loginReportService.GetReport(userId);
                var user = await _userManager.FindByIdAsync(userId.ToString());
                var email = user.Email;
                if (loginReports != null)
                {
                    if (user != null && !(await _userManager.IsInRoleAsync(user, "Admin")))
                    {
                        info.AppendLine($"{userId} , {email} , {loginReports.FirstLogin}, {loginReports.LastLogin}" +
                        $"{loginReports.CounterLogsIn} , {loginReports.PerMonth}, {loginReports.AvgPerday}");
                    }

                }

            }
            var bytearray = Encoding.ASCII.GetBytes(info.ToString());
            return File(new MemoryStream(bytearray), "text/csv", "LoginReport.csv");
        }
        [HttpGet]
        public async Task<FileResult> TransfersReport()
        {
            var usersInfos = await _userInfoService.Get();
            StringBuilder info = new StringBuilder();
            info.AppendLine($"User Id,Email,Money the user spent from acc per month,Money the user received on acc per month");

            foreach (var item in usersInfos)
            {
                var userId = item.UserId;
                var transfersReports = await _transfersReportService.GetReport(userId);
                var user = await _userManager.FindByIdAsync(userId.ToString());
                var email = user.Email;

                info.AppendLine($"{userId} , {email} ,");

            }
            var bytearray = Encoding.ASCII.GetBytes(info.ToString());
            return File(new MemoryStream(bytearray), "text/csv", "TransfersReport.csv");
        }
        [HttpGet]
        public async Task<FileResult> CountersReport()
        {
            var usersInfos = await _userInfoService.Get();
            StringBuilder info = new StringBuilder();
            info.AppendLine($"User Id,Email,user visited Deposits page,User visited Loans page,user visited Cards page,User visited Account page");

            foreach (var item in usersInfos)
            {
                var userId = item.UserId;
                var counterReports = await _countersReportService.GetReport(userId);
                var user = await _userManager.FindByIdAsync(userId.ToString());
                var email = user.Email;

                info.AppendLine($"{userId} , {email} ,");

            }
            var bytearray = Encoding.ASCII.GetBytes(info.ToString());
            return File(new MemoryStream(bytearray), "text/csv", "CountersReport.csv");
        }
        [HttpGet]
        public async Task<FileResult> FiltersReport()
        {
            var usersInfos = await _userInfoService.Get();
            StringBuilder info = new StringBuilder();
            info.AppendLine($"User Id,Email,Deposit filter parameters,Loan filter parameters");

            foreach (var item in usersInfos)
            {
                var userId = item.UserId;
                var filtersReport = await _filtersReportService.GetReport(userId);
                var user = await _userManager.FindByIdAsync(userId.ToString());
                var email = user.Email;

                info.AppendLine($"{userId} , {email} ,");

            }
            var bytearray = Encoding.ASCII.GetBytes(info.ToString());
            return File(new MemoryStream(bytearray), "text/csv", "FiltersReport.csv");
        }
    }
}