using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using static GymHub.Common.ApplicationConstants;

namespace GymHub.Web.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles =AdminRoleName)]
    public class BaseAdminController : Controller
    {
     
    }
}
