﻿using GymHub.Services.Data.Interfaces;
using GymHub.Web.ViewModels.Membership;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

using static GymHub.Common.ApplicationConstants;

namespace GymHub.Web.Controllers
{
    [Authorize]
    public class MembershipController : Controller
    {
        private readonly IMembershipService service;

        public MembershipController(IMembershipService _service)
        {
            service= _service;
        }
        public async Task<IActionResult> Index()
        {
            string userId = GetUserId();
            var memberships = await service.GetAllMembershipsAsync(userId);
           
            return View(memberships);
        }

        [HttpGet]
        public IActionResult Add()
        {
            AddMembershipInputModel model = new AddMembershipInputModel();
            model.Types = service.GetTypesNames().Where(m=>m!="Unknown");
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult>Add(AddMembershipInputModel model)
        {
            if(!ModelState.IsValid)
            {
                model.Types = service.GetTypesNames().Where(m => m != "Unknown");
                return View(model);
            }

            string userId = GetUserId();

            bool res = await service.AddMembershipAsync(model,userId);

            if(res!=true)
            {
                ModelState.AddModelError(nameof(model.StartDate),
                   String.Format("The Release Date must be in the following format: {0}", DateOnlyFormat));
                model.Types = service.GetTypesNames().Where(m => m != "Unknown");
                return View(model);
            }

            return RedirectToAction("Index");
            
        }
        private string GetUserId()
        {
            string userId = string.Empty;

            if (User != null)
            {
                userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            }
            return userId;
        }

        
    }
}
