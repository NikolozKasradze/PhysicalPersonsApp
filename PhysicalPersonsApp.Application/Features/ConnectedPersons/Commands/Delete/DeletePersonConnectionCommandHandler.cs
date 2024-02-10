using MediatR;
using PhysicalPersonsApp.Application.Contracts.Persistance;
using PhysicalPersonsApp.Application.Exceptions;
using PhysicalPersonsApp.Domain;

namespace PhysicalPersonsApp.Application.Features.ConnectedPersons.Commands.Delete;

public class DeletePersonConnectionCommandHandler : IRequestHandler<DeletePersonConnectionCommand>
{
    private readonly IRepository<PersonConnection> _repository;
    private readonly IUnitOfWork _unitOfWork;

    public DeletePersonConnectionCommandHandler(IRepository<PersonConnection> repository, IUnitOfWork unitOfWork)
    {
        _repository = repository;
        _unitOfWork = unitOfWork;

    }

    public async Task Handle(DeletePersonConnectionCommand request, CancellationToken cancellationToken)
    {
        var toDelete = await _repository.GetOneByIdAsync(request.Id);

        if (toDelete is null)
            throw new NotFoundException("PersonConnection", request.Id.ToString());

        await _unitOfWork.PersonConnections.DeleteAsync(toDelete);
        await _unitOfWork.SaveChangesAsync();
    }
}
