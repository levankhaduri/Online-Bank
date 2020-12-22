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
using AcademyBank.BLL.Services.Implementations;
using AcademyBank.Models.Enums;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.Extensions.Logging;
using AcademyBank.DAL.filter;
using AutoMapper;
using Microsoft.AspNetCore.Identity;
using AcademyBank.Web.Filters;
using AcademyBank.BLL.Helpers.Implementations;
using AcademyBank.Web.Controllers;

namespace AcademyBank.Controllers
{
    public class DepositsController : BaseController
    {
        private readonly BankDbContext _context;
        private readonly IDepositService _depositService;
        private readonly IAccountService _accountService;
        private readonly IAccountDepositService _accountDepositService;
        private readonly IMapper _mapper;
        private readonly UserManager<User> _userManager;
        private readonly IUserService _userService;

        public DepositsController(BankDbContext context,
            IDepositService depositService, IAccountService accountService
            , IAccountDepositService accountDepositService,
            IMapper mapper, UserManager<User> userManager, IUserService userService)
        {
            _context = context;
            _depositService = depositService;
            _accountService = accountService;
            _accountDepositService = accountDepositService;
            _mapper = mapper;
            _userManager = userManager;
            _userService = userService;
        }

        // GET: Deposits
        public async Task<IActionResult> Index(string? searchCurrency, decimal? searchAmount)
        {
            var deposits = await _depositService.Get();
            ViewBag.CurrencyEnums = typeof(Currency);
            deposits = await _depositService.GetFilteredDeposits(searchCurrency, searchAmount);

            if (searchAmount != null)
            {
                if (deposits.Count() > 1)
                {
                    Alert($"You Have found {deposits.Count()} Deposits", NotificationType.success);
                }
                else if (deposits.Count() == 1)
                {
                    Alert("You Have found 1 Deposit", NotificationType.success);
                }
                else
                {
                    Alert("Sorry, There is no such a kind of Deposit", NotificationType.info);
                }
            }

            var Deposit = _mapper.Map<IEnumerable<Deposit>>(deposits.ToList());
            return View(Deposit);
        }
        // GET: Deposits/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var deposit = await _depositService.GetById(id.GetValueOrDefault());
            if (deposit == null)
            {
                return NotFound();
            }
            return View(deposit);
        }

        // GET: Deposits/Create
        [HttpGet("{controller}/Create/{id}")]
        public async Task<IActionResult> Create(int? id)
        {
            var getCurrentUserId = _userManager.GetUserId(HttpContext.User);
            var currentUser = await _userService.GetById(int.Parse(getCurrentUserId));
            TempData["DepositId"] = id;
            var result = await _depositService.GetById(id.GetValueOrDefault());
            ViewData["AccountId"] = new SelectList(currentUser.Accounts, "Id", "AccountName");
            ViewBag.SelectedDeposit = result;
            ViewBag.Currency = typeof(Currency).GetAllEnumNames();
            ViewBag.Term = typeof(Term).GetValues();
            ViewBag.Status = Status.Pending.ToString();
            var accountdeposit = new AccountDepositViewModel
            {
                Deposit = result
            };
            return View(accountdeposit);
        }

        // POST: Deposits/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(AccountDepositViewModel accdeposit)
        {
            var depositId = TempData["DepositId"] as int?;
            if (ModelState.IsValid)
            {
                var accountDeposit = _mapper.Map<AccountDeposit>(accdeposit);
                accountDeposit.DepositId = depositId.GetValueOrDefault();

                await _accountDepositService.Create(accountDeposit);
                Alert("The Deposit Has Successfully Added To Your Account", NotificationType.success);
                return Redirect(Url.Action("Index", "Deposits"));
            }
            return View(accdeposit);
        }

        public async Task<IActionResult> AllDeposits()
        {
            var user = await _userManager.GetUserAsync(HttpContext.User);
            var userId = user.Id;

            var accountDeposits = await _accountDepositService.GetAccountDepositsByUserId(userId);
            var deposits = await _depositService.Get();
            ViewBag.deposits = deposits;
            return View(accountDeposits);
        }
    }
}
