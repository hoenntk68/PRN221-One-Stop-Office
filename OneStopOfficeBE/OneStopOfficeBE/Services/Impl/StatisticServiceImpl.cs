using OneStopOfficeBE.DTOs.Response;
using OneStopOfficeBE.Models;

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
    }
}