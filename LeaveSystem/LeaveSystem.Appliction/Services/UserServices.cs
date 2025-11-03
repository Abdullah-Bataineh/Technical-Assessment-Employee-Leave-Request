using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LeaveSystem.Appliction.Exceptions;
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
           var register= await _userRespositories.Register(user, password, role);
            if (!register.Succeeded)
            {
                
                throw new BusinessException(string.Join(",", register.Errors.Select(e => e.Description)));
            }
            return register;
        }
       public async Task<User> LoginUser(string email, string password)
        {
            var user= await _userRespositories.Login(email, password);
            if (user == null)
            {
                throw new BusinessException("Invalid Email or Password");
            }
            return user;
        }
        public async Task<bool> AssignRoleUser(User user, string role)
        {
            var assign= await _userRespositories.AssignRole(user, role);
            if (!assign)
            {
                throw new BusinessException($"Failed to assign role '{role}");
            }
            return assign;
        }
        public async Task LogoutUser()
        {
            try
            {
                await _userRespositories.Logout();
            }
            catch (Exception ex)
            {
                throw new BusinessException("An error occurred while logging out.");
            }
        }
    }
}
