using AutoMapper;
using MediatR;
using PhysicalPersonsApp.Application.Contracts.Persistance;
using PhysicalPersonsApp.Application.Exceptions;

namespace PhysicalPersonsApp.Application.Features.Persons.Queries.GetPersonDetails;

public class GetPersonDetailsQueryHandler : IRequestHandler<GetPersonDetailsQuery, PersonVm>
{

    private readonly IPersonRepository _personRepository;
    private readonly IMapper _mapper;

    public GetPersonDetailsQueryHandler(IPersonRepository personRepository, IMapper mapper)
    {
        _personRepository = personRepository;
        _mapper = mapper;
    }

    public async Task<PersonVm> Handle(GetPersonDetailsQuery request, CancellationToken cancellationToken)
    {

        var person = await _personRepository.GetPersonDetailedInfoAsync(request.Id);

        if (person is null)
            throw new NotFoundException("Person", request.Id.ToString());

        var result = _mapper.Map<PersonVm>(@person);

        return result;
    }
}
