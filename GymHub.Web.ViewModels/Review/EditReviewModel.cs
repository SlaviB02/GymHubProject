using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

using static GymHub.Common.EntityValidation.Review;
namespace GymHub.Web.ViewModels.Review
{
    public class EditReviewModel
    {
        [Required]
        [MinLength(TitleMinLength)]
        [MaxLength(TitleMaxLength)]
        public string Title { get; set; } = null!;


        [Required]
        [MinLength(MainBodyMinLength)]
        [MaxLength(MainBodyMaxLength)]
        public string Body { get; set; } = null!;

        public required Guid Id { get; set; }

        public required Guid UserId {  get; set; }

        public required Guid GymId {  get; set; }

    }
}
