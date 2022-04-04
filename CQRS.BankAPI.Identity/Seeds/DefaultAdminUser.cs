using CQRS.BankAPI.Application.Enums;
using CQRS.BankAPI.Identity.Models;
using Microsoft.AspNetCore.Identity;

namespace CQRS.BankAPI.Identity.Seeds
{
    public static class DefaultAdminUser
    {
        public static async Task SeedAsync(UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roles)
        {
            //Seed User Admin
            var defaultAdmin = new ApplicationUser()
            {
                //Id= Guid.NewGuid().ToString(),
                UserName = "mLeonelqAdmin",
                Email = "m_leonelq@admin.cqrsBank.com",
                FirstName = "Matias ",
                LastName = "Quiroga",
                EmailConfirmed = true,
                PhoneNumberConfirmed = true,
            };

            if (userManager.Users.All(u => u.Id != defaultAdmin.Id))
            { 
               var user = await userManager.FindByEmailAsync(defaultAdmin.Email);
                if (user == null)
                {
                    var res = await userManager.CreateAsync(defaultAdmin, "#userAdmin1");
                    var res1 = await userManager.AddToRoleAsync(defaultAdmin, RolesEnum.Administrator.ToString());
                    res1 = await userManager.AddToRoleAsync(defaultAdmin, RolesEnum.Basic.ToString());
                }
                

            }

        }
    }
}
