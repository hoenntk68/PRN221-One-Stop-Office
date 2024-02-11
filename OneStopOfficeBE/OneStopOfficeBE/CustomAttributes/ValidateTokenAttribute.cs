using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using OneStopOfficeBE.Models;
using System.IdentityModel.Tokens.Jwt;

namespace OneStopOfficeBE.CustomAttributes
{
    public class ValidateTokenAttribute : ActionFilterAttribute
    {
        //private readonly PRN221_OneStopOfficeContext _context;
        //public ValidateTokenAttribute(PRN221_OneStopOfficeContext context)
        //{
        //    _context = context;
        //}
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            //base.OnActionExecuting(context);
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

                    // Set the username in the request's Items collection
                    //context.HttpContext.Items["username"] = username;
                    PRN221_OneStopOfficeContext _context = new PRN221_OneStopOfficeContext();
                    var user = _context.Users.FirstOrDefault(u => u.UserId == username);
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
                    if (!(bool)user.IsTokenValid)
                    {
                        context.Result = new UnauthorizedResult();
                        return;
                    }
                }
                catch (Exception ex)
                {
                    // Handle Token decoding errors
                    // For example, log the error or return an error response
                    Console.WriteLine($"Error decoding JWT token: {ex.Message}");
                }
            }
        }
    }
}
