using GymHub.Web.ViewModels.Gym;
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

        Task<GymDetailsViewModel> GetDetailsGymAsync(Guid id);

        Task<IEnumerable<GymNamesViewModel>> GetGymNamesAsync();
    }
}
