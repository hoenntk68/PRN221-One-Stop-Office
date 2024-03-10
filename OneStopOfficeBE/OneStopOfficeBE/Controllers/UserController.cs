using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OneStopOfficeBE.CustomAttributes;
using OneStopOfficeBE.DTOs.Request;
using OneStopOfficeBE.DTOs.Response;
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
            return _userService.Login(loginDto);

        }

        [HttpGet("ViewProfile/{id}")]
        [Authorize]
        [ValidateToken]
        public BaseResponse ViewUserInfo(string id)
        {
            return _userService.GetInfo(id);

        }

        [HttpGet("Logout")]
        [Authorize]
        [ValidateToken]
        public BaseResponse Logout(string? username)
        {
            return _userService.Logout(username);
        }
    }
}
