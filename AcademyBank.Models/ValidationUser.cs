using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace AcademyBank.Models
{
    public class ValidationUser
    {
        private const string EmailPattern = @"^([\w.-]+)@([\w-]+)((.(\w){2,3})+)$";
        private const string PhoneNumberPattern = @"^(\+995)[\s-]?[5]\d{1,3}[\s-]?\d{1,3}[\s-]?\d{1,3}[\s-]?\d{1,3}$";
        private const string PasswordPattern = @"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[@$!%*?&])[A-Za-z\d@$!%*?&]{8,}$";

        [Required]
        [RegularExpression(EmailPattern, ErrorMessage = "Please provide valid email")]
        public string Email { get; set; }

        [Required]
        [RegularExpression(PhoneNumberPattern, ErrorMessage = "Please provide valid phone number(+995 5)")]
        public string PhoneNumber { get; set; }

        [Required]
        [RegularExpression(PasswordPattern, ErrorMessage = "Please Provide Valid Password(minimum 8 symbol, 1uppercase, 1lowercase and unique characters)")]
        public string Password { get; set; }
    }
}
