using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AcademyBank.Models
{
    public class AccountLoanViewModel
    {
        private const string PhoneNumberPattern = @"^(\+995)[\s-]?[5]\d{1,3}[\s-]?\d{1,3}[\s-]?\d{1,3}[\s-]?\d{1,3}$";

        public Loan Loan { get; set; }
        public int LoanId { get; set; }
        [Required]
        public string Employment { get; set; }
        [RegularExpression(PhoneNumberPattern, ErrorMessage = "Please provide valid phone number(+995 5)")]
        public string OfficePhoneNumber { get; set; }
        [Required]
        public decimal Sum { get; set; }
        [Required]
        public string Currency { get; set; }
        [Required]
        public string Term { get; set; }
        public int AccrueAccountId { get; set; }
        public int AccountId { get; set; }
        public string Status { get; set; }
    }
}
