using GymHub.Services.Data;
using GymHub.Services.Data.Interfaces;
using GymHub.Web.ViewModels.Gym;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace GymHub.Web.Controllers
{
    [Authorize]
    public class GymController : BaseController
    {
        private readonly IGymService service;

        public GymController(IGymService _service)
        {
            service= _service;
        }
        public async Task<IActionResult> Index(string searchText)
        {

            ViewData["SearchText"]=searchText;

            IEnumerable<AllGymViewModel> list;

            if(!String.IsNullOrEmpty(searchText))
            {
                list=await service.GetAllGymsBySearchAsync(searchText);
            }
            else
            {
                list = await service.GetAllGymsAsync();

            }
          

            return View(list);
        }
        public async Task<IActionResult>Details(string id)
        {
            bool isValidGuid = Guid.TryParse(id, out Guid guidId);
            if (!isValidGuid)
            {
                return BadRequest();
            }

            var model=await service.GetDetailsGymAsync(guidId);

            if(model==null)
            {
                return RedirectToAction("Index");
            }

            return View(model);
        }
        
    }
}
