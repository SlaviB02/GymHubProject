using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static GymHub.Common.EntityValidation.Trainer;

namespace GymHub.Data.Models
{
    public class Trainer
    {
        [Key]
        public Guid Id { get; set; }=Guid.NewGuid();

        [Required]
        [StringLength(FirstNameMaxLength)]
        public string FirstName { get; set; } = null!;

        [Required]
        [StringLength(LastNameMaxLength)]
        public string LastName { get; set; } = null!;

        [Required]
        [StringLength(PhoneMaxLength)]
        public string PhoneNumber { get; set; } = null!;

        [Required]
        [StringLength(EmailMaxLength)]
        public string Email { get; set; } = null!;

        [Required]

        public Guid GymId { get; set; }

        [ForeignKey(nameof(GymId))]

        public Gym Gym { get; set; } = null!;

    }
}
