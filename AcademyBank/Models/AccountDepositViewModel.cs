using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace AcademyBank.Models
{
    public class AccountDepositViewModel
    {
        [Required]
        public string Status { get; set; }
        public int AccountId { get; set; }
        public int DepositId { get; set; }
        public Deposit Deposit { get; set; }
        [RegularExpression(@"^(((\d{1,3})(,\d{3})*)|(\d+))(.\d+)?$", ErrorMessage = "Value should be only numbers")]
        [Range(0, Int32.MaxValue)]
        public decimal DepositAmount { get; set; }
        public string Term { get; set; }
    }
}
