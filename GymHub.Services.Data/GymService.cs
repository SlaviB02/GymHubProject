using GymHub.Data.Models;
using GymHub.Data.Repository;
using GymHub.Data.Repository.Interfaces;
using GymHub.Services.Data.Interfaces;
using GymHub.Web.ViewModels;
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
                })
                .ToListAsync();

            return gyms;
        }
    }
}
