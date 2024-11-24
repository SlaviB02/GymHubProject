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

        public async Task AddTrainerAsync(AddTrainerViewModel model)
        {
            Trainer trainer = new Trainer()
            {
                FirstName = model.FirstName,
                LastName = model.LastName,
                PhoneNumber = model.PhoneNumber,
                Email = model.Email,
                GymId = model.GymId,
                ImageUrl = model.ImageUrl,
            };

            await context.AddAsync(trainer);
        }

        public async Task<bool> DeleteTrainerAsync(Guid trainerId)
        {
            var trainer = await context.FirstOrDefaultAsync(c => c.Id == trainerId && c.isDeleted == false);

            if(trainer==null)
            {
                return false;
            }

            trainer.isDeleted = true;

            await context.UpdateAsync(trainer);

            return true;
        }

        public async Task<bool> EditTrainerAsync(EditTrainerViewModel model)
        {
            Trainer trainer = new Trainer()
            {
                FirstName = model.FirstName,
                LastName = model.LastName,
                PhoneNumber = model.PhoneNumber,
                Email = model.Email,
                ImageUrl = model.ImageUrl,
                GymId = model.GymId,
                Id = model.Id,
            };

            var res=await context.UpdateAsync(trainer);

            return res;
        }

        public async Task<IEnumerable<GymTrainerViewModel>> GetAllTrainersAsync()
        {
            var trainers = await context.GetAllAttached()
                .Where(t => t.isDeleted == false)
                .Select(t => new GymTrainerViewModel()
                {
                    FirstName = t.FirstName,
                    LastName = t.LastName,
                    Phone = t.PhoneNumber,
                    Email = t.Email,
                    ImageUrl = t.ImageUrl,
                    id = t.Id
                })
                .ToListAsync();

            return trainers;
        }

        public async Task<DeleteTrainerViewModel?> GetDeleteModelAsync(Guid trainerId)
        {
            var trainer=await context.FirstOrDefaultAsync(t=>t.Id == trainerId && t.isDeleted==false);

            DeleteTrainerViewModel? model = null;

            if(trainer!=null)
            {
                model = new DeleteTrainerViewModel()
                {
                    Name = trainer.FirstName + " " + trainer.LastName,
                    Id = trainer.Id
                };
            }

          

            return model;
        }

        public async Task<EditTrainerViewModel?> GetEditModelAsync(Guid trainerId)
        {
            var trainer=await context.FirstOrDefaultAsync(c=>c.Id == trainerId && c.isDeleted==false);

            EditTrainerViewModel? model = null;

            if (trainer != null)
            {

                model = new EditTrainerViewModel()
                {
                    FirstName = trainer.FirstName,
                    LastName = trainer.LastName,
                    PhoneNumber = trainer.PhoneNumber,
                    Email = trainer.Email,
                    Id = trainerId,
                    ImageUrl = trainer.ImageUrl,
                    GymId = trainer.GymId,
                };

            }

            return model;
        }

        public async Task<IEnumerable<GymTrainerViewModel>> GetTrainersForGymAsync(Guid gymId)
        {
            var trainers= await context.GetAllAttached()
                .Where(t=>t.GymId == gymId && t.isDeleted==false)
                .Select(t=>new GymTrainerViewModel()
                {
                    FirstName = t.FirstName,
                    LastName = t.LastName,
                    Phone=t.PhoneNumber,
                    Email=t.Email,
                    ImageUrl=t.ImageUrl,
                    id=t.Id
                })
                .ToListAsync();

            return trainers;
        }

    }
}
