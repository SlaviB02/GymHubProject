using GymHub.Web.ViewModels.Class;


namespace GymHub.Services.Data.Interfaces
{
  public interface IClassService
    {
        Task<IEnumerable<AllClassViewModel>> GetAllClassesForGymAsync(Guid gymId);

        Task<bool> AddClassAsync(AddClassViewModel model);
    }
}
