using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymHub.Data
{
    public class GymHubDbContext : IdentityDbContext
    {
        public GymHubDbContext(DbContextOptions<GymHubDbContext> options)
            : base(options)
        {
        }
    }
}
