using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace AcademyBank.Models
{
    public class TransactionsHistory
    {
        [Key]
        public int Id { get; set; }
        public int AccountId { get; set; }
        public int RecipientAccountId { get; set; }
        public Account Account { get; set; }
        public Account RecipientAccount { get; set; }
        [Display(Name = "Operation Description")]
        public string Description { get; set; }
        [Display(Name = "Operation Amount")]
        public decimal Amount { get; set; }
        public string Currency { get; set; }
        [DataType(DataType.Date)]
        [Display(Name = "Operation Date")]
        public DateTime Date { get; set; }
    }
}
