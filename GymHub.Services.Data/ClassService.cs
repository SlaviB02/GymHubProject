using GymHub.Data.Models;
using GymHub.Data.Repository.Interfaces;
using GymHub.Services.Data.Interfaces;
using GymHub.Web.ViewModels.Class;
using Microsoft.EntityFrameworkCore;
using System.Globalization;
using static GymHub.Common.ApplicationConstants;
using static GymHub.Common.EntityValidation.GymClass;


namespace GymHub.Services.Data
{
    public class ClassService:IClassService
    {
        private readonly IRepository<Class> context;

        public ClassService(IRepository<Class> _context)
        {
            context = _context; 
        }

        public async Task<bool> AddClassAsync(AddClassViewModel model)
        {
            bool isReleaseDateValid = DateTime
             .TryParseExact(model.DateAndTime, DateTimeFormat, CultureInfo.InvariantCulture, DateTimeStyles.None,
                 out DateTime dateTime);
            if (!isReleaseDateValid)
            {
                return false;
            }
            Class gymClass = new Class()
            {
                Name = model.Name,
                Duration = model.Duration,
                StartTimeAndDate=dateTime,
                GymId = model.GymId,
            };

            await context.AddAsync(gymClass);

            return true;
        }

        public async Task<IEnumerable<AllClassViewModel>> GetAllClassesForGymAsync(Guid gymId)
        {
            var list = await context.GetAllAttached()
                .Where(c => c.GymId == gymId)
                .Select(c => new AllClassViewModel()
                {
                    Name = c.Name,
                    Duration = c.Duration,
                    StartTimeAndDate = c.StartTimeAndDate.ToString(DateTimeFormat),
                    Id = c.Id,
                })
                .ToListAsync();

            return list;
        }
    }
}
