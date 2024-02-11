using OneStopOfficeBE.Constants;

namespace OneStopOfficeBE.DTOs.Response
{
    public class BaseResponse
    {
        public bool Success { get; set; }
        public string Message { get; set; }
        public object Data { get; set; }

        public static BaseResponse ofSucceeded()
        {
            return new BaseResponse
            {
                Success = true,
                Message = ErrorMessageConstant.SUCCESS
            };
        }

        public static BaseResponse ofSucceeded(object data)
        {
            return new BaseResponse
            {
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
    }
}
