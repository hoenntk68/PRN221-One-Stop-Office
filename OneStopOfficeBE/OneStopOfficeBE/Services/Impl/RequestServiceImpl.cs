using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using OneStopOfficeBE.DTOs.Request;
using OneStopOfficeBE.DTOs.Response;
using OneStopOfficeBE.Constants;
using Microsoft.EntityFrameworkCore;
using OneStopOfficeBE.Models;
using OneStopOfficeBE.DTOs;
using OneStopOfficeBE.Utils;
using System.Data;
using ClosedXML.Excel;

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
                    Attachment = attachmentFilePath,
                    Status = StatusEnum.Submitted.ToString(),
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

        public IActionResult DownloadAttachment(int id)
        {
            Request? request = _context.Requests.FirstOrDefault(r => r.RequestId == id);
            if (request == null)
            {
                return new NotFoundResult();
            }
            string attachmentPath = request.Attachment;
            if (!File.Exists(attachmentPath))
            {
                return new NotFoundResult();
            }
            string attachmentName = Path.GetFileName(attachmentPath);
            string attachmentType = "application/octet-stream";
            return new PhysicalFileResult(attachmentPath, attachmentType)
            {
                FileDownloadName = attachmentName
            };
        }

        public BaseResponse GetRequestByUsername(UserExtracted? user, int limit, int offset, string status, string sortBy = "created_at", string sortOption = "asc")
        {
            List<RequestListResponseDto> responseList = GetRequestByUsername(user, status)
                .Skip(offset)
                .Take(limit)
                .ToList();
            switch (sortOption)
            {
                case "desc":
                    responseList = responseList.OrderByDescending(item => item.SubmittedAt).ToList();
                    break;
                case "asc":
                    responseList = responseList.OrderBy(item => item.SubmittedAt).ToList();
                    break;
                default: break;
            }
            return BaseResponse.Success(responseList);
        }


        public BaseResponse UpdateRequest(string id)
        {
            throw new NotImplementedException();
        }

        public BaseResponse GetRequestDetail(int id)
        {
            Request? request = _context.Requests
                .Include(r => r.User)
                .Include(r => r.Category)
                .FirstOrDefault(r => r.RequestId == id);
            if (request == null)
            {
                return BaseResponse.Error(ErrorMessageConstant.REQUEST_NOT_FOUND);
            }
            RequestDetailResponse response = new RequestDetailResponse()
            {
                RequestId = request.RequestId,
                UserId = request.UserId,
                UserFullName = request.User.FullName,
                CategoryId = request.CategoryId,
                CategoryName = request.Category.CategoryName,
                Reason = request.Reason,
                ProcessNote = request.ProcessNote,
                Attachment = request.Attachment,
                CreatedAt = request.CreatedAt,
                UpdateAt = request.UpdateAt,
                Status = request.Status
            };
            return BaseResponse.Success(response);
        }

        public BaseResponse UpdateRequestStatus(UpdateStatusRequest request, UserExtracted? user)
        {
            Request? requestFound = null;
            if ((bool)user.IsAdmin)
            {
                // if ()
                // check if user is entitled to change request status
                requestFound = _context.Requests
                        .Include(r => r.Category)
                        // .ThenInclude(c => c.staff)
                        .Where(r => r.UserId != user.Username && r.AssignedToNavigation.UserId == user.Username)
                        .FirstOrDefault(r => r.RequestId == request.RequestId);
                if (requestFound == null)
                {
                    return BaseResponse.Error(ErrorMessageConstant.UNAUTHORIZED, 401);
                }
            }
            else
            {
                requestFound = _context.Requests
                    .FirstOrDefault(r => request.RequestId == r.RequestId && r.UserId == user.Username);
                if (requestFound == null)
                {
                    return BaseResponse.Error(ErrorMessageConstant.UNAUTHORIZED, 401);
                }
                if (request.Status != StatusEnum.Cancelled.ToString())
                {
                    return BaseResponse.Error(ErrorMessageConstant.UNAUTHORIZED, 401);
                }
            }
            if (!EnumHelper.IsValidStatus(request.Status))
            {
                return BaseResponse.Error(ErrorMessageConstant.INVALID_STATUS);
            }
            requestFound.Status = request.Status;
            // update process note if user is admin
            requestFound.ProcessNote = (bool)user.IsAdmin ? request.ProcessNote : requestFound.ProcessNote;
            requestFound.UpdateAt = DateTime.UtcNow;
            _context.Requests.Update(requestFound);
            _context.SaveChanges();
            return BaseResponse.Success(new Request()
            {
                RequestId = request.RequestId,
                Status = request.Status,
            });
        }

        public List<RequestListResponseDto> GetRequestByUsername(UserExtracted? user, string status)
        {
            string? username = user?.Username;
            List<Request> requestList = _context.Requests
                .Include(r => r.AssignedToNavigation)
                .Include(r => r.Category)
                .ToList();
            switch (user?.IsAdmin)
            {
                case false:
                    requestList = requestList
                        .Where(r => r.UserId == username && r.Status == status)
                        .ToList();
                    break;
                case true:
                    switch ((bool)user?.IsSuperAdmin)
                    {
                        case true:
                            requestList = requestList
                                .Where(r => r.Status == status || status == null)
                                .ToList();
                            break;
                        case false:
                            requestList = requestList
                                .Where(r => r.UserId != username && r.AssignedTo == user.Username && (r.Status == status || status == null))
                                .ToList();
                            break;
                    }
                    break;
            }

            List<RequestListResponseDto> responseList = requestList.Select(item =>
                new RequestListResponseDto
                {
                    Id = item.RequestId,
                    Category = item.Category.CategoryName,
                    Reason = item.Reason,
                    ProcessNote = item.ProcessNote,
                    SubmittedAt = item.CreatedAt,
                    ProcessedAt = item.UpdateAt,
                    File = item.Attachment,
                    Status = item.Status,
                }).ToList();
            return responseList;
        }

        public DataTable GetData()
        {
            DataTable dataTable = new DataTable
            {
                TableName = "Requests"
            };
            dataTable.Columns.Add("Id", typeof(int));
            dataTable.Columns.Add("Category", typeof(string));
            dataTable.Columns.Add("Reason", typeof(string));
            dataTable.Columns.Add("Created Time", typeof(DateTime));
            dataTable.Columns.Add("Status", typeof(string));
            dataTable.Columns.Add("Process Note", typeof(string));
            dataTable.Columns.Add("Last Processed", typeof(DateTime));

            List<Request> list = _context.Requests
                .Include(r => r.Category)
                .ToList();
            if (list.Count > 0)
            {
                list.ForEach(item =>
                {
                    dataTable.Rows.Add(
                        item.RequestId,
                        item.Category.CategoryName,
                        item.Reason,
                        item.CreatedAt,
                        item.Status,
                        item.ProcessNote,
                        item.UpdateAt
                    );
                });
            }
            return dataTable;
        }

    }
}
