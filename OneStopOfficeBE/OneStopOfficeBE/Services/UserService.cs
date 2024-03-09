using OneStopOfficeBE.DTOs.Request;
using OneStopOfficeBE.DTOs.Response;
using OneStopOfficeBE.Models;

namespace OneStopOfficeBE.Services
{
    public interface UserService
    {
        User GetInfo(string id);
        LoginResponseDto Login(LoginRequestDto loginRequest);
        bool Logout(string username);
    }
}
