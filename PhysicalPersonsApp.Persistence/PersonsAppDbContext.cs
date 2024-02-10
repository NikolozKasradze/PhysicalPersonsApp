using Microsoft.EntityFrameworkCore;
using PhysicalPersonsApp.Domain;

namespace PhysicalPersonsApp.Persistence;

public class PersonsAppDbContext : DbContext
{

    public PersonsAppDbContext(DbContextOptions<PersonsAppDbContext> options)
        : base(options)
    {
    }


    public DbSet<Person> Persons { get; set; }
    public DbSet<Gender> Genders { get; set; }
    public DbSet<City> Cities { get; set; }
    public DbSet<PersonConnection> PersonConnections { get; set; }
    public DbSet<PersonConnectionType> PersonConnectionTypes { get; set; }
    public DbSet<PhoneNumber> PhoneNumbers { get; set; }


    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(PersonsAppDbContext).Assembly);


        modelBuilder.Entity<Gender>()
            .HasData(
                new Gender { ID = 1, Name = "ქალი" },
                new Gender { ID = 2, Name = "კაცი" }
            );

        modelBuilder.Entity<PhoneType>()
           .HasData(
                new PhoneType { ID = 1, Name = "მობილური" },
                new PhoneType { ID = 2, Name = "ოფისის" },
                new PhoneType { ID = 3, Name = "სახლის" }
           );


        modelBuilder.Entity<PersonConnectionType>()
          .HasData(
            new PersonConnectionType { ID = 1, Name = "კოლეგა" },
            new PersonConnectionType { ID = 2, Name = "ნაცნობი" },
            new PersonConnectionType { ID = 3, Name = "ნათესავი" },
            new PersonConnectionType { ID = 4, Name = "სხვა" }
          );


        modelBuilder.Entity<City>().
           HasData(
               new City { ID = 1, Name = "თბილისი" },
               new City { ID = 2, Name = "ბათუმი" },
               new City { ID = 3, Name = "ქუთაისი" }
         );
    }
}
