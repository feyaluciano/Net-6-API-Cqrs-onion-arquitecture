using CQRS.BankAPI.Application.Enums;
using CQRS.BankAPI.Identity.Models;
using Microsoft.AspNetCore.Identity;

namespace CQRS.BankAPI.Identity.Seeds
{
    public static class DefaultBasicUser
    {
        public static async Task SeedAsync(UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roles)
        {
            //Seed User Admin
            var defaultAdmin = new ApplicationUser()
            {
           //     Id = Guid.NewGuid().ToString(),
                UserName = "JIfranBasic",
                Email = "j_ifran@basic.cqrsBank.com",
                FirstName = "Julian ",
                LastName = "Ifran",
                EmailConfirmed = true,
                PhoneNumberConfirmed = true,
            };

            if (userManager.Users.All(u => u.Id != defaultAdmin.Id))
            {
                var user = await userManager.FindByEmailAsync(defaultAdmin.Email);
                if (user == null)
                {
                    await userManager.CreateAsync(defaultAdmin, "#userBasic1");
                    await userManager.AddToRoleAsync(defaultAdmin, RolesEnum.Basic.ToString());
                }
            }

        }
    }
}
