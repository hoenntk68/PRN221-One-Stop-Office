using System.Net.Mail;
using System.Text.Json.Serialization;

namespace OneStopOfficeBE.DTOs.Request
{
    public class SubmitRequestDto
    {
        [JsonPropertyName("category")]
        public int category { get; set; }

        [JsonPropertyName("reason")]
        public string reason { get; set; }

        [JsonPropertyName("attachment")]
        public IFormFile attachment { get; set; }
    }
}
