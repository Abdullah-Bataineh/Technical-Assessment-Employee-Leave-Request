using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LeaveSystem.Domain.Entites;
using Microsoft.AspNetCore.Identity;

namespace LeaveSystem.Appliction.Interfaces.Respositories
{
    public interface IUserRespositories
    {
        Task<IdentityResult> Register(User user,string password,string role);
        Task<User> Login(string email,string password);
        Task<bool> AssignRole(User user,string role);
        Task Logout();
    }
}
