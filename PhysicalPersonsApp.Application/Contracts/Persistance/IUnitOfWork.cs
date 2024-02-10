namespace PhysicalPersonsApp.Application.Contracts.Persistance;

public interface IUnitOfWork : IDisposable
{
    IPersonConnectionRepository PersonConnections { get; }
    IPersonRepository Persons { get; }

    IPhoneNumberRepository PhoneNumbers { get; }

    Task<int> SaveChangesAsync();
}
