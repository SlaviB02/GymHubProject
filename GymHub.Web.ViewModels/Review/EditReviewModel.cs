using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymHub.Web.ViewModels.Review
{
    public class EditReviewModel
    {
        public required string Title { get; set; }

        public required string Body { get; set; }

        public required Guid Id { get; set; }

        public required Guid UserId {  get; set; }

        public required Guid GymId {  get; set; }

    }
}
