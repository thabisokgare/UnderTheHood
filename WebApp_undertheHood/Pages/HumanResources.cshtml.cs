using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace WebApp_undertheHood.Pages
{
    [Authorize(Policy = "HROnly")]
    public class HumanResourcesModel : PageModel
    {
        public void OnGet()
        {
        }
       
    }
}
