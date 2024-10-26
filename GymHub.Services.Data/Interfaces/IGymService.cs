using GymHub.Web.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymHub.Services.Data.Interfaces
{
    public interface IGymService
    {
        Task<IEnumerable<AllGymViewModel>>GetAllGymsAsync();
    }
}
