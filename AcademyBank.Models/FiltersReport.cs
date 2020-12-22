using System;
using System.Collections.Generic;
using System.Text;

namespace AcademyBank.Models
{
    public class FiltersReport
    {
        public int Id { get; set; }
        public User User { get; set; }
        public int UserId { get; set; }
        public decimal DepositAmount { get; set; }
        public string DepositCurrency { get; set; }
        public decimal LoanAmount { get; set; }
        public string LoanPurpose { get; set; }
        public string LoanTerm { get; set; }
    }
}
