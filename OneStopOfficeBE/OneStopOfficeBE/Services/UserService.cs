using OneStopOfficeBE.DTOs;
using OneStopOfficeBE.DTOs.Request;
using OneStopOfficeBE.DTOs.Response;
using OneStopOfficeBE.Models;

namespace OneStopOfficeBE.Services
{
    public interface UserService
    {
        BaseResponse GetInfo(string id);
        BaseResponse Login(LoginRequestDto loginRequest);
        BaseResponse Logout(UserExtracted? user);
        BaseResponse GetAdmins();
        User? FindByUsername(string username);
    }
}
