using Microsoft.EntityFrameworkCore;
using PhysicalPersonsApp.Application.Contracts.Persistance;
using PhysicalPersonsApp.Application.Features.Persons.Queries.GetPersonList;
using PhysicalPersonsApp.Domain;

namespace PhysicalPersonsApp.Persistence.Repositories;

public class PersonRepository : BaseRepository<Person>, IPersonRepository
{

    private readonly PersonsAppDbContext _dbContext;

    public PersonRepository(PersonsAppDbContext dbContext) : base(dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<Person> GetOneWithPhoneNumbersAsync(int id)
        => await _dbContext.Persons
                    .Include(x => x.PhoneNumbers)
                    .FirstOrDefaultAsync(x => x.ID == id);

    public async Task<Person> GetPersonDetailedInfoAsync(int id)
        => await _dbContext.Persons
                    .Include(x => x.PersonConnections).ThenInclude(x => x.ConnectionType)
                    .Include(x => x.PersonConnections).ThenInclude(x => x.ConnectedPerson)
                    .Include(x => x.PhoneNumbers).ThenInclude(x => x.PhoneType)
                    .Include(x => x.Gender).Include(x => x.City)
                    .FirstOrDefaultAsync(x => x.ID == id);

    public async Task<Person> GetPersonGetWithPersonalNAsync(string personalN)
        => await _dbContext.Persons.FirstOrDefaultAsync(x => x.PersonalN == personalN);

    public async Task<IEnumerable<Person>> PersonSearchAsync(GetPersonsListQuery filter)
    {
        IQueryable<Person> query = _dbContext.Set<Person>();

        if (filter.FirstName != null)
            query = query.Where(t => EF.Functions.Like(t.FirstName, $"%{filter.FirstName}%"));

        if (filter.LastName != null)
            query = query.Where(t => EF.Functions.Like(t.LastName, $"%{filter.LastName}%"));

        if (filter.PersonalN != null)
            query = query.Where(t => EF.Functions.Like(t.PersonalN, $"%{filter.PersonalN}%"));

        if (filter.BirthDate != null)
            query = query.Where(t => t.BirthDate == filter.BirthDate);

        if (filter.GenderId != null)
            query = query.Where(t => t.GenderId == filter.GenderId);

        if (filter.CityId != null)
            query = query.Where(t => t.CityId == filter.CityId);

        return await query.OrderBy(x => x.ID).Skip((filter.PageNum - 1) * filter.PageSize).Take(filter.PageSize).ToListAsync();
    }
}
