using AcademyBank.DAL.Configuration;
using AcademyBank.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace AcademyBank.DAL
{
    public class BankDbContext : IdentityDbContext<User, IdentityRole<int>, int>
    {
        private readonly IConfiguration _config;

        public DbSet<Card> Cards { get; set; }
        public DbSet<Loan> Loans { get; set; }
        public DbSet<User> UserLogin { get; set; }
        public DbSet<AccountDeposit> UserDeposits { get; set; }
        public DbSet<UserInfo> UserInfos { get; set; }
        public DbSet<AccountLoan> AccountLoans { get; set; }
        public DbSet<Account> Accounts { get; set; }
        public DbSet<Deposit> Deposits { get; set; }
        public DbSet<TransactionsHistory> TransactionsHistories { get; set; }
        public DbSet<LoginReport> LoginReports { get; set; }
        public DbSet<FiltersReport> FiltersReports { get; set; }
        public DbSet<CountersReport> CountersReports { get; set; }
        public DbSet<TransfersReport> TransfersReports { get; set; }

        public BankDbContext()
        {

        }
       
        public BankDbContext(DbContextOptions options, IConfiguration config) : base(options)
        {
            _config = config;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(_config.GetConnectionString("AcademyBankDb"), opt => opt.MigrationsAssembly("AcademyBank.DAL"));
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfiguration(new AccountDepositEntityTypeConfiguration());
            modelBuilder.ApplyConfiguration(new AccountEntityTypeConfiguration());
            modelBuilder.ApplyConfiguration(new AccountLoanEntityTypeConfiguration());
            modelBuilder.ApplyConfiguration(new CardEntityTypeConfiguration());
            modelBuilder.ApplyConfiguration(new DepositEntityTypeConfiguration());
            modelBuilder.ApplyConfiguration(new LoanEntityTypeConfiguration());
            modelBuilder.ApplyConfiguration(new TransactionsHistoryEntityTypeConfiguration());
            modelBuilder.ApplyConfiguration(new UserEntityTypeConfiguration());
            modelBuilder.ApplyConfiguration(new UserInfoEntityTypeConfiguration());
            modelBuilder.ApplyConfiguration(new LoginReportsTypeConfiguration());
            modelBuilder.ApplyConfiguration(new FiltersReportsTypeConfiguration());
            modelBuilder.ApplyConfiguration(new CountersReportsTypeConfiguration());
            modelBuilder.ApplyConfiguration(new TransfersReportsTypeConfiguration());

        }
    }
}
