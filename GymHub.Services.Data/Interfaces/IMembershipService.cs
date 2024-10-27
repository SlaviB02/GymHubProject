using GymHub.Data.Models.Enums;
using GymHub.Web.ViewModels.Membership;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymHub.Services.Data.Interfaces
{
    public interface IMembershipService
    {
        Task<IEnumerable<AllMembershipsViewModel>> GetAllMembershipsAsync(string userId);

        Task<bool>AddMembershipAsync(AddMembershipInputModel membership,string userId);

        IEnumerable<string> GetTypesNames();

        Task CancelMembershipAsync(Guid Id);

        
    }
}
