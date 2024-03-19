using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using OneStopOfficeBE.DTOs.Request;
using OneStopOfficeBE.DTOs.Response;
using OneStopOfficeBE.Constants;
using Microsoft.EntityFrameworkCore;
using OneStopOfficeBE.Models;
using OneStopOfficeBE.DTOs;

namespace OneStopOfficeBE.Services.Impl
{
    public class RequestServiceImpl : RequestService
    {
        private readonly PRN221_OneStopOfficeContext _context;

        private readonly AppSettings _appSettings;

        public RequestServiceImpl(PRN221_OneStopOfficeContext context, IOptionsMonitor<AppSettings> appSettings)
        {
            _context = context;
            _appSettings = appSettings.CurrentValue;
        }

        public BaseResponse CancelRequest(string id)
        {
            throw new NotImplementedException();
        }

        public BaseResponse GetRequest()
        {

            return BaseResponse.Success(_context.Requests.ToList());
        }

        public BaseResponse SubmitRequest(SubmitRequestDto submitRequest, UserExtracted? user)
        {
            string? username = user?.Username;
            try
            {
                if (submitRequest == null)
                {
                    return BaseResponse.Error("Request data is empty", 400);
                }

                int category = submitRequest.category;
                string reason = submitRequest.reason;
                IFormFile attachment = submitRequest.attachment;
                string attachmentFilePath = SaveAttachment(attachment);

                var request = new Request
                {
                    CategoryId = category,
                    UserId = username,
                    Reason = reason,
                    Attachment = attachmentFilePath
                };

                _context.Requests.Add(request);
                _context.SaveChanges();

                return BaseResponse.Success();
            }
            catch (Exception ex)
            {
                string errorMessage = $"An error occurred while processing the request: {ex.GetType()}: {ex.Message}\n{ex.StackTrace}";

                return BaseResponse.Error(errorMessage, 500);
            }
        }

        private string SaveAttachment(IFormFile attachment)
        {
            string attachmentFolderPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, RequestConstant.REQUEST_ATTACHMENT_PATH);
            if (!Directory.Exists(attachmentFolderPath))
            {
                Directory.CreateDirectory(attachmentFolderPath);
            }
            string attachmentFileName = Guid.NewGuid().ToString() + "_" + attachment.FileName;
            string attachmentFilePath = Path.Combine(attachmentFolderPath, attachmentFileName);
            using (var stream = new FileStream(attachmentFilePath, FileMode.Create))
            {
                attachment.CopyTo(stream);
            }
            return attachmentFilePath;
        }

        public BaseResponse GetRequestByUsername(UserExtracted? user, int limit, int offset)
        {
            string? username = user?.Username;
            List<Request> requestList = new List<Request>();
            switch (user?.IsAdmin)
            {
                case false:
                    requestList = _context.Requests
                        .Include(r => r.Category)
                        .ThenInclude(c => c.staff)
                        .Where(r => r.UserId == username)
                        .Skip(offset)
                        .Take(limit)
                        .OrderBy(r => r.RequestId)
                        .ToList();
                    break;
                case true:
                    requestList = _context.Requests
                        .Include(r => r.Category)
                        .ThenInclude(c => c.staff)
                        .Where(r => r.UserId != username && r.Category.staff.Any(s => s.UserId == username))
                        .Skip(offset)
                        .Take(limit)
                        .OrderBy(r => r.RequestId)
                        .ToList();
                    break;

            }
            List<RequestListResponseDto> responseList = requestList.Select(item =>
                new RequestListResponseDto
                {
                    Id = item.RequestId,
                    Category = item.Category.CategoryName,
                    Reason = item.Reason,
                    ProcessNote = item.ProcessNote,
                    ProcessedAt = null,
                    File = item.Attachment,
                    Status = "Submitted"
                }
            ).ToList();
            return BaseResponse.Success(responseList);
        }

        public BaseResponse UpdateRequest(string id)
        {
            throw new NotImplementedException();
        }
    }
}
