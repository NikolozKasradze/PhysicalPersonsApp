using MediatR;

namespace PhysicalPersonsApp.Application.Features.Persons.Queries.GetPersonList;

public class GetPersonsListQuery : IRequest<IEnumerable<PersonListVm>>
{
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string PersonalN { get; set; }
    public DateTime? BirthDate { get; set; }
    public int? GenderId { get; set; }
    public int? CityId { get; set; }
    public int PageNum { get; set; }
    public int PageSize { get; set; }

}
