using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using OneStopOfficeBE.Constants;
using OneStopOfficeBE.DTOs.Request;
using OneStopOfficeBE.DTOs.Response;
using OneStopOfficeBE.Models;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace OneStopOfficeBE.Services.Impl
{
    public class UserServiceImpl : UserService
    {
        private readonly PRN221_OneStopOfficeContext _context;

        private readonly AppSettings _appSettings;
        public UserServiceImpl(PRN221_OneStopOfficeContext context, IOptionsMonitor<AppSettings> appSettings)
        {
            _context = context;
            _appSettings = appSettings.CurrentValue;
        }
        public BaseResponse Login(LoginRequestDto loginDto)
        {
            staff? staff = _context.staff
                .Include(s => s.User)
                .SingleOrDefault(
                 s => s.UserId != null && s.UserId == loginDto.userName
                && s.Password != null && s.Password == loginDto.password
                );
            if (staff == null)
            {
                return BaseResponse.ofFailed(ErrorMessageConstant.LOGIN_FAILED);
            }

            LoginResponseDto responseData = new LoginResponseDto
            {
                token = GenerateToken(staff)
            };
            return BaseResponse.ofSucceeded(responseData);
            //return BaseResponse.ofSucceeded();
        }

        private string GenerateToken(staff staff)
        {
            var jwtTokenHandler = new JwtSecurityTokenHandler();
            var secretKeyBytes = Encoding.UTF8.GetBytes(_appSettings.SecretKey);
            var tokenDescription = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[]
                {
                    new Claim("IsSuperAdmin", staff.IsSuperAdmin.ToString()),
                    new Claim("TokenId", Guid.NewGuid().ToString())
                }),
                Expires = DateTime.UtcNow.AddSeconds(30),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(secretKeyBytes), SecurityAlgorithms.HmacSha512Signature)
            };
            var token = jwtTokenHandler.CreateToken(tokenDescription);
            return jwtTokenHandler.WriteToken(token);
        }
    }
}
