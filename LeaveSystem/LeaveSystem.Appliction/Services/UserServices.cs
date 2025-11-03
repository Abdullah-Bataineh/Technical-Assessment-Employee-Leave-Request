using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LeaveSystem.Appliction.Interfaces.Respositories;
using LeaveSystem.Domain.Entites;
using Microsoft.AspNetCore.Identity;

namespace LeaveSystem.Appliction.Services
{
    public class UserServices
    {
        private readonly IUserRespositories _userRespositories;
        public UserServices(IUserRespositories userRespositories)
        {
            _userRespositories = userRespositories;
        }

        public async Task<IdentityResult> RegisterUser(User user, string password, string role)
        {
           return await _userRespositories.Register(user, password, role);
        }
       public async Task<User> LoginUser(string email, string password)
        {
            return await _userRespositories.Login(email, password);
        }
        public async Task<bool> AssignRoleUser(User user, string role)
        {
            return await _userRespositories.AssignRole(user, role);
        }
        public async Task LogoutUser()
        {
             await _userRespositories.Logout();
        }
    }
}
