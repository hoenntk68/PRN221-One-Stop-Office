using OneStopOfficeBE.DTOs.Response;
using OneStopOfficeBE.Models;
using OneStopOfficeBE.Utils;

namespace OneStopOfficeBE.Services.Impl
{
    public class StatisticServiceImpl : StatisticService
    {
        private readonly PRN221_OneStopOfficeContext _context;
        public StatisticServiceImpl(PRN221_OneStopOfficeContext context)
        {
            _context = context;
        }

        public BaseResponse RequestCountPerCategory()
        {
            List<RequestPerCategoryDto> list = _context.Categories
            .Select(c =>
                new RequestPerCategoryDto
                {
                    CategoryId = c.CategoryId,
                    CategoryName = c.CategoryName,
                    RequestCount = c.Requests.Count
                }
            )
            // .OrderByDescending(item => item.RequestCount)
            .ToList();
            return BaseResponse.Success(list);
        }

        public BaseResponse EfficientStaffs(int? number)
        {
            List<EfficientStaffDto> list = _context.Users
            .Where(u => u.IsAdmin && !u.IsSuperAdmin)
            .Select(u => new EfficientStaffDto
            {
                UserId = u.UserId,
                FullName = u.FullName,
                RequestCount = u.RequestAssignedToNavigations.Count
            })
            .OrderByDescending(item => item.RequestCount)
            .ToList();
            if (number != null)
            {
                list = list.Take((int)number).ToList();
            }
            return BaseResponse.Success(list);

        }

        public BaseResponse GeneralStats()
        {
            GeneralStatsDto stats = new GeneralStatsDto()
            {
                RequestCount = _context.Requests.Count(),
                UserSystemCount = _context.Users.Count(),
                AdminCount = _context.Users.Where(u => u.IsAdmin).Count(),
                ClientCount = _context.Users.Where(u => !u.IsAdmin).Count(),
                CategoryCount = _context.Categories.Count()
            };
            return BaseResponse.Success(stats);
        }

        public BaseResponse RequestCountByStatus()
        {
            List<RequestCountByStatus> list = _context.Requests.GroupBy(r => r.Status)
            .Select(s => new RequestCountByStatus
            {
                Status = s.Key,
                RequestCount = s.Count()
            }).ToList();
            return BaseResponse.Success(list);
        }

        public BaseResponse ProcessTimeStats()
        {
            List<RequestProcessTimeDto> requests = _context.Requests
                .Where(r => r.CreatedAt != r.UpdateAt)
                .Select(r => new RequestProcessTimeDto()
                {
                    RequestId = r.RequestId,
                    ProcessTime = (TimeSpan)(r.UpdateAt - r.CreatedAt),
                })
                .ToList();

            TimeSpan? avgTime = requests.Any() ? TimeSpan.FromTicks((long)requests.Average(r => r.ProcessTime.Ticks)) : (TimeSpan?)null;
            RequestProcessTimeDto? avg = new RequestProcessTimeDto() {
                ProcessTime = (TimeSpan)avgTime,
                DayCount = (int)avgTime?.Days,
                HourCount = (int)avgTime?.Hours,
                MinuteCount = (int)avgTime?.Minutes,
            };
            RequestProcessTimeDto? min = requests.OrderBy(r => r.ProcessTime).FirstOrDefault();
            min = TimeHelper.SetTimeSpanProp(min);
            RequestProcessTimeDto? max = requests.OrderByDescending(r => r.ProcessTime).FirstOrDefault();
            max = TimeHelper.SetTimeSpanProp(max);
            ProcessTimeStatsDto response = new ProcessTimeStatsDto() {
                Avg = avg,
                Max = max,
                Min = min
            };
            return BaseResponse.Success(response);
        }
    }
}