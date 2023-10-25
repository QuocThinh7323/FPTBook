using Microsoft.AspNetCore.Identity;

namespace FPTBookDemo.Models
{
    public class ApplicationUser : IdentityUser
    {
        
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public int UsernameChangeLimit { get; set; } = 10;
        public byte[]? ProfilePicture { get; set; } // Depending on your requirements, you might remove the nullable (?)
    }
}
