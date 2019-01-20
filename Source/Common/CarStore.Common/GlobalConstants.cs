namespace CarStore.Common
{
    public static class GlobalConstants
    {
        // Customer Constants
        public const int MinNameLength = 3;
        public const int MaxNameLength = 25;
        public const int MinAge = 18;
        public const int MaxAge = 120;
        public const int MinPasswordLength = 6;
        public const int MaxPasswordLength = 32;

        public const string ConnectionStringKey = "DefaultConnection";
        public const string JsonFile = "appsettings.json";

        public const string LoginErrorKey = "1";
        public const string LoginErrorMessage = "Invalid login attempt.";
    }
}
