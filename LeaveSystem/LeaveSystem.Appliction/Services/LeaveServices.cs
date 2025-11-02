using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LeaveSystem.Appliction.Interfaces.Respositories;
using LeaveSystem.Domain.Entites;
using LeaveSystem.Domain.Enums;

namespace LeaveSystem.Appliction.Services
{
    public class LeaveServices
    {
        private readonly ILeaveRepositories _leaveRepositories;
        public LeaveServices(ILeaveRepositories leaveRepositories)
        {
            _leaveRepositories = leaveRepositories;
        }

        public async Task<List<Leave>> GetAllLeave()
        {
            return await _leaveRepositories.GetAll();
        }

        public async Task<Leave> GetLeaveById(int id)
        {
            var leave=await _leaveRepositories.GetById(id);
            return leave;
        }

        public async Task AddLeave(Leave leave)
        {
            leave.Status = LeaveStatus.Pending;
            await _leaveRepositories.Add(leave);

        }

        public async Task ApproveLeave(int id)
        {
            var leave=await _leaveRepositories.GetById(id);
            leave.Status = LeaveStatus.Pending;
            await _leaveRepositories.Update(leave);
        }

        public async Task RejextLeave(int id)
        {
            var leave = await _leaveRepositories.GetById(id);
            leave.Status = LeaveStatus.Rejected;
            await _leaveRepositories.Update(leave);
        }

        public async Task DeleteLeave(int id,string employeeId)
        {
            var leave=await _leaveRepositories.GetById(id);
            await _leaveRepositories.Delete(id);
        }
    }
}
