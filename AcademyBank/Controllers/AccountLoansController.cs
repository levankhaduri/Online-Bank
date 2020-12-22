using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using AcademyBank.DAL;
using AcademyBank.Models;
using AcademyBank.BLL.Services.Interfaces;
using AcademyBank.Models.Enums;
using System.ComponentModel;
using Microsoft.Extensions.Logging;
using AcademyBank.DAL.filter;
using AutoMapper;
using Microsoft.AspNetCore.Identity;
using AcademyBank.BLL.Helpers.Implementations;
using AcademyBank.Web.Controllers;
using System.Text.RegularExpressions;
using AcademyBank.DAL.Repositories.Interfaces;

namespace AcademyBank.Controllers
{
    public class AccountLoansController : BaseController
    {
        private readonly IAccountLoanService _accountLoanService;
        private readonly IAccountService _accountService;
        private readonly ILoanService _loanService;
        private readonly IMapper _mapper;
        private readonly UserManager<User> _userManager;
        private readonly IUserService _userService;

        public AccountLoansController(
            IAccountLoanService accountLoanService,
            IAccountService accountService,
            ILoanService loanService,
            IMapper mapper,
            UserManager<User> userManager,
            IUserService userService)
        {
            _accountLoanService = accountLoanService;
            _accountService = accountService;
            _loanService = loanService;
            _mapper = mapper;
            _userManager = userManager;
            _userService = userService;
        }

        // GET: AccountLoans
        public async Task<IActionResult> Index(string? searchPurpose, decimal? searchAmount, string? searchTerm)
        {
            if (User.Identity.IsAuthenticated)
            {
                var loans = await _loanService.Get();
                ViewBag.Purpose = typeof(LoanPurposes).GetValues();
                ViewBag.Term = typeof(Term).GetValues();
                if (searchAmount != null)
                {
                    loans = loans.Where(s => s.Purpose == searchPurpose
                                           && s.MaxAmount >= searchAmount
                                           && s.MinAmount <= searchAmount
                                           && s.Term == searchTerm);

                    if (loans.Count() > 1)
                    {
                        Alert($"You Have found {loans.Count()} Loans", NotificationType.success);
                    }
                    else if (loans.Count() == 1)
                    {
                        Alert("You Have found 1 Loan", NotificationType.success);
                    }
                    else
                    {
                        Alert("Sorry, There is no such a kind of Loan", NotificationType.info);
                    }
                }
                var Loan = _mapper.Map<IEnumerable<Loan>>(loans.ToList());
                return View(Loan);
            }
            else
            {
                return RedirectToAction("Login", "User");
            }
        }

        // GET: AccountLoans/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (User.Identity.IsAuthenticated)
            {
                if (id == null)
                {
                    return NotFound();
                }

                var accLoan = await _accountLoanService.GetById(id.GetValueOrDefault());
                if (accLoan == null)
                {
                    return NotFound();
                }
                return View(accLoan);
            }
            else
            {
                return RedirectToAction("Login", "User");
            }
        }

        [HttpGet("{controller}/Create/{loanId}")]
        public async Task<IActionResult> Create(int? loanId)
        {
            if (User.Identity.IsAuthenticated)
            {
                var getCurrentUserId = _userManager.GetUserId(HttpContext.User);
                var currentUser = await _userService.GetById(int.Parse(getCurrentUserId));

                TempData["LoanId"] = loanId;
                var loan = await _loanService.GetById(loanId.GetValueOrDefault());
                ViewBag.Term = typeof(Term).GetValues();
                ViewBag.Currencies = typeof(Currency).GetAllEnumNames();
                ViewBag.Status = Status.Pending.ToString();
                ViewData["AccrueAccountId"] = new SelectList(currentUser.Accounts, "Id", "AccountNumber");
                ViewData["AccountId"] = new SelectList(currentUser.Accounts, "Id", "AccountNumber");

                var model = new AccountLoanViewModel
                {
                    Loan = loan
                };
                return View(model);
            }
            else
            {
                return RedirectToAction("Login", "User");
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(AccountLoanViewModel accountLoanViewModel)
        {
            var loanId = TempData["loanId"] as int?;

            if (ModelState.IsValid)
            {
                var accountLoan = _mapper.Map<AccountLoan>(accountLoanViewModel);
                accountLoan.LoanId = loanId.GetValueOrDefault();

                await _accountLoanService.Create(accountLoan);
                Alert("The Loan Has Successfully Added To Your Account", NotificationType.success);
                return RedirectToAction(nameof(Index));
            }
            Alert("Something went wrong", NotificationType.error);
            return View(accountLoanViewModel);
        }

        public async Task<IActionResult> AllLoans()
        {
            var user = await _userManager.GetUserAsync(HttpContext.User);
            var userId = user.Id;

            var accountLoans = await _accountLoanService.GetAccountLoansByUserId(userId);

            var loans = await _loanService.Get();
            ViewBag.loans = loans;

            return View(accountLoans);
        }

        [HttpPost]
        public async Task<IActionResult> PayingLoans(int id)
        {
            var accountLoan = await _accountLoanService.GetById(id);
            var termstr = accountLoan.Term;
            var resultString = Regex.Match(termstr, @"\d+").Value;
            var term = Convert.ToInt32(resultString);
            var startdate = accountLoan.AcceptanceTime;
            var countdays = Convert.ToInt32((startdate.AddDays(term) - DateTime.Now).TotalDays);

            accountLoan.Loan = await _loanService.GetById(accountLoan.LoanId);

            var percent = accountLoan.Loan.Percentage;
            var loanPercent = (accountLoan.Sum * percent) / 100;
            var moneyToBringMonthly = (accountLoan.Sum + loanPercent) / term;

            var moneyNeeded = moneyToBringMonthly * countdays;

            var accounts = await _accountService.Get();
            var bankAccount = new Account();
            foreach (var acc in accounts)
            {
                if (acc.AccountName == "BankBalance")
                {
                    bankAccount = acc;
                }
            }

            if (accountLoan.Account.Balance >= moneyNeeded && accountLoan.Account != bankAccount)
            {
                accountLoan.Status = Status.Finished.ToString();
                accountLoan.Account.Balance -= moneyNeeded;
                accountLoan.Sum = 0;
                bankAccount.Balance += moneyNeeded;

                await _accountService.Update(accountLoan.Account);
                await _accountService.Update(bankAccount);

                Alert("Loan was successfuly paied", NotificationType.success);
            }
            else
            {
                Alert("Can not pay the Loan", NotificationType.error);
            }
            return RedirectToAction("AllLoans");
        }
    }
}
