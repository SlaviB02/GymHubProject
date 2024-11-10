using GymHub.Services.Data;
using GymHub.Services.Data.Interfaces;
using GymHub.Web.ViewModels.Class;
using GymHub.Web.ViewModels.Trainer;
using Microsoft.AspNetCore.Mvc;

namespace GymHub.Web.Controllers
{
    public class TrainerController : BaseController
    {
        private readonly ITrainerService TrainerService;
        private readonly IGymService GymService;

        public TrainerController(ITrainerService _TrainerService, IGymService gymService)
        {
            TrainerService = _TrainerService;
            GymService = gymService;
        }
        public async Task<IActionResult> GetTrainersByGym(string id)
        {
            bool isValidGuid = Guid.TryParse(id, out Guid gymId);
            if (!isValidGuid)
            {
                return BadRequest();
            }

            var list =await TrainerService.GetTrainersForGymAsync(gymId);

            return View(list);
        }
        public async Task<IActionResult>Manage()
        {
            var list = await TrainerService.GetAllTrainersAsync();

            return View(list);
        }
        [HttpGet]
        public async Task<IActionResult>Add()
        {
           AddTrainerViewModel model = new AddTrainerViewModel();
            model.Gyms=await GymService.GetGymNamesAsync();

            return View(model);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult>Add(AddTrainerViewModel model)
        {
                if(!ModelState.IsValid)
                {
                model.Gyms = await GymService.GetGymNamesAsync();
                  return View(model);
                }

                await TrainerService.AddTrainerAsync(model);

            return RedirectToAction("Manage");
        }
        [HttpGet]
        public async Task<IActionResult>Edit(string id)
        {
            bool isValidGuid = Guid.TryParse(id, out Guid trainerId);
            if (!isValidGuid)
            {
                return BadRequest();
            }

            var model=await TrainerService.GetEditModelAsync(trainerId);

            return View(model);

        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult>Edit(EditTrainerViewModel model)
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
