using System;

namespace ServerTracker.Global
{
    public static class Utility
    {
        public const int SecondsInDay = 86400;
        public const double LoadRate = 1.5;
        public const string PopulatedServer = "Mock_Server_1";
        public const string NewServer = "Mock_Server_2";
        public const int MinutesInMinute = 1;
        public const int MinutesInHour = 60;
        public const int HoursInDay = 24;


        public static double CreateRandomLoad(double minimum, double maximum)
        {
            var random = new Random();

            return random.NextDouble() * (maximum - minimum) + minimum;

        }

        public static DateTime CreateIncrementedDateTime(DateTime dateTime, int increment)
        {
            return dateTime.AddSeconds(-increment);
        }

    }
}