using GymHub.Web.ViewModels.Gym;
using System.ComponentModel.DataAnnotations;
using static GymHub.Common.EntityValidation.GymClass;
using static GymHub.Common.DataValidationMessages.GymClass;

namespace GymHub.Web.ViewModels.Class
{
    public class AddClassViewModel
    {
        [Required]
        [StringLength(NameMaxLength,ErrorMessage =NameLengthMessage,MinimumLength =NameMinLength)]
        public string Name { get; set; } = null!;
        [Required]
        public string DateAndTime { get; set; } = null!;
        [Required]
        [Range(MinDuration,MaxDuration,ErrorMessage =DurationRangeMessage)]
        public int Duration {  get; set; }

        [Required]
        [StringLength(InstructorMaxLength, ErrorMessage = InstructorNameLengthMessage, MinimumLength = InstructorMinLength)]
        public string Instructor { get; set; } = null!;

        public IEnumerable<GymNamesViewModel>? Gyms { get; set; }

        [Required]
        public Guid GymId { get; set; }
    }
}
