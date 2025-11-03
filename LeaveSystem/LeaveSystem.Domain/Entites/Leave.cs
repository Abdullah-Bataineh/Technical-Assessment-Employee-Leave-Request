using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LeaveSystem.Domain.Enums;

namespace LeaveSystem.Domain.Entites
{
    public class Leave
    {
        [Key]
        public int Id { get; set; }
        [Required(ErrorMessage = "Start Date is required")]
        [DataType(DataType.Date)]
        public DateTime StartDate { get; set; }
        [Required(ErrorMessage = "End Date is required")]
        [DataType(DataType.Date)]
        public DateTime EndDate { get; set; }
        [Required(ErrorMessage = "Reason is required")]
        [StringLength(500, ErrorMessage = "Reason cannot be longer than 500 characters")]
        public required string Reason {  get; set; }
        public required string EmployeeId { get; set; }
        public required User Employee { get; set; }
        public LeaveStatus Status { get; set; } = LeaveStatus.Pending;
    }
}
