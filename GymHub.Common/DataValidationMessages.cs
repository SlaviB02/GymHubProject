

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
    }
}
