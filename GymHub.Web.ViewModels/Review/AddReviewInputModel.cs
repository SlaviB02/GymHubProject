using System.ComponentModel.DataAnnotations;
using static GymHub.Common.EntityValidation.Review;
using static GymHub.Common.DataValidationMessages.Review;

namespace GymHub.Web.ViewModels.Review
{
    public class AddReviewInputModel
    {
        [Required]
        [StringLength(TitleMaxLength,ErrorMessage =TitleLengthMessage,MinimumLength =TitleMinLength)]
        public string Title { get; set; } = null!;

        [Required]
        [StringLength(MainBodyMaxLength, ErrorMessage = BodyLengthMessage, MinimumLength = MainBodyMinLength)]

        public string MainBody { get; set; } = null!;

        [Required]
        [Range(RatingMin, RatingMax,ErrorMessage =RatingRangeMessage)]
        public double Rating {  get; set; }
    }
}
