using Newtonsoft.Json.Linq;
using OneStopOfficeBE.DTOs;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text.Json;

namespace OneStopOfficeBE.Utils
{
    public class JwtHelper
    {
        public static UserExtracted? extractUser(string jwtToken)
        {
            return JsonSerializer.Deserialize<UserExtracted>(jwtToken);
        }
    }
}
