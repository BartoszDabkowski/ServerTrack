using ServerTracker.Dtos;

namespace ServerTracker.Tests.Utility
{
    class TestHelper
    {
        public static RamLoadAveragesDto CreateRamLoadAverages(double average)
        {
            var averageRamLoadsForLastHourByMinute = new double[Global.Utility.MinutesInHour];
            var averageRamLoadsForLastDayByHour = new double[Global.Utility.HoursInDay];

            for (var i = 0; i < Global.Utility.MinutesInHour; i++)
            {
                averageRamLoadsForLastHourByMinute[i] = average;
            }

            for (var i = 0; i < Global.Utility.HoursInDay; i++)
            {
                averageRamLoadsForLastDayByHour[i] = average;
            }

            return new RamLoadAveragesDto()
            {
                AverageRamLoadsForLastHourByMinute = averageRamLoadsForLastHourByMinute,
                AverageRamLoadsForLastDayByHour = averageRamLoadsForLastDayByHour
            };
        }

        public static bool AreRamLoadAveragesEqual(RamLoadAveragesDto load1, RamLoadAveragesDto load2)
        {
            if (!AreLoadAveragesSameLength(load1, load2))
                return false;

            for (var i = 0; i < load1.AverageRamLoadsForLastHourByMinute.Length; i++)
            {
                if (!load1.AverageRamLoadsForLastHourByMinute[i].Equals(load2.AverageRamLoadsForLastHourByMinute[i]))
                    return false;
            }

            for (var i = 0; i < load1.AverageRamLoadsForLastDayByHour.Length; i++)
            {
                if (!load1.AverageRamLoadsForLastDayByHour[i].Equals(load2.AverageRamLoadsForLastDayByHour[i]))
                    return false;
            }

            return true;
        }

        private static bool AreLoadAveragesSameLength(RamLoadAveragesDto load1, RamLoadAveragesDto load2)
        {
            return load1.AverageRamLoadsForLastHourByMinute.Length == load2.AverageRamLoadsForLastHourByMinute.Length
                   && load1.AverageRamLoadsForLastDayByHour.Length == load2.AverageRamLoadsForLastDayByHour.Length;
        }
    }
}
