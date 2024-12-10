using Microsoft.AspNetCore.Identity;

namespace Task.Domain.Entities
{
    public class VodafoneUser : IdentityUser
    {
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
    }
}
