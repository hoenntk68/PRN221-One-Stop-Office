using OneStopOfficeBE.DTOs.Request;
using OneStopOfficeBE.DTOs.Response;
using OneStopOfficeBE.Models;

namespace OneStopOfficeBE.Services
{
    public interface RequestService
    {
        List<Request> GetRequest();

        bool SubmitRequest(SubmitRequestDto submitRequest, string username);

        Request UpdateRequest(string id);

        bool CancelRequest(string id);

        List<RequestListResponseDto> GetRequestByUsername(string username);
    }
}
