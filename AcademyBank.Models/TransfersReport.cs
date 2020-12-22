using System;
using System.Collections.Generic;
using System.Text;

namespace AcademyBank.Models
{
    public class TransfersReport
    {
        public int Id { get; set; }
        public User User { get; set; }
        public int UserId { get; set; }
        public decimal Spent { get; set; }
        public decimal Received { get; set; }
    }
}
