using PhysicalPersonsApp.Domain;

namespace PhysicalPersonsApp.Application.Contracts.Persistance;

public interface IPersonConnectionRepository : IRepository<PersonConnection>
{
    Task<PersonConnection> PersonConnectionSearchAsync(int connectionTypeId, int personId, int connectedPersonId);
    Task<bool> CheckIfPersonIsConnectedAsync(int personId);
    Task<IEnumerable<PersonConnection>> ListForReportAsync();
}
