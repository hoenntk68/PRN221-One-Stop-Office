using OneStopOfficeBE.DTOs.Request;
using OneStopOfficeBE.DTOs.Response;

namespace OneStopOfficeBE.Services
{
    public interface RequestService
    {
        BaseResponse getRequest(string id);

        BaseResponse submitRequest(SubmitRequestDto submitRequest);

        //BaseResponse updateRequest(string id);

        BaseResponse cancelRequest(string id);

        BaseResponse GetRequestByUsername(string username);
    }
}
