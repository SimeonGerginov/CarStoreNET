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

        // Car Constants
        public const int MinBrandName = 3;
        public const int MaxBrandName = 25;
        public const int MinModelName = 3;
        public const int MaxModelName = 25;
        public const int MinCarName = 3;
        public const int MaxCarName = 25;
        public const int MinDescriptionLength = 20;
        public const int MaxDescriptionLength = 200;
        public const int MinYearLength = 1892;
        public const int MaxYearLength = 2019;

        // Category Constants
        public const int MinCategoryName = 3;
        public const int MaxCategoryName = 25;

        // Department Constants
        public const int MinDepartmentName = 3;
        public const int MaxDepartmentName = 25;

        // Review Constants
        public const int MinReviewLength = 20;
        public const int MaxReviewLength = 300;

        public const int CarsPerPage = 1;

        public const string ConnectionStringKey = "DefaultConnection";
        public const string JsonFile = "appsettings.json";

        public const string LoginErrorKey = "1";
        public const string LoginErrorMessage = "Invalid login attempt.";

        // Roles
        public const string AdminRole = "Administrator";
    }
}
