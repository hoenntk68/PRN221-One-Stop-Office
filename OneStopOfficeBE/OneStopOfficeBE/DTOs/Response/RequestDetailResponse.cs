using OneStopOfficeBE.Models;

namespace OneStopOfficeBE.DTOs.Response
{
    public class RequestDetailResponse
    {
        public int RequestId { get; set; }
        public string? UserId { get; set; }
        public string? UserFullName { get; set; }
        public int? CategoryId { get; set; }
        public string? CategoryName { get; set; }
        public string? Reason { get; set; }
        public string? Attachment { get; set; }
        public string? ProcessNote { get; set; }
        public DateTime? CreatedAt { get; set; }
        public DateTime? UpdateAt { get; set; }
        public string? Status { get; set; }
    }
}
