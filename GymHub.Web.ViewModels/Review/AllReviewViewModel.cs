using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymHub.Web.ViewModels.Review
{
    public class AllReviewViewModel
    {
        public required string Title {  get; set; }

        public required string Body {  get; set; }

        public required string UserName {  get; set; }

        public required string GymName { get; set; }

    }
}
