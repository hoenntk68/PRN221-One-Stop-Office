using OneStopOfficeBE.DTOs.Request;
using OneStopOfficeBE.DTOs.Response;

namespace OneStopOfficeBE.Services
{
    public interface RequestService
    {
        BaseResponse GetRequest();

        BaseResponse SubmitRequest(SubmitRequestDto submitRequest, string username);

        BaseResponse UpdateRequest(string id);

        BaseResponse CancelRequest(string id);

        BaseResponse GetRequestByUsername(string username);
    }
}
