namespace OneStopOfficeBE.DTOs.Response
{
    public class RequestPerCategoryDto
    {
        public int CategoryId { get; set; }
        public string CategoryName { get; set; }
        public int RequestCount { get; set; }
    }
}