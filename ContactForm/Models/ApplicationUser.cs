using Microsoft.AspNetCore.Identity;

namespace ContactForm.Models
{
    public class ApplicationUser : IdentityUser
    {
        public string FullName { get; set; } = string.Empty;
    }
}

