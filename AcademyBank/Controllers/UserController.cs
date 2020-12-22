using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using Microsoft.AspNetCore.Identity;
using SignInResult = Microsoft.AspNetCore.Identity.SignInResult;
using AcademyBank.Models;
using System.Text.RegularExpressions;
using AcademyBank.DAL.filter;
using System.Net;
using AcademyBank.BLL.Services.Interfaces;
using System.Dynamic;
using AcademyBank.BLL.Services;
using System.Net.Mail;
using WebMatrix.WebData;
using System.Web;
using AcademyBank.BLL.Helpers;
using Microsoft.Extensions.Logging;
using AcademyBank.Web.Filters;
using AcademyBank.Models.Enums;
using AcademyBank.BLL.Helpers.Implementations;
using Microsoft.AspNetCore.Http;
using System.IO;
using AcademyBank.Web.Controllers;

namespace AcademyBank.Controllers
{
    public class UserController : BaseController
    {
        private string PhoneNumberPattern = @"^((\+995)[5].{8,8})|([5].{8,8})$";
        private string PasswordPattern = @"^(?=.*\d)(?=.*[A-Z]*[a-z])(?=.*\W).{8,}$";
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;
        private readonly IUserInfoService _userInfoService;
        private readonly ILoginReportService _loginReportService;
        private readonly IUserService _userService;
        public UserController(UserManager<User> userManager,
            SignInManager<User> signInManager, IUserInfoService userInfoService,
            IUserService userService,
            ILoginReportService loginReportService
            )
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _userInfoService = userInfoService;
            _userService = userService;
            _loginReportService = loginReportService;
        }
        [HttpGet]
        public IActionResult Register()
        {
            if (User.Identity.IsAuthenticated)
            {
                return RedirectToAction("AccountsAndCards", "Cards");
            }
            else
            {
                return View();
            }
        }
        [SensitiveInfoLogFilter]
        [HttpPost]
        public async Task<IActionResult> Register(string phoneNumber, string email, string password)
        {
            phoneNumber = phoneNumber.Trim();

            var user = new User { UserName = phoneNumber, Email = email };

            var findEmail = await _userManager.FindByEmailAsync(email.ToUpper());
            var findPhone = await _userManager.FindByNameAsync(phoneNumber);

            bool emailExists = false;
            bool phoneExists = false;

            try
            {
                emailExists = findEmail.Email.Any();
            }
            catch { }
            finally
            {
                if (findPhone != null)
                {
                    phoneExists = findPhone.UserName.Any();
                }
            }

            if (emailExists)
            {
                ViewBag.EmailExistError = "User with this email already exists";
                return View();
            }
            else if (phoneExists)
            {
                ViewBag.PhoneExistError = "User with this phone Number already exists";
                return View();
            }
            else if (!Regex.IsMatch(phoneNumber, PhoneNumberPattern))
            {
                ViewBag.PhoneNumberError = "Please provide valid phone number: (+995 5)12 12 12 or (5)55 55 55 55";
                return View();
            }
            else if (!Regex.IsMatch(password, PasswordPattern))
            {
                ViewBag.PasswordError = "Password should contain at least 8 characters, one number, should contain both, upper, lower case letters and unique character";
                return View();
            }

            IdentityResult result = await _userManager.CreateAsync(user, password);

            if (result.Succeeded)
            {
                var createdUser = await _userManager.FindByNameAsync(phoneNumber);

                await _userManager.AddClaimAsync(createdUser, new Claim(ClaimTypes.Name, phoneNumber));
            }
            ModelState.AddModelError("Register Error", string.Join(",", result.Errors));

            return result.Succeeded
                ? RedirectToAction("RegisterContinue", "User", new { id = user.Id })
                : RedirectToAction("Register", "User");
        }
        [HttpGet]
        public IActionResult RegisterContinue(int? id)
        {
            if (User.Identity.IsAuthenticated)
            {
                return RedirectToAction("AccountAndCards", "Cards");
            }
            else
            {
                string stringId = "";

                if (id == null)
                {
                    return View();
                }
                else
                {
                    stringId = Convert.ToString(id);
                }

                User user = _userManager.FindByIdAsync(stringId).Result;
                ViewBag.Cities = typeof(Cities).GetAllEnumNames();
                ViewBag.Questions = typeof(SecurityQuestions).GetValues();
                ViewBag.Gender = typeof(Sex).GetAllEnumNames();
                return View(user);
            }
        }
        [SensitiveInfoLogFilter]
        [HttpPost]
        public async Task<IActionResult> RegisterContinue(string id, string firstName, string lastName, string passportId,
                                                          string gender, string city, string address,
                                                          string securityQuestion = "", string answer = "")
        {
            User user = await _userManager.FindByIdAsync(id);

            UserInfo userInfo = new UserInfo();

            userInfo.User = user;
            userInfo.FirstName = firstName;
            userInfo.LastName = lastName;
            userInfo.PassportId = passportId;
            userInfo.Sex = gender;
            userInfo.City = city;
            userInfo.Address = address;

            userInfo.ImageName = firstName[0].ToString().ToUpper() + ".png";
            var result = await _userInfoService.Create(userInfo);

            await _signInManager.SignInAsync(user, true);

            if(result != null)
            {
                Alert("You Have Been Successfully Registered", NotificationType.success);
                return RedirectToAction("AccountsAndCards", "Cards");
            }

            Alert("Something went wrong!!", NotificationType.error);
            return RedirectToAction("Register", "User");
        }
        [SensitiveInfoLogFilter]
        public ActionResult ForgotPassword()
        {
            if (User.Identity.IsAuthenticated)
            {
                return RedirectToAction("ProfileSettings", "User");
            }
            else
            {
                return View();
            }
        }
        [SensitiveInfoLogFilter]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> ForgotPassword(string email, string newPass)
        {
            var emailSender = new EmailSender();
            var user = await _userManager.FindByEmailAsync(email.ToUpper());

            bool emailExists = false;
            try
            {
                emailExists = user.Email.Any();
            }
            catch { }

            if (!emailExists)
            {
                ViewBag.EmailExistsError = "User with this email does not exist";
                return View();
            }
            else if (!Regex.IsMatch(newPass, PasswordPattern))
            {
                ViewBag.PasswordIsWeak = "Password should contain at least 8 characters, one number and should contain both, upper and lower case";
                return View();
            }
            else
            {
                var token = _userManager.GeneratePasswordResetTokenAsync(user);

                var body = System.IO.File.ReadAllText("../AcademyBank/Views/User/Email.html");

                var resetLink = Url.Action("ResetPassword", "User", new { userEmail = email, passToken = token.Result, updatedPass = newPass }, "http");

                body = body.Replace("url", resetLink);

                var emailid = user.Email;
                //send mail
                string subject = "Academy Bank Password Change Request";
                try
                {
                    emailSender.SendEmail(emailid, subject, body);

                    ViewBag.EmailSent = "Mail Sent!";
                    Alert("The Email Has Been Successfully Sent", NotificationType.success);
                }
                catch (Exception ex)
                {
                    ViewBag.SomethingWentWrong = "Error occured while sending email." + ex.Message;
                }
            }

            return View();
        }
        [SensitiveInfoLogFilter]
        [HttpGet]
        public async Task<ActionResult> ResetPassword(string userEmail, string passToken, string updatedPass)
        {
            var user = _userManager.FindByEmailAsync(userEmail);

            await _userManager.ResetPasswordAsync(user.Result, passToken, updatedPass);

            return View("Login");
        }
        [SensitiveInfoLogFilter]
        [HttpGet]
        public IActionResult Login()
        {
            if (User.Identity.IsAuthenticated)
            {
                return RedirectToAction("AccountsAndCards", "Cards");
            }
            else
            {
                return View();
            }
        }
        [SensitiveInfoLogFilter]
        [HttpPost]
        public async Task<IActionResult> Login(string input, string password)
        {
            Regex regex = new Regex(PhoneNumberPattern);

            input = input.Replace(" ", "");

            var result = SignInResult.NotAllowed;

            var user = regex.IsMatch(input)
                 ? await _userManager.FindByNameAsync(input)
                 : await _userManager.FindByEmailAsync(input);

            if (user != null)
            {
                result = await _signInManager.PasswordSignInAsync(user, password, false, false);
            }

            if (result.IsNotAllowed) ModelState.AddModelError("Login Error", "Not allowed");

            if (result.Succeeded)
            {
                if (_userManager.IsInRoleAsync(user, Constants.AdminRole).Result)
                {
                    return RedirectToAction("DepositManagment", Constants.AdminRole);
                }
                await _loginReportService.CreateOrUpdate(user);
                return RedirectToAction("AccountsAndCards", "Cards");
            }
            else
            {
                ViewBag.ErrorMessage = "Invalid email or password";
                return View();
            }
        }

        public async Task<IActionResult> Logout()
        {
            if (User.Identity.IsAuthenticated)
            {
                await _signInManager.SignOutAsync();
                return RedirectToAction("Login", "User");
            }
            else
            {
                return RedirectToAction("Login", "User");
            }
        }

        [HttpGet]
        public async Task<IActionResult> ProfileSettings()
        {
            if (User.Identity.IsAuthenticated)
            {
                var user = await _userManager.FindByNameAsync(User.Identity.Name);

                var userInfo = _userInfoService.GetById(user.Id);

                ViewBag.Email = user.Email;

                return View(userInfo.Result);
            }
            else
            {
                return RedirectToAction("Login", "User");
            }
        }
        [SensitiveInfoLogFilter]
        [HttpPost]
        public async Task<IActionResult> ProfileSettings(string oldPassword, string newPassword)
        {
            var user = await _userManager.FindByNameAsync(User.Identity.Name);

            var userInfo = await _userInfoService.GetById(user.Id);

            ViewBag.Email = user.Email;

            if ((oldPassword != null && newPassword == null) || (oldPassword == null && newPassword != null))
            {
                ViewBag.ErrorMessage = "One of the fields is empty";
                return View(userInfo);
            }
            else if (oldPassword == null && newPassword == null)
            {
                return View(userInfo);
            }
            else if (oldPassword == newPassword)
            {
                ViewBag.ErrorMessage = "You entered same password";
                return View(userInfo);
            }
            else
            {
                var result = await _userManager.ChangePasswordAsync(user, oldPassword, newPassword);

                if (result.Succeeded)
                {
                    await _signInManager.SignOutAsync();
                    Alert("Your Profile Has Been Updated Successfully", NotificationType.success);
                    return RedirectToAction("Login", "User");
                }
                else
                {
                    Alert("Something went wrong!!", NotificationType.error);
                    ViewBag.ErrorMessage = "Something went wrong";
                    return View(userInfo);
                }
            }
        }

        [HttpPost]
        public async Task<IActionResult> UploadPhoto(IFormFile image)
        {
            if (image?.Length > 0)
            {
                await _userService.UploadPhoto(image, User.Identity.Name);
                Alert("The Photo Uploaded Successfully", NotificationType.success);
            }
            return RedirectToAction(nameof(ProfileSettings));
        }
    }
}
