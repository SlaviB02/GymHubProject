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

namespace GymHub.Services.Data
{
    public class GymService : IGymService
    {
        private readonly IRepository<Gym> context;

        public GymService(IRepository<Gym> _context)
        {
            context= _context;
        }
        public async Task<IEnumerable<AllGymViewModel>> GetAllGymsAsync()
        {
            var gyms= await context
                .GetAllAttached()
                .Select(e=>new AllGymViewModel()
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

        public async Task<GymDetailsViewModel> GetDetailsGymAsync(Guid id)
        {
           
            GymDetailsViewModel? model = null;

            Gym? gym= await context.GetByIdAsync(id);

            if(gym!=null)
            {
                model = new GymDetailsViewModel
                {
                    Name = gym.Name,
                    Address = gym.Address,
                    Description = gym.Description,
                    ImageUrl = gym.ImageUrl,
                    OpeningHour = gym.OpeningHour,
                    ClosingHour = gym.ClosingHour,
                };
            }



#pragma warning disable CS8603 // Possible null reference return.
            return model;
#pragma warning restore CS8603 // Possible null reference return.
        }
    }
}
