using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymHub.Web.ViewModels.Trainer
{
    public class GymTrainerViewModel
    {
        public required string FirstName {  get; set; }

        public required string LastName { get; set; }

        public required string Email {  get; set; }

        public required string Phone {  get; set; } 

        public string? ImageUrl {  get; set; }


    }
}
