using GymHub.Services.Data.Interfaces;
using GymHub.Web.Models;
using GymHub.Web.ViewModels.Class;
using GymHub.Web.ViewModels.Review;
using Microsoft.AspNetCore.Mvc;
using static GymHub.Common.ErrorMessages;

namespace GymHub.Web.Areas.Admin.Controllers
{
    public class ReviewController : BaseAdminController
    {


        private readonly IReviewService service;

        public ReviewController(IReviewService _service)
        {
            service = _service;
        }
        [HttpGet]
        public async Task<IActionResult> Delete(string id)
        {
            bool isValidGuid = Guid.TryParse(id, out Guid reviewId);
            if (!isValidGuid)
            {
                return BadRequest();
            }
            DeleteReveiwViewModel? model = await service.GetDeleteModelAsync(reviewId);


            if (model == null)
            {
                TempData["Message"] = InvalidIdMessage;
                return RedirectToAction("Manage");
            }

            return View(model);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            bool isValidGuid = Guid.TryParse(id, out Guid reviewId);
            if (!isValidGuid)
            {
                return BadRequest();
            }

            bool res = await service.DeleteReviewAsync(reviewId);

            if (res == false)
            {
                return RedirectToAction("Manage");
            }

            return RedirectToAction("Manage");
        }
        public async Task<IActionResult> Manage(int? pageNumber)
        {
            var list = await service.GetAllReviewsAsync();

            int pageSize = 4;
            int page = (pageNumber ?? 1);
            var res = PaginatedList<AllReviewViewModel>.Create(list, page, pageSize);

            return View(res);

        }
        [HttpGet]
        public async Task<IActionResult> Edit(string id)
        {
            bool isValidGuid = Guid.TryParse(id, out Guid reviewId);
            if (!isValidGuid)
            {
                return BadRequest();
            }
            var model = await service.GetEditReviewModelAsync(reviewId);

            if (model == null)
            {
                TempData["Message"] = InvalidIdMessage;
                return RedirectToAction("Manage");
            }

            return View(model);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(EditReviewModel model)
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
