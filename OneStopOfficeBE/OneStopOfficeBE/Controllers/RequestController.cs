﻿using ClosedXML.Excel;
using Microsoft.AspNetCore.Mvc;
using OneStopOfficeBE.Constants;
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

        [HttpGet("{id}/download")]
        public IActionResult DownloadAttachment(int id)
        {
            return _requestService.DownloadAttachment(id);
        }

        [HttpGet("client/all")]
        [ValidateToken]
        public BaseResponse GetRequestPerUser(
            string? jsonClaims,
            [FromQuery] int limit = 10,
            [FromQuery] int offset = 0,
            [FromQuery] string? status = "Submitted",
            [FromQuery] string? sortBy = "created_at",
            [FromQuery] string? sortOption = "asc",
            [FromQuery] string? categoryId = "0"
        )
        {
            UserExtracted? user = JwtHelper.extractUser(jsonClaims);
            if (user == null)
            {
                return BaseResponse.Error("Unauthorized", 401);
            }
            int cateId = int.Parse(categoryId);
            return _requestService.GetRequestByUsername(user, limit, offset, status, cateId, sortBy, sortOption);
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
            if (jsonClaims == null)
            {
                return BaseResponse.Error(ErrorMessageConstant.UNAUTHORIZED, 401);
            }
            UserExtracted? user = JwtHelper.extractUser(jsonClaims);
            if (user == null)
            {
                return BaseResponse.Error(ErrorMessageConstant.UNAUTHORIZED, 401);
            }
            return _requestService.UpdateRequestStatus(request, user);
        }

        [HttpGet("export")]
        [ValidateToken]
        public ActionResult ExportRequest(
            string? jsonClaims,
            [FromQuery] string? status = "Submitted",
            [FromQuery] string? sortBy = "created_at",
            [FromQuery] string? sortOption = "asc",
            [FromQuery] string? categoryId = "0"
        )
        {
            if (jsonClaims == null)
            {
                return Unauthorized(ErrorMessageConstant.UNAUTHORIZED);
            }
            UserExtracted? user = JwtHelper.extractUser(jsonClaims);
            if (user == null)
            {
                return Unauthorized(ErrorMessageConstant.UNAUTHORIZED);
            }
            int cateId = int.Parse(categoryId);
            string fileName = "Request_" + DateTime.Now.ToString("yyyyMMddHHmmss") + ".xlsx";
            var table = _requestService.GetData(user, status, cateId, sortBy, sortOption);
            using (XLWorkbook workbook = new XLWorkbook())
            {
                workbook.AddWorksheet(table, "Request Report");
                using (MemoryStream stream = new MemoryStream())
                {
                    workbook.SaveAs(stream);
                    return File(stream.ToArray(), "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", fileName);
                }
            }
        }

        [HttpPatch("assign")]
        [ValidateToken]
        public BaseResponse AssignRequests(string? jsonClaims, AssignRequestDto request)
        {
            if (jsonClaims == null)
            {
                return BaseResponse.Error(ErrorMessageConstant.UNAUTHORIZED);
            }
            UserExtracted? user = JwtHelper.extractUser(jsonClaims);
            if (user == null || !(bool)user.IsSuperAdmin)
            {
                return BaseResponse.Error(ErrorMessageConstant.UNAUTHORIZED);
            }
            return _requestService.AssignRequests(request);
        }
    }
}
