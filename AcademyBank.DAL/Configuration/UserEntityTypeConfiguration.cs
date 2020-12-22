using AcademyBank.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace AcademyBank.DAL.Configuration
{
    public class UserEntityTypeConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
                builder.HasKey(x => x.Id);
                builder.HasOne(x => x.UserInfo).WithOne(x => x.User).HasForeignKey<UserInfo>(f => f.UserId);
        }
    }
}
