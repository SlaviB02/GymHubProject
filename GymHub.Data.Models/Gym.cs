using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static GymHub.Common.EntityValidation.Gym;

namespace GymHub.Data.Models
{
    public class Gym
    {
        [Key]
        [Comment("The unique identifier of Gym")]
        public Guid Id { get; set; }= Guid.NewGuid();

        [Required]
        [StringLength(NameMaxLength)]
        [Comment("The Name of the Gym")]
        public string Name { get; set; } = null!;

        [Required]
        [StringLength(AddressMaxLength)]
        [Comment("The Address of the Gym")]

        public string Address {  get; set; } = null!;

        [Comment("The ImageUrl of the Gym")]
        public string? ImageUrl { get; set; }

        [Required]
        [StringLength(DescriptionMaxLength)]
        [Comment("Description of the gym")]
        public string Description {  get; set; } = null!;

        [Required]
        [Comment("Opening hour of Gym")]
        public int OpeningHour { get; set; }
        [Required]
        [Comment("Closing hour of Gym")]
        public int ClosingHour { get; set; }

        public bool IsDeleted {  get; set; }

        public ICollection<GymEquipment> GymsEquipments { get; set; } = new HashSet<GymEquipment>();

        public ICollection<Review> Reviews { get; set; }=new HashSet<Review>();

        public ICollection<Trainer> Trainers { get; set; } = new HashSet<Trainer>();

        public ICollection<Membership> Memberships { get; set; } = new HashSet<Membership>();

        public ICollection<Class> Classes { get; set; } = new HashSet<Class>();
    }
}
