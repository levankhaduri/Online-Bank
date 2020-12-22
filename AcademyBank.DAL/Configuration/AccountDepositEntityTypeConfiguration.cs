using AcademyBank.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace AcademyBank.DAL.Configuration
{
    public class AccountDepositEntityTypeConfiguration : IEntityTypeConfiguration<AccountDeposit>
    {
        public void Configure(EntityTypeBuilder<AccountDeposit> builder)
        {
            builder.HasKey(x => x.Id);
            builder.HasOne(x => x.Deposit)
                   .WithMany(x => x.AccountDeposits)
                   .HasForeignKey(x => x.DepositId);
            builder.HasOne(x => x.Account)
                   .WithMany(x => x.AccountDeposits)
                   .HasForeignKey(x => x.AccountId);
            builder.Property(x => x.DepositAmount)
                   .HasColumnType("decimal(18,2)");
        }
    }
}
