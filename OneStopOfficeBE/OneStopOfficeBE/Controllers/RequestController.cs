using Microsoft.AspNetCore.Mvc;
using OneStopOfficeBE.CustomAttributes;
using OneStopOfficeBE.DTOs.Request;
using OneStopOfficeBE.DTOs.Response;
using OneStopOfficeBE.Services;

namespace OneStopOfficeBE.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RequestController : Controller
    {
        private RequestService _requestService;

        public RequestController(RequestService requestService)
        {
            _requestService = requestService;
        }

        [HttpGet("request")]
        public BaseResponse Index()
        {
            return _requestService.GetRequest();
        }

        [HttpPost("store")]
        [ValidateToken]
        public BaseResponse Store([FromForm] SubmitRequestDto submitRequest, string? username)
        {
            var token = HttpContext.Request.Headers["Authorization"].FirstOrDefault()?.Split(" ").Last();
            if (string.IsNullOrEmpty(token))
            {
                return BaseResponse.Error("Unauthorized", 401);
            }

            return _requestService.SubmitRequest(submitRequest, username);
        }

        [HttpGet("client/all")]
        [ValidateToken]
        public BaseResponse GetRequestPerUser(string? username)
        {
            if (string.IsNullOrEmpty(username))
            {
                return BaseResponse.Success();
            }

            return _requestService.GetRequestByUsername(username);
        }
    }
}
