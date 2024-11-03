using GymHub.Web.ViewModels.Class;


namespace GymHub.Services.Data.Interfaces
{
  public interface IClassService
    {
        Task<IEnumerable<AllClassViewModel>> GetAllClassesForGymAsync(Guid gymId);

        Task<bool> AddClassAsync(AddClassViewModel model);

        Task<IEnumerable<AllClassViewModel>> GetAllClassesAsync();

        Task<EditClassFormModel> GetEditModelAsync(Guid classId);

        Task<bool> EditClassAsync(EditClassFormModel model);

        Task<bool> DeleteClassAsync(Guid classId);

        Task<IEnumerable<UserClassViewModel>> GetClassesForUserAsync(Guid userId);

        Task<bool> SignUserForClassAsync(Guid userId, Guid classId);

        Task<bool>UnsignUserFromClassAsync(Guid userId, Guid classId);

    }
}
