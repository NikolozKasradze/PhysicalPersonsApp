using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PhysicalPersonsApp.Domain;

namespace PhysicalPersonsApp.Persistence;

public class PersonConfiguration : IEntityTypeConfiguration<Person>
{
    public void Configure(EntityTypeBuilder<Person> builder)
    {
        builder.Property(d => d.FirstName).IsRequired().HasMaxLength(50);
        builder.Property(d => d.LastName).IsRequired().HasMaxLength(50);
        builder.Property(d => d.PersonalN).IsRequired().HasMaxLength(11);
        builder.Property(d => d.BirthDate).IsRequired().HasColumnType("date");
    }
}
