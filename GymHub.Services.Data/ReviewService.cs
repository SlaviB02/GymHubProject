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
using static GymHub.Common.EntityValidation;
using Review = GymHub.Data.Models.Review;

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

        public async Task<IEnumerable<AllReviewViewModel>> GetAllReviewsForGymAsync(Guid gymId)
        {
            var list = await context
               .GetAllAttached()
               .Where(r=>r.GymId==gymId && r.IsDeleted==false)
               .Select(r => new AllReviewViewModel()
               {
                   Title = r.Title,
                   Body = r.MainBody,
                   UserName = r.User.UserName!,
                   ReviewId=r.Id
               })
               .ToListAsync();

            return list;
        }

        public async Task<IEnumerable<AllReviewViewModel>> GetAllReviewsAsync()
        {
            var list= await context
                .GetAllAttached()
                .Where(r=> r.IsDeleted == false)
                .Select(r=>new AllReviewViewModel()
                {
                    Title=r.Title,
                    Body=r.MainBody,
                    UserName=r.User.UserName!,
                    GymName=r.Gym.Name,
                    ReviewId=r.Id
                })
                .ToListAsync();

            return list;

        }

        public async Task<bool> DeleteReviewAsync(Guid reviewId)
        {
            Review review=await context.FirstOrDefaultAsync(r=>r.Id==reviewId && r.IsDeleted==false);


            if(review==null)
            {
                return false;
            }

            review.IsDeleted = true;

            await context.UpdateAsync(review);

            return true;
        }

        public async Task<EditReviewModel> GetEditReviewModelAsync(Guid id)
        {
            Review review = await context.FirstOrDefaultAsync(r => r.Id == id && r.IsDeleted == false);

            EditReviewModel model = new EditReviewModel()
            {
                Title = review.Title,
                Body = review.MainBody,
                Id = id,
                GymId=review.GymId, 
                UserId=review.UserId
            };

            return model;
        }

        public async Task<bool> UpdateReviewAsync(EditReviewModel model)
        {
            
            Review rev = new Review()
            {
                Title = model.Title,
                MainBody = model.Body,
                Id = model.Id,
                GymId=model.GymId,
                UserId=model.UserId
            };
            bool res = await context.UpdateAsync(rev);

            return res;
        }

        public async Task<DeleteReveiwViewModel> GetDeleteModelAsync(Guid reviewId)
        {
            Review review = await context.FirstOrDefaultAsync(r => r.Id == reviewId && r.IsDeleted == false);

            DeleteReveiwViewModel model=new DeleteReveiwViewModel()
            {
                Title=review.Title,
                Id=review.Id
            };

            return model;
        }
    }
}
