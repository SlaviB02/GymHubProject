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
       
    }
}
