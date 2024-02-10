using Microsoft.EntityFrameworkCore;
using PhysicalPersonsApp.Application.Contracts.Persistance;

namespace PhysicalPersonsApp.Persistence.Repositories;

public class BaseRepository<T> : IRepository<T> where T : class
{

    private readonly PersonsAppDbContext _dbContext;

    public BaseRepository(PersonsAppDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<T> AddAsync(T entity)
    {
        await _dbContext.Set<T>().AddAsync(entity);
        await _dbContext.SaveChangesAsync();

        return entity;
    }

    public async Task DeleteAsync(T entity)
    {
        _dbContext.Set<T>().Remove(entity);
        await _dbContext.SaveChangesAsync();
    }

    public async Task<T> GetOneByIdAsync(int id)
    {
        return await _dbContext.Set<T>().FindAsync(id);
    }

    public async Task<IReadOnlyList<T>> ListAsync()
    {
        return await _dbContext.Set<T>().ToListAsync();
    }

    public async Task UpdateAsync(T entity)
    {
        _dbContext.Entry(entity).State = EntityState.Modified;
        await _dbContext.SaveChangesAsync();
    }
}
