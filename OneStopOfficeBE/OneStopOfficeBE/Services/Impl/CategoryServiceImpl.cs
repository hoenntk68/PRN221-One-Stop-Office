using Microsoft.Extensions.Options;
using OneStopOfficeBE.DTOs.Response;
using OneStopOfficeBE.Models;

namespace OneStopOfficeBE.Services.Impl
{
    public class CategoryServiceImpl : CategoryService
    {
        private readonly PRN221_OneStopOfficeContext _context;

        private readonly AppSettings _appSettings;
        public CategoryServiceImpl(PRN221_OneStopOfficeContext context, IOptionsMonitor<AppSettings> appSettings)
        {
            _context = context;
            _appSettings = appSettings.CurrentValue;
        }

        public BaseResponse GetCategories()
        {
            List<Category> categories = _context.Categories.ToList();
            List<CategoryInfoResponse> response = categories.Select(c => new CategoryInfoResponse
            {
                CategoryId = c.CategoryId,
                CategoryName = c.CategoryName,
            }).ToList();
            return BaseResponse.Success(response);
        }
    }
}
