using AutoMapper;
using MediatR;
using PhysicalPersonsApp.Application.Contracts.Persistance;
using PhysicalPersonsApp.Application.Exceptions;
using PhysicalPersonsApp.Domain;

namespace PhysicalPersonsApp.Application.Features.ConnectedPersons.Commands;

public class CreatePersonConnectionCommandHandler : IRequestHandler<CreatePersonConnectionCommand, int>
{
    private readonly IPersonConnectionRepository _personConnectionRepository;
    private readonly IUnitOfWork _unitOfWork;
    //private readonly IStringLocalizer _localizer;
    private readonly IMapper _mapper;

    public CreatePersonConnectionCommandHandler(IPersonConnectionRepository personConnectionRepository, IMapper mapper, IUnitOfWork unitOfWork)//, IStringLocalizer localizer)
    {
        _personConnectionRepository = personConnectionRepository;
        _mapper = mapper;
        _unitOfWork = unitOfWork;

        //_localizer = localizer;

    }

    public async Task<int> Handle(CreatePersonConnectionCommand request, CancellationToken cancellationToken)
    {
        if (request.ConnectedPersonId == request.PersonId)
            throw new BadRequestException("Can not add yourself as connected person");

        var duplicate = await _personConnectionRepository.PersonConnectionSearchAsync(request.ConnectionTypeId, request.PersonId, request.ConnectedPersonId);

        if (duplicate != null)
            throw new DuplicateException("Connection Already Exists");//_localizer["ConnectionAllreadyExists"]);

        var personConnection = _mapper.Map<PersonConnection>(request);

        personConnection = await _unitOfWork.PersonConnections.AddAsync(personConnection);
        await _unitOfWork.SaveChangesAsync();

        return personConnection.ID;
    }
}
