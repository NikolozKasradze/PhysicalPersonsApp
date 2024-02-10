using AutoMapper;
using MediatR;
using PhysicalPersonsApp.Application.Contracts.Persistance;
using PhysicalPersonsApp.Application.Exceptions;
using PhysicalPersonsApp.Domain;

namespace PhysicalPersonsApp.Application.Features.Persons.Commands.Create;

public class CreatePersonCommandHandler : IRequestHandler<CreatePersonCommand, int>
{
    private readonly IPersonRepository _personRepository;
    private readonly IUnitOfWork _unitOfWork;
    //private readonly IStringLocalizer _localizer;
    private readonly IMapper _mapper;

    public CreatePersonCommandHandler(IPersonRepository personRepository, IMapper mapper, IUnitOfWork unitOfWork)//, IStringLocalizer localizer)
    {
        _personRepository = personRepository;
        _mapper = mapper;
        _unitOfWork = unitOfWork;
        //_localizer = localizer;
    }

    public async Task<int> Handle(CreatePersonCommand request, CancellationToken cancellationToken)
    {
        var duplicate = await _personRepository.GetPersonGetWithPersonalNAsync(request.PersonalN);
        if (duplicate != null)
            throw new DuplicateException("Person Already Exists");//_localizer["PersonWithPersonalnExists"]);

        var person = _mapper.Map<Person>(request);

        person = await _unitOfWork.Persons.AddAsync(person);
        await _unitOfWork.SaveChangesAsync();
        return person.ID;
    }
}
