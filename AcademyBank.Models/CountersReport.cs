using System;
using System.Collections.Generic;
using System.Text;

namespace AcademyBank.Models
{
    public class CountersReport
    {
        public int Id { get; set; }
        public User User { get; set; }
        public int UserId { get; set; }
        public int DepositPage { get; set; }
        public int LoansPage { get; set; }
        public int CardsPage { get; set; }
        public int AccountPage { get; set; }
    }
}