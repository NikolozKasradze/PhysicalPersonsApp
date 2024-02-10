using MediatR;

namespace PhysicalPersonsApp.Application.Features.Persons.Queries.GetPersonDetails;

public class GetPersonDetailsQuery : IRequest<PersonVm>
{
    public int Id { get; set; }
}
