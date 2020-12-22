using AcademyBank.BLL.Services.Interfaces;
using AcademyBank.DAL.Repositories.Interfaces;
using AcademyBank.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AcademyBank.BLL.Services.Implementations
{
	public class UserService : IUserService
	{
		private readonly UserManager<User> _user;
		private readonly IUserInfoService _userInfoService;
		private readonly UserManager<User> _userManager;

		public UserService(UserManager<User> user, IUserInfoService userInfoService, UserManager<User> userManager)
		{
			_user = user;
			_userInfoService = userInfoService;
			_userManager = userManager;
		}
		public async Task<IdentityResult> Create(User user)
		{
			var result = await _user.CreateAsync(user);
			return result;
		}
		public async Task Delete(User user)
		{
			await _user.DeleteAsync(user);
		}
		public async Task<IEnumerable<User>> Get()
		{
			var result = await _user.Users.ToListAsync();
			return result;
		}

		public async Task<User> GetByEmail(string email)
		{
			var result = await _user.FindByEmailAsync(email);
			return result;
		}

		public async Task<User> GetById(int id)
		{
			var result = await _user.Users
				.Include(x => x.Accounts)
				.SingleOrDefaultAsync(x => x.Id == id);
			return result;
		}
		public async Task<IdentityResult> Update(User user)
		{
			var existingUser = await _user.FindByIdAsync(user.Id.ToString());
			existingUser.PhoneNumber = user.PhoneNumber;
			existingUser.Email = user.Email;

			var existingUserInfo = (await _userInfoService.Get()).FirstOrDefault(x => x.UserId == existingUser.Id);
			existingUserInfo.FirstName = user.UserInfo.FirstName;
			existingUserInfo.LastName = user.UserInfo.LastName;
			existingUserInfo.Sex = user.UserInfo.Sex;
			existingUserInfo.PassportId = user.UserInfo.PassportId;
			existingUserInfo.City = user.UserInfo.City;
			existingUserInfo.Address = user.UserInfo.Address;
			existingUserInfo.ImageName = user.UserInfo.ImageName;

			await _userInfoService.Update(existingUserInfo);

			var result = await _user.UpdateAsync(existingUser);

			return result;
		}
		public async Task<IEnumerable<User>> GetActiveUsers()
		{
			return await _user.Users.Include(x => x.UserInfo).Where(x => x.IsBanned == false).ToListAsync();
		}
		public async Task<IEnumerable<User>> GetBlockedUsers()
		{
			return await _user.Users.Include(x => x.UserInfo).Where(x => x.IsBanned == true).ToListAsync();
		}
		public async Task BlockUserById(int id)
		{
			User user = await GetById(id);
			if (user == null) return;
			user.IsBanned = true;
			await Update(user);
			return;
		}
		public async Task UnBlockUserById(int id)
		{
			User user = await GetById(id);
			if (user == null) return;
			user.IsBanned = false;
			await Update(user);
			return;
		}

		public async Task<User> GetByName(string name)
		{
			var result = await _user.Users.Include(x => x.UserInfo).SingleAsync(x => x.UserName == name);

			return result;
		}

		public async Task  UploadPhoto(IFormFile image, string userName)
		{
			if (image?.Length > 0)
			{
				var uploads = Path.Combine(Environment.CurrentDirectory, "wwwroot/images/ProfilePhotos");
				var ImageName = Path.GetRandomFileName() + '.' + image.FileName.Split('.').Last();
				var filePath = Path.Combine(uploads, ImageName);

				using (var fileStream = new FileStream(filePath, FileMode.Create))
				{
					await image.CopyToAsync(fileStream);
				}

				var user = await _userManager.FindByNameAsync(userName);

				var userInfo = await _userInfoService.GetById(user.Id);
				userInfo.ImageName = ImageName;
				await _userInfoService.Update(userInfo);
			}
		}
	}
}
