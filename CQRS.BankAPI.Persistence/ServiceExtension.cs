using CQRS.BankAPI.Application.Interfaces;
using CQRS.BankAPI.Persistence.Contexts;
using CQRS.BankAPI.Persistence.Repository;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace CQRS.BankAPI.Persistence
{
    public static class ServiceExtension
    {
        public static void AddPersistenceInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<AppBankDbContext>(options =>
            options.UseSqlServer(connectionString: configuration.GetConnectionString("DefaultConnection"),
            b => b.MigrationsAssembly(typeof(AppBankDbContext).Assembly.FullName)));

            #region Repositories

            services.AddTransient(typeof(IRepositoryAsync<>), typeof(MyRepositoryAsync<>));
            #endregion

            #region Caching

            services.AddStackExchangeRedisCache(options =>
            {
                options.Configuration = configuration.GetSection("Caching:RedisConnection").Get<string>();


            });
            #endregion

        }
    }
}
