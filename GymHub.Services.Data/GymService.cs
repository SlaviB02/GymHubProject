using GymHub.Data.Models;
using GymHub.Data.Repository;
using GymHub.Data.Repository.Interfaces;
using GymHub.Services.Data.Interfaces;
using GymHub.Web.ViewModels.Gym;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static GymHub.Common.EntityValidation.Gym;

namespace GymHub.Services.Data
{
    public class GymService : IGymService
    {
        private readonly IRepository<Gym> context;

        public GymService(IRepository<Gym> _context)
        {
            context= _context;
        }

        public async Task AddGymAsync(AddGymFormModel model)
        {
            Gym gym = new Gym()
            {
                Name = model.Name,
                Address = model.Address,
                ImageUrl = model.ImageUrl,
                Description = model.Description,
                OpeningHour = model.OpeningHour,
                ClosingHour = model.ClosingHour,
            };

          await context.AddAsync(gym);

        }

        public async Task<bool> DeleteGymAsync(Guid gymId)
        {
           var gym=await context
                .GetAllAttached()
                .Include(g=>g.Memberships)
                .Include(g=>g.Classes)
                .FirstOrDefaultAsync(g => g.Id == gymId && g.IsDeleted == false);

            if(gym==null)
            {
                return false;
            }
            bool hasActiveMemberships = gym.Memberships.Count() > 0;
            bool hasClasses=gym.Classes.Count() > 0;

            if(hasActiveMemberships==true || hasClasses==true)
            {
                return false;
            }

            gym.IsDeleted = true;

            await context.UpdateAsync(gym);

            return true;

        }

        public async Task<IEnumerable<AllGymViewModel>> GetAllGymsAsync(string? text)
        {
            IQueryable<AllGymViewModel> query = context
                .GetAllAttached()
                .Where(g => !g.IsDeleted)
                .Select(e => new AllGymViewModel()
                {
                    Name = e.Name,
                    Address = e.Address,
                    ImageUrl = e.ImageUrl,
                    OpeningHour = e.OpeningHour,
                    ClosingHour = e.ClosingHour,
                    Id = e.Id,
                });
               

            if (!String.IsNullOrEmpty(text))
            {
                query = query.Where(g => g.Name.ToLower().Contains(text.ToLower()));
            }
            
            var gyms=await query.ToListAsync();


            return gyms;
        }

        public async Task<IEnumerable<AllGymViewModel>> GetAllGymsBySearchAsync(string text)
        {
            var gyms = await context
               .GetAllAttached()
               .Where(g => !g.IsDeleted && g.Name.ToLower().Contains(text.ToLower()))
               .Select(e => new AllGymViewModel()
               {
                   Name = e.Name,
                   Address = e.Address,
                   ImageUrl = e.ImageUrl,
                   OpeningHour = e.OpeningHour,
                   ClosingHour = e.ClosingHour,
                   Id = e.Id,
               })
               .ToListAsync();

            return gyms;
        }

        public async Task<DeleteGymModel?> GetDeleteModelAsync(Guid gymId)
        {
            var gym= await context.FirstOrDefaultAsync(g=>g.Id == gymId && g.IsDeleted==false);

            DeleteGymModel? model = null;

            if (gym != null)
            {

               model = new DeleteGymModel()
                {
                    Name = gym.Name,
                    Id = gymId,
                    Address = gym.Address,
                };

            }
            return model;
        }

        public async Task<GymDetailsViewModel?> GetDetailsGymAsync(Guid id)
        {
            GymDetailsViewModel? model = null;

            Gym gym= await context.FirstOrDefaultAsync(g => g.Id == id && g.IsDeleted == false);



            if (gym!=null)
            {
                model = new GymDetailsViewModel
                {
                    Name = gym.Name,
                    Address = gym.Address,
                    Description = gym.Description,
                    ImageUrl = gym.ImageUrl,
                    OpeningHour = gym.OpeningHour,
                    ClosingHour = gym.ClosingHour,
                    Id = id
                };
            }



            return model;

        }

        public async Task<EditGymFormModel?> GetEditModelAsync(Guid gymId)
        {
            var gym= await context.FirstOrDefaultAsync(g => g.Id == gymId && g.IsDeleted == false);
            EditGymFormModel? model=null;

            if (gym!=null)
            {

                model = new EditGymFormModel()
                {
                    Id = gymId,
                    Name = gym.Name,
                    Address = gym.Address,
                    Description = gym.Description,
                    ImageUrl = gym.ImageUrl,
                    OpeningHour = gym.OpeningHour,
                    ClosingHour = gym.ClosingHour,
                };

            }
            return model;
        }

        public async Task<IEnumerable<GymNamesViewModel>> GetGymNamesAsync()
        {
            var list =await context.GetAllAttached()
                .Where(g => g.IsDeleted == false)
                .Select(g => new GymNamesViewModel()
                {
                    Name = g.Name,
                    Id = g.Id,
                })
                .ToListAsync();
            return list;
        }

        public async Task<bool> UpdateGymAsync(EditGymFormModel model)
        {
            Gym gym = new Gym()
            {
                Id = model.Id,
                Name = model.Name,
                Address = model.Address,
                Description = model.Description,
                ImageUrl = model.ImageUrl,
                OpeningHour = model.OpeningHour,
                ClosingHour = model.ClosingHour

            };

           bool res=await context.UpdateAsync(gym);

            return res;
        }
    }
}
