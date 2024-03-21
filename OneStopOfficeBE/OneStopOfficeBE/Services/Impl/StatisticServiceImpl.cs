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
            .Select(u => new EfficientStaffDto
            {
                UserId = u.UserId,
                FullName = u.FullName,
                RequestCount = u.RequestAssignedToNavigations.Count
            })
            .OrderByDescending(item => item.RequestCount)
            .Take((int)number)
            .ToList();
            return BaseResponse.Success(list);

        }
    }
}