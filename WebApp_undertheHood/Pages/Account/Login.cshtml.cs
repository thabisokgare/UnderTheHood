using System.ComponentModel.DataAnnotations;
using System.Security.Claims;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace WebApp_undertheHood.Pages.Account
{
    public class LoginModel : PageModel
    {
        [BindProperty]
        public Credential credential { get; set; } = new Credential();
        public void OnGet()
        {
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }
            // verify credentials
            if (credential.UserName == "admin" && credential.Password == "password")
            {
                // create claims and sign in with a persistent cookie that expires in 60 seconds
                var claims = new List<Claim>
                {
                    new Claim(ClaimTypes.Name, "admin"),
                    new Claim(ClaimTypes.Email, "admin@domain.local"),
                    new Claim("Department", "HR"),
                    new Claim("manager", "true"),
                    new Claim("Manger", "true"),
                    new Claim("EmploymentDate", "2025-01-01")
                };

                var identity = new ClaimsIdentity(claims, "MyCookieAuth");
                var principal = new ClaimsPrincipal(identity);

                var props = new AuthenticationProperties
                {
                    IsPersistent = true, 
                    ExpiresUtc = DateTimeOffset.UtcNow.AddSeconds(60),
                    AllowRefresh = false
                };

                await HttpContext.SignInAsync("MyCookieAuth", principal, props);

                return LocalRedirect(Url.Content("~/"));
            }

            ModelState.AddModelError(string.Empty, "Invalid login.");
            return Page();
        }
    }

    public class Credential
    {
        [Required]
        [Display(Description = "User Name")]
        public string UserName { get; set; } = string.Empty; // Changed to PascalCase

        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; } = string.Empty;
    }
}
