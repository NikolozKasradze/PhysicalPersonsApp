using AutoMapper;
using MediatR;
using PhysicalPersonsApp.Application.Contracts.Persistance;
using PhysicalPersonsApp.Application.Exceptions;

namespace PhysicalPersonsApp.Application.Features.Persons.Commands.Delete;

public class DeletePersonCommandHandler : IRequestHandler<DeletePersonCommand>
{
    private readonly IPersonRepository _personRepository;
    private readonly IPersonConnectionRepository _personConnectionRepository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public DeletePersonCommandHandler(IPersonRepository personRepository, IMapper mapper, IPersonConnectionRepository personConnectionRepository, IUnitOfWork unitOfWork)
    {
        _personRepository = personRepository;
        _mapper = mapper;
        _personConnectionRepository = personConnectionRepository;
        _unitOfWork = unitOfWork;

    }

    public async Task Handle(DeletePersonCommand request, CancellationToken cancellationToken)
    {
        if (await _personConnectionRepository.CheckIfPersonIsConnectedAsync(request.Id))
            throw new BadRequestException("წაშლა შეუძლებელია პირი იძებნება დაკავშირებულთა სიაში");

        var toDelete = await _personRepository.GetOneByIdAsync(request.Id);

        if (toDelete is null)
            throw new NotFoundException("Person", request.Id.ToString());

        await _unitOfWork.Persons.DeleteAsync(toDelete);
        await _unitOfWork.SaveChangesAsync();
    }
}
