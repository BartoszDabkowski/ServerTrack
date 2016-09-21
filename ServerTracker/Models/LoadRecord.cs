using System;

namespace ServerTracker.Models
{
    public class LoadRecord
    {
        public DateTime DateTime { get; set; }
        public double CpuLoad { get; set; }
        public double RamLoad { get; set; }
    }
}