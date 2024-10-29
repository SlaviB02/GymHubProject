using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace GymHub.Web.Controllers
{
    public class BaseController : Controller
    {
        protected Guid GetUserId()
        {
            Guid userId = Guid.Empty;

            if (User != null)
            {
                userId = Guid.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)!);
            }
            return userId;
        }
    }
}
