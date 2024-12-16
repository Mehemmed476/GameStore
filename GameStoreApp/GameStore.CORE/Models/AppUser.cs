using Microsoft.AspNetCore.Identity;

namespace GameStore.CORE.Models;

public class AppUser : IdentityUser
{
    public string FirstName { get; set; }
    public string LastName { get; set; }
}