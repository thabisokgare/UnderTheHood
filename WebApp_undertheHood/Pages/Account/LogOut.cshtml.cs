using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace WebApp_undertheHood.Pages.Account
{
    public class LogoutModel : PageModel
    {
        public void OnGet() { }

        public async Task<IActionResult> OnPostAsync()
        {
            // must match the scheme used in Program.cs / SignInAsync
            await HttpContext.SignOutAsync("MyCookieAuth");

            // ensure browser cookie is removed
            Response.Cookies.Delete("MyCookieAuth");

            return RedirectToPage("/Index");
        }
    }
}
