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
    private readonly IConfiguration configuration;

    public AuthController(IConfiguration configuration) // Constructor Dependency Injection
    {
      this.configuration = configuration;
    }

   
    [HttpPost]
    public IActionResult Login([FromBody] LoginRequest request)
    {
      // Implement your login logic here
      if (request.Username == "admin" && request.Password == "password")
      {
        // create claims and sign in with a persistent cookie that expires in 60 seconds
        var claims = new List<Claim>
                {
                    new Claim(ClaimTypes.Name, "admin"),
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
   private string CreateToken(List<Claim> claims, DateTime expiresAt)
{
   var claimsDec = new Dictionary<string , object>();
   if(claims is not null && claims.Count > 0)
   {
       foreach (var claim in claims)
       {
           claimsDec.Add(claim.Type, claim.Value);
       }
   }
    var tokenDescriptor = new SecurityTokenDescriptor
    {
        Claims = claimsDec,
        Expires = expiresAt,
        NotBefore = DateTime.UtcNow,
        SigningCredentials = new SigningCredentials(
            new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["SuperSecretKey"] ?? string.Empty)),
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