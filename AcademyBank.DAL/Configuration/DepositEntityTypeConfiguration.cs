using AcademyBank.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace AcademyBank.DAL.Configuration
{
    public class DepositEntityTypeConfiguration : IEntityTypeConfiguration<Deposit>
    {
        public void Configure(EntityTypeBuilder<Deposit> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Benefits)
               .HasColumnType("decimal(18,2)");
            builder.Property(x => x.Annual)
                .HasColumnType("decimal(18,2)");
            builder.Property(x => x.Bonus)
                .HasColumnType("decimal(18,2)");
            builder.Property(x => x.MinAmount)
                .HasColumnType("decimal(18,2)");
            builder.Property(x => x.MaxAMount)
                .HasColumnType("decimal(18,2)");
            builder.Property(x => x.Replenishment)
                .HasColumnType("decimal(18,2)");
            builder.Property(x => x.InterestRate)
               .HasColumnType("decimal(18,2)");
        }
    }
}
