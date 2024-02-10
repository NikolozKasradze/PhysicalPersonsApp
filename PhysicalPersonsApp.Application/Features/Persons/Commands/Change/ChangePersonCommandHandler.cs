using AutoMapper;
using MediatR;
using PhysicalPersonsApp.Application.Contracts.Persistance;
using PhysicalPersonsApp.Application.Exceptions;
using PhysicalPersonsApp.Domain;

namespace PhysicalPersonsApp.Application.Features.Persons.Commands.Change;

public class ChangePersonCommandHandler : IRequestHandler<ChangePersonCommand>
{
    private readonly IPersonRepository _personRepository;
    private readonly IUnitOfWork _unitOfWork;
    //private readonly IStringLocalizer _localizer;
    private readonly IMapper _mapper;

    public ChangePersonCommandHandler(IPersonRepository personRepository, IMapper mapper, IUnitOfWork unitOfWork)//, IStringLocalizer localizer)
    {
        _personRepository = personRepository;
        _mapper = mapper;
        _unitOfWork = unitOfWork;
        //_localizer = localizer;
    }

    public async Task Handle(ChangePersonCommand request, CancellationToken cancellationToken)
    {
        var duplicate = await _personRepository.GetPersonGetWithPersonalNAsync(request.PersonalN);
        var toUpdate = await _personRepository.GetOneWithPhoneNumbersAsync(request.Id);
        if (duplicate != null && duplicate.ID != toUpdate.ID)
            throw new DuplicateException("Person Already Exists");//_localizer["PersonWithPersonalnExists"]);

        _mapper.Map(request, toUpdate, typeof(ChangePersonCommand), typeof(Person));

        await _unitOfWork.Persons.UpdateAsync(toUpdate);
        await _unitOfWork.SaveChangesAsync();
    }
}
