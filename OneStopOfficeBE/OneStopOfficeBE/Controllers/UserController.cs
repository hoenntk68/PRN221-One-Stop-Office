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

        public UserController(UserService userService) {
            _userService = userService;
        }

        [HttpPost("Login")]
        public IActionResult Login(LoginRequestDto loginDto)
        {
            BaseResponse response = _userService.Login(loginDto);
            if (BaseResponse.isSucceeded(response))
            {
                return Ok(response);
            }
            return BadRequest(response);
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
        public IActionResult Logout(string? username)
        {
            BaseResponse response = _userService.Logout(username);
            if (BaseResponse.isSucceeded(response))
            {
                return Ok(response);
            }
            return BadRequest(response);
        }
    }
}
