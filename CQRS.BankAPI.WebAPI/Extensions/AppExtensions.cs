using CQRS.BankAPI.WebAPI.Middlewares;

namespace CQRS.BankAPI.WebAPI.Extensions
{
    public static class AppExtensions
    {
        public static void UseErrorHandlingMiddleware(this IApplicationBuilder app) 
        {
            app.UseMiddleware<ErrorHandlerMiddleware>();
        }
    }
}
