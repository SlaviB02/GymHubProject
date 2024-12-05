using GymHub.Data.Models;
using GymHub.Data.Repository.Interfaces;
using GymHub.Services.Data.Interfaces;
using GymHub.Web.ViewModels.Class;
using Microsoft.EntityFrameworkCore;
using System.Globalization;
using static GymHub.Common.ApplicationConstants;
using static GymHub.Common.EntityValidation;
using static GymHub.Common.EntityValidation.GymClass;


namespace GymHub.Services.Data
{
    public class ClassService:IClassService
    {
        private readonly IRepository<Class> ClassContext;
        private readonly IRepository<ClassUser> ClassUserContext;

        public ClassService(IRepository<Class> _context, IRepository<ClassUser> classUserContext)
        {
            ClassContext = _context;
            ClassUserContext = classUserContext;
        }

        public async Task<bool> AddClassAsync(AddClassViewModel model)
        {
            bool isDateValid = DateTime
             .TryParseExact(model.DateAndTime, DateAndTimeFormat, CultureInfo.InvariantCulture, DateTimeStyles.None,
                 out DateTime dateTime);
            if (!isDateValid)
            {
                return false;
            }
            Class gymClass = new Class()
            {
                Name = model.Name,
                Duration = model.Duration,
                StartTimeAndDate=dateTime,
                GymId = model.GymId,
                Instructor=model.Instructor,
            };

            await ClassContext.AddAsync(gymClass);

            return true;
        }

        public async Task<bool> DeleteClassAsync(Guid classId)
        {
            Class? gymClass = await ClassContext
                .GetAllAttached()
                .Where(c => c.Id == classId && c.isDeleted == false)
                .Include(c=>c.ClassesUsers)
                .FirstOrDefaultAsync();


            if (gymClass == null || gymClass.ClassesUsers.Any())
            {
                return false;
            }
            
            gymClass.isDeleted = true;

            await ClassContext.UpdateAsync(gymClass);

            return true;
        }

        public async Task<bool> EditClassAsync(EditClassFormModel model)
        {
            
            Class gymClass=new Class()
            {
                Name=model.Name,
                Duration = model.Duration,
                StartTimeAndDate=DateTime.ParseExact(model.DateAndTime,DateAndTimeFormat,CultureInfo.InvariantCulture),
                Instructor=model.Instructor,
                Id=model.Id,
                GymId=model.GymId,
            };

            bool res = await ClassContext.UpdateAsync(gymClass);

            return res;
        }

        public async Task<IEnumerable<AllClassViewModel>> GetAllClassesAsync()
        {
            var list = await ClassContext.GetAllAttached()
               .Where(c=>c.isDeleted == false)
               .Select(c => new AllClassViewModel()
               {
                   Name = c.Name,
                   Duration = c.Duration,
                   StartTimeAndDate = c.StartTimeAndDate.ToString(DateAndTimeFormat),
                   Id = c.Id,
                   Instructor = c.Instructor,
                   GymName=c.Gym.Name,
               })
               .ToListAsync();

            return list;    
        }

        public async Task<IEnumerable<AllClassViewModel>> GetAllClassesForGymAsync(Guid gymId)
        {
            var list = await ClassContext.GetAllAttached()
                .Where(c => c.GymId == gymId && c.isDeleted==false)
                .Select(c => new AllClassViewModel()
                {
                    Name = c.Name,
                    Duration = c.Duration,
                    StartTimeAndDate = c.StartTimeAndDate.ToString(DateAndTimeFormat),
                    Id = c.Id,
                    Instructor = c.Instructor,
                })
                .ToListAsync();

            return list;
        }

        public async Task<IEnumerable<UserClassViewModel>> GetClassesForUserAsync(Guid userId)
        {
            var list= await ClassContext.GetAllAttached()
                .Include(c=>c.ClassesUsers)
                .Where(c=>c.ClassesUsers.Any(cu=>cu.UserId==userId) && c.isDeleted==false)
                .Select(c=>new UserClassViewModel
                {  
                    Name=c.Name,
                    Duration=c.Duration,
                    DateAndTime=c.StartTimeAndDate.ToString(DateAndTimeFormat),
                    Instructor=c.Instructor,
                    GymName=c.Gym.Name,
                    Id=c.Id,
                })
                .ToListAsync();

            return list;
        }

        public async Task<DeleteClassViewModel?> GetDeleteModelAsync(Guid classId)
        {
            var gymClass=await ClassContext.FirstOrDefaultAsync(c=>c.Id==classId && c.isDeleted==false);

            DeleteClassViewModel? model = null;

            if (gymClass != null)
            {

                model = new DeleteClassViewModel()
                {
                    Name = gymClass.Name,
                    Id = gymClass.Id,
                };
            }

            return model;
        }

        public async Task<EditClassFormModel?> GetEditModelAsync(Guid classId)
        {
            var gymClass = await ClassContext.FirstOrDefaultAsync(c => c.isDeleted == false && c.Id == classId);

            EditClassFormModel? model = null;

            if (gymClass != null)
            {

                model = new EditClassFormModel()
                {
                    Name = gymClass.Name,
                    Duration = gymClass.Duration,
                    DateAndTime = gymClass.StartTimeAndDate.ToString(DateAndTimeFormat),
                    Instructor = gymClass.Instructor,
                    Id = classId,
                    GymId = gymClass.GymId
                };
            }



            return model;
        }

        public async Task<bool> SignUserForClassAsync(Guid userId, Guid classId)
        {
            var existingClass =await ClassUserContext.FirstOrDefaultAsync(cu => cu.ClassId == classId && cu.UserId == userId);

            if (existingClass != null)
            {
                return false;
            }

            ClassUser cu = new ClassUser()
            {
                UserId = userId,
                ClassId = classId
            };

            await ClassUserContext.AddAsync(cu);

            return true;
        }

        public async Task<bool> UnsignUserFromClassAsync(Guid userId, Guid classId)
        {
            var existingClass = await ClassUserContext.FirstOrDefaultAsync(cu => cu.ClassId == classId && cu.UserId == userId);

            if (existingClass==null)
            {
                return false;
            }

            await ClassUserContext.DeleteByItemAsync(existingClass);

            return true;
        }
    }
}
