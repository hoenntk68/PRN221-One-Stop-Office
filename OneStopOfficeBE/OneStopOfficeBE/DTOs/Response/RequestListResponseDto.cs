namespace OneStopOfficeBE.DTOs.Response
{
    public class RequestListResponseDto
    {
        public int Id { get; set; }
        public string Category { get; set; }
        public string? ProcessNote { get; set; }
        public string? File { get; set; }
        public string? Reason { get; set; }
        public DateTime? SubmittedAt { get; set; }
        public DateTime? ProcessedAt { get; set; }
        public string Status { get; set; }
    }
}
