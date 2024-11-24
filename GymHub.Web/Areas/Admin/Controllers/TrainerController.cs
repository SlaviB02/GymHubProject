using GymHub.Services.Data.Interfaces;
using GymHub.Web.ViewModels.Trainer;
using Microsoft.AspNetCore.Mvc;
using static GymHub.Common.ErrorMessages;

namespace GymHub.Web.Areas.Admin.Controllers
{
    public class TrainerController : BaseAdminController
    {
        private readonly ITrainerService TrainerService;
        private readonly IGymService GymService;

        public TrainerController(ITrainerService _TrainerService, IGymService gymService)
        {
            TrainerService = _TrainerService;
            GymService = gymService;
        }
        public async Task<IActionResult> Manage()
        {
            var list = await TrainerService.GetAllTrainersAsync();

            return View(list);
        }
        [HttpGet]
        public async Task<IActionResult> Add()
        {
            AddTrainerViewModel model = new AddTrainerViewModel();
            model.Gyms = await GymService.GetGymNamesAsync();

            return View(model);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Add(AddTrainerViewModel model)
        {
            if (!ModelState.IsValid)
            {
                model.Gyms = await GymService.GetGymNamesAsync();
                return View(model);
            }

            await TrainerService.AddTrainerAsync(model);

            return RedirectToAction("Manage");
        }
        [HttpGet]
        public async Task<IActionResult> Edit(string id)
        {
            bool isValidGuid = Guid.TryParse(id, out Guid trainerId);
            if (!isValidGuid)
            {
                return BadRequest();
            }

            var model = await TrainerService.GetEditModelAsync(trainerId);

            if (model == null)
            {
                TempData["Message"] = InvalidIdMessage;
                return RedirectToAction("Manage");
            }

            return View(model);

        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(EditTrainerViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            await TrainerService.EditTrainerAsync(model);

            return RedirectToAction("Manage");
        }

        [HttpGet]
        public async Task<IActionResult> Delete(string id)
        {
            bool isValidGuid = Guid.TryParse(id, out Guid trainerId);
            if (!isValidGuid)
            {
                return BadRequest();
            }
            DeleteTrainerViewModel? model = await TrainerService.GetDeleteModelAsync(trainerId);


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
            bool isValidGuid = Guid.TryParse(id, out Guid trainerId);
            if (!isValidGuid)
            {
                return BadRequest();
            }

            bool res = await TrainerService.DeleteTrainerAsync(trainerId);

            if (res == false)
            {
                return RedirectToAction("Manage");
            }

            return RedirectToAction("Manage");
        }
    }
}
