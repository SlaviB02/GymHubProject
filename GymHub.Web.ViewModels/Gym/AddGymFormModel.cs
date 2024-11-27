using System.ComponentModel.DataAnnotations;
using static GymHub.Common.EntityValidation.Gym;
using static GymHub.Common.DataValidationMessages.Gym;
namespace GymHub.Web.ViewModels.Gym
{
   public class AddGymFormModel
    {
        [Required]
        [StringLength(NameMaxLength,ErrorMessage =NameLengthMessage,MinimumLength =NameMinLength)]
        public string Name { get; set; } = null!;
        [Required]
        [StringLength(DescriptionMaxLength, ErrorMessage = DescriptionLengthMessage, MinimumLength = DescriptionMinLength)]
        public string Description { get; set; } = null!;

        public string? ImageUrl {  get; set; }
        [Required]
        [StringLength(AddressMaxLength, ErrorMessage = AddressLengthMessage, MinimumLength = AddressMinLength)]
        public string Address {  get; set; }=null!;

        [Required]
        [Range(HourMinRange,HourMaxRange,ErrorMessage =HourRangeMessage)]
        public int OpeningHour {  get; set; }
        [Required]
        [Range(HourMinRange, HourMaxRange, ErrorMessage = HourRangeMessage)]
        public int ClosingHour {  get; set; }


    }
}
