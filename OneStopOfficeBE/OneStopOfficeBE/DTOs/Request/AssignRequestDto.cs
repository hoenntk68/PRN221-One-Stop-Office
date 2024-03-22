using System.Text.Json.Serialization;

namespace OneStopOfficeBE.DTOs.Request
{
    public class AssignRequestDto
    {
        [JsonPropertyName("requests")]
        public int[] Requests { get; set; }

        [JsonPropertyName("assignedTo")]
        public string AssignedTo { get; set; }
    }
}
