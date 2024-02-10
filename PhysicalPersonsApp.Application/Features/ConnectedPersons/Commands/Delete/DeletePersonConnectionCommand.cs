using MediatR;

namespace PhysicalPersonsApp.Application.Features.ConnectedPersons.Commands.Delete;

public class DeletePersonConnectionCommand : IRequest
{
    public int Id { get; set; }
}
