using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace AcademyBank.Models
{
    public class Loan
    {
        public Loan()
        {
            AccountLoans = new List<AccountLoan>();
        }

        [Key]
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        [Range(0, 100)]
        public decimal Percentage { get; set; }
        [Required]
        public decimal AccidentInsurance { get; set; }
        [Required]
        public decimal InsuranceLoan { get; set; }
        public decimal ServiceFee { get; set; }
        public IEnumerable<AccountLoan> AccountLoans { get; set; }
        [Required]
        public string Purpose { get; set; }
        [Required]
        public string Term { get; set; }
        [Required]
        public string Currency { get; set; }
        [Required]
        [Display(Name = "Repayment Schedule")]
        public string RepaymentSchedule { get; set; }
        [Required]
        public string AdvancedPayment { get; set; }
        [Required]
        public decimal InterestRate { get; set; }
        [Required]
        public decimal MinAmount { get; set; }
        [Required]
        public decimal MaxAmount { get; set; }
        public decimal LoanInterestRate { get; set; }
    }
}
