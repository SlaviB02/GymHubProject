using GymHub.Data.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace GymHub.Web.Controllers
{
    public class AdminController : BaseController
    {
        private readonly RoleManager<IdentityRole<Guid>> roleManager;

        private readonly UserManager<ApplicationUser> userManager;

        public AdminController(RoleManager<IdentityRole<Guid>> _roleManager, UserManager<ApplicationUser> _userManager)
        {
            roleManager = _roleManager;
            userManager = _userManager;
        }
        public async Task<IActionResult> MakeMeAdmin()
        {
            string roleName = "Admin";

            IdentityResult? result = null;

            if(await roleManager.RoleExistsAsync(roleName)==false)
            {
                result=await roleManager.CreateAsync(new IdentityRole<Guid>(roleName));
            }

            if(User.IsInRole(roleName)==false && (result==null||result.Succeeded))
            {
                var user=await userManager.FindByNameAsync(User.Identity.Name);

                if(user!=null)
                {
                    await userManager.AddToRoleAsync(user,roleName);
                }
            }

            return RedirectToAction("Index", "Home");
        }
    }
}
