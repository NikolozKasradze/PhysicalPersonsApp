namespace PhysicalPersonsApp.Domain;

public class Person
{
    public int ID { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string PersonalN { get; set; }
    public DateTime? BirthDate { get; set; }
    public string? Image { get; set; }

    public int GenderId { get; set; }
    public virtual Gender Gender { get; set; }

    public int CityId { get; set; }
    public virtual City City { get; set; }


    public virtual IEnumerable<PhoneNumber> PhoneNumbers { get; set; }
    public virtual IEnumerable<PersonConnection> PersonConnections { get; set; }


}
