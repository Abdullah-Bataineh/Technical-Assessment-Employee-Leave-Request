using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LeaveSystem.Appliction.Exceptions;
using LeaveSystem.Appliction.Interfaces.Respositories;
using LeaveSystem.Domain.Entites;
using LeaveSystem.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace LeaveSystem.Infrastructure.Respositories
{
    public class LeaveRepositories : ILeaveRepositories
    {
        private readonly ApplicationDbContext _context;
        public LeaveRepositories(ApplicationDbContext context) { 
        _context = context;
        }
        public async Task Add(Leave leave)
        {
            _context.Leaves.Add(leave);
            await _context.SaveChangesAsync();
        }

        public async Task Delete(int id)
        {
            var leave=await _context.Leaves.FindAsync(id);
            if (leave != null) {
            _context.Leaves.Remove(leave);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<List<Leave>> GetAll()
        {
            return await _context.Leaves.Include(l=>l.Employee).ToListAsync();
        }

        public async Task<Leave> GetById(int id)
        {
            
             var leave=await _context.Leaves.Include(l => l.Employee).FirstOrDefaultAsync(l => l.Id == id);
            if (leave == null)
            {
                throw new BusinessException($"Leave with Id {id} not found");
            }
            return leave;
        }

        public async Task Update(Leave leave)
        {
            _context.Leaves.Update(leave);
            await _context.SaveChangesAsync();
        }
    }
}
