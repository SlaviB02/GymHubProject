using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static GymHub.Common.EntityValidation.Review;

namespace GymHub.Data.Models
{
    public class Review
    {
        [Key]
        [Comment("The unique identifier of Review")]
        public Guid Id { get; set; }=Guid.NewGuid();

        [Required]
        [StringLength(TitleMaxLength)]
        [Comment("The title of the Review")]
        public string Title { get; set; } = null!;

        [Required]
        [StringLength(MainBodyMaxLength)]
        [Comment("The MainBody of the Review")]
        public string MainBody { get; set; } = null!;

        [Required]
        [Comment("The unique identifier of the User that posted the review")]
        public Guid UserId { get; set; }

        [ForeignKey(nameof(UserId))]

        public ApplicationUser User { get; set; } = null!;


        [Required]
        [Comment("The unique identifier of the Gym that the review is on")]
        public Guid GymId { get; set; }

        [ForeignKey(nameof(GymId))]

        public Gym Gym { get; set; }=null!;
    }
}
