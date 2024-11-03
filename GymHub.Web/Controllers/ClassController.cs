using GymHub.Services.Data;
using GymHub.Services.Data.Interfaces;
using GymHub.Web.ViewModels.Class;
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
        [HttpGet]
        public async Task<IActionResult>Add()
        {
            AddClassViewModel model = new AddClassViewModel();
            model.Gyms = await GymService.GetGymNamesAsync();
            
            return View(model);
        }
        [HttpPost]
        public async Task<IActionResult>Add(AddClassViewModel model)
        {
            if (!ModelState.IsValid)
            {
                model.Gyms = await GymService.GetGymNamesAsync();
                return View(model);
            }


            bool res = await ClassService.AddClassAsync(model);

            if (res != true)
            {
                ModelState.AddModelError(nameof(model.DateAndTime),
                   String.Format("The Date and Time must be in the following format: {0}", DateTimeFormat));
                model.Gyms = await GymService.GetGymNamesAsync();
                return View(model);
            }

            return RedirectToAction("ClassesForGym", new {id=model.GymId});
        }
        public async Task<IActionResult>Manage()
        {
            var list=await ClassService.GetAllClassesAsync();

            return View(list);
        }
        [HttpGet]
        public async Task<IActionResult>Edit(string id)
        {
            bool isValidGuid = Guid.TryParse(id, out Guid reviewId);
            if (!isValidGuid)
            {
                return BadRequest();
            }

            var model = await ClassService.GetEditModelAsync(reviewId);


            return View(model);
        }
        [HttpPost]
        public async Task<IActionResult> Edit(EditClassFormModel model)
        {
            bool isReleaseDateValid = DateTime
           .TryParseExact(model.DateAndTime, DateTimeFormat, CultureInfo.InvariantCulture, DateTimeStyles.None,
               out DateTime dateTime);

            if (!ModelState.IsValid || !isReleaseDateValid)
            {
                return View(model);
            }

          await ClassService.EditClassAsync(model);


            return RedirectToAction("Manage");

        }
        public async Task<IActionResult> Delete(string id)
        {
            bool isValidGuid = Guid.TryParse(id, out Guid classId);
            if (!isValidGuid)
            {
                return BadRequest();
            }

           await ClassService.DeleteClassAsync(classId);

        

            return RedirectToAction("Manage");
        }
    }
}
