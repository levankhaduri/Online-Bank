using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace AcademyBank.Models
{
    public class UserInfo
    {
        [Key]
        [Required]
        public int Id { get; set; }

        [Required]
        [MinLength(2, ErrorMessage = "Enter at least 2 letters")]
        [RegularExpression("[A-z]*$", ErrorMessage = "Invalid character")]
        [Display(Name = "Fist name")]
        public string FirstName { get; set; }

        [Required]
        [MinLength(2, ErrorMessage = "Enter at least 2 letters")]
        [RegularExpression("[A-z]*$", ErrorMessage = "Invalid character")]
        [Display(Name = "Last name")]
        public string LastName { get; set; }

        [Required]
        public string Sex { get; set; }

        [Required]
        [MinLength(11, ErrorMessage = "Passport Id Should Be 11 Numbers")]
        [MaxLength(11)]
        [RegularExpression("^[0-9]*$", ErrorMessage = "Passport Id should be only numbers")]
        public string PassportId { get; set; }

        [Required]
        public string City { get; set; }    

        [Required]
        [MinLength(4, ErrorMessage = "Address should be more than 4 characters")]
        [MaxLength(50, ErrorMessage ="Address can't be more than 50 characters")]
        public string Address { get; set; }

        public int UserId { get; set; }

        public User User { get; set; }
        public string ImageName { get; set; }
    }
}
