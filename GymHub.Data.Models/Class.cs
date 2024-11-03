using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

using static GymHub.Common.EntityValidation.GymClass;
namespace GymHub.Data.Models
{
    public class Class
    {
        [Key]
        [Comment("The unique identifier of the Class")]
        public Guid Id { get; set; }= Guid.NewGuid();

        [Required]
        [StringLength(NameMaxLength)]
        [Comment("The name of the Class")]
        public string Name { get; set; } = null!;

        [Required]
        [StringLength(InstructorMaxLength)]
        [Comment("The name of the Instructor of the class")]
        public string Instructor { get; set; } = null!;

        [Required]
        [Comment("Starting time and date of the Class")]
        public DateTime StartTimeAndDate { get; set; }

        [Required]
        [Comment("The duration of the Class")]
        public int Duration {  get; set; }

        [Required]
        [Comment("The unique identifier of the Gym that the class is in")]
        public Guid GymId { get; set; }

        [ForeignKey(nameof(GymId))]

        public Gym Gym { get; set; } = null!;

        public ICollection<ClassUser> ClassesUsers { get; set; }= new HashSet<ClassUser>();

        [Comment("Flag for seeing if the entity is deleted or not")]
        public bool isDeleted { get; set; }
    }
}
