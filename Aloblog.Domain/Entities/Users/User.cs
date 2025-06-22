using Microsoft.AspNetCore.Identity;

namespace Aloblog.Domain.Entities.Users;

public class User : IdentityUser<int>
{
    public string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
}