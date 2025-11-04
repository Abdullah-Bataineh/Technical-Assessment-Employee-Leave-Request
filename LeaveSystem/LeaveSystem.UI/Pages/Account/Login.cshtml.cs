using LeaveSystem.Appliction.Exceptions;
using LeaveSystem.Appliction.Services;
using LeaveSystem.Domain.DTO;
using LeaveSystem.Domain.Entites;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace LeaveSystem.UI.Pages.Account
{
    public class LoginModel : PageModel
    {
        private readonly SignInManager<User> _signInManager;
        private readonly UserManager<User> _userManager;
        private readonly UserServices _userServices;

        public LoginModel(SignInManager<User> signInManager, UserManager<User> userManager, UserServices userServices)
        {
            _signInManager = signInManager;
            _userManager = userManager;
            _userServices = userServices;
        }


        [BindProperty]
        public required LoginDTO Input { get; set; }
      
        public void OnGet()
        {
        }
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
                return Page();

            try
            {
                var result = await _userServices.LoginUser(Input.Email, Input.Password);

                var user = await _userManager.FindByEmailAsync(Input.Email);
                if (user != null)
                {
                    var roles = await _userManager.GetRolesAsync(user);

                    HttpContext.Session.SetString("FirstName", user.FirstName);
                    HttpContext.Session.SetString("LastName", user.LastName);
                    HttpContext.Session.SetString("EmployeeId", user.Id ?? "");
                    HttpContext.Session.SetString("Roles", string.Join(",", roles));
                }

                return RedirectToPage("/Index");
            }
            catch (BusinessException ex)
            {
                ModelState.AddModelError(string.Empty, ex.Message);
                return Page();
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, "Unexpected error occurred. Please try again later");

                return Page();
            }

        }
    }
}

