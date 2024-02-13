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

        public BaseResponse GetInfo(string id)
        {
            User? user = _context.Users.SingleOrDefault(u => u.UserId == id);
            if (user == null)
            {
                return BaseResponse.ofFailed(ErrorMessageConstant.USER_NOT_FOUND);
            }
            return BaseResponse.ofSucceeded(user);
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
                Token = GenerateToken(staff),
                Username = loginDto.userName,
                Fullname = staff.User.FullName
            };
            var user = staff.User;
            user.Token = responseData.Token;
            user.IsTokenValid = true;
            _context.Users.Update(user);
            _context.SaveChanges();
            return BaseResponse.ofSucceeded(responseData);
        }

        public BaseResponse Logout(string id)
        {
            var user = _context.Users.SingleOrDefault(u => u.UserId == id);
            if (user == null)
            {
                return BaseResponse.ofFailed(ErrorMessageConstant.UNAUTHORIZED);
            }
            user.IsTokenValid = false;
            _context.Users.Update(user);
            _context.SaveChanges();
            return BaseResponse.ofSucceeded();
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
                    new Claim("TokenId", Guid.NewGuid().ToString()),
                    new Claim("Username", staff.User.UserId.ToString()),
                }),
                Expires = DateTime.UtcNow.AddMilliseconds(_appSettings.JwtExpInMs),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(secretKeyBytes), SecurityAlgorithms.HmacSha512Signature)
            };
            var token = jwtTokenHandler.CreateToken(tokenDescription);
            return jwtTokenHandler.WriteToken(token);
        }
    }
}
