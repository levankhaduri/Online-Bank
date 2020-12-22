using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace AcademyBank.Models
{
    public class AccountDeposit
    {
		[Key]
		public int Id { get; set; }
		[Required]
        public string Status { get; set; }
        [DataType(DataType.Date)]
        public DateTime AcceptanceTime { get; set; }
        public int AccountId { get; set; }
        public Account Account { get; set; }
        public int DepositId { get; set; }
        public Deposit Deposit { get; set; }
        public decimal DepositAmount { get; set; }
        public string Term { get; set; }
    }
}
