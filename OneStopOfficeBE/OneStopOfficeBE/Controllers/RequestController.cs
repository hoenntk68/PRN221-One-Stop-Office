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
            string id = "1";

            return _requestService.getRequest(id);
        }

        [HttpPost("store")]
        public BaseResponse Store([FromForm] SubmitRequestDto submitRequest)
        {
            return _requestService.submitRequest(submitRequest);
        }

        [HttpGet("client/all")]
        [ValidateToken]
        public BaseResponse GetRequestPerUser(string? username) 
        {
            if (string.IsNullOrEmpty(username))
            {
                return BaseResponse.ofSucceeded();
            }
            return _requestService.GetRequestByUsername(username);
        }
    }
}
