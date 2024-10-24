using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymHub.Data.Models
{
    public class Class
    {
        [Key]
        [Comment("The unique identifier of the Class")]
        public Guid Id { get; set; }= Guid.NewGuid();

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
    }
}
