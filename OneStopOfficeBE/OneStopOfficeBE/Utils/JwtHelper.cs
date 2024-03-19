using Newtonsoft.Json.Linq;
using OneStopOfficeBE.CustomAttributes;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text.Json;

namespace OneStopOfficeBE.Utils
{
    public class JwtHelper
    {
        public static UserExtracted? extractUser(string jwtToken)
        {

            //JwtPayload? payload = JsonSerializer.Deserialize<JwtPayload>(payloadString);
            //var properties = typeof(UserExtracted).GetProperties();
            //foreach (var property in properties)
            //{
            //    var claim = payload?.Claims.FirstOrDefault(c => c.Type == property.Name);
            //    if (claim != null)
            //    {
            //        object value = Convert.ChangeType(claim.Value, property.PropertyType);
            //        property.SetValue(userExtracted, value);
            //    }
            //}
            //string? username = payload?.Claims.FirstOrDefault(c => c.Type == "Username")?.Value;
            //bool.TryParse(payload?.Claims.FirstOrDefault(c => c.Type == "IsAdmin")?.Value, out bool isAdmin);
            //bool.TryParse(payload?.Claims.FirstOrDefault(c => c.Type == "IsSuperAdmin")?.Value, out bool isSuperAdmin);
            //UserExtracted userExtracted = new UserExtracted()
            //{
            //    Username = username,
            //    IsAdmin = isAdmin,
            //    IsSuperAdmin = isSuperAdmin
            //};
            return JsonSerializer.Deserialize<UserExtracted>(jwtToken);
        }
    }
}
