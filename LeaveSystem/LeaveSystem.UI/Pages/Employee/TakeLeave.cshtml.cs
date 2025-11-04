using System.ComponentModel.DataAnnotations;
using LeaveSystem.Appliction.Exceptions;
using LeaveSystem.Appliction.Services;
using LeaveSystem.Domain.DTO;
using LeaveSystem.Domain.Entites;
using LeaveSystem.Domain.Enums;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace LeaveSystem.UI.Pages.Employee
{
    [Authorize(Roles = "Employee")]
    public class TakeLeaveModel : PageModel
    {
        private readonly LeaveServices _leaveService;
        private readonly UserManager<User> _userManager;

        public TakeLeaveModel(LeaveServices leaveService, UserManager<User> userManager)
        {
            _leaveService = leaveService;
            _userManager = userManager;
        }

        [BindProperty]
        public LeaveDTO Input { get; set; } = new();

        public void OnGet()
        {
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
                return Page();

            var userId = User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)?.Value;

            if (!string.IsNullOrEmpty(userId))
            {
                var currentUser = await _userManager.FindByIdAsync(userId);

                if (currentUser != null)
                {
                    var leave = new Leave
                    {
                        EmployeeId = currentUser.Id,
                        StartDate = Input.StartDate,
                        EndDate = Input.EndDate,
                        Status = LeaveStatus.Pending,
                        Reason = Input.Reason,
                        Employee = currentUser
                    };
                    try {
                        await _leaveService.AddLeave(leave);

                    }
                    catch (BusinessException ex)
                    {
                        ModelState.AddModelError(string.Empty,ex.Message);
                        return Page();
                    }

                }
            }

            return RedirectToPage("/Employee/Dashboard");
        }

        
    }
}
