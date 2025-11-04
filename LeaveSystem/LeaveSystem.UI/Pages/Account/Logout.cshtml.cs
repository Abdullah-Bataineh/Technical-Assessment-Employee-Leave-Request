using LeaveSystem.Appliction.Services;
using LeaveSystem.Domain.Entites;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace LeaveSystem.UI.Pages.Account
{
    public class LogoutModel : PageModel
    {
        public void OnGet()
        {
        }
        private readonly UserServices userServices;

        public LogoutModel(UserServices _userServices)
        {
            userServices = _userServices;
        }

        public async Task<IActionResult> OnPostAsync()
        {
            await userServices.LogoutUser();

            HttpContext.Session.Clear();

            return RedirectToPage("/Account/Login");
        }
    }
}
