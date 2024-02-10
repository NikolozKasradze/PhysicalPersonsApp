using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PhysicalPersonsApp.Domain;

namespace PhysicalPersonsApp.Persistence;

public class PhoneTypeConfiguration : IEntityTypeConfiguration<PhoneType>
{
    public void Configure(EntityTypeBuilder<PhoneType> builder)
    {
        builder.Property(d => d.Name).IsRequired().HasMaxLength(50);
    }
}
