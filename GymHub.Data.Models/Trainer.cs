using Microsoft.EntityFrameworkCore;
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
        [Comment("The unique identifier of Trainer")]
        public Guid Id { get; set; }=Guid.NewGuid();

        [Required]
        [StringLength(FirstNameMaxLength)]
        [Comment("First name of Trainer")]
        public string FirstName { get; set; } = null!;

        [Required]
        [StringLength(LastNameMaxLength)]
        [Comment("Last name of Trainer")]
        public string LastName { get; set; } = null!;

        [Required]
        [StringLength(PhoneMaxLength)]
        [Comment("Phone Number of Trainer")]
        public string PhoneNumber { get; set; } = null!;

        [Required]
        [StringLength(EmailMaxLength)]
        [Comment("Email of Trainer")]
        public string Email { get; set; } = null!;

        [Comment("The ImageUrl of the Trainer")]
        public string? ImageUrl { get; set; }

        [Required]
        [Comment("The unique identifier of the Gym that the trainer is in")]
        public Guid GymId { get; set; }

        [ForeignKey(nameof(GymId))]

        public Gym Gym { get; set; } = null!;

        public bool isDeleted { get; set; }

    }
}
