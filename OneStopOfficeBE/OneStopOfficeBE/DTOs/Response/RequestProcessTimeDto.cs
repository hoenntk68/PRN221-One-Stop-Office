namespace OneStopOfficeBE.DTOs.Response
{
    public class RequestProcessTimeDto
    {
        public int RequestId { get; set; }
        public TimeSpan ProcessTime { get; set; }
        public int DayCount {get; set;}
        public int HourCount {get; set;}
        public int MinuteCount {get; set;}
    }
}
