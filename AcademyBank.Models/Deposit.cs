using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace AcademyBank.Models
{
    public class Deposit
    {
        public Deposit()
        {
            AccountDeposits = new List<AccountDeposit>();
        }

        [Key]
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Benefits { get; set; }

        [Required]
        [Range(0,100)]
        [Display(Name = "Annual rate")]
        public decimal Annual { get; set; }

        [Required]
        [Range(0, 100)]
        [Display(Name = "Bonus rate")]
        public decimal Bonus { get; set; }
        [Required]
        [DataType(DataType.Date)]
        public DateTime InterestPaymentDate { get; set; }

        [Required]
        public decimal MinAmount { get; set; }

        [Required]
        public decimal MaxAMount { get; set; }
        [Required]
        public decimal Replenishment { get; set; }
        public IEnumerable<AccountDeposit> AccountDeposits { get; set; }
        [Required]
        public decimal InterestRate { get; set; }
        public string Currency { get; set; }
    }
}
