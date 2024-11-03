using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymHub.Web.ViewModels.Class
{
    public class AllClassViewModel
    {
        public required string Name { get; set; }

        public required string StartTimeAndDate { get; set; }

        public int Duration { get; set; }

        public Guid Id { get; set; }

    }
}
