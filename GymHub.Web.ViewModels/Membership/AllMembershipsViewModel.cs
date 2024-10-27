using GymHub.Data.Models.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymHub.Web.ViewModels.Membership
{
    public class AllMembershipsViewModel
    {
        public required string Type { get; set; }

        public required string StartDate {  get; set; }

        public required string FirstName {  get; set; }

        public required string LastName { get; set; }

        public required string GymName {  get; set; }
    }
}
