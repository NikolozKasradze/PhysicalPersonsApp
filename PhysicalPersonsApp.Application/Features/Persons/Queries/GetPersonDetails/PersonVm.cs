namespace PhysicalPersonsApp.Application.Features.Persons.Queries.GetPersonDetails;

public class PersonVm
{
    public int? ID { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string PersonalN { get; set; }
    public DateTime? BirthDate { get; set; }
    public string Image { get; set; }
    public string Gender { get; set; }
    public string City { get; set; }

    public List<PersonConnectionDto> PersonConnections { get; set; }
    public List<PhoneNumberDto> PhoneNumbers { get; set; }
}
