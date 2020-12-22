using AcademyBank.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace AcademyBank.DAL.Configuration
{

    public class LoginReportsTypeConfiguration : IEntityTypeConfiguration<LoginReport>
    {
        public void Configure(EntityTypeBuilder<LoginReport> builder)
        {
            builder.HasKey(x => x.Id);
            builder.HasOne(x => x.User)
                   .WithMany(x => x.LoginReports)
                   .HasForeignKey(x => x.UserId);
        }
    }
}
