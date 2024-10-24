namespace GymHub.Common
{
    public static class EntityValidation
    {
        public static class Trainer
        {
            public const int FirstNameMinLength = 3;
            public const int FirstNameMaxLength = 30;

            public const int LastNameMinLength = 5;
            public const int LastNameMaxLength = 30;

            public const int PhoneMinLength = 10;
            public const int PhoneMaxLength = 15;

            public const int EmailMinLength = 10;
            public const int EmailMaxLength = 50;

        }
        public static class Equipment
        {
            public const int ModelMinLength = 3;
            public const int ModelMaxLength = 40;
        }
        public static class Review
        {
            public const int TitleMinLength = 5;
            public const int TitleMaxLength = 80;

            public const int MainBodyMinLength = 20;
            public const int MainBodyMaxLength = 500;
        }

        public static class Membership
        {
            public const int FirstNameMinLength = 3;
            public const int FirstNameMaxLength = 30;

            public const int LastNameMinLength = 5;
            public const int LastNameMaxLength = 30;

            public const int PhoneMinLength = 10;
            public const int PhoneMaxLength = 15;
        }


    }
}
