namespace Infrastructure.Data.Configurations
{
    using Domain.Entities;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;

    public class EmploymentsConfiguration : IEntityTypeConfiguration<Employment>
    {
        public void Configure(EntityTypeBuilder<Employment> builder)
        {
            var navigation = builder.Metadata.FindNavigation(nameof(User.Employments));
            navigation?.SetPropertyAccessMode(PropertyAccessMode.Field);

            builder.HasKey(c => c.Id);

            builder.Property(ci => ci.Id)
                 .UseHiLo("employment_hilo")
                 .IsRequired();

            builder.Property(a => a.Company)
              .IsRequired(true);

            builder.Property(a => a.MonthsOfExperience)
                .IsRequired(true);

            builder.Property(a => a.Salary)
              .IsRequired(true);

            builder.Property(a => a.StartDate)
                .IsRequired(true);
        }
    }
}

