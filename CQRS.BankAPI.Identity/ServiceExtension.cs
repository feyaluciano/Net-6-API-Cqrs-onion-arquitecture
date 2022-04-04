using CQRS.BankAPI.Application.Interfaces;
using CQRS.BankAPI.Application.Wrappers;
using CQRS.BankAPI.Domain.Settings;
using CQRS.BankAPI.Identity.Context;
using CQRS.BankAPI.Identity.Models;
using CQRS.BankAPI.Identity.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using System.Text.Json;

namespace CQRS.BankAPI.Identity
{
    public static class ServiceExtension
    {
        public static void AddIdentityInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {

            services.AddDbContext<IdentityContext>(options =>
            {

                options.UseSqlServer(
                    configuration.GetConnectionString("IdentityConnection"),
                    op => op.MigrationsAssembly(typeof(IdentityContext).Assembly.FullName));
            });

            services.AddIdentity<ApplicationUser, IdentityRole>().AddEntityFrameworkStores<IdentityContext>()
                .AddDefaultTokenProviders();

            services.AddTransient<IAccountService, AccountService>();
            services.Configure<JwtSetting>(configuration.GetSection("JwtSettings"));
            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;

             }).AddJwtBearer(jwt => {
                 jwt.RequireHttpsMetadata = false;
                 jwt.SaveToken = false;
                 jwt.TokenValidationParameters = new()
                 {
                     ValidateIssuerSigningKey = true,
                     ValidateIssuer = true,
                     ValidateAudience = false,
                     ValidateLifetime = true,
                     ClockSkew= TimeSpan.Zero,
                     ValidIssuer = configuration["JwtSettings:Issuer"],
                     ValidAudience = configuration["JwtSettings:Audience"],
                     IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["JwtSettings:Key"])),

                 };
                 jwt.Events = new JwtBearerEvents()
                 {
                     OnAuthenticationFailed = c =>
                     {
                         c.NoResult();
                         c.Response.StatusCode = 500;
                         c.Response.ContentType = "text/plain";
                         return c.Response.WriteAsync(c.Exception.ToString());

                     },
                     OnChallenge = context =>
                     {
                         context.HandleResponse();
                         context.Response.StatusCode = 401;
                         context.Response.ContentType = "application/json";
                         var res = JsonSerializer.Serialize(new Response<string>("Access denied. You are not authorized to access the resource"));
                         return context.Response.WriteAsync(res);
                      },
                     OnForbidden = context =>
                     {
                         context.Response.StatusCode = 403;
                         context.Response.ContentType = "application/json";
                         var res = JsonSerializer.Serialize(new Response<string>("You do not have permissions to access the resource"));
                         return context.Response.WriteAsync(res);

                     }


                 };

            });

           
        }
    }
}
