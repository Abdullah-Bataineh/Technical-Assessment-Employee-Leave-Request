using LeaveSystem.Appliction.Exceptions;
using LeaveSystem.Appliction.Services;
using LeaveSystem.Domain.DTO;
using LeaveSystem.Domain.Entites;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace LeaveSystem.UI.Pages.Account
{
    public class RegisterModel:PageModel
    {
        private readonly UserServices _userServices;
        public RegisterModel(UserServices userServices)
        {
            _userServices = userServices;
        }

        [BindProperty]
        public required RegisterDTO Input { get; set; }
       
        public void OnGet()
        {
        }
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
                return Page();

            try
            {
                var user = new User
                {
                    UserName = Input.Email,
                    Email = Input.Email,
                    FirstName = Input.FirstName,
                    LastName = Input.LastName
                };

                var result = await _userServices.RegisterUser(user, Input.Password, Input.Role);

                if (result.Succeeded)
                    return RedirectToPage("/Index");

                foreach (var error in result.Errors)
                    ModelState.AddModelError(string.Empty, error.Description);
            }
            catch (BusinessException ex)
            {
                ModelState.AddModelError(string.Empty, ex.Message);
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, "Unexpected error occurred during registration");
            }

            return Page();
        }

    }
}
