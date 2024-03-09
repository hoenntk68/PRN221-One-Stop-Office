using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OneStopOfficeBE.Constants;
using OneStopOfficeBE.CustomAttributes;
using OneStopOfficeBE.DTOs.Request;
using OneStopOfficeBE.DTOs.Response;
using OneStopOfficeBE.Models;
using OneStopOfficeBE.Services;

namespace OneStopOfficeBE.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private UserService _userService;

        public UserController(UserService userService)
        {
            _userService = userService;
        }

        [HttpPost("Login")]
        public BaseResponse Login(LoginRequestDto loginDto)
        {
            LoginResponseDto response = _userService.Login(loginDto);
            if (response != null)
            {
                return BaseResponse.Success(response);
            }
            else
            {
                return BaseResponse.Error("error");
            }
        }

        [HttpGet("ViewProfile/{id}")]
        [Authorize]
        [ValidateToken]
        public BaseResponse ViewUserInfo(string id)
        {
            User user = _userService.GetInfo(id);
            if (user == null)
            {
                return BaseResponse.Error("error");
            }
            return BaseResponse.Success(user);
        }

        [HttpGet("Logout")]
        [Authorize]
        [ValidateToken]
        public BaseResponse Logout(string? username)
        {
            bool response = _userService.Logout(username);
            if (response)
            {
                return BaseResponse.Success();
            }
            return BaseResponse.Error("error");
        }
    }
}
