using FluentValidation;
using Microsoft.Extensions.Localization;
using PhysicalPersonsApp.Application.Contracts.Persistance;
using PhysicalPersonsApp.Application.Resources;
using PhysicalPersonsApp.Domain;

namespace PhysicalPersonsApp.Application.Features.Persons.Commands.ChangeImage;

public class AddPictureCommandValidator : AbstractValidator<AddPictureCommand>
{
    private readonly IRepository<Person> _personRepository;

    public AddPictureCommandValidator(IRepository<Person> personRepository, IStringLocalizer<FluentValidationMessages> localizer)
    {
        _personRepository = personRepository;

        RuleFor(e => e.PersonId)
            .GreaterThan(0).WithMessage(localizer["MustBeGreaterThan", "{PropertyName}", 0])
            .MustAsync(PersonExists).WithMessage(localizer["NotExists", "{PropertyName}"]);

        RuleFor(e => e.Image)
            .NotNull().WithMessage(localizer["ImageIsMandatory"]);
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
}
