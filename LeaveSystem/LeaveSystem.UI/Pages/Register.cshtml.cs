using LeaveSystem.Appliction.Services;
using LeaveSystem.Domain.Entites;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace LeaveSystem.UI.Pages
{
    public class RegisterModel(UserServices userServices) : PageModel
    {
        private readonly UserServices _userServices = userServices;

        [BindProperty]
        public required RegisterInputModel Input { get; set; }
        public class RegisterInputModel
        {
            public string FirstName { get; set; } = string.Empty;
            public string LastName { get; set; } = string.Empty;
            public string Email { get; set; } = string.Empty;
            public string Password { get; set; } = string.Empty;
            public string Role { get; set; } = "Employee";
        }
        public void OnGet()
        {
        }
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
                return Page();

            var user = new User
            {
                UserName = Input.Email,
                Email = Input.Email,
                FirstName = Input.FirstName,
                LastName = Input.LastName
            };

            var result = await _userServices.RegisterUser(user, Input.Password, Input.Role);

            if (result.Succeeded)
            {
                return RedirectToPage("/Index");
            }

            foreach (var error in result.Errors)
            {
                ModelState.AddModelError(string.Empty, error.Description);
            }

            return Page();
        }

    }
}
