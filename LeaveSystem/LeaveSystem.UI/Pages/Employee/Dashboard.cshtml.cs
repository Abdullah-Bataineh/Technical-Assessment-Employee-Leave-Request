using LeaveSystem.Appliction.Exceptions;
using LeaveSystem.Appliction.Services;
using LeaveSystem.Domain.Entites;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace LeaveSystem.UI.Pages.Employee
{
    [Authorize(Roles = "Employee")]
    public class DashboardModel : PageModel
    {
        
        private readonly LeaveServices _leaveService;

        public DashboardModel(LeaveServices leaveService)
        {
            _leaveService = leaveService;
        }

        public List<Leave> Leaves { get; set; } = new();

        
        public async Task<IActionResult> OnPostDeleteAsync(int id)
        {
            var userId = User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)?.Value;
            try
            {
                await _leaveService.DeleteLeave(id,userId);
            }
            catch (BusinessException ex) {
                ModelState.AddModelError(string.Empty, ex.Message);
                return Page();
            }
            
            return RedirectToPage();
        }
        public async Task OnGetAsync()
        {
            var userId = User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)?.Value;

            if (!string.IsNullOrEmpty(userId))
            {
                Leaves = await _leaveService.GetLeaveByEmployeeId(userId);
            }

            
            if (!string.IsNullOrEmpty(FilterStatus))
            {
                Leaves = Leaves.Where(l => l.Status.ToString() == FilterStatus).ToList();
            }

            if (FilterStartDate.HasValue)
            {
                Leaves = Leaves.Where(l => l.StartDate.Date >= FilterStartDate.Value.Date).ToList();
            }

            if (FilterEndDate.HasValue)
            {
                Leaves = Leaves.Where(l => l.EndDate.Date <= FilterEndDate.Value.Date).ToList();
            }

            Leaves = (SortColumn, SortDirection?.ToLower()) switch
            {
                ("StartDate", "asc") => Leaves.OrderBy(l => l.StartDate).ToList(),
                ("StartDate", "desc") => Leaves.OrderByDescending(l => l.StartDate).ToList(),
                ("EndDate", "asc") => Leaves.OrderBy(l => l.EndDate).ToList(),
                ("EndDate", "desc") => Leaves.OrderByDescending(l => l.EndDate).ToList(),
                ("Status", "asc") => Leaves.OrderBy(l => l.Status).ToList(),
                ("Status", "desc") => Leaves.OrderByDescending(l => l.Status).ToList(),
                _ => Leaves
            };
        }
        [BindProperty(SupportsGet = true)]
        public string? SortColumn { get; set; }

        [BindProperty(SupportsGet = true)]
        public string? SortDirection { get; set; } = "asc";

        [BindProperty(SupportsGet = true)]
        public string? FilterStatus { get; set; }

        [BindProperty(SupportsGet = true)]
        public DateTime? FilterStartDate { get; set; }

        [BindProperty(SupportsGet = true)]
        public DateTime? FilterEndDate { get; set; }

    }
}
