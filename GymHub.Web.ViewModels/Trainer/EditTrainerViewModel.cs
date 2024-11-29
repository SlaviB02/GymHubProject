using System.ComponentModel.DataAnnotations;
using static GymHub.Common.EntityValidation.Trainer;
using static GymHub.Common.DataValidationMessages.Trainer;

namespace GymHub.Web.ViewModels.Trainer
{
    public class EditTrainerViewModel
    {

        [Required]
        [StringLength(FirstNameMaxLength, ErrorMessage = NameLengthMessage, MinimumLength = FirstNameMinLength)]
        public string FirstName { get; set; } = null!;
        [Required]
        [StringLength(LastNameMaxLength, ErrorMessage = NameLengthMessage, MinimumLength = LastNameMinLength)]
        public string LastName { get; set; } = null!;
        [Required]
        [RegularExpression(PhoneRegex, ErrorMessage = PhoneRegexMessage)]
        public string PhoneNumber { get; set; } = null!;
        [Required]
        [StringLength(EmailMaxLength, ErrorMessage = EmailLengthMessage, MinimumLength = EmailMinLength)]
        public string Email { get; set; } = null!;

        public string? ImageUrl { get; set; }

        [Required]
        public Guid Id { get; set; }

        [Required]
        public Guid GymId { get; set; }
    }
}
