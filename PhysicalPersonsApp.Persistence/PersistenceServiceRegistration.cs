using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using PhysicalPersonsApp.Application.Contracts.Persistance;
using PhysicalPersonsApp.Persistence.Repositories;

namespace PhysicalPersonsApp.Persistence;

public static class PersistenceServiceRegistration
{
    public static IServiceCollection AddPersistenceServices(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<PersonsAppDbContext>(x => x.UseSqlServer(configuration.GetConnectionString("PhysicalPersonsAppDb")));

        services.AddScoped(typeof(IRepository<>), typeof(BaseRepository<>));
        services.AddScoped<IPersonRepository, PersonRepository>();
        services.AddScoped<IPersonConnectionRepository, PersonConnectionRepository>();
        services.AddScoped<IPhoneNumberRepository, PhoneNumberRepository>();
        services.AddScoped<IUnitOfWork, UnitOfWork>();

        return services;
    }
}
