using GymHub.Data.Models;
using GymHub.Data.Repository.Interfaces;
using GymHub.Services.Data.Interfaces;
using GymHub.Web.ViewModels.Trainer;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymHub.Services.Data
{
    public class TrainerService:ITrainerService
    {
        private readonly IRepository<Trainer> context;

        public TrainerService(IRepository<Trainer> _context)
        {
            context = _context;
        }

        public async Task<IEnumerable<GymTrainerViewModel>> GetTrainersForGym(Guid gymId)
        {
            var trainers= await context.GetAllAttached()
                .Where(t=>t.GymId == gymId)
                .Select(t=>new GymTrainerViewModel()
                {
                    FirstName = t.FirstName,
                    LastName = t.LastName,
                    Phone=t.PhoneNumber,
                    Email=t.Email,
                    ImageUrl=t.ImageUrl,
                })
                .ToListAsync();

            return trainers;
        }
    }
}
