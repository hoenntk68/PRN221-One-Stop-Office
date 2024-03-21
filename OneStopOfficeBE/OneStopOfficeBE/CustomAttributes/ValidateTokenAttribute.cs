using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.EntityFrameworkCore;
using OneStopOfficeBE.Controllers;
using OneStopOfficeBE.DTOs;
using OneStopOfficeBE.DTOs.Request;
using OneStopOfficeBE.Models;
using System.IdentityModel.Tokens.Jwt;
using System.Text.Json;

namespace OneStopOfficeBE.CustomAttributes
{
    public class ValidateTokenAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            var jwtToken = context.HttpContext.Request.Headers["Authorization"].FirstOrDefault()?.Split(" ").Last();
            if (!string.IsNullOrEmpty(jwtToken))
            {
                try
                {
                    // Decode the JWT Token to get the claims (e.g., username)
                    var tokenHandler = new JwtSecurityTokenHandler();
                    var token = tokenHandler.ReadJwtToken(jwtToken);

                    // Extract the username claim
                    var username = token.Claims.FirstOrDefault(claim => claim.Type == "Username")?.Value;

                    PRN221_OneStopOfficeContext _context = new PRN221_OneStopOfficeContext();
                    User? user = _context.Users
                        // .Include(u => u.staff)
                        .FirstOrDefault(u => u.UserId == username);
                    if (user == null)
                    {
                        context.Result = new UnauthorizedResult();
                        return;
                    }
                    if (user.Token == null)
                    {
                        context.Result = new UnauthorizedResult();
                        return;
                    }
                    if (user.IsTokenValid != null && !(bool)user.IsTokenValid)
                    {
                        context.Result = new UnauthorizedResult();
                        return;
                    }

                    //context.ActionArguments["username"] = username;
                    // bool isAd = false;
                    // bool isSuperAd = false;
                    // if (user.staff != null)
                    // {
                    //     List<staff> staffList = user.staff.ToList();
                    //     if (staffList.Count > 0)
                    //     {
                    //         isAd = true;
                    //         isSuperAd = staffList[0].IsSuperAdmin;
                    //     }
                    // }

                    UserExtracted userExtracted = new UserExtracted()
                    {
                        Username = user.UserId,
                        IsAdmin = user.IsAdmin,
                        IsSuperAdmin = user.IsSuperAdmin,
                    };
                    context.ActionArguments["jsonClaims"] = JsonSerializer.Serialize(userExtracted);

                }
                catch (Exception ex)
                {
                    // Handle Token decoding errors
                    // For example, log the error or return an error response
                    Console.WriteLine($"Error decoding JWT token: {ex.Message}");
                }
            }
                base.OnActionExecuting(context);
        }
    }
}
