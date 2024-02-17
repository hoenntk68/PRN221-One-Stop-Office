namespace OneStopOfficeBE.DTOs.Response
{
    public class RequestListResponseDto
    {
        public int Id { get; set; }

        public string Category { get; set; }

        public DateTime SubmittedAt { get; set; }

        public string Status { get; set; }
    }
}
