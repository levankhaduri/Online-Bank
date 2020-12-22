using System;
using System.ComponentModel.DataAnnotations;

namespace AcademyBank.Models
{
    public partial class Transactions
    {
        public int Id { get; set; }
        public int SenderCardId { get; set; }
        public int SenderAccountId { get; set; }
        public int ReceiverAccountId { get; set; }
        [Required]
        [Display(Name = "Personal Account")]
        public string PersonalAccountId { get; set; }
        public string Description { get; set; }
        [Required]
        [RegularExpression("([1-9][0-9]*[/.]?[0-9]*)", ErrorMessage = "Amount must be a number")]
        public decimal Amount { get; set; }
        public DateTime Date { get; set; }

        public virtual Account SenderAccount { get; set; }
        public virtual Account ReceiverAccount { get; set; }
        public virtual Card SenderCard { get; set; }
    }
}
