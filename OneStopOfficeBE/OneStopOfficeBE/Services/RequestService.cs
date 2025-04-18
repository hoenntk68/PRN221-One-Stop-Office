﻿using System.Data;
using Microsoft.AspNetCore.Mvc;
using OneStopOfficeBE.DTOs;
using OneStopOfficeBE.DTOs.Request;
using OneStopOfficeBE.DTOs.Response;

namespace OneStopOfficeBE.Services
{
    public interface RequestService
    {
        BaseResponse GetRequest();

        BaseResponse SubmitRequest(SubmitRequestDto submitRequest, UserExtracted? user);

        IActionResult DownloadAttachment(int id);

        BaseResponse UpdateRequest(string id);

        BaseResponse CancelRequest(string id);

        BaseResponse GetRequestByUsername(UserExtracted? user, int limit, int offset, string status, int categoryId, string? sortBy, string? sortOption);

        BaseResponse GetRequestDetail(int id);

        BaseResponse UpdateRequestStatus(UpdateStatusRequest request, UserExtracted? user);

        DataTable GetData(UserExtracted? user, string status, int categoryId, string sortBy = "created_at", string sortOption = "asc");

        BaseResponse AssignRequests(AssignRequestDto request);
        
        BaseResponse ReassignRequests(ReassignRequestDto request);
    }
}
