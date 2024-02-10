using AutoMapper;
using PhysicalPersonsApp.Application.Features.ConnectedPersons.Commands;
using PhysicalPersonsApp.Application.Features.Persons.Commands.Change;
using PhysicalPersonsApp.Application.Features.Persons.Commands.Create;
using PhysicalPersonsApp.Application.Features.Persons.Queries.GetPersonDetails;
using PhysicalPersonsApp.Application.Features.Persons.Queries.GetPersonList;
using PhysicalPersonsApp.Domain;

namespace PhysicalPersonsApp.Application.Profiles;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<Person, PersonListVm>().ReverseMap();


        CreateMap<Person, PersonVm>()
            .ForMember(x => x.City, y => y.MapFrom(z => z.City.Name))
            .ForMember(x => x.Gender, y => y.MapFrom(z => z.Gender.Name));
        CreateMap<PhoneNumber, PhoneNumberDto>()
            .ForMember(x => x.PhoneType, y => y.MapFrom(z => z.PhoneType.Name));
        CreateMap<PersonConnection, PersonConnectionDto>()
            .ForMember(x => x.ConnectionType, y => y.MapFrom(z => z.ConnectionType.Name))
            .ForMember(x => x.ConnectedPerson, y => y.MapFrom(z => $"{z.ConnectedPerson.FirstName} {z.ConnectedPerson.LastName}"));


        CreateMap<Person, CreatePersonCommand>().ReverseMap();
        CreateMap<PhoneNumber, CreatePhoneNumberDto>().ReverseMap();

        CreateMap<Person, ChangePersonCommand>().ReverseMap();
        CreateMap<PhoneNumber, ChangePhoneNumberDto>().ReverseMap();

        CreateMap<PersonConnection, CreatePersonConnectionCommand>().ReverseMap();

    }
}
