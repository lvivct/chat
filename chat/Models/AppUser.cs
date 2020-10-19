using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;

namespace chat.Models
{
    public class AppUser : IdentityUser
    {
        public ICollection<AppUserChat> AppUsersChats { get; set; }
        public string Photopath { get; set; }
    }
}
