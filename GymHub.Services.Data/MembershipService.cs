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

        public async Task<bool> AddMembershipAsync(AddMembershipInputModel membership, string userId)
        {
            bool isReleaseDateValid = DateTime
              .TryParseExact(membership.StartDate, DateOnlyFormat, CultureInfo.InvariantCulture, DateTimeStyles.None,
                  out DateTime date);
            if (!isReleaseDateValid)
            {
                return false;
            }

            Membership m = new Membership()
            {
                UserId = userId,
                FirstName = membership.FirstName,
                LastName = membership.LastName,
                PhoneNumber = membership.PhoneNumber,
                StartDate = date,
                Type = (MembershipType)Enum.Parse(typeof(MembershipType), membership.Type),
                //GymId = Guid.Parse("A514009C-F2EE-46E9-A3F9-58F4D9221076")
                GymId=membership.GymId,//TODO
            };

            await context.AddAsync(m);

              return true;
        }

        public async Task<IEnumerable<AllMembershipsViewModel>> GetAllMembershipsAsync(string userId)
        {
           var list= await context
                .GetAllAttached()
                .Where(x => x.UserId == userId)
                .Select(m=>new AllMembershipsViewModel()
                {
                    FirstName = m.FirstName,
                    LastName = m.LastName,
                    GymName=m.Gym.Name,
                    StartDate=m.StartDate.ToString(DateOnlyFormat),
                    Type = m.Type.ToString(),
                    
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
