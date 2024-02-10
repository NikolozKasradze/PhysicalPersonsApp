using FluentValidation;
using Microsoft.Extensions.Localization;
using PhysicalPersonsApp.Application.Resources;

namespace PhysicalPersonsApp.Application.Features.Persons.Queries.GetPersonList;

public class GetPersonsListQueryValidator : AbstractValidator<GetPersonsListQuery>
{
    public GetPersonsListQueryValidator(IStringLocalizer<FluentValidationMessages> localizer)
    {
        RuleFor(x => x.PageNum).GreaterThan(0).WithMessage(localizer["MustBeGreaterThan", "{PropertyName}", 0]);
        RuleFor(x => x.PageSize).GreaterThan(0).WithMessage(localizer["MustBeGreaterThan", "{PropertyName}", 0]);
    }
}
