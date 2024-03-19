using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OneStopOfficeBE.CustomAttributes;
using OneStopOfficeBE.DTOs;
using OneStopOfficeBE.DTOs.Request;
using OneStopOfficeBE.DTOs.Response;
using OneStopOfficeBE.Services;
using OneStopOfficeBE.Utils;
using System.Text.Json;

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

        [ValidateToken]
        [HttpGet("Logout")]
        [Authorize]
        public BaseResponse Logout(string? jsonClaims)
        {
            UserExtracted user = JwtHelper.extractUser(jsonClaims);
            return _userService.Logout(user);
        }
    }
}
