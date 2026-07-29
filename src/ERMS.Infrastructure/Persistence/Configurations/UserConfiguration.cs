using ERMS.Domain.Users;
using ERMS.Infrastructure.Persistence.Converters;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace ERMS.Infrastructure.Persistence.Configurations
{
    public sealed class UserConfiguration : IEntityTypeConfiguration<User> 
    { 
        public void Configure(EntityTypeBuilder<User> builder) 
        { 
            builder.ToTable("USERS"); builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).HasColumnName("ID");
            builder.Property(x => x.Email).HasColumnName("EMAIL").HasConversion<EmailConverter>().HasMaxLength(UserConstants.EmailMaxLength).IsRequired(); 
            builder.OwnsOne(x => x.FullName, name => { name.Property(x => x.FirstName).HasColumnName("FIRST_NAME").HasMaxLength(UserConstants.FirstNameMaxLength).IsRequired(); name.Property(x => x.LastName).HasColumnName("LAST_NAME").HasMaxLength(UserConstants.LastNameMaxLength).IsRequired(); });
            builder.Property(x => x.IsActive).HasColumnName("IS_ACTIVE").IsRequired(); 
            builder.Ignore(x => x.DomainEvents); 
        } 
    }
}
