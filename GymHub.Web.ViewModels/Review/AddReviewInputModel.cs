using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static GymHub.Common.EntityValidation.Review;

namespace GymHub.Web.ViewModels.Review
{
    public class AddReviewInputModel
    {
        [Required]
        [MinLength(TitleMinLength)]
        [MaxLength(TitleMaxLength)]
        public string Title { get; set; } = null!;

        [Required]
        [MinLength(MainBodyMinLength)]
        [MaxLength(MainBodyMaxLength)]

        public string MainBody { get; set; } = null!;
    }
}
