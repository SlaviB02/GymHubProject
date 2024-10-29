using GymHub.Data.Models;
using GymHub.Data.Repository;
using GymHub.Data.Repository.Interfaces;
using GymHub.Services.Data.Interfaces;
using GymHub.Web.ViewModels.Review;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymHub.Services.Data
{
    public class ReviewService:IReviewService
    {
        private readonly IRepository<Review> context;

        public ReviewService(IRepository<Review> _context)
        {
            context = _context;
        }

        public async Task<bool> AddReviewAsync(AddReviewInputModel model, Guid userId, Guid GymId)
        {
            Review review = new Review()
            {
                Title = model.Title,
                MainBody = model.MainBody,
                UserId = userId,
                GymId = GymId
            };

            await context.AddAsync(review);

            return true;
        }

        public async Task<IEnumerable<AllReviewViewModel>> GetAllReviesForGymAsync(Guid gymId)
        {
            var list = await context
               .GetAllAttached()
               .Where(r=>r.GymId==gymId)
               .Select(r => new AllReviewViewModel()
               {
                   Title = r.Title,
                   Body = r.MainBody,
                   UserName = r.User.UserName!,
                   GymName = r.Gym.Name,
               })
               .ToListAsync();

            return list;
        }

        public async Task<IEnumerable<AllReviewViewModel>> GetAllReviewsAsync()
        {
            var list= await context
                .GetAllAttached()
                .Select(r=>new AllReviewViewModel()
                {
                    Title=r.Title,
                    Body=r.MainBody,
                    UserName=r.User.UserName!,
                    GymName=r.Gym.Name,
                })
                .ToListAsync();

            return list;

        }
    }
}
