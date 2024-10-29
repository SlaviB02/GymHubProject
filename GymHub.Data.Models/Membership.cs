using GymHub.Data.Models.Enums;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static GymHub.Common.EntityValidation.Membership;

namespace GymHub.Data.Models
{
    public class Membership
    {
        [Key]
        [Comment("The unique identifier of Membership")]
        public Guid Id { get; set; }= Guid.NewGuid();

        [Required]
        [Comment("The type of Membership")]
        public MembershipType Type { get; set; }

        [Required]
        [Comment("Starting Date of the Membership")]
        public DateTime StartDate { get; set; }

        [Required]
        [StringLength(PhoneMaxLength)]
        [Comment("Phone number of the person")]
        public string PhoneNumber { get; set; } = null!;

        [Required]
        [StringLength(FirstNameMaxLength)]
        [Comment("First name of the person")]
        public string FirstName { get; set; } = null!;

        [Required]
        [StringLength(LastNameMaxLength)]
        [Comment("Last name of the person")]
        public string LastName { get; set; } = null!;

        [Required]
        [Comment("The unique identifier of the User that made the membership")]
        public Guid UserId { get; set; }

        [ForeignKey(nameof(UserId))]
        public ApplicationUser User { get; set; } = null!;

        [Required]
        [Comment("The unique identifier of the Gym for the membership")]
        public Guid GymId { get; set; }

        [ForeignKey(nameof(GymId))]

        public Gym Gym { get; set; } = null!;
    }
}
