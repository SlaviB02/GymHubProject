using GymHub.Data.Models;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using static GymHub.Common.ApplicationConstants;

namespace GymHub.Web.Infrastructure.Extensions
{
    public static class ApplicationBuilderExtensions
    {
        public static void SeedAdmin(this IApplicationBuilder app)
        {
            using IServiceScope serviceScope = app.ApplicationServices.CreateAsyncScope();
            IServiceProvider serviceProvider = serviceScope.ServiceProvider;


            var roleManager = serviceProvider.GetRequiredService<RoleManager<IdentityRole<Guid>>>();

            var userManager = serviceProvider.GetRequiredService<UserManager<ApplicationUser>>();

           var roleName=AdminRoleName;

           
                var roleExists = roleManager.RoleExistsAsync(roleName).GetAwaiter().GetResult();
                if (!roleExists)
                {
                    var result = roleManager.CreateAsync(new IdentityRole<Guid> { Name = roleName }).GetAwaiter().GetResult();
                    if (!result.Succeeded)
                    {
                        throw new Exception($"Failed to create role: {roleName}");
                    }
                }
            

            CreateAdminUser(userManager);

        }

        private static void CreateAdminUser(UserManager<ApplicationUser> userManager)
        {
           

            string adminEmail = "admin@email.bg";
            string adminPassword = "admin123";
            string firstName = "Admin";
            string lastName = "Admin";

            var adminUser = userManager.FindByEmailAsync(adminEmail).GetAwaiter().GetResult();
            if (adminUser == null)
            {
                adminUser = new ApplicationUser
                {
                    UserName = adminEmail,
                    Email = adminEmail,
                    FirstName = firstName,
                    LastName = lastName,
                };
                var createUserResult = userManager.CreateAsync(adminUser, adminPassword).GetAwaiter().GetResult();
                if (!createUserResult.Succeeded)
                {
                    throw new Exception($"Failed to create admin user: {adminEmail}");
                }
            }

            var isInRole = userManager.IsInRoleAsync(adminUser, "Admin").GetAwaiter().GetResult();
            if (!isInRole)
            {
                var addRoleResult = userManager.AddToRoleAsync(adminUser, "Admin").GetAwaiter().GetResult();
                if (!addRoleResult.Succeeded)
                {
                    throw new Exception($"Failed to assign admin role to user: {adminEmail}");
                }
            }
          

        }

    }
}
