using FluentValidation;
using Microsoft.Extensions.Localization;
using PhysicalPersonsApp.Application.Resources;

namespace PhysicalPersonsApp.Application.Features.Persons.Commands.Delete;

public class DeletePersonCommandValidator : AbstractValidator<DeletePersonCommand>
{
    public DeletePersonCommandValidator(IStringLocalizer<FluentValidationMessages> localizer)
    {
        RuleFor(x => x.Id).GreaterThan(0).WithMessage(localizer["MustBeGreaterThan", "{PropertyName}", 0]);
    }
}
