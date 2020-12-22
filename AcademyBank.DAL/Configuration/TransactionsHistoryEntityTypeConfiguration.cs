using AcademyBank.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace AcademyBank.DAL.Configuration
{
    public class TransactionsHistoryEntityTypeConfiguration : IEntityTypeConfiguration<TransactionsHistory>
    {
        public void Configure(EntityTypeBuilder<TransactionsHistory> builder)
        {
            builder.HasKey(x => x.Id);
            builder.HasOne(x => x.Account)
                    .WithMany(x => x.Transactions)
                    .HasForeignKey(x => x.AccountId);
            builder.HasOne(x => x.RecipientAccount)
                    .WithMany(x => x.MoneyTransfers)
                    .HasForeignKey(x => x.RecipientAccountId)
                    .OnDelete(DeleteBehavior.NoAction);
            builder.Property(x => x.Amount)
                .HasColumnType("decimal(18,2)");
        }
    }
}
