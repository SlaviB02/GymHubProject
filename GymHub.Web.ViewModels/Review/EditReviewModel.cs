using System.ComponentModel.DataAnnotations;
using static GymHub.Common.EntityValidation.Review;
using static GymHub.Common.DataValidationMessages.Review;
namespace GymHub.Web.ViewModels.Review
{
    public class EditReviewModel
    {
        [Required]
        [StringLength(TitleMaxLength, ErrorMessage = TitleLengthMessage, MinimumLength = TitleMinLength)]
        public string Title { get; set; } = null!;


        [Required]
        [StringLength(MainBodyMaxLength, ErrorMessage = BodyLengthMessage, MinimumLength = MainBodyMinLength)]
        public string Body { get; set; } = null!;

        [Required]
        public double Rating {  get; set; }

        public required Guid Id { get; set; }

        public required Guid UserId {  get; set; }

        public required Guid GymId {  get; set; }

    }
}
