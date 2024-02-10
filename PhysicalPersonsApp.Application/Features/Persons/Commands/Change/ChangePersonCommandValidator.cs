using FluentValidation;
using Microsoft.Extensions.Localization;
using PhysicalPersonsApp.Application.Contracts.Persistance;
using PhysicalPersonsApp.Application.Resources;
using PhysicalPersonsApp.Domain;

namespace PhysicalPersonsApp.Application.Features.Persons.Commands.Change;

public class ChangePersonCommandValidator : AbstractValidator<ChangePersonCommand>
{
    private readonly IRepository<Gender> _genderRepository;
    private readonly IRepository<City> _cityRepository;
    private readonly IRepository<Person> _personRepository;
    private readonly IRepository<PhoneType> _phoneTypeRepository;

    public ChangePersonCommandValidator(IRepository<Gender> genderRepository, IRepository<City> cityRepository,
        IRepository<Person> personRepository, IRepository<PhoneType> phoneTypeRepository, IStringLocalizer<FluentValidationMessages> localizer)
    {
        _genderRepository = genderRepository;
        _cityRepository = cityRepository;
        _personRepository = personRepository;
        _phoneTypeRepository = phoneTypeRepository;

        RuleFor(e => e.Id)
            .GreaterThan(0).WithMessage(localizer["MustBeGreaterThan", "{PropertyName}", 0])
            .MustAsync(PersonExists).WithMessage(localizer["NotExists", "{PropertyName}"]);

        RuleFor(e => e.FirstName)
            .MinimumLength(2).WithMessage(localizer["ShorterThanMinLength", "{PropertyName}", 2])
            .MaximumLength(50).WithMessage(localizer["LongerThanMaxLength", "{PropertyName}", 50])
            .Matches("^([a-zA-Z]*|[ა-ჰ]*)$").WithMessage(localizer["MustContainGeorgianAndLatin", "{PropertyName}"]);

        RuleFor(e => e.LastName)
            .MinimumLength(2).WithMessage(localizer["ShorterThanMinLength", "{PropertyName}", 2])
            .MaximumLength(50).WithMessage(localizer["LongerThanMaxLength", "{PropertyName}", 50])
            .Matches("^([a-zA-Z]*|[ა-ჰ]*)$").WithMessage(localizer["MustContainGeorgianAndLatin", "{PropertyName}"]);

        RuleFor(e => e.PersonalN)
            .Length(11).WithMessage(localizer["ExactLength", "{PropertyName}", 11])
            .Matches("^\\d*$").WithMessage(localizer["MustContainOnlyDigits", "{PropertyName}"]);

        RuleFor(e => e.BirthDate)
            .ExclusiveBetween(DateTime.MinValue, DateTime.Now.AddYears(-18)).WithMessage(localizer["MinAge"]);

        RuleFor(e => e.GenderId)
            .GreaterThan(0).WithMessage(localizer["MustBeGreaterThan", "{PropertyName}", 0])
            .MustAsync(GenderExists).WithMessage(localizer["NotExists", "{PropertyName}"]);

        RuleFor(e => e.CityId)
            .GreaterThan(0).WithMessage(localizer["MustBeGreaterThan", "{PropertyName}", 0])
            .MustAsync(CityExists).WithMessage(localizer["NotExists", "{PropertyName}"]);

        RuleForEach(x => x.PhoneNumbers).ChildRules(orders =>
        {
            orders.RuleFor(x => x.Number)
            .MinimumLength(24).WithMessage(localizer["ShorterThanMinLength", "{PropertyName}", 4])
            .MaximumLength(50).WithMessage(localizer["LongerThanMaxLength", "{PropertyName}", 50])
                .Matches("^\\d*$").WithMessage(localizer["MustContainOnlyDigits", "{PropertyName}"]);

            orders.RuleFor(x => x.PhoneTypeId)
                .GreaterThan(0).WithMessage(localizer["MustBeGreaterThan", "{PropertyName}", 0])
                .MustAsync(PhoneTypeExists).WithMessage(localizer["NotExists", "{PropertyName}"]);

        });
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
    private async Task<bool> PhoneTypeExists(int id, CancellationToken token)
    {
        if (id > 0)
        {
            var result = await _phoneTypeRepository.GetOneByIdAsync(id);
            if (result is null)
                return false;
        }

        return true;
    }
    private async Task<bool> CityExists(int id, CancellationToken token)
    {
        if (id > 0)
        {
            var result = await _cityRepository.GetOneByIdAsync(id);
            if (result is null)
                return false;
        }

        return true;
    }
    private async Task<bool> GenderExists(int id, CancellationToken token)
    {
        if (id > 0)
        {
            var result = await _genderRepository.GetOneByIdAsync(id);
            if (result is null)
                return false;
        }

        return true;
    }
}
