using Microsoft.AspNetCore.Identity;

namespace Domain.Entities
{
    public class VodafoneUser : IdentityUser
    {
        public string? FirstName { get; set; }
        public string? LastName { get; set; }

        public ICollection<Tasks> Tasks { get; set; }
    }
}
