using AcademyBank.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace AcademyBank.DAL.Configuration
{
    public class CountersReportsTypeConfiguration : IEntityTypeConfiguration<CountersReport>
    {
        public void Configure(EntityTypeBuilder<CountersReport> builder)
        {
            builder.HasKey(x => x.Id);
            builder.HasOne(x => x.User)
                   .WithMany(x => x.CountersReports)
                   .HasForeignKey(x => x.UserId);
        }
    }
}
