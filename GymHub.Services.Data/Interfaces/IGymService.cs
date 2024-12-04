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

        Task<IEnumerable<AllGymViewModel>> GetAllGymsBySearchAsync(string text);

        Task<GymDetailsViewModel?> GetDetailsGymAsync(Guid id);

        Task<IEnumerable<GymNamesViewModel>> GetGymNamesAsync();

        Task AddGymAsync(AddGymFormModel model);

        Task<EditGymFormModel?> GetEditModelAsync(Guid gymId);

        Task<bool> UpdateGymAsync(EditGymFormModel model);

        Task<bool> DeleteGymAsync(Guid gymId);

        Task<DeleteGymModel?> GetDeleteModelAsync(Guid gymId);


    }
}
