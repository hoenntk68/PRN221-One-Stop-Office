using OneStopOfficeBE.Models;

namespace OneStopOfficeBE.DTOs.Response
{
    public class GeneralStatsDto
    {
        public int RequestCount { get; set; }
        public int AdminCount { get; set; }
        public int ClientCount { get; set; }
        public int UserSystemCount { get; set; }
        public int CategoryCount { get; set; }
    }
}
