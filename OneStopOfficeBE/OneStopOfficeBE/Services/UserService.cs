using OneStopOfficeBE.DTOs.Request;
using OneStopOfficeBE.DTOs.Response;

namespace OneStopOfficeBE.Services
{
    public interface UserService
    {
        BaseResponse GetInfo(string id);
        BaseResponse Login (LoginRequestDto loginRequest);
        BaseResponse Logout(string id);
    }
}
