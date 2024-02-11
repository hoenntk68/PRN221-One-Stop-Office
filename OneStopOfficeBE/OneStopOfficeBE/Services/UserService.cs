using OneStopOfficeBE.DTOs.Request;
using OneStopOfficeBE.DTOs.Response;

namespace OneStopOfficeBE.Services
{
    public interface UserService
    {
        BaseResponse Login (LoginRequestDto loginRequest);
    }
}
