using GymHub.Data.Models.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static GymHub.Common.EntityValidation.Equipment;

namespace GymHub.Data.Models
{
    public class Equipment
    {
        [Key]
        public Guid Id { get; set; }=Guid.NewGuid();

        [Required]
        [StringLength(ModelMaxLength)]

        public string Model { get; set; } = null!;

        [Required]

        public EquipmentType Type { get; set; }

        public ICollection<GymEquipment> GymsEquipments { get; set; }=new HashSet<GymEquipment>();
    }
}
