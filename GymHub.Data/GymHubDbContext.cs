using GymHub.Data.Models;
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

        public DbSet<Trainer> Trainers { get; set; } = null!;

        public DbSet<Equipment> Equipments { get; set; } = null!;

        public DbSet<Review> Reviews { get; set; } = null!;

        public DbSet<Membership> Memberships { get; set; } = null!;
    }
}
