namespace ServerTracker.Dtos
{
    public class RamLoadAveragesDto
    {
        public double[] AverageRamLoadsForLastHourByMinute { get; set; }
        public double[] AverageRamLoadsForLastDayByHour { get; set; }
    }
}