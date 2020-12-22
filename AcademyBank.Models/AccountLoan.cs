using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace AcademyBank.Models
{
    public class AccountLoan
    {
        private const string PhoneNumberPattern = @"^(\+995)[\s-]?[5]\d{1,3}[\s-]?\d{1,3}[\s-]?\d{1,3}[\s-]?\d{1,3}$";

        [Key]
		public int Id { get; set; }
		public int AccountId { get; set; }
        public Account Account { get; set; }
        [DataType(DataType.Date)]
        public DateTime AcceptanceTime { get; set; }
        public int LoanId { get; set; }
        public Loan Loan { get; set; }
        [Required]
        public string Currency { get; set; }
        [Required]
        public string Term { get; set; }
        [Required]
        [Display(Name = "Loan Sum")]
        public decimal Sum { get; set; }
        public int AccrueAccountId { get; set; }
        public Account AccrueAccount { get; set; }
        [Required]
        public string Status { get; set; }
        [Required]
        public string Employment { get; set; }
        [RegularExpression(PhoneNumberPattern, ErrorMessage = "Please provide valid phone number(+995 5)")]
        public string OfficePhoneNumber { get; set; }
    }
}
