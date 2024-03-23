namespace OneStopOfficeBE.DTOs.Response
{
    public class ProcessTimeStatsDto
    {
        public RequestProcessTimeDto Avg { get; set; }
        public RequestProcessTimeDto Min { get; set; }
        public RequestProcessTimeDto Max { get; set; }
    }
}