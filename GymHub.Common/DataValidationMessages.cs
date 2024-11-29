

namespace GymHub.Common
{
   public static class DataValidationMessages
    {
        public static class GymClass
        {
            public const string NameLengthMessage = "Class {0} should be between {2} and {1} characters!";
            public const string DurationRangeMessage = "{0} should be between {1} and {2} !";
            public const string InstructorNameLengthMessage = "{0} should be between {2} and {1} characters!";
        }
        public static class Gym
        {
            public const string NameLengthMessage = "Gym {0} should be between {2} and {1} characters!";
            public const string DescriptionLengthMessage = "Gym {0} should be between {2} and {1} characters!";
            public const string AddressLengthMessage = "Gym {0} should be between {2} and {1} characters!";
            public const string HourRangeMessage = "{0} should be between {1} and {2}";
        }
        public static class Membership
        {
            public const string PhoneRegexMessage= "Membership {0} should be in DDD-DDD-DDDD format!";
        }
        public static class Review
        {
            public const string TitleLengthMessage = "Review {0} should be between {2} and {1} characters!";
            public const string BodyLengthMessage = "Review {0} should be between {2} and {1} characters!";
            public const string RatingRangeMessage = "{0} should be between {1} and {2}";
        }
        public static class Trainer
        {
            public const string NameLengthMessage = "Trainer {0} should be between {2} and {1} characters!";
            public const string PhoneRegexMessage = "Trainer {0} should be in format: + followed by 11 digits!";
            public const string EmailLengthMessage = "{0} should be between {2} and {1} characters!";
        }
    }
}
