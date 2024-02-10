using MediatR;

namespace PhysicalPersonsApp.Application.Features.Persons.Commands.Delete;

public class DeletePersonCommand : IRequest
{
    public int Id { get; set; }
}
