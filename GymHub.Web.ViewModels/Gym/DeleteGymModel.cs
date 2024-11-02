using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymHub.Web.ViewModels.Gym
{
    public class DeleteGymModel
    {

        public Guid Id { get; set; }

        public required string Name { get; set; }    
    }
}
