using AcademyBank.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace AcademyBank.DAL.Configuration
{
    public class CardEntityTypeConfiguration : IEntityTypeConfiguration<Card>
    {
        public void Configure(EntityTypeBuilder<Card> builder)
        {
            builder.HasKey(x => x.Id);
            builder.HasOne(x => x.Account)
                   .WithMany(x => x.Cards)
                   .HasForeignKey(x => x.AccountId);
        }
    }
}
