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
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using AcademyBank.DAL.filter;
using AcademyBank.Models.Enums;
using AcademyBank.Web.Controllers;

namespace AcademyBank.Controllers
{
    public class AccountsController : BaseController
    {
        private readonly IAccountService _account;
        private readonly UserManager<User> _userManager;
        private readonly ILoanService _loanService;
        private readonly IDepositService _depositService;
        private readonly IAccountDepositService _accountDepositService;
        private readonly IAccountLoanService _accountLoanService;
        public AccountsController(
            IAccountService account,
            UserManager<User> userManager,
            ILoanService loanService,
            IDepositService depositService,
            IAccountDepositService accountDepositService,
            IAccountLoanService accountLoanService
            )
        {
            _account = account;
            _userManager = userManager;
            _loanService = loanService;
            _depositService = depositService;
            _accountDepositService = accountDepositService;
            _accountLoanService = accountLoanService;
        }

        // GET: Accounts
        public async Task<IActionResult> Index()
        {
            var user = await _userManager.GetUserAsync(HttpContext.User);
            var userId = user.Id;

            return View(await _account.Get(userId));
        }

        // GET: Accounts/Details/5
        public async Task<IActionResult> Details(int id)
        {
            var account = await _account.GetById(id);

            if (account == null)
            {
                return NotFound();
            }

            var loans = await _loanService.Get();
            ViewBag.loans = loans;
            var deposits = await _depositService.Get();
            ViewBag.deposits = deposits;

            return View(account);
        }

        // GET: Accounts/Create
        public IActionResult Create()

        {
            return View();
        }

        // POST: Accounts/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(string accountName)
        {
            var user = await _userManager.GetUserAsync(HttpContext.User);
            if (User.Identity.IsAuthenticated)
            {
                if (accountName != null)
                {
                    if (await _account.GetByUserId(user.Id) == null)
                    {
                        Account account = new Account()
                        {
                            AccountName = accountName,
                            UserId = user.Id
                        };
                        await _account.Create(account);
                        Alert("The Account Has Been Successfully Created", NotificationType.success);
                        return RedirectToAction("Index", "Accounts");
                    }
                    else
                    {
                        Alert("An user can not have multiple accounts", NotificationType.info);
                        return RedirectToAction("Index", "Accounts");
                    }
                }
                else
                {
                    Alert("Account name field is required", NotificationType.info);
                    ModelState.AddModelError("Invalid account name", "Account name field is required");
                    return View();
                }
            }
            else
            {
                return RedirectToAction("Login", "User");
            }
        }

        // GET: Accounts/Delete/5
        public async Task<IActionResult> Delete(int id)
        {
            var account = await _account.GetById(id);

            if (account == null)
            {
                return NotFound();
            }

            return View(account);
        }

        // POST: Accounts/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var account = await _account.GetById(id);
            await _account.Delete(account);
            Alert("The Account Has Been Successfully Deleted", NotificationType.success);
            return RedirectToAction(nameof(Index));
        }    
    }
}
