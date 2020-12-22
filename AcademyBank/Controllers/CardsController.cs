using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AcademyBank.BLL.Services.Interfaces;
using AcademyBank.DAL.filter;
using AcademyBank.Models;
using AcademyBank.Models.Enums;
using AcademyBank.Web.Controllers;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Logging;

namespace AcademyBank.Controllers
{
    public class CardsController : BaseController
    {
        private readonly UserManager<User> _userManager;
        private readonly ICardService _cardService;
        private readonly IAccountService _accountService;
        private readonly IEncryptionService _encryptionService;
        public CardsController(
            UserManager<User> userManager,
            ICardService cardService,
            IAccountService accountService,
            IEncryptionService encryptionService)
        {
            _userManager = userManager;
            _cardService = cardService;
            _accountService = accountService;
            _encryptionService = encryptionService;
        }

        [HttpGet("{Controller}/RequestCard")]
        public async Task<IActionResult> RequestCard()
        {
            if (User.Identity.IsAuthenticated)
            {
                try
                {
                    var user = await _userManager.GetUserAsync(HttpContext.User);
                    ViewBag.CardTypes = new SelectList(Enum.GetValues(typeof(CardTypes)));
                    ViewBag.Accounts = user.Accounts;
                    return View("AccountsAndCards");
                }
                catch (Exception)
                {
                    return RedirectToAction("Create", "Accounts");
                    throw;
                }
            }
            else
            {
                return RedirectToAction("Login", "User");
            }
        }

        //Needs Authorisation 
        [HttpPost]
        public async Task<IActionResult> RequestCard(string cardType)
        {
            if (User.Identity.IsAuthenticated)
            {
                var user = await _userManager.GetUserAsync(HttpContext.User);

                if (cardType != null)
                {
                    Card card = new Card()
                    {
                        CardType = cardType,
                        AccountId = _accountService.GetByUserId(user.Id).Result.Id,
                        CreatedAt = DateTime.Now,
                        Status = CardStatus.Pending.ToString()
                    };

                    await _cardService.Create(card);
                    Alert("The Card Is Under Review", NotificationType.success);
                    return RedirectToAction("AccountsAndCards", "Cards");
                }
                else
                {
                    Alert("Something Went Wrong!", NotificationType.error);
                    return RedirectToAction("AccountsAndCards", "Cards");
                }
            }
            else
            {
                return RedirectToAction("Login", "User");
            }
        }

        [HttpGet]
        public async Task<IActionResult> Details(int id)
        {
            if (User.Identity.IsAuthenticated)
            {
                var cardInfo = await _cardService.GetById(id);
                return View(cardInfo);
            }
            else
            {
                return RedirectToAction("Login", "User");
            }
        }

        [HttpGet]
        public async Task<IActionResult> AccountsAndCards()
        {
            if (User.Identity.IsAuthenticated)
            {
                try
                {
                    var user = await _userManager.GetUserAsync(HttpContext.User);

                    Account realAccount = await _accountService.GetByUserId(user.Id);

                    if (realAccount==null)
                    {
                        ViewBag.Account = realAccount;

                        var fakeCards = new List<Card>();

                        ViewBag.History = new List<TransactionsHistory>();

                        return View(fakeCards);
                    }

                    var cards = realAccount.Cards;

                    var transactions = realAccount.Transactions.ToList();

                    ViewBag.Account = realAccount;

                    ViewBag.History = (transactions == null) ? null : new List<TransactionsHistory>();

                    return View(cards);
                }
                catch (Exception)
                {
                    var cards = new List<Card>();

                    ViewBag.History = new List<TransactionsHistory>();

                    return View(cards);
                }
            }
            else
            {
                return RedirectToAction("Login", "User");
            }
        }

        [HttpGet]
        public async Task<IActionResult> TransferAndPayment()
        {
            try
            {
                if (User.Identity.IsAuthenticated)
                {
                    var user = await _userManager.GetUserAsync(HttpContext.User);

                    var account = await _accountService.GetByUserId(user.Id);

                    if (account == null)
                    {
                        return View();
                    }

                    var cards = account.Cards;

                    return View(cards);
                }
                else
                {
                    return RedirectToAction("Login", "User");
                }
            }
            catch (Exception ex)
            {
                var cards = new List<Card>();
                return View(cards);
            }
        }

        [HttpGet]
        public async Task<IActionResult> Transfer()
        {
            var user = await _userManager.GetUserAsync(HttpContext.User);

            var realAccount = await _accountService.GetByUserId(user.Id);

            var cards = realAccount.Cards;

            return RedirectToAction("TransferAndPayment", cards);
        }

        [HttpPost]
        public async Task<IActionResult> Transfer(string cardNumberFrom, string cardNumberTo, decimal amount)
        {
            if (User.Identity.IsAuthenticated)
            {
                var user = await _userManager.GetUserAsync(HttpContext.User);

                var realAccount = await _accountService.GetByUserId(user.Id);

                var cards = realAccount.Cards;

                cardNumberFrom = cardNumberFrom.Trim();

                cardNumberTo = cardNumberTo.Trim();

                var accountFrom = await _accountService.GetById((_cardService.GetByNumber(cardNumberFrom).Result.AccountId));

                var accountTo = await _accountService.GetById((_cardService.GetByNumber(cardNumberTo).Result.AccountId));

                if (accountFrom.Balance < amount)
                {
                    Alert("Insufficent Balance!!!", NotificationType.info);
                    ViewBag.Message = "Insufficent balance";

                    return RedirectToAction("AccountsAndCards", cards);
                }
                else
                {
                    await _cardService.Transfer(accountFrom, accountTo, amount);

                    Alert("Money was sent Successfully", NotificationType.success);
                    ViewBag.Message = "Money was sent";

                    return RedirectToAction("AccountsAndCards", cards);
                }
            }
            else
            {
                return RedirectToAction("Login", "User");
            }
        }

        [HttpGet]
        public async Task<IActionResult> UtilityTransfer()
        {
            var user = await _userManager.GetUserAsync(HttpContext.User);

            var realAccount = await _accountService.GetByUserId(user.Id);

            var cards = realAccount.Cards;

            return RedirectToAction("TransferAndPayment", cards);
        }

        [HttpPost]
        public async Task<IActionResult> UtilityTransfer(string cardNumberFrom, decimal amount)
        {
            if (User.Identity.IsAuthenticated)
            {
                var user = await _userManager.GetUserAsync(HttpContext.User);

                var realAccount = await _accountService.GetByUserId(user.Id);

                var cards = realAccount.Cards;

                cardNumberFrom = cardNumberFrom.Trim();

                var accountFrom = await _accountService.GetById((_cardService.GetByNumber(cardNumberFrom).Result.AccountId));

                if (accountFrom.Balance < amount)
                {
                    Alert("Insufficent Balance!!!", NotificationType.info);
                    ViewBag.Message = "Insufficent balance";
                    
                    return RedirectToAction("AccountsAndCards", cards);
                }
                else
                {
                    await _cardService.UtilityTransfer(accountFrom, amount);

                    Alert("Money was sent Successfully", NotificationType.success);
                    ViewBag.Message = "Money was sent";

                    return RedirectToAction("AccountsAndCards", cards);
                }
            }
            else
            {
                return RedirectToAction("Login", "User");
            }
        }
    }
}
