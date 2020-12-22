using AcademyBank.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace AcademyBank.DAL.Configuration
{
    public class AccountLoanEntityTypeConfiguration : IEntityTypeConfiguration<AccountLoan>
    {
        public void Configure(EntityTypeBuilder<AccountLoan> builder)
        {
            builder.HasKey(x => x.Id);
            builder.HasOne(x => x.Loan)
                   .WithMany(x => x.AccountLoans)
                   .HasForeignKey(x => x.LoanId);
            builder.HasOne(x => x.Account)
                   .WithMany(x => x.AccountLoans)
                   .HasForeignKey(x => x.AccountId);
            builder.HasOne(x => x.Account)
                   .WithMany(x => x.AccountLoans)
                   .HasForeignKey(x => x.AccrueAccountId);
            builder.Property(x => x.Sum)
                .HasColumnType("decimal(18,2)");
        }
    }
}
