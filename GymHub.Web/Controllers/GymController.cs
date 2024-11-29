using GymHub.Services.Data.Interfaces;
using GymHub.Web.Models;
using GymHub.Web.ViewModels.Gym;
using Microsoft.AspNetCore.Mvc;

namespace GymHub.Web.Controllers
{
  
    public class GymController : BaseController
    {
        private readonly IGymService service;

        public GymController(IGymService _service)
        {
            service= _service;
        }
        public async Task<IActionResult> Index(string searchText,int?pageNumber)
        {

            

            IEnumerable<AllGymViewModel> list;

            if(!String.IsNullOrEmpty(searchText))
            {
                list=await service.GetAllGymsBySearchAsync(searchText);
                pageNumber = 1;
            }
            else
            {
                list = await service.GetAllGymsAsync();
            }

            ViewData["SearchText"] = searchText;

            int pageSize = 4;
            int page = (pageNumber ?? 1);
            var res = PaginatedList<AllGymViewModel>.Create(list, page, pageSize);
          

            return View(res);
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
