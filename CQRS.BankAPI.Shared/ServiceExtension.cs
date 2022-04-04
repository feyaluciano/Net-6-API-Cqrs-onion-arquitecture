using CQRS.BankAPI.Application.Interfaces;
using CQRS.BankAPI.Shared.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace CQRS.BankAPI.Shared
{
    public static class ServiceExtension
    {
        public static void AddSharedInfrastructure(this IServiceCollection services,IConfiguration configuration) 
        {
            services.AddTransient<IDateTimeService, DateTimeService>();
        
        }
    }
}
