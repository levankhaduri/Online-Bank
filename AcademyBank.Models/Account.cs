using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace AcademyBank.Models
{
    public class Account
    {
        public Account()
        {
            AccountDeposits = new List<AccountDeposit>();
            AccountLoans = new List<AccountLoan>();
            Cards = new List<Card>();
            Transactions = new List<TransactionsHistory>();
            MoneyTransfers = new List<TransactionsHistory>();
        }

        [Key]
        [Required]
        public int Id { get; set; }

        [Required]
        public int UserId { get; set; }

        public User User { get; set; }

        [Required]
        [Display(Name = "Account balance")]
        public decimal Balance { get; set; }

        [Required]
        [Display(Name = "Account name")]
        public string AccountName { get; set; }

        [Required]
        [Display(Name = "Account number")]
        public string AccountNumber { get; set; }

        public IEnumerable<AccountDeposit> AccountDeposits { get; set; }
        public IEnumerable<AccountLoan> AccountLoans { get; set; }
        public IEnumerable<AccountLoan> AccrueAccountLoans { get; set; }

        public IEnumerable<Card> Cards { get; set; }
        public IEnumerable<TransactionsHistory> Transactions { get; set; }
        public IEnumerable<TransactionsHistory> MoneyTransfers { get; set; }
    }
}