using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using static GymHub.Common.EntityValidation.ApplicationUser;

namespace GymHub.Data.Models
{
    public class ApplicationUser:IdentityUser<Guid>
    {
        public ApplicationUser()
        {
            this.Id = Guid.NewGuid();
        }

        [Required]
        [StringLength(FirstNameMaxLength)]
        [Comment("The First name of the user")]
        public string FirstName { get; set; } = null!;

        [Required]
        [StringLength(LastNameMaxLength)]
        [Comment("The Last name of the user")]
        public string LastName { get; set; } = null!;

        public ICollection<Review> Reviews { get; set; }=new HashSet<Review>();
    }
}
