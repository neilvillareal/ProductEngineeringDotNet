namespace Infrastructure.Data.Configurations
{
    using System;
    using Domain.Entities;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;

    public class UsersConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.HasKey(c => c.Id);

            builder.Property(ci => ci.Id)
                   .UseHiLo("user_hilo")
                   .IsRequired();

            builder.Property(a => a.FirstName)
                .IsRequired(true);

            builder.Property(a => a.LastName)
                .IsRequired(true);

            builder.Property(a => a.Email)
                .IsRequired(true);

            builder.HasIndex(c => c.Email)
                .IsUnique();

        }
    }
}

