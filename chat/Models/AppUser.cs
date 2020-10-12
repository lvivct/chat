using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace chat.Models
{
    public class AppUser : IdentityUser
    {
        public ICollection<AppUserChat> AppUsersChats { get; set; }
    }
}
