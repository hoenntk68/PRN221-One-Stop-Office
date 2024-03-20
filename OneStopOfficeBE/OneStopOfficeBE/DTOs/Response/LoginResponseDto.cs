namespace OneStopOfficeBE.DTOs.Response
{
    public class LoginResponseDto
    {
        public string Token { get; set; }

        public string Username { get; set; }   

        public string Fullname { get; set; }

        public bool IsAdmin { get; set; }

        public bool IsSuperAdmin { get; set;}
    }
}
