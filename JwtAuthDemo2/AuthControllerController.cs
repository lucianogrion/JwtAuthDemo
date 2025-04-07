using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

[ApiController]
[Route("[controller]")]
public class AuthController : ControllerBase
{
    private readonly string key = "clave-super-secreta-para-demo";

    [HttpPost("login")]
    public IActionResult Login([FromBody] LoginRequest request)
    {
        if (request.Username == "admin" && request.Password == "1234")
        {
            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Sub, request.Username),
                new Claim(JwtRegisteredClaimNames.Email, "admin@demo.com"),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
            };

            var keyBytes = Encoding.UTF8.GetBytes(key);
            var securityKey = new SymmetricSecurityKey(keyBytes);
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                issuer: "MiApp",
                audience: "MisUsuarios",
                claims: claims,
                expires: DateTime.UtcNow.AddMinutes(30),
                signingCredentials: credentials
            );

            var tokenString = new JwtSecurityTokenHandler().WriteToken(token);

            return Ok(new { token = tokenString });
        }

        return Unauthorized();
    }

    [HttpGet("secure")]
    [Microsoft.AspNetCore.Authorization.Authorize]
    public IActionResult SecureEndpoint()
    {
        return Ok(new { message = "Este endpoint está protegido", user = User.Identity?.Name });
    }

    [HttpGet("index")]
    public IActionResult index()
    {
        return Ok(new { message = "Este endpoint NO está protegido", user = User.Identity?.Name });
    }

}

public class LoginRequest
{
    public string Username { get; set; }
    public string Password { get; set; }
}
