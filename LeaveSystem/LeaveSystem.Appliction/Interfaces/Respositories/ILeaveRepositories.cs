using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LeaveSystem.Domain.Entites;

namespace LeaveSystem.Appliction.Interfaces.Respositories
{
    public interface ILeaveRepositories
    {
         Task<List<Leave>> GetAll();
        Task<Leave> GetById(int id);
        Task Add (Leave leave);
        Task Update (Leave leave);
        Task Delete (int id);

    }
}
