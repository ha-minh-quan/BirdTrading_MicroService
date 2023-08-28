using Microsoft.AspNetCore.Identity;

namespace BirdTrading.Services.AuthAPI.Models
{
    public class ApplicationUser : IdentityUser
    {
        public string Name { get; set; }
    }
}
