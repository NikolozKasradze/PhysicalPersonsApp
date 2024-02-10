namespace PhysicalPersonsApp.Application.Contracts.Persistance;

public interface IRepository<T> where T : class
{
    Task<T> GetOneByIdAsync(int id);
    Task<IReadOnlyList<T>> ListAsync();
    Task<T> AddAsync(T entity);
    Task UpdateAsync(T entity);
    Task DeleteAsync(T entity);
}
