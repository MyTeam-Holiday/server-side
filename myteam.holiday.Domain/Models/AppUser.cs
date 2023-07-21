using Microsoft.AspNetCore.Identity;

namespace myteam.holiday.Domain.Models
{
    public class AppUser : IdentityUser
    {
        public int EmailConfirmationAttempts { get; set; }
    }
}
