using CQRS.BankAPI.Application.Enums;
using CQRS.BankAPI.Identity.Models;
using Microsoft.AspNetCore.Identity;

namespace CQRS.BankAPI.Identity.Seeds
{
    public static class DefaultRole
    {
        public static async Task SeedAsync(UserManager<ApplicationUser> userManager,RoleManager<IdentityRole>roles)
        {
            //Seed Roles
            await roles.CreateAsync(new IdentityRole(RolesEnum.Administrator.ToString()));
            await roles.CreateAsync(new IdentityRole(RolesEnum.Basic.ToString()));


        }
    }
}
