using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LeaveSystem.Domain.Enums;

namespace LeaveSystem.Domain.Entites
{
    public class Leave
    {
        public int Id { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public required string EmployeeId { get; set; }
        public required User Employee { get; set; }
        public LeaveStatus Status { get; set; } = LeaveStatus.Pending;
    }
}
