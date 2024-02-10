using FluentValidation;
using Microsoft.Extensions.Localization;
using PhysicalPersonsApp.Application.Resources;

namespace PhysicalPersonsApp.Application.Features.ConnectedPersons.Commands.Delete;

public class DeletePersonConnectionCommandValidator : AbstractValidator<DeletePersonConnectionCommand>
{
    public DeletePersonConnectionCommandValidator(IStringLocalizer<FluentValidationMessages> localizer)
    {
        RuleFor(x => x.Id)
            .GreaterThan(0).WithMessage(localizer["MustBeGreaterThan", "{PropertyName}", 0]);
    }
}
