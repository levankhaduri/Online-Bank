using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace AcademyBank.Models
{
	public class User : IdentityUser<int>
	{
		public User()
		{
			Accounts = new List<Account>();
			LoginReports = new List<LoginReport>();
			FiltersReports = new List<FiltersReport>();
			TransfersReports = new List<TransfersReport>();
			CountersReports = new List<CountersReport>();

		}

		[DefaultValue(false)]
		public bool IsBanned { get; set; }

		[Required]
		public int UserInfoId { get; set; }

		public UserInfo UserInfo { get; set; }

		public IEnumerable<Account> Accounts { get; set; }
		public IEnumerable<LoginReport> LoginReports { get; set; }
		public IEnumerable<FiltersReport> FiltersReports { get; set; }
		public IEnumerable<TransfersReport> TransfersReports { get; set; }
		public IEnumerable<CountersReport> CountersReports { get; set; }

	}
}
