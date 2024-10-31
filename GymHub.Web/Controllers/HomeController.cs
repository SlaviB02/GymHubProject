
using GymHub.Web.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace GymHub.Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        public IActionResult Error(int sc)
        {
            if(sc==404)
            {
                return View("NotFound");
            }
         if(sc==400)
            {
                return View("BadRequest");
            }
            return View();
        }
    }
}
