using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using OneStopOfficeBE.Constants;
using OneStopOfficeBE.DTOs;
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

        public BaseResponse GetAdmins()
        {
            List<AdminInfoDto> admins = _context.Users
            .Where(u => u.IsAdmin)
            .Select(u => new AdminInfoDto
            {
                UserId = u.UserId,
                FullName = u.FullName
            })
            .ToList();
            return BaseResponse.Success(admins);
        }

        public BaseResponse GetInfo(string id)
        {
            User? user = _context.Users.SingleOrDefault(u => u.UserId == id);
            if (user == null)
            {
                return BaseResponse.Error("User not found", 404);
            }
            return BaseResponse.Success(user);
        }

        public BaseResponse Login(LoginRequestDto loginDto)
        {
            User? user = _context.Users
                // .Include(u => u.staff)
                .SingleOrDefault(
                 s => s.UserId != null && s.UserId == loginDto.userName
                && s.Password != null && s.Password == loginDto.password
                );
            if (user == null)
            {
                return BaseResponse.Error(ErrorMessageConstant.LOGIN_FAILED);
            }

            // bool isAd = false;
            // bool isSuperAd = false;

            // if (user.staff.Count > 0)
            // {
            //     isAd = true;
            //     if (user.staff.First().IsSuperAdmin)
            //     {
            //         isSuperAd = true;
            //     }
            // }


            LoginResponseDto responseData = new LoginResponseDto
            {
                Token = GenerateToken(user),
                Username = loginDto.userName,
                Fullname = user.FullName,
                IsAdmin = user.IsAdmin,
                IsSuperAdmin = user.IsSuperAdmin,
            };
            user.Token = responseData.Token;
            user.IsTokenValid = true;
            _context.Users.Update(user);
            _context.SaveChanges();
            return BaseResponse.Success(responseData);
        }

        public BaseResponse Logout(UserExtracted? userExtracted)
        {
            string? username = userExtracted?.Username;
            var user = _context.Users.SingleOrDefault(u => u.UserId == username);
            if (user == null)
            {
                return BaseResponse.Error("User not found", 404);
            }
            user.IsTokenValid = false;
            _context.Users.Update(user);
            _context.SaveChanges();
            return BaseResponse.Success();
        }

        private string GenerateToken(User user)
        {
            var jwtTokenHandler = new JwtSecurityTokenHandler();
            var secretKeyBytes = Encoding.UTF8.GetBytes(_appSettings.SecretKey);
            // string isAd = "false";
            // string isSuperAd = "false";
            // if (user.staff != null && user.staff.Count > 0)
            // {
            //     isAd = "true";
            //     List<staff> staffList = user.staff.ToList();
            //     if (staffList[0].IsSuperAdmin)
            //     {
            //         isSuperAd = "true";
            //     }
            // }

            var tokenDescription = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[]
                {
                    new Claim("IsAdmin", user.IsAdmin.ToString()),
                    new Claim("IsSuperAdmin", user.IsSuperAdmin.ToString()),
                    new Claim("TokenId", Guid.NewGuid().ToString()),
                    new Claim("Username", user.UserId.ToString()),
                    new Claim("FullName", user.FullName),
                }),
                Expires = DateTime.UtcNow.AddMilliseconds(_appSettings.JwtExpInMs),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(secretKeyBytes), SecurityAlgorithms.HmacSha512Signature)
            };
            var token = jwtTokenHandler.CreateToken(tokenDescription);
            return jwtTokenHandler.WriteToken(token);
        }
    }
}
