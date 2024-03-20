using System.Text.Json.Serialization;

namespace OneStopOfficeBE.DTOs.Request
{
    public class UpdateStatusRequest
    {
        [JsonPropertyName("requestId")]
        public int RequestId { get; set; }

        [JsonPropertyName("status")]
        public string Status { get; set; }
    }
}
