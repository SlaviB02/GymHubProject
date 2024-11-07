using System.ComponentModel.DataAnnotations;
using static GymHub.Common.EntityValidation.Trainer;

namespace GymHub.Web.ViewModels.Trainer
{
    public class EditTrainerViewModel
    {

        [Required]
        [MinLength(FirstNameMinLength)]
        [MaxLength(FirstNameMaxLength)]
        public string FirstName { get; set; } = null!;
        [Required]
        [MinLength(LastNameMinLength)]
        [MaxLength(LastNameMaxLength)]
        public string LastName { get; set; } = null!;
        [Required]
        [MinLength(PhoneMinLength)]
        [MaxLength(PhoneMaxLength)]
        public string PhoneNumber { get; set; } = null!;
        [Required]
        [MinLength(EmailMinLength)]
        [MaxLength(EmailMaxLength)]
        public string Email { get; set; } = null!;

        public string? ImageUrl { get; set; }

        [Required]
        public Guid Id { get; set; }

        [Required]
        public Guid GymId { get; set; }
    }
}
