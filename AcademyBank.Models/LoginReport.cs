using System;
using System.Collections.Generic;
using System.Text;

namespace AcademyBank.Models
{
    public class LoginReport
    {
        public int Id { get; set; }
        public User User { get; set; }
        public int UserId { get; set; }
        public int CounterLogsIn { get; set; }
        public DateTime FirstLogin { get; set; }
        public DateTime LastLogin { get; set; }
        public decimal AvgPerday { get; set; }
        public int PerMonth { get; set; }
    }
}
