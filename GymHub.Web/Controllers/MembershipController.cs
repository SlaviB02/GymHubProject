using GymHub.Services.Data.Interfaces;
using GymHub.Web.ViewModels.Gym;
using GymHub.Web.ViewModels.Membership;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Globalization;
using System.Security.Claims;

using static GymHub.Common.ApplicationConstants;
using static GymHub.Common.ErrorMessages;

namespace GymHub.Web.Controllers
{
    [Authorize]
    public class MembershipController : BaseController
    {
        private readonly IMembershipService service;
        private readonly IGymService gymService;

        public MembershipController(IMembershipService _service, IGymService _gymService)
        {
            service = _service;
            gymService = _gymService;
        }
        public async Task<IActionResult> Index()
        {
            Guid userId = GetUserId();
            var memberships = await service.GetAllMembershipsAsync(userId);
           
            return View(memberships);
        }

        [HttpGet]
        public async Task<IActionResult> Add()
        {
            AddMembershipInputModel model = new AddMembershipInputModel();
            model.Types = service.GetTypesNames();
            model.Gyms = await gymService.GetGymNamesAsync();
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult>Add(AddMembershipInputModel model)
        {
            IEnumerable<string> types = service.GetTypesNames();

            bool isValidDate = DateTime
            .TryParseExact(model.StartDate, DateOnlyFormat, CultureInfo.InvariantCulture, DateTimeStyles.None,
                out DateTime date);

            if (!ModelState.IsValid)
            {
                model.Types = types;
                model.Gyms = await gymService.GetGymNamesAsync();
                return View(model);
            }

            if (!isValidDate)
            {
                ModelState.AddModelError(nameof(model.StartDate),
                  String.Format("The Date must be in the following format: {0}", DateOnlyFormat));
                model.Types = types;
                model.Gyms = await gymService.GetGymNamesAsync();
                return View(model);
            }

            Guid userId = GetUserId();

            bool res = await service.AddMembershipAsync(model,userId);


            if(res!=true)
            {
                TempData["Message"] = AlreadyHaveMembershipForGym;
                return RedirectToAction("Index");
            }

            return RedirectToAction("Index");
            
        }
        public async Task<IActionResult>Cancel(string id)
        {
            bool isValidGuid = Guid.TryParse(id, out Guid membershipId);
            if (!isValidGuid)
            {
                return BadRequest();
            }
            var res= await service.CancelMembershipAsync(membershipId);

            if (res!=true)
            {
                TempData["Message"] = DontHaveMembership;
                return RedirectToAction("Index");
            }

            return RedirectToAction("Index");
        }
       

        
    }
}
