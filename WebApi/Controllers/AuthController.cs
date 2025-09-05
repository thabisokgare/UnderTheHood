using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers;

[ApiController]
[Route("api/[controller]")]

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
    var claimsDic = new Dictionary<string, object>();

    if (claims is not ull && claims.Count > 0)
    {

      foreach (var claim in claims)
      {
        if (claim.Type == ClaimTypes.Name)
        {
          claimsDic.Add(claim.Type, claim.Value);
        }
      }
    }



    var TokenDescriptor = new SecurityTokenDescriptor
    {
      Claims = claimsDic,
      SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["SuperSecretKey"]??string.Empty)),
       SecurityAlgorithms.HmacSha256Signature)
    };
    // Implement your token creation logic here
    var tokenHandler = new JsonWebTokenHandler();
    tokenHandler.CreateToken(TokenDescriptor);
    return token;
  }

}