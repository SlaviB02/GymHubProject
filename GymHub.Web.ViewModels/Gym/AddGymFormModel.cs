using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static GymHub.Common.EntityValidation.Gym;
namespace GymHub.Web.ViewModels.Gym
{
   public class AddGymFormModel
    {
        [Required]
        [MinLength(NameMinLength)]
        [MaxLength(NameMaxLength)]
        public string Name { get; set; } = null!;
        [Required]
        [MinLength(DescriptionMinLength)]
        [MaxLength(DescriptionMaxLength)]
        public string Description { get; set; } = null!;

        public string? ImageUrl {  get; set; }
        [Required]
        [MinLength(AddressMinLength)]
        [MaxLength(AddressMaxLength)]
        public string Address {  get; set; }=null!;

        [Required]
        [Range(HourMinRange,HourMaxRange)]
        public int OpeningHour {  get; set; }
        [Required]
        [Range(HourMinRange, HourMaxRange)]
        public int ClosingHour {  get; set; }


    }
}
