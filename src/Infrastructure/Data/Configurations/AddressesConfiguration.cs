namespace Infrastructure.Data.Configurations
{
    using System;
    using Domain.Entities;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;

    public class AddressesConfiguration : IEntityTypeConfiguration<Address>
    {
        public void Configure(EntityTypeBuilder<Address> builder)
        {
            var navigation = builder.Metadata.FindNavigation(nameof(User.Address));
            navigation?.SetPropertyAccessMode(PropertyAccessMode.Field);

            builder.HasKey(c => c.Id);

            builder.Property(ci => ci.Id)
                 .UseHiLo("address_hilo")
                 .IsRequired();

            builder.Property(a => a.City)
              .IsRequired(true);

            builder.Property(a => a.Street)
                .IsRequired(true);
        }
    }
}

