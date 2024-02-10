using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PhysicalPersonsApp.Domain;

namespace PhysicalPersonsApp.Persistence;

public class PersonConnectionTypeConfiguration : IEntityTypeConfiguration<PersonConnectionType>
{
    public void Configure(EntityTypeBuilder<PersonConnectionType> builder)
    {
        builder.Property(d => d.Name).IsRequired().HasMaxLength(50);
    }
}
