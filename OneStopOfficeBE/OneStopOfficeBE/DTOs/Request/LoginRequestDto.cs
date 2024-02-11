using System.Text.Json.Serialization;

namespace OneStopOfficeBE.DTOs.Request
{
    public class LoginRequestDto
    {
        [JsonPropertyName("user_name")]
        public string userName { get; set; }

        [JsonPropertyName("password")]
        public string password { get; set; }
    }
}
