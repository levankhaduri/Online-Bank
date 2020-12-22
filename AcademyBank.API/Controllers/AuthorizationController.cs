using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using AcademyBank.BLL.Services.Interfaces;
using AcademyBank.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

using SignInResult = Microsoft.AspNetCore.Identity.SignInResult;

namespace AcademyBank.API.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class AuthorizationController : ControllerBase
	{
		private readonly SignInManager<User> _signInManager;
		private readonly IUserService _userService;
		private string PasswordPattern = @"^(?=.*\d)(?=.*[A-Z])(?=.*\W).{8,}$";
		private string PhoneNumberPattern = @"^((\+995)[5].{8,8})|([5].{8,8})$";
		private readonly UserManager<User> _userManager;

		public AuthorizationController(
			SignInManager<User> signInManager,
			IUserService userService,
			UserManager<User> userManager)
		{
			_signInManager = signInManager;
			_userService = userService;
			_userManager = userManager;
		}

		[HttpGet]
		public async Task<User> SignIn(string mailOrPhone, string password)
		{
			Regex regex = new Regex(PhoneNumberPattern);

			mailOrPhone = mailOrPhone.Replace(" ", "");

			var user = regex.IsMatch(mailOrPhone)
				? await _userService.GetByName(mailOrPhone)
				: await _userService.GetByEmail(mailOrPhone);

			if (user != null)
			{
				var result = SignInResult.NotAllowed;
				result = await _signInManager.PasswordSignInAsync(user, password, false, false);

				if (result.Succeeded)
				{
					return user;
				}
			}
			return null;
		}

		[HttpPost]
		public async Task<User> SignUp(string phoneNumber, string email, string password)
		{
			phoneNumber = phoneNumber.Trim();

			var user = new User { UserName = phoneNumber, Email = email };
			if (!Regex.IsMatch(phoneNumber, PhoneNumberPattern))
			{
				return null;
			}
			else if (!Regex.IsMatch(password, PasswordPattern))
			{
				return null;
			}

			IdentityResult result = await _userManager.CreateAsync(user, password);

			if (result.Succeeded)
			{
				var createdUser = await _userManager.FindByNameAsync(phoneNumber);

				await _userManager.AddClaimAsync(createdUser, new Claim(ClaimTypes.Name, phoneNumber));
			}
			ModelState.AddModelError("Register Error", string.Join(",", result.Errors));

			return user;
		}
	}
}