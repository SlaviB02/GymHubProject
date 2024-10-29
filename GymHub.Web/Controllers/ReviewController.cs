using GymHub.Services.Data.Interfaces;
using GymHub.Web.ViewModels.Review;
using Microsoft.AspNetCore.Mvc;

namespace GymHub.Web.Controllers
{
    public class ReviewController : BaseController
    {

        private readonly IReviewService service;

        public ReviewController(IReviewService _service)
        {
            service= _service;
        }
        public async Task<IActionResult> Index()
        {
            var list=await service.GetAllReviewsAsync();

            return View(list);
        }
        [HttpGet]
        public IActionResult Add()
        {
            var model = new AddReviewInputModel();

            return View(model);
        }
        [HttpPost]
        public async Task<IActionResult> Add(AddReviewInputModel model,string Id)
        {
            if(!ModelState.IsValid)
            {
                return View(model);
            }
            Guid gymId = Guid.Parse(Id);

            Guid userId=GetUserId();

            await service.AddReviewAsync(model, userId,gymId);

           
            return RedirectToAction("Index");


        }
        public async Task<IActionResult> ReviewsForGym(string Id)
        {
            Guid gymId=Guid.Parse(Id);

            var list=await service.GetAllReviesForGymAsync(gymId);

            return View("Index",list);
        }
    }
}
