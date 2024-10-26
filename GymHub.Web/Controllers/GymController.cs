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
    }
}
