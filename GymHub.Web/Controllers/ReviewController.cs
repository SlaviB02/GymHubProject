using GymHub.Services.Data;
using GymHub.Services.Data.Interfaces;
using GymHub.Web.ViewModels.Class;
using GymHub.Web.ViewModels.Review;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using static GymHub.Common.ApplicationConstants;

namespace GymHub.Web.Controllers
{
    [Authorize(Roles = AdminRoleName + "," + UserRoleName)]
    public class ReviewController : BaseController
    {

        private readonly IReviewService service;

        public ReviewController(IReviewService _service)
        {
            service= _service;
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

           
            return RedirectToAction("ReviewsForGym",new {id=gymId});


        }
        [AllowAnonymous]
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
       
    }
}
