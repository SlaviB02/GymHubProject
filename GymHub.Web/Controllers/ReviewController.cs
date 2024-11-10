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
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Add(AddReviewInputModel model,string Id)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            bool isValidGuid = Guid.TryParse(Id, out Guid gymId);
            if (!isValidGuid)
            {
                return BadRequest();
            }

            Guid userId=GetUserId();

            await service.AddReviewAsync(model, userId,gymId);

           
            return RedirectToAction("Index");


        }
        public async Task<IActionResult> ReviewsForGym(string Id)
        {

            bool isValidGuid = Guid.TryParse(Id, out Guid gymId);
            if (!isValidGuid)
            {
                return BadRequest();
            }

            var list=await service.GetAllReviewsForGymAsync(gymId);

            return View(list);
        }
        public async Task<IActionResult>Delete(string Id)
        {
            bool isValidGuid = Guid.TryParse(Id, out Guid reviewId);
            if (!isValidGuid)
            {
                return BadRequest();
            }

          var res=await service.DeleteReviewAsync(reviewId);

            if(res==false)
            {
                return RedirectToAction("Index");
            }

            return RedirectToAction("Manage");
        }
        public async Task<IActionResult>Manage()
        {
            var list = await service.GetAllReviewsAsync();

            return View(list);
        }
        [HttpGet]
        public async Task<IActionResult> Edit(string id)
        {
            bool isValidGuid = Guid.TryParse(id, out Guid reviewId);
            if (!isValidGuid)
            {
                return BadRequest();
            }
            var model=await service.GetEditReviewModelAsync(reviewId);

            return View(model);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult>Edit(EditReviewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            bool res = await service.UpdateReviewAsync(model);

            if (res == false)
            {
                return RedirectToAction("Manage");
            }

            return RedirectToAction("Manage");
        }
    }
}
