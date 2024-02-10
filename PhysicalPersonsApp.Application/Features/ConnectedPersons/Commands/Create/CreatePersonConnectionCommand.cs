using MediatR;

namespace PhysicalPersonsApp.Application.Features.ConnectedPersons.Commands;

public class CreatePersonConnectionCommand : IRequest<int>
{
    public int ConnectionTypeId { get; set; }
    public int PersonId { get; set; }
    public int ConnectedPersonId { get; set; }
}
