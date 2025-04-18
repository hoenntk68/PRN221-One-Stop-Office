
using OneStopOfficeBE.DTOs.Response;

namespace OneStopOfficeBE.Services
{
    public interface StatisticService
    {
        BaseResponse RequestCountPerCategory();
        BaseResponse EfficientStaffs(int? take);
        BaseResponse GeneralStats();
        BaseResponse RequestCountByStatus();
        BaseResponse ProcessTimeStats();
    }
}
