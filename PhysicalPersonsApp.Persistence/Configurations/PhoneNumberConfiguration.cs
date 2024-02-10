using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PhysicalPersonsApp.Domain;

namespace PhysicalPersonsApp.Persistence;

public class PhoneNumberConfiguration : IEntityTypeConfiguration<PhoneNumber>
{
    public void Configure(EntityTypeBuilder<PhoneNumber> builder)
    {
        builder.Property(d => d.Number).IsRequired().HasMaxLength(50);
    }
}
