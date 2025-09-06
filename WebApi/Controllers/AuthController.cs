using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.IdentityModel.JsonWebTokens;



namespace WebApi.Controllers
{
  [ApiController]
  [Route("[controller]")]
  public class AuthController : ControllerBase
  {
    private readonly IConfiguration _configuration;

    public AuthController(IConfiguration configuration)
    {
      _configuration = configuration;
    }

   
    [HttpPost("login")]
    public IActionResult Login([FromBody] LoginRequest request)
    {
      // Implement your login logic here
      if (request.Username == "admin" && request.Password == "password")
      {
        // create claims and sign in with a persistent cookie that expires in 60 seconds
        var claims = new List<Claim>
                {
                    new Claim(ClaimTypes.Name, request.Username),
                    new Claim(ClaimTypes.Email, "admin@domain.local"),
                    new Claim("Department", "HR"),
                    new Claim("manager", "true"),
                    new Claim("EmploymentDate", "2025-01-01")
                };

        var expiresAt = DateTime.UtcNow.AddMinutes(1);


        return Ok(new
        {
          access_token = CreateToken(claims, expiresAt),
          expires_at = expiresAt
        });
      }
      ModelState.AddModelError("Unauthorized", "Invalid login.");
      var problemDetails = new ValidationProblemDetails(ModelState)
      {
        Title = "Invalid login",
        Status = StatusCodes.Status401Unauthorized,

      };
      return Unauthorized(problemDetails);
    }
   private string CreateToken(IEnumerable<Claim> claims, DateTime expiresAt)
{
    var claimsDic = claims
        .Where(c => c.Type == ClaimTypes.Name)
        .ToDictionary(c => c.Type, c => (object)c.Value);

    var tokenDescriptor = new SecurityTokenDescriptor
    {
        Claims = claimsDic,
        Expires = expiresAt,
        NotBefore = DateTime.UtcNow,
        SigningCredentials = new SigningCredentials(
            new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["SuperSecretKey"] ?? string.Empty)),
            SecurityAlgorithms.HmacSha256Signature)
    };

    var tokenHandler = new JsonWebTokenHandler();
    return tokenHandler.CreateToken(tokenDescriptor);
}




    public class LoginRequest
    {
      public string Username { get; set; } = string.Empty;
      public string Password { get; set; } = string.Empty;
    }
  }
}