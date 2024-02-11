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

        [HttpGet("Logout/{id}")]
        [Authorize]
        public BaseResponse Logout(string id)
        {
            return _userService.Logout(id);
        }
    }
}
