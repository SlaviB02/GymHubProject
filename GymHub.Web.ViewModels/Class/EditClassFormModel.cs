using GymHub.Web.ViewModels.Gym;

using System.ComponentModel.DataAnnotations;
using static GymHub.Common.EntityValidation.GymClass;

namespace GymHub.Web.ViewModels.Class
{
    public class EditClassFormModel
    {
        [Required]
        [MinLength(NameMinLength)]
        [MaxLength(NameMaxLength)]
        public string Name { get; set; } = null!;
        [Required]
        public string DateAndTime { get; set; } = null!;
        [Required]
        [Range(MinDuration, MaxDuration)]
        public int Duration { get; set; }

        [Required]
        [MinLength(InstructorMinLength)]
        [MaxLength(InstructorMaxLength)]
        public string Instructor { get; set; } = null!;
        [Required]
        public Guid Id { get; set; }

        [Required]
        public Guid GymId { get; set; }

    }
}
