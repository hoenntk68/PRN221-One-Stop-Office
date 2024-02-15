using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using OneStopOfficeBE.DTOs.Request;
using OneStopOfficeBE.DTOs.Response;
using OneStopOfficeBE.Models;
using System.Security.Cryptography.X509Certificates;

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
            //return BaseResponse.ofSucceeded(new { Name = "test", file = "test file", reason = "reson" });
        }

        public BaseResponse submitRequest(SubmitRequestDto submitRequest)
        {
            return BaseResponse.ofSucceeded(submitRequest);
        }
    }
}
