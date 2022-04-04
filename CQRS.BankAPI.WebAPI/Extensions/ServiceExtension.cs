namespace CQRS.BankAPI.WebAPI.Extensions
{
    public static class ServiceExtension
    {
        public static void AddApiVersioningExtension(this IServiceCollection services)
        {

            services.AddApiVersioning(opt =>
            {
                opt.DefaultApiVersion = new Microsoft.AspNetCore.Mvc.ApiVersion(1,0);
                opt.AssumeDefaultVersionWhenUnspecified = true;
                opt.ReportApiVersions = true;
            });
        }
    }
}
