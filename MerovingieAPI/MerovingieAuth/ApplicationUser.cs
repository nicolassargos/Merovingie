using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MerovingieAuth
{
    public class ApplicationUser : IdentityUser
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string ChatTextColor { get; set; }
        public string ChatNickName { get; set; }

        public virtual ICollection<ChatMessage> Messages { get; set; }
    }
}
