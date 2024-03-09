using Newtonsoft.Json;
using OneStopOfficeBE.Constants;
using System.Text.Json.Serialization;

namespace OneStopOfficeBE.DTOs.Response
{
    public class BaseResponse
    {
        public int ResponseCode { get; set; }
        public bool IsSuccess { get; set; }
        public string ErrorMessage { get; set; }
        public object Data { get; set; }

        private BaseResponse(int responseCode, bool isSuccess, string errorMessage, object data)
        {
            ResponseCode = responseCode;
            IsSuccess = isSuccess;
            ErrorMessage = errorMessage;
            Data = data;
        }

        public static BaseResponse Success(object data = null, int responseCode = 200)
        {
            return new BaseResponse(responseCode, true, null, data);
        }

        public static BaseResponse Error(string errorMessage, int responseCode = 400)
        {
            return new BaseResponse(responseCode, false, errorMessage, null);
        }
    }

}
