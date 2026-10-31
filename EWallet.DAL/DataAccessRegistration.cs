using EWallet.DAL.Data;
using EWallet.DAL.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace EWallet.DAL;

public static class DataAccessRegistration
{
    public static IServiceCollection AddDataAccess(this IServiceCollection services, string connectionString)
    {
        services.AddDbContext<AppDbContext>(options => options.UseNpgsql(connectionString));

        services.AddScoped<IUserRepository, UserRepository>();
        services.AddScoped<ITransactionRepository, TransactionRepository>();
        services.AddScoped<IVendorRepository, VendorRepository>();
        services.AddScoped<ISubscriberRepository, SubscriberRepository>();
        services.AddScoped<IUnitOfWork, UnitOfWork>();

        return services;
    }
}
