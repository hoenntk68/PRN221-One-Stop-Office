﻿using Microsoft.AspNetCore.Mvc;
using OneStopOfficeBE.CustomAttributes;
using OneStopOfficeBE.DTOs;
using OneStopOfficeBE.DTOs.Request;
using OneStopOfficeBE.DTOs.Response;
using OneStopOfficeBE.Services;
using OneStopOfficeBE.Utils;

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
        public BaseResponse Store([FromForm] SubmitRequestDto submitRequest, string? jsonClaims)
        {
            UserExtracted? user = JwtHelper.extractUser(jsonClaims);

            //var token = HttpContext.Request.Headers["Authorization"].FirstOrDefault()?.Split(" ").Last();
            if (user == null)
            {
                return BaseResponse.Error("Unauthorized", 401);
            }

            return _requestService.SubmitRequest(submitRequest, user);
        }

        [HttpGet("client/all")]
        [ValidateToken]
        public BaseResponse GetRequestPerUser(
            string? jsonClaims,
            [FromQuery] int limit = 10,
            [FromQuery] int offset = 0
        )
        {
            UserExtracted? user = JwtHelper.extractUser(jsonClaims);
            if (user == null)
            {
                return BaseResponse.Error("Unauthorized", 401);
            }

            return _requestService.GetRequestByUsername(user, limit, offset);
        }

        [HttpGet("detail/{id}")]
        [ValidateToken]
        public BaseResponse GetRequestDetail(int id = 0)
        {
            if (id == 0)
            {
                return BaseResponse.Error("Invalid request ID", 400);
            }
            return _requestService.GetRequestDetail(id);
        }

        [HttpPost("update")]
        [ValidateToken]
        public BaseResponse UpdateRequestStatus(UpdateStatusRequest request, string? jsonClaims)
        {
            UserExtracted? user = JwtHelper.extractUser(jsonClaims);
            if (user == null)
            {
                return BaseResponse.Error("Unauthorized", 401);
            }
            return _requestService.UpdateRequestStatus(request, user);
        }
    }
}
