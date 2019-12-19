using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IdentityServerCenter.Model
{
    public class AppUser
    {
        public string Name { get; set; }
        public string Password { get; set; }
        public string ReturnUrl { get; set; }
    }
}
