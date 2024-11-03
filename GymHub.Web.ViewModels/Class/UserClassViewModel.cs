using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymHub.Web.ViewModels.Class
{
    public class UserClassViewModel
    {
        public required string Name { get; set; }

        public required string Instructor {  get; set; }

        public required string DateAndTime { get; set; }

        public int Duration {  get; set; }

        public required string GymName {  get; set; }

        public Guid Id { get; set; }
    }
}
