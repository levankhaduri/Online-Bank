using AcademyBank.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace AcademyBank.DAL.Configuration
{
    public class LoanEntityTypeConfiguration : IEntityTypeConfiguration<Loan>
    {
        public void Configure(EntityTypeBuilder<Loan> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.MinAmount)
                .HasColumnType("decimal(18,2)");
            builder.Property(x => x.Percentage)
                .HasColumnType("decimal(18,2)");
            builder.Property(x => x.AccidentInsurance)
                .HasColumnType("decimal(18,2)");
            builder.Property(x => x.InsuranceLoan)
                .HasColumnType("decimal(18,2)");
            builder.Property(x => x.ServiceFee)
                .HasColumnType("decimal(18,2)");
            builder.Property(x => x.MaxAmount)
                .HasColumnType("decimal(18,2)");
            builder.Property(x => x.LoanInterestRate)
                .HasColumnType("decimal(18,2)");
            builder.Property(x => x.InterestRate)
                .HasColumnType("decimal(18,2)");
        }
    }
}
