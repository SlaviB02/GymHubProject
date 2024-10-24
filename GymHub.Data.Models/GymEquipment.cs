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
    [PrimaryKey(nameof(GymId),nameof(EquipmentId))]
    public class GymEquipment
    {
        [Required]
        [Comment("The unique identifier of the Gym")]
        public Guid GymId { get; set; }
        [ForeignKey(nameof(GymId))] 
        public Gym Gym { get; set; } = null!;

        [Required]
        [Comment("The unique identifier of the Equipment")]
        public Guid EquipmentId { get; set; }

        [ForeignKey(nameof(EquipmentId))]

        public Equipment Equipment { get; set; } = null!;
    }
}
