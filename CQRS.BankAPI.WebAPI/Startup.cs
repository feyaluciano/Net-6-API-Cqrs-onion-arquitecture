using CQRS.BankAPI.Application;
using CQRS.BankAPI.Application.Enums;
using CQRS.BankAPI.Identity;
using CQRS.BankAPI.Persistence;
using CQRS.BankAPI.Shared;
using CQRS.BankAPI.WebAPI.Extensions;

namespace CQRS.BankAPI.WebAPI
{
    public class Startup
    {
        public Startup (IConfiguration configuration)
        {
            Configuration = configuration;
        }
        public IConfiguration Configuration { get; }
        public void ConfigureServices(IServiceCollection services)
        {

            #region Services Config
            // Add services to the container.

            //Add Application Layer
            services.AddApplicationLayer();
            services.AddIdentityInfrastructure(Configuration);
            services.AddSharedInfrastructure(Configuration);
            services.AddPersistenceInfrastructure(Configuration);
            services.AddControllers();
            services.AddApiVersioningExtension();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new Microsoft.OpenApi.Models.OpenApiInfo { Title = "CQrS Bank API", Version = "v1.0" });

            });
            services.AddAuthorization(auth =>
            {
                auth.AddPolicy("Admin", p => p.RequireRole(RolesEnum.Administrator.ToString()));
                auth.AddPolicy("Basic", p => p.RequireRole(RolesEnum.Basic.ToString()));
            });
            
            #endregion


        }


        public void Configure (IApplicationBuilder app, IWebHostEnvironment env)
        {

            if (env.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI(c =>
                {
                    c.SwaggerEndpoint("/swagger/v1/swagger.json", "v1");
                    c.RoutePrefix = String.Empty;
                });
            }

            app.UseHttpsRedirection();
            app.UseRouting();
            app.UseAuthentication();
            app.UseAuthorization();
           
            app.UseErrorHandlingMiddleware();
            app.UseEndpoints(endpoints => { endpoints.MapControllers(); });
           //app.MapControllers();

            //app.Run();


        }
    }
}
