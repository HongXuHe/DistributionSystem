using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IdentityServerCenter.Model
{
    public class ServerContext:IdentityDbContext
    {
        public ServerContext(DbContextOptions<ServerContext> options)
            :base(options)
            
        {

        }
    }
}
