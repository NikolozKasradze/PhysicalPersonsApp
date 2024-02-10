using FluentValidation;
using Microsoft.Extensions.Localization;
using PhysicalPersonsApp.Application.Contracts.Persistance;
using PhysicalPersonsApp.Application.Resources;
using PhysicalPersonsApp.Domain;

namespace PhysicalPersonsApp.Application.Features.ConnectedPersons.Commands;

public class CreatePersonConnectionCommandValidator : AbstractValidator<CreatePersonConnectionCommand>
{
    private readonly IRepository<Person> _personRepository;
    private readonly IRepository<PersonConnectionType> _connectionTypeRepository;

    public CreatePersonConnectionCommandValidator(IRepository<Person> personRepository, IRepository<PersonConnectionType> connectionTypeRepository, IStringLocalizer<FluentValidationMessages> localizer)
    {
        _personRepository = personRepository;
        _connectionTypeRepository = connectionTypeRepository;

        RuleFor(x => x.ConnectionTypeId)
            .GreaterThan(0).WithMessage(localizer["MustBeGreaterThan", "{PropertyName}", 0])
            .MustAsync(ConnectionTypeExists).WithMessage(localizer["NotExists", "{PropertyName}"]);

        RuleFor(x => x.PersonId)
            .GreaterThan(0).WithMessage(localizer["MustBeGreaterThan", "{PropertyName}", 0])
            .MustAsync(PersonExists).WithMessage(localizer["NotExists", "{PropertyName}"]);

        RuleFor(x => x.ConnectedPersonId)
            .GreaterThan(0).WithMessage(localizer["MustBeGreaterThan", "{PropertyName}", 0])
            .MustAsync(PersonExists).WithMessage(localizer["NotExists", "{PropertyName}"]);
    }


    private async Task<bool> PersonExists(int id, CancellationToken token)
    {
        if (id > 0)
        {
            var result = await _personRepository.GetOneByIdAsync(id);
            if (result is null)
                return false;
        }

        return true;
    }
    private async Task<bool> ConnectionTypeExists(int id, CancellationToken token)
    {
        if (id > 0)
        {
            var result = await _connectionTypeRepository.GetOneByIdAsync(id);
            if (result is null)
                return false;
        }

        return true;
    }
}
