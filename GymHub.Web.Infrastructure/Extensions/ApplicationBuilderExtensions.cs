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
            CreateUsers(userManager);

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
        private static void CreateUsers(UserManager<ApplicationUser> userManager)
        {

            var users = new[]
    {
        new
        {
            Id =Guid.Parse("69178581-3d61-4a35-b5c3-2403663b7734"),
            Email = "user1@gymhub.com",
            UserName = "user1@gymhub.com",
            Password = "user123",
            FirstName = "Jack",
            LastName = "Red",
        },
        new
        {
            Id = Guid.Parse("bb75e197-5f40-4287-8746-09cd2112c4ff"),
            Email = "user2@gymhub.com",
            UserName = "user2@gymhub.com",
            Password = "user123",
            FirstName = "John",
            LastName = "Doe",
        },
        new
        {
            Id = Guid.Parse("1d773456-8353-4d6c-9b36-414fe221d8a0"),
            Email = "user3@gymhub.com",
            UserName = "user3@gymhub.com",
            Password = "user123",
            FirstName = "Jane",
            LastName = "Smith",
        },
        new
        {
            Id = Guid.Parse("5b3af4e7-4b0a-4fd7-86f1-435b036f6f01"),
            Email = "user4@gymhub.com",
            UserName = "user4@gymhub.com",
            Password = "user123",
            FirstName = "Alice",
            LastName = "Brown",
        },
        new
        {
            Id = Guid.Parse("cd421621-1c3c-4cd4-a14b-1fe2f9f46ade"),
            Email = "user5@gymhub.com",
            UserName = "user5@gymhub.com",
            Password = "user123",
            FirstName = "Bob",
            LastName = "Johnson",
        },
    };

            foreach (var userInfo in users)
            {

                var userExists = userManager.FindByEmailAsync(userInfo.Email).GetAwaiter().GetResult();

                if (userExists == null)
                {
                    var user = new ApplicationUser
                    {
                        Id = userInfo.Id,
                        UserName = userInfo.UserName,
                        Email = userInfo.Email,
                        FirstName = userInfo.FirstName,
                        LastName = userInfo.LastName
                    };

                    var createUserResult = userManager.CreateAsync(user, userInfo.Password).GetAwaiter().GetResult();
                    if (!createUserResult.Succeeded)
                    {
                        throw new Exception($"Failed to create admin user: {user.Email}");
                    }
                }
            }

        }


    }
}
