using PhysicalPersonsApp.Domain;

namespace PhysicalPersonsApp.Application.Contracts.Persistance;

public interface IPhoneNumberRepository : IRepository<PhoneNumber>
{
    Task<IEnumerable<PhoneNumber>> GetPersonsAllNumbersAsync(int personId);
    Task DeletePersonsAllNumbersAsync(int personId);
}
