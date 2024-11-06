﻿using GymHub.Services.Data.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace GymHub.Web.Controllers
{
    public class TrainerController : BaseController
    {
        private readonly ITrainerService service;

        public TrainerController(ITrainerService _service)
        {
            service = _service;
        }
        public async Task<IActionResult> GetTrainersByGym(string id)
        {
            bool isValidGuid = Guid.TryParse(id, out Guid gymId);
            if (!isValidGuid)
            {
                return BadRequest();
            }

            var list =await service.GetTrainersForGym(gymId);

            return View(list);
        }
    }
}
