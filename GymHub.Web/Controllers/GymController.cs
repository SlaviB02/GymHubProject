using GymHub.Services.Data;
using GymHub.Services.Data.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace GymHub.Web.Controllers
{
    public class GymController : Controller
    {
        private readonly IGymService service;

        public GymController(IGymService _service)
        {
            service= _service;
        }
        public async Task<IActionResult> Index()
        {
           var list= await service.GetAllGymsAsync();

            return View(list);
        }
        public async Task<IActionResult>Details(string id)
        {
            bool isValidGuid = Guid.TryParse(id, out Guid guidId);
            if (!isValidGuid)
            {
                return RedirectToAction("Index");
            }

            var model=await service.GetDetailsGymAsync(guidId);

            return View(model);
        }
    }
}
