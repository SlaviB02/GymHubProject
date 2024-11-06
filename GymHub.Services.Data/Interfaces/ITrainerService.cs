using GymHub.Web.ViewModels.Trainer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymHub.Services.Data.Interfaces
{
    public interface ITrainerService
    {
        Task<IEnumerable<GymTrainerViewModel>> GetTrainersForGymAsync(Guid gymId);

        Task<IEnumerable<GymTrainerViewModel>> GetAllTrainersAsync();
    }
}
