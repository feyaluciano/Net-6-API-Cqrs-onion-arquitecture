#nullable disable
using Microsoft.AspNetCore.Identity;

namespace CQRS.BankAPI.Identity.Models
{
    public class ApplicationUser : IdentityUser
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
    }
}
