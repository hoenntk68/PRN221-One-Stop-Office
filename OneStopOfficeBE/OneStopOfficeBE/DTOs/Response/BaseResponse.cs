using Newtonsoft.Json;
using OneStopOfficeBE.Constants;
using System.Text.Json.Serialization;

namespace OneStopOfficeBE.DTOs.Response
{
    public class BaseResponse
    {
        [JsonPropertyName("success")]
        public bool Success { get; set; }

        [JsonPropertyName("code")]
        public int Code { get; set; }

        [JsonPropertyName("message")]
        public string Message { get; set; }

        [JsonPropertyName("data")]
        public object Data { get; set; }

        public static BaseResponse ofSucceeded()
        {
            return new BaseResponse
            {
                Code = 200,
                Success = true,
                Message = ErrorMessageConstant.SUCCESS
            };
        }

        public static BaseResponse ofSucceeded(object data, int responseCode = 200)
        {
            return new BaseResponse
            {
                Code = responseCode,
                Success = true,
                Message = ErrorMessageConstant.SUCCESS,
                Data =  data
            };
        }

        public static BaseResponse ofFailed(string message, object data)
        {
            return new BaseResponse
            {
                Success = false,
                Message = message,
                Data = data
            };
        }

        public static BaseResponse ofFailed(string message)
        {
            return new BaseResponse
            {
                Success = false,
                Message = message,
            };
        }

        public static bool isSucceeded(BaseResponse response)
        {
            return response.Code == 200;
        }
    }
}
