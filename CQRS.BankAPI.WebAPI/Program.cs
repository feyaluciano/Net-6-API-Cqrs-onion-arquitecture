using CQRS.BankAPI.Identity.Models;
using CQRS.BankAPI.Identity.Seeds;
using Microsoft.AspNetCore.Identity;

namespace CQRS.BankAPI.WebAPI
{
    public class Program
    {


        public async static Task Main(string[] args)
        {
            var host = CreateHostBuilder(args).Build();
            using var scope = host.Services.CreateScope();
            {
                var services = scope.ServiceProvider;
                try
                {
                    var tManager = services.GetRequiredService<RoleManager<IdentityRole>>();
                    var uManager = services.GetRequiredService<UserManager<ApplicationUser>>();
                    await DefaultRole.SeedAsync(uManager, tManager);
                    await DefaultAdminUser.SeedAsync(uManager, tManager);
                    await DefaultBasicUser.SeedAsync(uManager, tManager);
                }
                catch (Exception e)
                {

                    throw ;
                }
            }
          await host.RunAsync();
        }
        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args).ConfigureWebHostDefaults(webBuilder =>
            {
                webBuilder.UseStartup<Startup>();
            });


    }
}
//var builder = WebApplication.CreateBuilder(args);

//using var host = builder.Build();
//using var scope = host.Services.CreateScope();
//{
//    var services = scope.ServiceProvider;
//    try
//    {
//        var uManager = services.GetRequiredService<UserManager<ApplicationUser>>();
//        var tManager = services.GetRequiredService<RoleManager<IdentityRole>>();
//        await DefaultRole.SeedAsync(uManager, tManager);
//        await DefaultAdminUser.SeedAsync(uManager, tManager);
//        await DefaultBasicUser.SeedAsync(uManager, tManager);
//    }
//    catch (Exception e)
//    {

//        throw;
//    }
//}
//host.Run();

//#region Services Config
//// Add services to the container.

////Add Application Layer
//builder.Services.AddApplicationLayer();
//builder.Services.AddIdentityInfrastructure(builder.Configuration);
//builder.Services.AddSharedInfrastructure(builder.Configuration);
//builder.Services.AddPersistenceInfrastructure(builder.Configuration);
//builder.Services.AddControllers();
//builder.Services.AddApiVersioningExtension();
//builder.Services.AddSwaggerGen(c =>
//{
//    c.SwaggerDoc("v1", new Microsoft.OpenApi.Models.OpenApiInfo { Title = "CQrS Bank API", Version = "v1.0" });

//});
//#endregion

//#region Middlewares Config
//var app = builder.Build();




//if (app.Environment.IsDevelopment())
//{
//    app.UseSwagger();
//    app.UseSwaggerUI(c =>
//    {
//        c.SwaggerEndpoint("/swagger/v1/swagger.json", "v1");
//        c.RoutePrefix = String.Empty;
//    });
//}

//app.UseHttpsRedirection();


//app.UseRouting();

//app.UseAuthorization();
//app.UseErrorHandlingMiddleware();

//app.MapControllers();

//app.Run();
//#endregion