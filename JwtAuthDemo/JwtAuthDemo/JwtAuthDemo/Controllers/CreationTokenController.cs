using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;

namespace JwtAuthDemo.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CreationTokenController : ControllerBase
    {

        public CreationTokenController()
        {
            
        }

        [HttpGet]
        public string Get()
        {
            var token = GenerateJwtToken();
            var cookieOptions = new CookieOptions
            {
                HttpOnly = true,
                Secure = true, // Use true in production
                SameSite = SameSiteMode.None,
                Expires = DateTime.UtcNow.AddHours(1)

            };
            Response.Cookies.Append("token", token, cookieOptions);
            Response.Headers.Append("Authorization", "Bearer " + token);
            var authString = "jwt token";
            Request.Headers["Authorization"] = $"Bearer {authString}";

            Response.Headers.Authorization.Append("Bearer " + token);
            return  "TOKEN " + token  + " test it on values";

        }


        private string GenerateJwtToken()
        {
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("your_secret_keyyour_secret_keyyour_secret_keyyour_secret_keyyour_secret_keyyour_secret_keyyour_secret_keyyour_secret_keyyour_secret_key"));
            //        new SymmetricSecurityKey(Encoding.UTF8.GetBytes("your_secret_keyyour_secret_keyyour_secret_keyyour_secret_keyyour_secret_keyyour_secret_keyyour_secret_keyyour_secret_keyyour_secret_key"))
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                issuer: "your_issuer",
                audience: "your_audience",
                claims: new List<Claim>(),
                expires: DateTime.Now.AddMinutes(30),
                signingCredentials: creds);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

    }
}
