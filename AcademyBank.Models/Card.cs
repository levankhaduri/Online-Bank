using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace AcademyBank.Models
{
    public class Card
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [Display(Name = "Card Number")]
        public string CardNumber { get; set; }

        [Required]
        [DataType(DataType.Date)]
        [Display(Name = "Created At")]
        public DateTime CreatedAt { get; set; }

        [Required]
        [DataType(DataType.Date)]
        [Display(Name = "Expiration Date")]
        public DateTime ExpireDate { get; set; }

        [Required]
        public int CCV { get; set; }

        [Required]
        public string Status { get; set; }

        [Display(Name = "Account ID")]
        public int AccountId { get; set; }
        public Account Account { get; set; }

        [Required]
        [Display(Name = "Card Type")]
        public string CardType { get; set; }
    }
}