using Microsoft.AspNetCore.Mvc;
using OneStopOfficeBE.DTOs.Response;
using OneStopOfficeBE.Services;

namespace OneStopOfficeBE.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StatisticController : Controller
    {
        private readonly StatisticService _statisticService;
        public StatisticController(StatisticService statisticService)
        {
            _statisticService = statisticService;
        }
        
        [HttpGet("request-per-category")]
        public BaseResponse GetRequestPerCategory()
        {
            return _statisticService.RequestCountPerCategory();
        }
    }
}