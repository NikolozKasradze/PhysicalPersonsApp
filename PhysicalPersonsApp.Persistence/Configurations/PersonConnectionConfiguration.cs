using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PhysicalPersonsApp.Domain;

namespace PhysicalPersonsApp.Persistence;

public class PersonConnectionConfiguration : IEntityTypeConfiguration<PersonConnection>
{
    public void Configure(EntityTypeBuilder<PersonConnection> builder)
    {
        builder.HasOne(d => d.ConnectedPerson).WithMany().HasForeignKey(d => d.ConnectedPersonId).OnDelete(DeleteBehavior.NoAction);
        builder.HasOne(d => d.Person).WithMany(x => x.PersonConnections).HasForeignKey(d => d.PersonId);
    }
}
