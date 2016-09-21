namespace ServerTracker.Dtos
{
    public class CreateServerLoadDto
    {
        public string ServerName { get; set; }
        public double CpuLoad { get; set; }
        public double RamLoad { get; set; }
    }
}