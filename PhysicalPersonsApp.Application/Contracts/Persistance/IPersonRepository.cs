using PhysicalPersonsApp.Application.Features.Persons.Queries.GetPersonList;
using PhysicalPersonsApp.Domain;

namespace PhysicalPersonsApp.Application.Contracts.Persistance;

public interface IPersonRepository : IRepository<Person>
{
    Task<Person> GetPersonDetailedInfoAsync(int id);
    Task<Person> GetPersonGetWithPersonalNAsync(string personalN);
    Task<IEnumerable<Person>> PersonSearchAsync(GetPersonsListQuery filter);
    Task<Person> GetOneWithPhoneNumbersAsync(int id);
}
