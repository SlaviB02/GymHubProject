using GymHub.Services.Data.Interfaces;
using GymHub.Web.ViewModels.Membership;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

using static GymHub.Common.ApplicationConstants;

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
        public async Task<IActionResult>Add(AddMembershipInputModel model)
        {
            if(!ModelState.IsValid)
            {
                model.Types = service.GetTypesNames();
                model.Gyms = await gymService.GetGymNamesAsync();
                return View(model);
            }

            Guid userId = GetUserId();

            bool res = await service.AddMembershipAsync(model,userId);

            if(res!=true)
            {
                ModelState.AddModelError(nameof(model.StartDate),
                   String.Format("The Date must be in the following format: {0}", DateOnlyFormat));
                model.Types = service.GetTypesNames();
                model.Gyms = await gymService.GetGymNamesAsync();
                return View(model);
            }

            return RedirectToAction("Index");
            
        }
        public async Task<IActionResult>Cancel(Guid id)
        {
           await service.CancelMembershipAsync(id);

            return RedirectToAction("Index");
        }
       

        
    }
}
