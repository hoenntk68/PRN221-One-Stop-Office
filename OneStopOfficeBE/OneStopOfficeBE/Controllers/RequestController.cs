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
            List<Request> request = _requestService.GetRequest();

            return BaseResponse.Success(request);
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

            bool response = _requestService.SubmitRequest(submitRequest, username);

            return response ? BaseResponse.Success() : BaseResponse.Error("error");
        }

        [HttpGet("client/all")]
        [ValidateToken]
        public BaseResponse GetRequestPerUser(string? username)
        {
            if (string.IsNullOrEmpty(username))
            {
                return BaseResponse.Success();
            }
            List<RequestListResponseDto> request = _requestService.GetRequestByUsername(username);

            return BaseResponse.Success(request);
        }
    }
}
