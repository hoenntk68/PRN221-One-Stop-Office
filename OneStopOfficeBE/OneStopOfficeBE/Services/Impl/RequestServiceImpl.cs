using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using OneStopOfficeBE.DTOs.Request;
using OneStopOfficeBE.DTOs.Response;
using OneStopOfficeBE.Models;
using OneStopOfficeBE.Constants;
using System.Security.Cryptography.X509Certificates;
using Microsoft.EntityFrameworkCore;

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

        public BaseResponse cancelRequest(string id)
        {
            throw new NotImplementedException();
        }

        public BaseResponse getRequest(string id)
        {
            return BaseResponse.ofSucceeded(_context.Requests.ToList());
        }

        public BaseResponse submitRequest(SubmitRequestDto submitRequest)
        {
            try
            {
                if (submitRequest == null)
                {
                    return BaseResponse.ofFailed("Invalid request data.");
                }

                int category = submitRequest.category;
                string reason = submitRequest.reason;
                IFormFile attachment = submitRequest.attachment;
                string attachmentFilePath = SaveAttachment(attachment);

                var request = new Request
                {
                    CategoryId = category,
                    Reason = reason,
                    Attachment = attachmentFilePath
                };

                _context.Requests.Add(request);
                _context.SaveChanges();

                return BaseResponse.ofSucceeded(request);
            }
            catch (Exception ex)
            {
                string errorMessage = $"An error occurred while processing the request: {ex.GetType()}: {ex.Message}\n{ex.StackTrace}";

                return BaseResponse.ofFailed(errorMessage);
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

        public BaseResponse GetRequestByUsername(string username)
        {
            List<Request> requestList = _context.Requests
                .Include(r => r.Category)
                .Where(r => r.UserId == username)
                .ToList();
            List<RequestListResponseDto> responseList = requestList.Select(item =>
                new RequestListResponseDto
                {
                    Id = item.RequestId,
                    Category = item.Category.CategoryName,
                    SubmittedAt = DateTime.Now,
                    Status = "Submitted"
                }
            ).ToList();
            return BaseResponse.ofSucceeded(responseList);
        }
    }
}
