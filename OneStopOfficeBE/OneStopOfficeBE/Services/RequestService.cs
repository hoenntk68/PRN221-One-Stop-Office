using OneStopOfficeBE.DTOs;
using OneStopOfficeBE.DTOs.Request;
using OneStopOfficeBE.DTOs.Response;

namespace OneStopOfficeBE.Services
{
    public interface RequestService
    {
        BaseResponse GetRequest();

        BaseResponse SubmitRequest(SubmitRequestDto submitRequest, UserExtracted? user);

        BaseResponse UpdateRequest(string id);

        BaseResponse CancelRequest(string id);

        BaseResponse GetRequestByUsername(UserExtracted? user, int limit, int offset);

        BaseResponse GetRequestDetail(int id);

        BaseResponse UpdateRequestStatus(UpdateStatusRequest request, UserExtracted? user);
    }
}
