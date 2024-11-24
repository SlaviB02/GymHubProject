using GymHub.Services.Data.Interfaces;
using GymHub.Web.ViewModels.Gym;
using Microsoft.AspNetCore.Mvc;
using static GymHub.Common.ErrorMessages;

namespace GymHub.Web.Areas.Admin.Controllers
{
    public class GymController :BaseAdminController
    {

        private readonly IGymService service;

        public GymController(IGymService _service)
        {
            service = _service;
        }
        public async Task<IActionResult> Manage()
        {
            var list = await service.GetAllGymsAsync();
            return View(list);
        }
        [HttpGet]
        public IActionResult Add()
        {
            AddGymFormModel model = new AddGymFormModel();

            return View(model);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Add(AddGymFormModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            await service.AddGymAsync(model);

            return RedirectToAction("Manage");
        }
        [HttpGet]
        public async Task<IActionResult> Edit(string id)
        {
            bool isValidGuid = Guid.TryParse(id, out Guid gymId);
            if (!isValidGuid)
            {
                return BadRequest();
            }

            var model = await service.GetEditModelAsync(gymId);

            if (model == null)
            {
                TempData["Message"] = InvalidIdMessage;
                return RedirectToAction("Manage");
            }

            return View(model);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(EditGymFormModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            bool res = await service.UpdateGymAsync(model);

            if (res == false)
            {
                return RedirectToAction("Manage");
            }

            return RedirectToAction("Details", new {area="" ,id = model.Id });
        }
        [HttpGet]
        public async Task<IActionResult> Delete(string id)
        {
            bool isValidGuid = Guid.TryParse(id, out Guid gymId);
            if (!isValidGuid)
            {
                return BadRequest();
            }
            DeleteGymModel? model = await service.GetDeleteModelAsync(gymId);


            if (model == null)
            {
                TempData["Message"]=InvalidIdMessage;
                return RedirectToAction("Manage");
            }

            return View(model);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            bool isValidGuid = Guid.TryParse(id, out Guid gymId);
            if (!isValidGuid)
            {
                return BadRequest();
            }

            bool res = await service.DeleteGymAsync(gymId);

            if (res == false)
            {
                TempData["Message"] = GymHasClassesOrMemberships;
                return RedirectToAction("Manage");
            }

            return RedirectToAction("Manage");
        }

    }
}
