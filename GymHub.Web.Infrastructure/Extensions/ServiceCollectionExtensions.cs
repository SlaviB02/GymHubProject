using GymHub.Data.Models;
using GymHub.Data.Repository;
using GymHub.Data.Repository.Interfaces;
using GymHub.Services.Data;
using GymHub.Services.Data.Interfaces;
using Microsoft.Extensions.DependencyInjection;


namespace GymHub.Web.Infrastructure.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static void RegisterRepositories(this IServiceCollection services)
        {
            services.AddScoped<IRepository<Gym>, Repository<Gym>>();
            services.AddScoped<IRepository<Membership>, Repository<Membership>>();
            services.AddScoped<IRepository<Review>, Repository<Review>>();
            services.AddScoped<IRepository<Class>, Repository<Class>>();
            services.AddScoped<IRepository<ClassUser>, Repository<ClassUser>>();
            services.AddScoped<IRepository<Trainer>, Repository<Trainer>>();
        }
        public static void RegisterServices(this IServiceCollection services)
        {
            services.AddScoped<IGymService, GymService>();
            services.AddScoped<IMembershipService, MembershipService>();
            services.AddScoped<IReviewService, ReviewService>();
            services.AddScoped<IClassService, ClassService>();
            services.AddScoped<ITrainerService, TrainerService>();

        }
    }
}
