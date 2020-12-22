using AcademyBank.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace AcademyBank.DAL.Configuration
{
    public class TransfersReportsTypeConfiguration : IEntityTypeConfiguration<TransfersReport>
    {
        public void Configure(EntityTypeBuilder<TransfersReport> builder)
        {
            builder.HasKey(x => x.Id);
            builder.HasOne(x => x.User)
                   .WithMany(x => x.TransfersReports)
                   .HasForeignKey(x => x.UserId);
        }
    }
}
