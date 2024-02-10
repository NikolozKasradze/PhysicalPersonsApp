using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PhysicalPersonsApp.Domain;

namespace PhysicalPersonsApp.Persistence;

public class GenderConfiguration : IEntityTypeConfiguration<Gender>
{
    public void Configure(EntityTypeBuilder<Gender> builder)
    {
        builder.Property(d => d.Name).IsRequired().HasMaxLength(50);
    }
}
