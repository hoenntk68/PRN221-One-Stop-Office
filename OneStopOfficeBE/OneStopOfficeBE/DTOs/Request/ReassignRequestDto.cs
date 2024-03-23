using System.Text.Json.Serialization;

namespace OneStopOfficeBE.DTOs.Request
{
    public class ReassignRequestDto
    {
        [JsonPropertyName("request_id")]
        public int RequestId { get; set; }

        [JsonPropertyName("assigned_to")]
        public string AssignedTo { get; set; }
    }
}
