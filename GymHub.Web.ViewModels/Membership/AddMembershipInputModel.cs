﻿using GymHub.Web.ViewModels.Gym;
using System.ComponentModel.DataAnnotations;
using static GymHub.Common.EntityValidation.Membership;
using static GymHub.Common.DataValidationMessages.Membership;

namespace GymHub.Web.ViewModels.Membership
{
    public class AddMembershipInputModel
    {
        [Required]
        public string Type { get; set; } = null!;

        [Required]

        public string StartDate { get; set; } = null!;

        [Required]
        [RegularExpression(PhoneRegex,ErrorMessage =PhoneRegexMessage)]

        public string PhoneNumber { set; get; } = null!;


        public IEnumerable<string>? Types { get; set; }

        public IEnumerable<GymNamesViewModel>? Gyms { get; set; }

        [Required]
        public Guid GymId { get; set; }
    }
}
