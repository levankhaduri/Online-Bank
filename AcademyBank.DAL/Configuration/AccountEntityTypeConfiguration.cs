using AcademyBank.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace AcademyBank.DAL.Configuration
{
    public class AccountEntityTypeConfiguration : IEntityTypeConfiguration<Account>
    {
        public void Configure(EntityTypeBuilder<Account> builder)
        {
            builder.HasKey(x => x.Id);
            builder.HasOne(x => x.User)
                    .WithMany(x => x.Accounts)
                    .HasForeignKey(x => x.UserId);
            builder.Property(x => x.Balance)
                .HasColumnType("decimal(18,2)");
        }
    }
}
