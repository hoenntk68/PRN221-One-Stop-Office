using Newtonsoft.Json.Linq;
using OneStopOfficeBE.DTOs;
using OneStopOfficeBE.DTOs.Response;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text.Json;

namespace OneStopOfficeBE.Utils
{
    public class TimeHelper
    {
        public static RequestProcessTimeDto SetTimeSpanProp(RequestProcessTimeDto dto)
        {
            TimeSpan? processTime = dto.ProcessTime;
            dto.DayCount = (int)processTime?.Days;
            dto.HourCount = (int)processTime?.Hours;
            dto.MinuteCount = (int)processTime?.Minutes;
            return dto;
        }
    }
}
