using P7CreateRestApi.Models;

namespace P7CreateRestApi.Services
{
    public interface IUsersService
    {
        Task<bool> Login(LoginUser user);
        Task<bool> RegisterUser(LoginUser user);
    }
}