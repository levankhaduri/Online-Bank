using AcademyBank.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace AcademyBank.DAL.Configuration
{
    public class FiltersReportsTypeConfiguration : IEntityTypeConfiguration<FiltersReport>
    {
        public void Configure(EntityTypeBuilder<FiltersReport> builder)
        {
            builder.HasKey(x => x.Id);
            builder.HasOne(x => x.User)
                   .WithMany(x => x.FiltersReports)
                   .HasForeignKey(x => x.UserId);
        }
    }
}
