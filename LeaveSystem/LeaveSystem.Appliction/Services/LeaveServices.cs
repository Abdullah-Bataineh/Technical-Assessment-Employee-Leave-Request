using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LeaveSystem.Appliction.Exceptions;
using LeaveSystem.Appliction.Interfaces.Respositories;
using LeaveSystem.Domain.Entites;
using LeaveSystem.Domain.Enums;
using Microsoft.EntityFrameworkCore;

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
            if (leave == null)
            {
                throw new BusinessException($"Leave with Id {id} not found");
            }
            return leave;
        }

        public async Task<List<Leave>> GetLeaveByEmployeeId(string EmployeeId)
        {
            var leave=await _leaveRepositories.GetLeaveByEmployeeId(EmployeeId);
            if (leave == null)
            {
                throw new BusinessException($"Leaves with EmployeeId {EmployeeId} not found");
            }
            return leave;
        }


        public async Task AddLeave(Leave leave)
        {
            if (string.IsNullOrWhiteSpace(leave.Reason))
            {
                throw new BusinessException("Reason is Required");
            }
            if (leave.StartDate.Date > leave.EndDate.Date)
            {
                throw new BusinessException("End date cannot be earlier then start date");
            }
            leave.Status = LeaveStatus.Pending;
            await _leaveRepositories.Add(leave);

        }

        public async Task ApproveLeave(int id)
        {
            var leave=await _leaveRepositories.GetById(id);
            if (leave.Status != LeaveStatus.Pending) {
                throw new BusinessException("Only Pending Leave can be Approve");
            }
            leave.Status = LeaveStatus.Approved;
            await _leaveRepositories.Update(leave);
        }

        public async Task RejextLeave(int id)
        {
            var leave = await _leaveRepositories.GetById(id);
            if (leave.Status != LeaveStatus.Pending)
            {
                throw new BusinessException("Only Pending Leave can be Rejected");
            }
            
            leave.Status = LeaveStatus.Rejected;
            await _leaveRepositories.Update(leave);
        }

        public async Task DeleteLeave(int id,string employeeId)
        {
            var leave=await _leaveRepositories.GetById(id);
            if (leave.EmployeeId != employeeId)
            {
                throw new BusinessException("you are not authorized to delete this leave");
            }
            if(leave.Status != LeaveStatus.Pending)
            {
                throw new BusinessException("Only Pending Leaves can be deleted");
            }

            await _leaveRepositories.Delete(id);
        }
    }
}
