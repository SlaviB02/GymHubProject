using Microsoft.AspNetCore.Identity;
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
    [PrimaryKey(nameof(ClassId),nameof(UserId))]
    public class ClassUser
    {
        [Required]
        [Comment("The unique identifier of the Class")]
        public Guid ClassId { get; set; }
        [ForeignKey(nameof(ClassId))] 
        public Class Class { get; set; } = null!;

        [Required]
        [Comment("The unique identifier of the User")]
        public Guid UserId { get; set; }

        [ForeignKey(nameof(UserId))]

        public ApplicationUser User { get; set; }=null!;
    }
}
