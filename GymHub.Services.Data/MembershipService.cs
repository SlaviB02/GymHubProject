using GymHub.Data.Models;
using GymHub.Data.Models.Enums;
using GymHub.Data.Repository.Interfaces;
using GymHub.Services.Data.Interfaces;
using GymHub.Web.ViewModels.Membership;
using Microsoft.EntityFrameworkCore;
using System;
using System.Globalization;
using System.Reflection;
using static GymHub.Common.ApplicationConstants;

namespace GymHub.Services.Data
{
    public class MembershipService:IMembershipService
    {
        private readonly IRepository<Membership> context;

        public MembershipService(IRepository<Membership> _context)
        {
            context = _context;
        }

        public async Task<bool> AddMembershipAsync(AddMembershipInputModel membership, Guid userId)
        {
            var isAdded =await context.FirstOrDefaultAsync(m => m.UserId == userId && m.GymId == membership.GymId);

            if (isAdded!=null)
            {
                return false;
            }

            Membership m = new Membership()
            {
                UserId = userId,
                PhoneNumber = membership.PhoneNumber,
                StartDate = DateTime.Parse(membership.StartDate),
                Type = (MembershipType)Enum.Parse(typeof(MembershipType), membership.Type),
                GymId=membership.GymId,
            };

            await context.AddAsync(m);

              return true;
        }

        public async Task CancelMembershipAsync(Guid Id)
        { 

            await context.DeleteByIdAsync(Id);
        }

        public async Task<IEnumerable<AllMembershipsViewModel>> GetAllMembershipsAsync(Guid userId)
        {
           var list= await context
                .GetAllAttached()
                .Where(x => x.UserId == userId)
                .Select(m=>new AllMembershipsViewModel()
                {
                    GymName=m.Gym.Name,
                    StartDate=m.StartDate.ToString(DateOnlyFormat),
                    Type = m.Type.ToString(),
                    Id=m.Id
                })
                .ToListAsync();

            return list;
        }

        public IEnumerable<string> GetTypesNames()
        {
            var list=Enum.GetValues(typeof(MembershipType)).Cast<MembershipType>();

            return list.Select(m=>m.ToString());
        }
       
    }
}
