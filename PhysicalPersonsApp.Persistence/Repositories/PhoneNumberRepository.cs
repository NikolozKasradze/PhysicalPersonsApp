using PhysicalPersonsApp.Application.Contracts.Persistance;
using PhysicalPersonsApp.Domain;

namespace PhysicalPersonsApp.Persistence.Repositories;

public class PhoneNumberRepository : BaseRepository<PhoneNumber>, IPhoneNumberRepository
{

    private readonly PersonsAppDbContext _dbContext;

    public PhoneNumberRepository(PersonsAppDbContext dbContext) : base(dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task DeletePersonsAllNumbersAsync(int personId)
    {
        var numbers = _dbContext.PhoneNumbers.Where(x => x.PersonId == personId);
        foreach (var number in numbers)
            await DeleteAsync(number);
    }

    public Task<IEnumerable<PhoneNumber>> GetPersonsAllNumbersAsync(int personId)
    {
        throw new NotImplementedException();
    }
}
