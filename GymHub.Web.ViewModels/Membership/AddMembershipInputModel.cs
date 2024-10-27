using GymHub.Data.Models.Enums;
using GymHub.Web.ViewModels.Gym;
using System.ComponentModel.DataAnnotations;
using static GymHub.Common.EntityValidation.Membership;

namespace GymHub.Web.ViewModels.Membership
{
    public class AddMembershipInputModel
    {
        [Required]
        public string Type { get; set; } = null!;

        [Required]

        public string StartDate { get; set; } = null!;

        [Required]
        [MinLength(PhoneMinLength)]
        [MaxLength(PhoneMaxLength)]

        public string PhoneNumber { set; get; } = null!;

        [Required]
        [MinLength(FirstNameMinLength)]
        [MaxLength(FirstNameMaxLength)]

        public string FirstName { get; set; } = null!;

        [Required]
        [MinLength(LastNameMinLength)]
        [MaxLength(LastNameMaxLength)]

        public string LastName { get; set; } = null!;

        public IEnumerable<string>? Types { get; set; }

        public IEnumerable<GymNamesViewModel>? Gyms { get; set; }

        [Required]
        public Guid GymId { get; set; }
    }
}
