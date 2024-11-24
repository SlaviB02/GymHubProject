
using GymHub.Services.Data.Interfaces;
using GymHub.Web.ViewModels.Class;
using Microsoft.AspNetCore.Mvc;
using System.Globalization;
using System.Runtime.Serialization;
using static GymHub.Common.ApplicationConstants;
using static GymHub.Common.ErrorMessages;

namespace GymHub.Web.Areas.Admin.Controllers
{
    public class ClassController : BaseAdminController
    {

        private readonly IClassService ClassService;
        private readonly IGymService GymService;

        public ClassController(IClassService _ClassService, IGymService _GymService)
        {
            ClassService = _ClassService;
            GymService = _GymService;
        }

        [HttpGet]
        public async Task<IActionResult> Add()
        {
            AddClassViewModel model = new AddClassViewModel();
            model.Gyms = await GymService.GetGymNamesAsync();

            return View(model);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Add(AddClassViewModel model)
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
                   String.Format("The Date and Time must be in the following format: {0}",DateAndTimeFormat));
                model.Gyms = await GymService.GetGymNamesAsync();
                return View(model);
            }

            return RedirectToAction("Manage");
        }
        public async Task<IActionResult> Manage()
        {
            var list = await ClassService.GetAllClassesAsync();

            return View(list);
        }
        [HttpGet]
        public async Task<IActionResult> Edit(string id)
        {
            bool isValidGuid = Guid.TryParse(id, out Guid reviewId);
            if (!isValidGuid)
            {
                return BadRequest();
            }

            var model = await ClassService.GetEditModelAsync(reviewId);

            if (model == null)
            {
                TempData["Message"] = InvalidIdMessage;
                return RedirectToAction("Manage");
            }


            return View(model);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(EditClassFormModel model)
        {
            bool isReleaseDateValid = DateTime
           .TryParseExact(model.DateAndTime, DateAndTimeFormat, CultureInfo.InvariantCulture, DateTimeStyles.None,
               out DateTime dateTime);

            if (!ModelState.IsValid || !isReleaseDateValid)
            {
                return View(model);
            }

            await ClassService.EditClassAsync(model);


            return RedirectToAction("Manage");

        }
        [HttpGet]
        public async Task<IActionResult> Delete(string id)
        {
            bool isValidGuid = Guid.TryParse(id, out Guid classId);
            if (!isValidGuid)
            {
                return BadRequest();
            }
            DeleteClassViewModel? model = await ClassService.GetDeleteModelAsync(classId);


            if (model == null)
            {
                TempData["Message"] = InvalidIdMessage;
                return RedirectToAction("Manage");
            }

            return View(model);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            bool isValidGuid = Guid.TryParse(id, out Guid classId);
            if (!isValidGuid)
            {
                return BadRequest();
            }

            bool res = await ClassService.DeleteClassAsync(classId);

            if (res == false)
            {
                TempData["Message"]=UsersAreSignedForClass;
                return RedirectToAction("Manage");
            }

            return RedirectToAction("Manage");
        }
    }
}
