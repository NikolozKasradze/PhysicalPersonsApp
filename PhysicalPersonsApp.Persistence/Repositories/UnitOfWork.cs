using Microsoft.EntityFrameworkCore.Storage;
using PhysicalPersonsApp.Application.Contracts.Persistance;
using PhysicalPersonsApp.Persistence;
using PhysicalPersonsApp.Persistence.Repositories;


public class UnitOfWork : IUnitOfWork
{
    private readonly PersonsAppDbContext _context;
    private readonly IDbContextTransaction _transaction;
    public IPersonRepository Persons { get; }
    public IPhoneNumberRepository PhoneNumbers { get; }
    public IPersonConnectionRepository PersonConnections { get; }

    public UnitOfWork(PersonsAppDbContext context)
    {
        _context = context;
        _transaction = _context.Database.BeginTransaction();
        Persons = new PersonRepository(_context);
        PhoneNumbers = new PhoneNumberRepository(_context);
        PersonConnections = new PersonConnectionRepository(_context);
    }

    public async Task<int> SaveChangesAsync()
    {
        var changes = await _context.SaveChangesAsync();
        _transaction.Commit();
        return changes;
    }

    public void Dispose()
    {
        _transaction.Dispose();
        _context.Dispose();
    }
}
