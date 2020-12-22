using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AcademyBank.Models
{
	public class AdminStatistics
	{
		public int RequestedDeposits { get; set; }
		public int AcceptedDeposits { get; set; }
		public int RejectedDeposits { get; set; }
		public int RequestedLoans { get; set; }
		public int AcceptedLoans { get; set; }
		public int RejectedLoans { get; set; }
		public int RequestedCards { get; set; }
		public int AcceptedCards { get; set; }
		public int RejectedCards { get; set; }
		public int ActiveUsersCount { get; set; }
		public int BlockedUsersCount { get; set; }

	}
}
