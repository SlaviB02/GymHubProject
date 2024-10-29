using GymHub.Web.ViewModels.Review;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymHub.Services.Data.Interfaces
{
    public interface IReviewService
    {
        Task<bool> AddReviewAsync(AddReviewInputModel model, Guid userId,Guid GymId);

        Task<IEnumerable<AllReviewViewModel>>GetAllReviewsAsync();

        Task<IEnumerable<AllReviewViewModel>> GetAllReviesForGymAsync(Guid gymId);
    }
}
