using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymHub.Web.ViewModels.Gym
{
    public class AllGymViewModel
    {
        public required string Name { get; set; }

        public required string Address { get; set; }

        public string? ImageUrl { get; set; }

        public int OpeningHour { get; set; }

        public int ClosingHour { get; set; }

        public Guid Id { get; set; }
    }
}
