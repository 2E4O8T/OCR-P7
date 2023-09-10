using Microsoft.AspNetCore.Identity;
using P7CreateRestApi.Models;

namespace P7CreateRestApi.Services
{
    public class UsersService : IUsersService
    {
        private readonly UserManager<IdentityUser> _userManager;
        public UsersService(UserManager<IdentityUser> userManager)
        {
            _userManager = userManager;
        }

        public async Task<bool> Login(LoginUser user)
        {
            var identityUser = await _userManager.FindByEmailAsync(user.UserName);
            if (identityUser == null)
            {
                return false;
            }

            return await _userManager.CheckPasswordAsync(identityUser, user.Password);
        }

        public async Task<bool> RegisterUser(LoginUser user)
        {
            var identityUser = new IdentityUser
            {
                UserName = user.UserName,
                Email = user.UserName
            };

            var result = await _userManager.CreateAsync(identityUser, user.Password);
            return result.Succeeded;
        }
    }
}
