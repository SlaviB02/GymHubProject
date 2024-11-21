using GymHub.Services.Data;
using GymHub.Services.Data.Interfaces;
using GymHub.Web.ViewModels.Class;
using GymHub.Web.ViewModels.Gym;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Globalization;
using static GymHub.Common.ApplicationConstants;

namespace GymHub.Web.Controllers
{
    public class ClassController : BaseController
    {
        private readonly IClassService ClassService;
        private readonly IGymService GymService;

        public ClassController(IClassService _ClassService, IGymService _GymService)
        {
            ClassService = _ClassService;
            GymService = _GymService;
        }
        public async Task<IActionResult> ClassesForGym(string id)
        {

            bool isValidGuid = Guid.TryParse(id, out Guid gymId);
            if (!isValidGuid)
            {
                return BadRequest();
            }

            var list=await ClassService.GetAllClassesForGymAsync(gymId);

            return View(list);
        }
       
        [Authorize]
        public async Task<IActionResult>MyClasses()
        {
            Guid userId= GetUserId();

            var list= await ClassService.GetClassesForUserAsync(userId);

            return View(list);
        }
        [Authorize]
        public async Task<IActionResult>SignUp(string id)
        {
           
            bool isValidGuid = Guid.TryParse(id, out Guid classId);
            if (!isValidGuid)
            {
                return BadRequest();
            }
            Guid userId = GetUserId();

            var res=await ClassService.SignUserForClassAsync(userId, classId);  

            if(res==false)
            {
                return RedirectToAction("Index", "Gym");
            }

            return RedirectToAction("MyClasses", new { id = userId });
        }
        [Authorize]
        public async Task<IActionResult>Cancel(string id)
        {
            bool isValidGuid = Guid.TryParse(id, out Guid classId);
            if (!isValidGuid)
            {
                return BadRequest();
            }
            Guid userId = GetUserId();

            var res = await ClassService.UnsignUserFromClassAsync(userId, classId);

            if (res == false)
            {
                return RedirectToAction("MyClasses", new { id = userId });
            }

            return RedirectToAction("MyClasses", new { id = userId });
      
        }
    }
}
