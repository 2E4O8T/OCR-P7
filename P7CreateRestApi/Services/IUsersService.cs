using P7CreateRestApi.Models;

namespace P7CreateRestApi.Services
{
    public interface IUsersService
    {
        string GenerateTokenString(LoginUser user);
        Task<bool> Login(LoginUser user);
        Task<bool> RegisterUser(LoginUser user);
    }
}