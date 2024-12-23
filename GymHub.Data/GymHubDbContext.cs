﻿using GymHub.Data.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace GymHub.Data
{
    public class GymHubDbContext : IdentityDbContext<ApplicationUser,IdentityRole<Guid>,Guid>
    {
        public GymHubDbContext(DbContextOptions<GymHubDbContext> options)
            : base(options)
        {
            
        }

        public DbSet<Trainer> Trainers { get; set; } = null!;


        public DbSet<Review> Reviews { get; set; } = null!;

        public DbSet<Membership> Memberships { get; set; } = null!;


        public DbSet<Class> Classes { get; set; } = null!;

        public DbSet<ClassUser>ClassesUsers { get; set; } = null!;

        public DbSet<Gym> Gyms {  get; set; } = null!;

        protected override void OnModelCreating(ModelBuilder builder)
        {
            

            base.OnModelCreating(builder);

            builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        }
    }
}
